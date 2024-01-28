using TrucksApi.DataAccess.Models;
using TrucksApi.Domain;

namespace TrucksApi.Repositories.Abstract;

public interface ITrucksRepository : IBaseRepository<Truck>
{
    IQueryable<Truck> FilterQuery(IQueryable<Truck> query, TrucksFilter filter);
}
