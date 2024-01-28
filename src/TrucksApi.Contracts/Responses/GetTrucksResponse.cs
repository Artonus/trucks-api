using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrucksApi.Contracts.Responses
{
    public class GetTrucksResponse
    {
        public IEnumerable<TruckResponse> Trucks { get; set; } = Enumerable.Empty<TruckResponse>();
    }
}
