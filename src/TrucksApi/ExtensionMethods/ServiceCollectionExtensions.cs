using Microsoft.EntityFrameworkCore;
using TrucksApi.Config;
using TrucksApi.DataAccess;
using TrucksApi.Repositories;
using TrucksApi.Repositories.Abstract;

namespace TrucksApi.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrucksDbConnection(this IServiceCollection services, DbConfig config)
    {
        services.AddDbContext<TrucksContext>(opt =>
            opt.UseSqlServer(config.ConnectionString));

        services.AddScoped<ITrucksRepository, TrucksRepository>();

        return services;
    }
}
