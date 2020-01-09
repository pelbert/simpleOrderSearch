using Newtonsoft.Json;
using System;

namespace SimpleOrderSearchAPI.Model
{
    public class Order
    {
        [JsonProperty("OrderID")]
        public int OrderId { get; set; }
        [JsonProperty("ShipperID")]
        public int ShipperID { get; set; }
        [JsonProperty("DriverID")]
        public int DriverID { get; set; }
        [JsonProperty("CompletionDte")]
        public DateTime CompletionDte { get; set; }
        [JsonProperty("Status")]
        public int Status { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("MSA")]
        public int MSA { get; set; }
        [JsonProperty("Duration")]
        public string Duration { get; set; }
        [JsonProperty("OfferType")]
        public int OfferType { get; set; }
    }
}
