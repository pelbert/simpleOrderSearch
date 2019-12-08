using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleOrderWebApi.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string Code { get; set; }

        public int ShipperID { get; set; }
        public int DriverID { get; set; }
        public DateTime CompletionDte { get; set; }
        public int Status { get; set; }

        public string MSA { get; set; }
        public string Duration { get; set; }
        public int OfferType { get; set; }


     //Paging
        public int Limit { get; set; } = 0;
        public int Offset { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}