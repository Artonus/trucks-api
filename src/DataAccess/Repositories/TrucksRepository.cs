using DataAccess.Models;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories;

public class TrucksRepository : BaseRepository<Truck>, ITrucksRepository
{
    public TrucksRepository(TrucksContext ctx) : base(ctx)
    {
    }   
}
