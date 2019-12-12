using System;
using System.Linq;
using System.Collections.Generic;

namespace simpleOrderSearch
{
    public class OrderResult
    {
        public IEnumerable<Order> Results
        {
            get;
            set;
        }

        public int Count
        {
            get { return Results.Count(); }
        }

        public int PageNumber
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }
    }
}
