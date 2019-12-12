using System;
using System.Linq;
using System.Collections.Generic;

namespace simpleOrderSearch
{
    public class OrderResult
    {
        public IEnumerable<Order> results
        {
            get;
            set;
        }

        public int count
        {
            get { return results.Count(); }
        }

        public int pageNumber
        {
            get;
            set;
        }

        public int pageSize
        {
            get;
            set;
        }
    }
}
