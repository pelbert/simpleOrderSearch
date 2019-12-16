using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleOrderSearch.Model
{
    public class OrderSearchQuery
    {
        public OrderSearchQuery()
        {

        }
        public int? OrderNumber { get; set; }

        public int? MSA { get; set; }

        public int? Status { get; set; }

        public DateTime? CompletionDate { get; set; }

        public int Page { get; set; }

        public int Offset { get; set; }

        public int PageLimit { get; set; }
    }
}
