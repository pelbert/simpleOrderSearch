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

        public PageInfo PageInfo { get; set; }

        public string Cursor { get; set; }
        //public string AfterCursor { get; set; }

        //public string  BeforeCursor { get; set; }

        public bool? IsPageUp { get; set; } = null;
    }
}
