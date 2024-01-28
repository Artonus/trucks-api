using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrucksApi.DataAccess.Models;

namespace TrucksApi.DataAccess;

public class TrucksContext : DbContext
{
    public TrucksContext(DbContextOptions<TrucksContext> options): base(options)
    {        
    }

    public DbSet<Truck> Trucks { get; set; }
}
