using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using TrucksApi.DataAccess;
using TrucksApi.DataAccess.Models;

namespace TrucksApi.Installer;

public class StartupDataInstaller : IStartupDataInstaller
{
    private readonly TrucksContext _dbContext;

    public StartupDataInstaller(TrucksContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Install()
    {
        //await _dbContext.Database.EnsureCreatedAsync();
        await _dbContext.Database.MigrateAsync();

        var recordsExist = _dbContext.Trucks.Any();

        if (recordsExist == false)
        {
            await AddExampleData();
        }
    }

    private async Task AddExampleData()
    {
        var trucks = Enumerable.Range(1, 15).Select(i => new Truck()
        {
            Id = "trc" + i,
            Name = (i % 2 == 0 ? "Scania" : "Volvo") + i,
            Status = "Out Of Service"
        });

        await _dbContext.Trucks.AddRangeAsync(trucks);
        await _dbContext.SaveChangesAsync();
    }
}
