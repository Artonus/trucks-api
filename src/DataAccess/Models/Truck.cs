using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Truck
{
    [MaxLength(30)]
    public string Id { get; set; } = default!;
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    [MaxLength(500)]
    public string? Description { get; set; }
    [MaxLength(30)]
    public string Status { get; set; } = default!;
}
