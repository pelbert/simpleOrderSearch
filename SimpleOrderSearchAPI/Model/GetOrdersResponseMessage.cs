using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearchAPI.Model
{
    public class GetOrdersResponseMessage
    {
        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
