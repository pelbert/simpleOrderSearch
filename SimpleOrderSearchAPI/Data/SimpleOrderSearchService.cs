using Newtonsoft.Json;
using SimpleOrderSearchAPI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleOrderSearchAPI.Data
{
    public class SimpleOrderSearchService : ISimpleOrderSearchService
    {
        public PageList<Order> GetSearchOrders(Params request)
        {
            try
            {
                var orders = GetAllOrders();
                var selectOrders = orders.Where(a =>
                                    (a.OrderId == request.OrderId || (a.MSA == request.MSA && a.Status == request.Status))
                                    && a.CompletionDte == request.CompletionDte)
                                    .Select(a => a).ToList();
                return PageList<Order>.CreateAsync(selectOrders, request.PageNumber, request.ItemsPerPage, request.Offset);
            }
            catch
            {
                throw;
            }
        }

        private List<Order> GetAllOrders()
        {
            try
            {
                var a = Directory.GetCurrentDirectory();
                List<Order> orders = new List<Order>();
                using (StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + @"/Resources/orderInfo.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    orders = (List<Order>)serializer.Deserialize(file, typeof(List<Order>));
                };
                return orders;
            }
            catch
            {
                throw;
            }
        }
    }
}
