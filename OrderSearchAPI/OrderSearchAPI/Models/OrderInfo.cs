using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderSearchAPI.Models
{

    public class OrderInfo
    {
        public class OrderData
        {
            public int OrderID { get; set; }
            public int ShipperID { get; set; }
            public int DriverID { get; set; }
            public DateTime CompletionDte { get; set; }
            public int Status { get; set; }
            public string Code { get; set; }
            public int MSA { get; set; }
            public string Duration { get; set; }
            public int OfferType { get; set; }
        }

    }
}