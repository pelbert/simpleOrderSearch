using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSearch.Model;
using SimpleOrderSearch.Service.Contracts;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SimpleOrderSearch.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSearchController : ControllerBase
    {
        IDataAccessor<OrderInfo> dataAccessor;

        public OrderSearchController(IDataAccessor<OrderInfo> dataAccessor)
        {
            this.dataAccessor = dataAccessor;
        }

        // GET: api/OrderSearch
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ObjectResult Get(int? orderNo, int? status, int? msa, DateTime? completionDate, int page = 1, int pageLimit = 5)
        {
            if (!completionDate.HasValue && !orderNo.HasValue || (!msa.HasValue || !status.HasValue))
                return BadRequest("Invalid CompletionDate, OrderNo, MSA or StatusValue Parameter.");

            Func<OrderInfo, bool> prediate = p => completionDate.HasValue && p.CompletionDte.Date == completionDate.Value.Date && ((orderNo.HasValue && p.OrderID == orderNo.Value) ||
                                             msa.HasValue && status.HasValue && msa.Value == p.MSA && status.Value == p.Status);

            IEnumerable<OrderInfo> requestedOrders = this.dataAccessor.GetAllRequested(prediate);
            var totalResults = requestedOrders.Count();
            var pagedResults = requestedOrders.OrderBy(p => p.OrderID)
                                              .Skip(page - 1 * pageLimit + 1)
                                              .Take(pageLimit);

            return Ok(new PagedOrderInfo()
            {
                 Orders = pagedResults,
                 PageLimit = pageLimit,
                 PageNumber = page,
                 IsStart = page == 1,
                 IsEnd = page * pageLimit >= totalResults
            });
        }
    }
}
