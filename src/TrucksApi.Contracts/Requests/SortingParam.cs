using System;
using System.Collections.Generic;
using System.Text;

namespace TrucksApi.Contracts.Requests
{
    public class SortingParam
    {
        public string? SortFileld { get; set; }
        public bool Ascending { get; set; }
    }
}
