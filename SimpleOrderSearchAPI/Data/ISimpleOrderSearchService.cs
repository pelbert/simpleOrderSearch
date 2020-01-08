using SimpleOrderSearchAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearchAPI.Data
{
    public interface ISimpleOrderSearchService
    {
        PageList<Order> GetSearchOrders(Params param);

    }
}
