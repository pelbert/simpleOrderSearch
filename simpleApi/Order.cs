using System;

namespace simpleApi
{
    public class Order
    {
        public int OrderID { get; set; }
        public int ShipperID { get; set; }
        public int DriverID { get; set; }
        public DateTime CompletionDte { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public int MSA { get; set; }
        public double Duration { get; set; }
        public int OfferType { get; set; }
    }
}
