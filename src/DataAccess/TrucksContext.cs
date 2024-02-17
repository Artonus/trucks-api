using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class TrucksContext : DbContext
{
    public TrucksContext(DbContextOptions<TrucksContext> options): base(options)
    {        
    }

    public DbSet<Truck> Trucks { get; set; }
}
