using System;
using System.Globalization;
using Newtonsoft.Json;

namespace simpleOrderSearch
{
        public class Order
        {
            [JsonProperty("OrderID")]
            public long OrderId { get; set; }

            [JsonProperty("ShipperID")]
            public long ShipperId { get; set; }

            [JsonProperty("DriverID")]
            public long DriverId { get; set; }

            [JsonProperty("CompletionDte")]
            public DateTime CompletionDte { get; set; }

            [JsonProperty("Status")]
            public long Status { get; set; }

            [JsonProperty("Code")]
            public string Code { get; set; }

            [JsonProperty("MSA")]
            public long Msa { get; set; }

            [JsonProperty("Duration")]
            public string Duration { get; set; }

            [JsonProperty("OfferType")]
            public long OfferType { get; set; }
        }

}
