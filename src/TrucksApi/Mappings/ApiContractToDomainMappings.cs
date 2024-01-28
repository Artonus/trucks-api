using TrucksApi.Contracts.Requests;
using TrucksApi.Domain;
using TrucksApi.Domain.TruckStatuses;

namespace TrucksApi.Mappings;

public static class ApiContractToDomainMappings
{
    public static TruckModel ToTruck(this TruckRequest x)
    {
        return new TruckModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Status = TruckStatus.FromString(x.Status),
        };
    }

    public static TruckModel ToTruck(this UpdateTruckRequest x, string id)
    {
        return new TruckModel
        {
            Id = id,
            Name = x.Name,
            Description = x.Description,
            Status = TruckStatus.FromString(x.Status),
        };
    }
}
