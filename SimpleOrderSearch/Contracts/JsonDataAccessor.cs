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

        public IEnumerable<OrderInfo> GetAll()
        {
           return GetData();
        }

        /// <summary>
        /// Returns data based using a HOF as a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<OrderInfo> GetAllRequested(Func<OrderInfo, bool> predicate)
        {
            return GetData().Where(predicate); ;
        }

        public OrderInfo GetById(int id)
        {
            return GetData().ToList().Find(p => p.OrderID == id);
        }

        public OrderInfo GetDataFromSource()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<OrderInfo> GetData()
        {
            string text = null;

            try
            {
                text = File.ReadAllText(@"..\SimpleOrderSearch\Data\orderInfo.json");
            }
            catch
            {
                throw;
            }
            var orders = JsonConvert.DeserializeObject<List<OrderInfo>>(text);
            return orders;
        }
    }
}
