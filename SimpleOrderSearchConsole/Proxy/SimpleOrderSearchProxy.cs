using Newtonsoft.Json;
using SimpleOrderSearchAPI.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOrderSearchConsole.Proxy
{
    public class SimpleOrderSearchProxy
    {
        public const string endpoint = "http://localhost:55768/api/SimpleOrderSearch";
        private HttpClient _client;

        public SimpleOrderSearchProxy()
        {
            _client = new HttpClient();
        }

        public async Task<GetOrdersResponseMessage> GetOrder(Params request)
        {
            UriBuilder builder = new UriBuilder(endpoint);
            builder.Query = BuildQuery(request);
            var response = await _client.GetAsync(builder.Uri);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetOrdersResponseMessage>(content);
            }
            return null;
            
        }

        private string BuildQuery(Params parameters)
        {
            string query = parameters.CompletionDte != null ? $"completionDte={parameters.CompletionDte}&" : string.Empty;
            return $"{query}offset={parameters.Offset}&pageNumber={parameters.PageNumber}&msa={parameters.MSA}&status={parameters.Status}&itemsPerPage={parameters.ItemsPerPage}&orderId={parameters.OrderId}";
        }
    }
}
