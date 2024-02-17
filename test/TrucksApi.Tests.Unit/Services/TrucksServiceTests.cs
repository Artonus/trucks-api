using DataAccess.Models;
using DataAccess.Repositories.Abstract;
using Domain;
using Domain.TruckStatuses;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TrucksApi.Mappings;
using TrucksApi.Services;

namespace TrucksApi.Tests.Unit.Services;

public class TrucksServiceTests
{
    private readonly ITrucksRepository _repository = Substitute.For<ITrucksRepository>();
    private readonly TrucksService _sut;

    public TrucksServiceTests()
    {
        _sut = new TrucksService(_repository);
    }

    [Fact]
    public async Task Add_ShouldAddNewEntity_WhenItDoesntExist()
    {
        //arrange
        var truck = new TruckModel()
        {
            Id = "trck60",
            Name = "Mercedes-Benz Actros",
            Status = new OutOfServiceStatus()
        };
        var expected = new TruckResult(truck);
        _repository.GetById(truck.Id).ReturnsNull();
        _repository.Add(Arg.Any<Truck>()).Returns(truck.ToDto());
        //act
        var result = await _sut.Add(truck);
        //assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected);
        await _repository.Received(1).CommitChanges();
    }
    [Fact]
    public async Task Add_ShouldReturnNegativeResult_WhenTruckAlreadyExists()
    {
        //arrange
        var truck = new TruckModel()
        {
            Id = "trck60",
            Name = "Mercedes-Benz Actros",
            Status = new OutOfServiceStatus()
        };
        var existingTruck = truck.ToDto();
        _repository.GetById(truck.Id).Returns(existingTruck);
        //act
        var result = await _sut.Add(truck);
        //assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TruckResult>();
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNullOrEmpty();
        result.ErrorMessage.Should().Be("There is already a truck with specified id");
        await _repository.Received(0).CommitChanges();
    }
    [Fact]
    public async Task Delete_ShouldDeleteEntity_WhenEntityExists()
    {
        //arrange
        var truck = new TruckModel()
        {
            Id = "trck60",
            Name = "Mercedes-Benz Actros",
            Status = new OutOfServiceStatus()
        };
        var existingTruck = truck.ToDto();
        _repository.GetById(truck.Id).Returns(existingTruck);

        //act
        await _sut.Delete(truck.Id);

        //assert
        _repository.Received(1).Delete(existingTruck);
        await _repository.Received(1).CommitChanges();
    }

    [Fact]
    public async Task Delete_ShouldNotDeleteEntity_WhenEntityDoesntExists()
    {
        //arrange        
        var truckId = "trck60";
        _repository.GetById(truckId).ReturnsNull();

        //act
        await _sut.Delete(truckId);

        //assert
        _repository.Received(0).Delete(Arg.Any<Truck>());
        await _repository.Received(0).CommitChanges();
    }

    [Fact]
    public async Task GetFiltered_ShouldReturnAll_WhenNoFilterAndNoSortIsGiven()
    {
        // Arrange
        var scania = new TruckModel()
        {
            Id = "sc12",
            Name = "Scania12",
            Status = new LoadingStatus()
        };
        var volvo = new TruckModel()
        {
            Id = "vol12",
            Name = "Volvo6",
            Status = new LoadingStatus()
        };
        var allTrucks = new List<Truck> { scania.ToDto(), volvo.ToDto() };
        var allTruckModels = new List<TruckModel> { scania, volvo };
        _repository.GetAll().Returns(allTrucks);

        // Act
        var result = await _sut.GetFiltered(null, null, null);

        // Assert
        allTrucks.Count.Should().Be(result.Count);
        await _repository.Received(1).GetAll();
        result.Should().BeEquivalentTo(allTruckModels);
    }

    [Fact]
    public async Task GetFiltered_ShouldReturnFilteredTrucks_WhenFilterIsGiven()
    {
        // Arrange
        var scania = new TruckModel()
        {
            Id = "sc12",
            Name = "Scania12",
            Status = new OutOfServiceStatus()
        };
        var volvo = new TruckModel()
        {
            Id = "vol12",
            Name = "Volvo6",
            Status = new LoadingStatus()
        };

        var filter = new TrucksFilter()
        {
            StatusFilter = "Loading"
        };

        var expected = new List<TruckModel> { volvo };
        var allTrucks = new List<Truck> { scania.ToDto(), volvo.ToDto() };
        _repository.GetAll().Returns(allTrucks);
        _repository.Query().Returns(allTrucks.AsQueryable());
        // Act
        var result = await _sut.GetFiltered(filter, null, null);

        // Assert
        result.Count.Should().Be(expected.Count);
        await _repository.Received(0).GetAll();
        _repository.Received(1).Query();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetFiltered_ShouldReturnSortedTrucksByParam_WhenSortIsGiven()
    {
        // Arrange
        var scania = new TruckModel()
        {
            Id = "sc12",
            Name = "Scania12",
            Status = new OutOfServiceStatus()
        };
        var volvo = new TruckModel()
        {
            Id = "vol12",
            Name = "Volvo6",
            Status = new LoadingStatus()
        };

        var sort = new SortingModel()
        {
            SortFileld = "Status"
        };

        var expected = new List<TruckModel> { volvo, scania };
        var allTrucks = new List<Truck> { scania.ToDto(), volvo.ToDto() };
        _repository.GetAll().Returns(allTrucks);
        _repository.Query().Returns(allTrucks.AsQueryable());
        // Act
        var result = await _sut.GetFiltered(null, null, sort);

        // Assert
        result.Count.Should().Be(expected.Count);
        await _repository.Received(0).GetAll();
        _repository.Received(1).Query();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task SetStatus_ShouldReturnError_WhenTruckDoesNotExist()
    {
        //arrange
        var id = "trck12";
        _repository.GetById(id).ReturnsNull();

        //act
        var result = await _sut.SetStatus(id, "Loading");
        //assert
        result.IsSuccess.Should().BeFalse();
        result.NotFound.Should().BeTrue();
        result.ErrorMessage.Should().Be("Specified truck was not found");
    }

    [Fact]
    public async Task SetStatus_ShouldSetStatus_WhenCorrectStatusIsGiven()
    {
        //arrange
        var truck = new TruckModel
        {
            Id = "trck12",
            Name = "Truck12",
            Status = new OutOfServiceStatus()
        };
        var expectedTruck = new TruckModel
        {
            Id = "trck12",
            Name = "Truck12",
            Status = new LoadingStatus()
        };
        var expected = new TruckResult(expectedTruck);

        _repository.GetById(truck.Id).Returns(truck.ToDto());
        _repository.Update(truck.Id, Arg.Any<Truck>()).Returns(expectedTruck.ToDto());

        //act
        var result = await _sut.SetStatus(truck.Id, "Loading");
        //assert
        result.IsSuccess.Should().BeTrue();
        result.NotFound.Should().BeFalse();
        result.ErrorMessage.Should().BeNullOrEmpty();
        result.Truck.Should().BeEquivalentTo(expectedTruck);
    }
}
