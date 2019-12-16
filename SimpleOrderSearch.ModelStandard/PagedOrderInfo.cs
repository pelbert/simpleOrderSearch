using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleOrderSearch.Model
{
    public class PagedOrderInfo
    {
        public IEnumerable<OrderInfo> Orders { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageLimit { get; set; } = 5;

        public int Offset { get; set; } = 0;

        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
    }
}

