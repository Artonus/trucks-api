using TrucksApi.DataAccess;
using TrucksApi.DataAccess.Models;
using TrucksApi.Domain;
using TrucksApi.Repositories.Abstract;

namespace TrucksApi.Repositories;

public class TrucksRepository : BaseRepository<Truck>, ITrucksRepository
{
    public TrucksRepository(TrucksContext ctx) : base(ctx)
    {
    }

    public IQueryable<Truck> FilterQuery(IQueryable<Truck> query, TrucksFilter filter)
    {
        if (string.IsNullOrEmpty(filter.IdFilter) == false)
        {
            query = query.Where(t => t.Id.Contains(filter.IdFilter));
        }
        if (string.IsNullOrEmpty(filter.NameFilter) == false)
        {
            query = query.Where(t => t.Name.Contains(filter.NameFilter));
        }
        if (string.IsNullOrEmpty(filter.DescriptionFilter) == false)
        {
            query = query.Where(t => t.Description != null && t.Description.Contains(filter.DescriptionFilter));
        }
        if (string.IsNullOrEmpty(filter.StatusFilter) == false)
        {
            query = query.Where(t => t.Status.Contains(filter.StatusFilter));
        }

        return query;
    }
}
