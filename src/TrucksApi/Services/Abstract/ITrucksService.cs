using TrucksApi.Domain;

namespace TrucksApi.Services.Abstract;

public interface ITrucksService
{
    Task<List<TruckModel>> GetAll();
    Task<List<TruckModel>> GetFiltered(TrucksFilter filter, SortingModel sort);
    Task<TruckModel?> GetById(string id);
    Task<TruckResult> Update(TruckModel truck);
    Task<TruckResult> SetStatus(string id, string status);
    Task Delete(string id);
    Task<TruckResult> Add(TruckModel truck);
}
