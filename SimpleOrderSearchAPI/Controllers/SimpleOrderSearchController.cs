using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleOrderSearchAPI.Data;
using SimpleOrderSearchAPI.Model;

namespace SimpleOrderSearchAPI.Controllers
{
    [Route("api/[controller]")]
    public class SimpleOrderSearchController : Controller
    {
        private readonly ISimpleOrderSearchService _service;
        public SimpleOrderSearchController(ISimpleOrderSearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetOrders([FromQuery]Params orderParam)
        {
            try
            {
                var orders = _service.GetSearchOrders(orderParam);
                var pagination = new Pagination
                {
                    CurrentPage = orders.CurrentPage,
                    ItemPerPage = orders.ItemPerPage,
                    TotalItems = orders.TotalItem,
                    TotalPages = orders.TotalPages,
                    Offset = orders.Offset
                };
                var result = new GetOrdersResponseMessage
                {
                    Orders = orders,
                    Pagination = pagination
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
            
        }
    }
}