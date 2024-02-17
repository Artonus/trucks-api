using System;
using System.Collections.Generic;
using System.Text;

namespace TrucksApi.Contracts.Requests
{
    public class CreateTruckRequest
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string Status { get; set; } = default!;
    }
}
