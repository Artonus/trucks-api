using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.Abstract;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TrucksApi.Config;

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
    public static IServiceCollection AddFluentValidation(this IServiceCollection services, Type assemblyType)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining(assemblyType);
        return services;
    }
}
