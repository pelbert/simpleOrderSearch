using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace simpleOrderSearch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public OrderResult Post([FromBody]IDictionary<string, System.Text.Json.JsonElement> request)
        {
            if(!request.ContainsKey("CompletionDte"))
            {
                throw new Exception("missing date");
            }
            //now for finding an item, we can look at either order number or a combination of MSA and Status
            //since OrderID is our "primary key", we look for that first before looking for MSA and Status combination
            if(request.ContainsKey("OrderID"))
            {
                var id = request["OrderID"].GetInt64();
                //DateTime date = DateTime.Parse(request["CompletionDte"].ToString());
                DateTime date = request["CompletionDte"].GetDateTime();
                var page = (request.ContainsKey("page")) ? Math.Max(request["page"].GetInt32(), 0) : 0;
                var take = (request.ContainsKey("size")) ? Math.Max(request["size"].GetInt32(), 0) : 0;
                return new OrderResult()
                {
                    results = OrderStore.instance.fetch(id, date, page, take),
                    pageNumber = page,
                    pageSize = (take == 0 && page > 0) ? 25 : take
                };
            }
            else if(request.ContainsKey("MSA") && request.ContainsKey("Status"))
            {
                long status = request["Status"].GetInt64();
                long msa = request["MSA"].GetInt64();
                DateTime date = request["CompletionDte"].GetDateTime();
                var page = (request.ContainsKey("page")) ? Math.Max(request["page"].GetInt32(), 0) : 0;
                var take = (request.ContainsKey("size")) ? Math.Max(request["size"].GetInt32(), 0) : 0;
                return new OrderResult()
                {
                    results = OrderStore.instance.fetch(msa, status, date, page, take),
                    pageNumber = page,
                    pageSize = (take == 0 && page > 0) ? 25 : take
                };
            }
            else
            {
                throw new Exception("To search, you must provide either an order id or a combination of MSA and status.");
            }
        }
    }
}
