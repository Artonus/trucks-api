using Domain;
using Domain.TruckStatuses;
using TrucksApi.Contracts.Requests;

namespace TrucksApi.Mappings;

public static class ApiContractToDomainMappings
{
    public static TruckModel ToTruck(this CreateTruckRequest x)
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
    public static TrucksFilter ToTruckFilter(this GetTrucksFilter x)
    {
        return new TrucksFilter()
        {
            DescriptionFilter = x.DescriptionFilter,
            StatusFilter = x.StatusFilter,
            IdFilter = x.IdFilter,
            NameFilter = x.NameFilter
        };
    }
    public static SortingModel ToSorting(this SortingParam x)
    {
        return new SortingModel()
        {
            Ascending = x.Ascending,
            SortFileld = x.SortFileld
        };
    }

    public static PaginationModel ToPagination(this PaginationFilter x)
    {
        return new PaginationModel()
        {
            Page = x.Page,
            PageSize = x.PageSize
        };
    }
}
