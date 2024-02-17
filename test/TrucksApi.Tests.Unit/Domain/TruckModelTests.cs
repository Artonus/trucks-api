using Domain;
using Domain.TruckStatuses;
using FluentAssertions;

namespace TrucksApi.Tests.Unit.Domain;

public class TruckModelTests
{
    [Theory]
    [InlineData("Loading", true)]
    [InlineData("At Job", true)]
    [InlineData("To Job", true)]
    [InlineData("Returning", true)]    
    public void SetStatus_ShouldChangeToAnyStatus_WhenStatusIsOutOfService(string status, bool expected)
    {
        //arrange
        var truck = new TruckModel
        {
            Id = "any",
            Status = new OutOfServiceStatus()
        };
        //act
        var (changed, _) = truck.SetStatus(status);
        //assert

        changed.Should().Be(expected);
    }

    [Theory]
    [InlineData("Out Of Service", true)]
    [InlineData("At Job", false)]
    [InlineData("To Job", true)]
    [InlineData("Returning", false)]
    public void SetStatus_ShouldChangeToAnyStatus_WhenStatusIsLoading(string status, bool expected)
    {
        //arrange
        var truck = new TruckModel
        {
            Id = "any",
            Status = new LoadingStatus()
        };
        //act
        var (changed, _) = truck.SetStatus(status);
        //assert

        changed.Should().Be(expected);
    }

    [Theory]
    [InlineData("Out Of Service", true)]
    [InlineData("At Job", true)]
    [InlineData("Loading", false)]
    [InlineData("Returning", false)]
    public void SetStatus_ShouldChangeToAnyStatus_WhenStatusIsToJob(string status, bool expected)
    {
        //arrange
        var truck = new TruckModel
        {
            Id = "any",
            Status = new ToJobStatus()
        };
        //act
        var (changed, _) = truck.SetStatus(status);
        //assert

        changed.Should().Be(expected);
    }
    [Theory]
    [InlineData("Out Of Service", true)]
    [InlineData("To Job", false)]
    [InlineData("Loading", false)]
    [InlineData("Returning", true)]
    public void SetStatus_ShouldChangeToAnyStatus_WhenStatusIsAtJob(string status, bool expected)
    {
        //arrange
        var truck = new TruckModel
        {
            Id = "any",
            Status = new AtJobStatus()
        };
        //act
        var (changed, _) = truck.SetStatus(status);
        //assert

        changed.Should().Be(expected);
    }

    [Theory]
    [InlineData("Out Of Service", true)]
    [InlineData("To Job", false)]
    [InlineData("Loading", true)]
    [InlineData("At Job", false)]
    public void SetStatus_ShouldChangeToAnyStatus_WhenStatusIsReturning(string status, bool expected)
    {
        //arrange
        var truck = new TruckModel
        {
            Id = "any",
            Status = new ReturningStatus()
        };
        //act
        var (changed, _) = truck.SetStatus(status);
        //assert

        changed.Should().Be(expected);
    }
}
