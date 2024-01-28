using TrucksApi.DataAccess;
using TrucksApi.DataAccess.Models;
using TrucksApi.Repositories.Abstract;

namespace TrucksApi.Repositories;

public class TrucksRepository : BaseRepository<Truck>, ITrucksRepository
{
    public TrucksRepository(TrucksContext ctx) : base(ctx)
    {
    }
}
