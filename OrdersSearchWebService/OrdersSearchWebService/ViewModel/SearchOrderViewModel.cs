using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrdersSearchWebService.ViewModel
{
    [Serializable]
    public class SearchOrderViewModel
    {        
        public int OrderId { get; set; }
        
        public int MSA { get; set; }
        
        [Required]
        public DateTime CompletionDte { get; set; }
        
        public int Status { get; set; }
    }
}