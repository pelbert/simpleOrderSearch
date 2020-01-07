using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuYaDemo.Models.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "Order Id")]
        [JsonProperty]
        [Range(1, 9999999999, ErrorMessage = "You have entered an invalid Order ID")]
        public int OrderID { get; set; }

        [Display(Name = "Shipper Id")]
        [JsonProperty]
        public int ShipperID { get; set; }
       
        [Display(Name = "Driver Id")]
        [JsonProperty]
        public int DriverID { get; set; }
        
        [Display(Name = "Completion Date")]
        [JsonProperty]
        public DateTime? CompletionDte { get; set; }
        
        [Display(Name = "Status")]
        [JsonProperty]
        public int? Status { get; set; }
        
        [Display(Name = "Code")]
        [JsonProperty]
        public string Code { get; set; }
        
        [Display(Name = "MSA")]
        [JsonProperty]
        public int? MSA { get; set; }
        
        [Display(Name = "Duration")]
        [JsonProperty]
        public decimal? Duration { get; set; }
        
        [Display(Name = "Offer Type")]
        [JsonProperty]
        public int? OfferType { get; set; }       
    }
}