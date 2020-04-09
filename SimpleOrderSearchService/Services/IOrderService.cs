using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleOrderSearchService.Models;

namespace SimpleOrderSearchService.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetByOrderID(long orderID);
        IEnumerable<Order> GetByMsaStatusCompletionDte(long msa, long status, DateTime completionDte);
    }
}
