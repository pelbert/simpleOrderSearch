using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearchAPI.Model
{
    public class Params
    {
        [JsonProperty("offset")]
        public int Offset { get; set; } = 0;
        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; } = 1;
        [JsonProperty("orderId")]
        public int OrderId { get; set; }
        [JsonProperty("msa")]
        public int MSA { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("totalItem")]
        public int TotalItem { get; set; }
        [JsonProperty("completionDte")]
        public DateTime CompletionDte { get; set; }
        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; } = 2;
    }
}
