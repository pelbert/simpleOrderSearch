using SimpleOrderSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SimpleOrderSearch.Desktop.ProxyClient
{
    public static class OrderSearchProxy
    {
        const string uri = "https://localhost:44345/api/ordersearch";
        
        public static PagedOrderInfo GetOrders(OrderSearchQuery searchQuery)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddParameter("OrderNo", searchQuery.OrderNumber);
            request.AddParameter("Status", searchQuery.Status);
            request.AddParameter("MSA", searchQuery.MSA);
            request.AddParameter("Page", searchQuery.Page);
            request.AddParameter("CompletionDate", searchQuery.CompletionDate);
            request.AddParameter("PageLimit", searchQuery.PageLimit);
            IRestResponse response = client.Execute(request);

            RestSharp.Serialization.Json.JsonDeserializer jsonDeserializer = new RestSharp.Serialization.Json.JsonDeserializer();
            var pagedOrderInfo = jsonDeserializer.Deserialize<PagedOrderInfo>(response);
            return pagedOrderInfo;
        }

        public static PagedOrderInfo PostOrdersQuery(OrderSearchQuery searchQuery)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(searchQuery);
            IRestResponse response = client.ExecuteAsPost(request, Method.POST.ToString());
            RestSharp.Serialization.Json.JsonDeserializer jsonDeserializer = new RestSharp.Serialization.Json.JsonDeserializer();
            var orders = jsonDeserializer.Deserialize<PagedOrderInfo>(response);
            return orders;
        }
    }
}