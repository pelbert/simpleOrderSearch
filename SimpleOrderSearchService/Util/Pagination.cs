using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearchService.Util
{
    public static class Pagination
    {
        public static IEnumerable<T> SelectItems<T>(IEnumerable<T> items, int itemsPerPage, int pageIdx)
        {
            return items.Skip(pageIdx * itemsPerPage).Take(itemsPerPage);
        }
    }
}
