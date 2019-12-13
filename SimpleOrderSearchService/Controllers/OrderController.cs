using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSearchService.Models;
using SimpleOrderSearchService.Services;
using SimpleOrderSearchService.Util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleOrderSearchService.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly int _defaultItemsPerPage = 3;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // note: returning IActionResult instead of the actual data type so we can also return errors

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get
            ( [FromQuery] long? orderID
            , [FromQuery] long? msa
            , [FromQuery] long? status
            , [FromQuery] DateTime? completionDte
            , [FromQuery] int? page
            , [FromQuery] int? itemsPerPage
            )
        {
            // sorry, but the specification requiring an OrderID AND a
            // CompletionDte seems so wrong that I'll be implementing things as
            // if valid search criteria are:
            //      1: OrderID
            //      2: MSA and Status and CompletionDte
            // and am very open to changing things once I become convinced that
            // always requiring the CompletionDte is a good/necessary thing

            // yes, I am assuming OrderID will be unique

            if (orderID.HasValue)
            {
                return Ok(_orderService.GetByOrderID(orderID.Value));
            }
            else if (msa.HasValue && status.HasValue && completionDte.HasValue)
            {
                var safePageIdx = page.HasValue ? page.Value : 0;
                var safeItemsPerPage = itemsPerPage.HasValue ? itemsPerPage.Value : _defaultItemsPerPage;

                var orders = _orderService.GetByMsaStatusCompletionDte(msa.Value, status.Value, completionDte.Value);
                var pagedOrders = Pagination.SelectItems(orders, safeItemsPerPage, safePageIdx);
                return Ok(pagedOrders);
            }

            return ValidationProblem("you need to specify orderID or {msa, status, completionDte}");
            //return Ok(_orderService.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{orderID}")]
        public IActionResult Get(long orderID)
        {
            var order = _orderService.GetByOrderID(orderID).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
