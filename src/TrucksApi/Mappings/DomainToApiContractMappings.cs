using Domain;
using TrucksApi.Contracts.Requests;
using TrucksApi.Contracts.Responses;

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

    public static GetTrucksResponse ToTrucksResponse(this IEnumerable<TruckModel> x, PaginationFilter? pagination, string currAddress, int count)
    {
        var retVal = new GetTrucksResponse
        {
            Trucks = x.Select(s => s.ToTruckResponse())
        };
        if (pagination is not null)
        {
            var prevPage = pagination.Page > 1 ? pagination.Page - 1 : 1;
            var nextPage = (pagination.Page + 1) * pagination.PageSize > count ? pagination.Page : pagination.Page + 1;
            retVal.Page = pagination.Page;
            retVal.PageSize = pagination.PageSize;
            retVal.PrevPage = prevPage;
            retVal.PrevPageUrl = new Uri(currAddress + $"?page={prevPage}&pageSize={pagination.PageSize}");
            retVal.NextPage = nextPage;
            retVal.NextPageUrl = new Uri(currAddress + $"?page={nextPage}&pageSize={pagination.PageSize}");
        }

        return retVal;
    }
}