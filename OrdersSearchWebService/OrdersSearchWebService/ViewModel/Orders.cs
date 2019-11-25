using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersSearchWebService.ViewModel
{
    [Serializable]
    public class Orders
    {
        public int OrderId { get; set; }
        
        public int ShipperId { get; set; }
        
        public int DriverId { get; set; }
        
        public DateTime CompletionDte { get; set; }
        
        public int Status { get; set; }
        
        public string Code { get; set; }
        
        public int MSA { get; set; }
        
        public float Duration { get; set; }
        
        public int OfferType { get; set; }
    }
}