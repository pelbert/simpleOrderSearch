using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using SimpleOrderSearchService.Models;

namespace SimpleOrderSearchService.Services
{
    public class OrderService : IOrderService
    {
        public IEnumerable<Order> GetAll()
        {
            string jsonBlob;
            using (var reader = new StreamReader("Data/orderInfo.json"))
            {
                jsonBlob = reader.ReadToEnd();
            }

            var orders = JsonConvert.DeserializeObject<List<Order>>(jsonBlob);
            return orders;
        }

        public IEnumerable<Order> GetByOrderID(long orderID)
        {
            return GetAll().Where(order => order.OrderID == orderID);
        }

        public IEnumerable<Order> GetByMsaStatusCompletionDte(long msa, long status, DateTime completionDte)
        {
            return GetAll().Where( order
                => order.MSA == msa
                && order.Status == status
                && order.CompletionDte == completionDte);
        }
    }
}
