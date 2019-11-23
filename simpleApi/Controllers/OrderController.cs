using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace simpleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private List<Order> orders;
        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
            orders = new List<Order>();
        }

        private async Task ReadJson() {
            string filename = "data/orderinfo.json";
            string body = "";
            Console.WriteLine("reading file");
            using (var sr = new StreamReader(filename)) {
                body = await sr.ReadToEndAsync();
                orders = JsonConvert.DeserializeObject<List<Order>>(body);
                Console.WriteLine(orders.Count);
            }
        }

        [HttpGet]
        public async Task<List<Order>> Get()
        {
            if (orders.Count == 0) {
                await ReadJson();
            }
            return orders;
        }

        [HttpGet("{id}")]
        public async Task<Order> GetOrderById(int id) {
             if (orders.Count == 0) {
                await ReadJson();
            }
            return orders.SingleOrDefault(order => order.OrderID == id);
        }
    }
}
