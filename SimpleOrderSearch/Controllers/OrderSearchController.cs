using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSearch.Model;
using SimpleOrderSearch.Service.Contracts;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="queryValidator"></param>
        public OrderSearchController(IDataAccessor<OrderInfo> dataAccessor, AbstractValidator<OrderSearchQuery> queryValidator)
        {
            this.dataAccessor = dataAccessor;
            this.queryValidator = queryValidator;
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
        public ObjectResult Get([FromQuery] int? orderNo, int? status, int? msa, DateTime? completionDate, int page = 1, int pageLimit = 5)
        {
            var validationResponse = queryValidator.Validate(new OrderSearchQuery()
            {
                MSA = msa,
                Status = status,
                OrderNumber = orderNo,
                CompletionDate = completionDate,
                Page = page,
                PageLimit = pageLimit
            });

            if (!validationResponse.IsValid)
            {
                var errorResponse = new ErrorResponse()
                {
                    Errors = validationResponse.Errors
                                               .Select(p => new ErrorModel()
                                                {
                                                    FieldName = p.PropertyName,
                                                    Message = p.ErrorMessage
                                                })
                                               .ToList()
                };
                return BadRequest(new PagedOrderInfo() { ErrorResponse = errorResponse,  IsValid = false });
            }

            Func<OrderInfo, bool> prediate = p => completionDate.HasValue && 
                                                  p.CompletionDte.Date == completionDate.Value.Date && ((orderNo.HasValue && p.OrderID == orderNo.Value) ||
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