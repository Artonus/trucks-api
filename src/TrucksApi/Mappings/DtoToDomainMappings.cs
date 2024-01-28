using TrucksApi.DataAccess.Models;
using TrucksApi.Domain;
using TrucksApi.Domain.TruckStatuses;

namespace TrucksApi.Mappings;

public static class DtoToDomainMappings
{
    public static TruckModel ToModel(this Truck x)
    {
        return new TruckModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Status = TruckStatus.FromString(x.Status)
        };
    }
    public static List<TruckModel> ToModel(this IEnumerable<Truck> x)
    {
        return x.Select(s => s.ToModel()).ToList();
    }
}
