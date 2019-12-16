using SimpleOrderSearch.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleOrderSearch.Service.Contracts
{
    public class JsonDataAccessor : IDataAccessor<OrderInfo>
    {
        private List<OrderInfo> allOrders = new List<OrderInfo>();
        public JsonDataAccessor()
        {
            var text = File.ReadAllText(@"..\SimpleOrderSearch\Data\orderInfo.json");
            allOrders = JsonConvert.DeserializeObject<List<OrderInfo>>(text);
        }

        public IEnumerable<OrderInfo> GetAll()
        {
            return allOrders;
        }

        public IEnumerable<OrderInfo> GetAllRequested(Func<OrderInfo, bool> predicate)
        {
            var orders =  allOrders.Where(predicate);
            return orders;
        }

        public OrderInfo GetById(int id)
        {
            return allOrders.Find(p => p.OrderID == id);
        }

        public OrderInfo GetDataFromSource()
        {
            throw new NotImplementedException();
        }
    }
}
