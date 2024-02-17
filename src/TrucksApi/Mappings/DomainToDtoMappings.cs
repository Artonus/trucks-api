using DataAccess.Models;
using Domain;

namespace TrucksApi.Mappings;

public static class DomainToDtoMappings
{
    public static Truck ToDto(this TruckModel x)
    {
        return new Truck()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Status = x.Status.StatusName
        };
    }

    public static List<Truck> ToDto(this IEnumerable<TruckModel> x)
    {
        return x.Select(s => s.ToDto()).ToList();
    }
}
