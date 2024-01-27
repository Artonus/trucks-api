using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrucksApi.DataAccess.Models;

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
