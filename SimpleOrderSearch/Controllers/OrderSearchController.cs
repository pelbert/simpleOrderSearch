using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using FluentValidation.Results;
using LaYumba.Functional;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSearch.Model;
using SimpleOrderSearch.Service.Contracts;
using static LaYumba.Functional.Either;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SimpleOrderSearch.Service.Controllers
{
    /// <summary>
    /// Order Search COntroller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSearchController : ControllerBase
    {
        IDataAccessor<OrderInfo> dataAccessor;
        AbstractValidator<OrderSearchQuery> queryValidator;
        IOrderSearchHandler handler;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="queryValidator"></param>
        public OrderSearchController(IDataAccessor<OrderInfo> dataAccessor, AbstractValidator<OrderSearchQuery> queryValidator, IOrderSearchHandler handler)
        {
            this.dataAccessor = dataAccessor;
            this.queryValidator = queryValidator;
            this.handler = handler;
        }


        // GET: api/OrderSearch
        /// <summary>
        /// Gets searched orders results by criteria. Results are returned paged.
        /// </summary>
        /// <param name="orderNo">Not required but must be greater than zero (0) if specified.</param>
        /// <param name="status">Not required. If specified must be in combination with msa.</param>
        /// <param name="msa">Not required. If specified must be in combination with status.</param>
        /// <param name="completionDate">Always required</param>
        /// <param name="page">Indicates number page to be retrived.</param>
        /// <param name="pageLimit">Indicates max number of results to be returned per page.</param>
        /// <returns>PagedOrderInfo</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Get([FromQuery] int? orderNo, int? status, int? msa, DateTime? completionDate, int page = 1, int pageLimit = 5) =>
                                        this.handler.HandleSearchOrderRequest(new OrderSearchQuery() // this can be improved.
                                        {
                                            MSA = msa,
                                            Status = status,
                                            OrderNumber = orderNo,
                                            CompletionDate = completionDate,
                                            Page = page,
                                            PageLimit = pageLimit
                                        },
                                        this.queryValidator,
                                        this.dataAccessor)
                                            .Match(Invalid: (p) => BadRequest(p.Select(x => x.Message)),
                                                   Valid: result => result.Match(Exception: (e) => StatusCode(500, "Exception Occurred In Service."),
                                                                                 Success: (pagedResult) => Ok(pagedResult)));
    }
}