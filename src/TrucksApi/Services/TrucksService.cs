using TrucksApi.Domain;
using TrucksApi.Repositories.Abstract;
using TrucksApi.Services.Abstract;
using TrucksApi.Mappings;

namespace TrucksApi.Services;

public class TrucksService : ITrucksService
{
    private readonly ITrucksRepository _trucksRepository;

    public TrucksService(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }
    public Task Delete(TruckModel truck)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TruckModel>> GetAll()
    {
        var trucks = await _trucksRepository.GetAll();
        return trucks.ToModel();
    }

    public Task<TruckModel?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TruckModel>> GetFiltered()
    {
        throw new NotImplementedException();
    }

    public TruckModel Update(TruckModel truck)
    {
        throw new NotImplementedException();
    }
}
