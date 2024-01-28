using TrucksApi.Domain;

namespace TrucksApi.Services.Abstract;

public interface ITrucksService
{
    Task<List<TruckModel>> GetAll();
    Task<List<TruckModel>> GetFiltered();
    Task<TruckModel?> GetById(string id);
    TruckModel Update(TruckModel truck);
    Task Delete(TruckModel truck);
}
