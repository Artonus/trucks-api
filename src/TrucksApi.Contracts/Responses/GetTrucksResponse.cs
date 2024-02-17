using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TrucksApi.Contracts.Responses
{
    public class GetTrucksResponse
    {
        public IEnumerable<TruckResponse> Trucks { get; set; } = Enumerable.Empty<TruckResponse>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int NextPage { get; set; }
        public Uri? NextPageUrl { get; set; }
        public int PrevPage { get; set; }
        public Uri? PrevPageUrl { get; set; }
    }
}
