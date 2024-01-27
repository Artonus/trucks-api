using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using TrucksApi.Config;
using TrucksApi.DataAccess;

namespace TrucksApi.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrucksDbConnection(this IServiceCollection services, DbConfig config)
    {
        services.AddDbContext<TrucksContext>(opt =>
            opt.UseSqlServer(config.ConnectionString));

        return services;
    }
}
