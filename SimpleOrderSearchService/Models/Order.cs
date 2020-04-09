using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearchService.Models
{
    public class Order
    {
        public long OrderID { get; set; }
        public long ShipperID { get; set; }
        public long DriverID { get; set; }
        public DateTime CompletionDte { get; set; }
        public long Status { get; set; }
        public string Code { get; set; }
        public long MSA { get; set; }
        public decimal Duration { get; set; }
        public long OfferType { get; set; }
    }
}
