using TrucksApi.Contracts.Responses;
using TrucksApi.Domain;

namespace TrucksApi.Mappings;

public static class DomainToApiContractMappings
{
    public static TruckResponse ToTruckResponse(this TruckModel x)
    {
        return new TruckResponse
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Status = x.Status.StatusName
        };
    }

    public static GetTrucksResponse ToTrucksResponse(this IEnumerable<TruckModel> x)
    {
        return new GetTrucksResponse
        {
            Trucks = x.Select(s => s.ToTruckResponse())
        };
    }
}
