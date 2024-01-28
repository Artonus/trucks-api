using TrucksApi.Domain.TruckStatuses;

namespace TrucksApi.Domain;

public class TruckModel
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public TruckStatus Status { get; set; } = new OutOfServiceStatus();

    public (bool, string) SetStatus(string newStatus)
    {        
        if (Status.StatusName == newStatus) return (true, string.Empty);
        
        if (!Status.AllowedChangelist.Contains(newStatus))
        {
            return (false, $"Cannot change status from \"{Status.StatusName}\" to \"{newStatus}\"");
        }

        Status = TruckStatus.FromString(newStatus);
        return (true, string.Empty);
    }
}
