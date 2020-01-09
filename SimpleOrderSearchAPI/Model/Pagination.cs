using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearchAPI.Model
{
    public class Pagination
    {
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; } = 1;
        [JsonProperty("itemPerPage")]
        public int ItemPerPage { get; set; } = 2;
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}
