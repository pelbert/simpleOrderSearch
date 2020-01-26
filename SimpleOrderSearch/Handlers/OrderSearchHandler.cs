using FluentValidation;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using SimpleOrderSearch.Model;
using SimpleOrderSearch.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearch.Service.Handlers
{
    /// <summary>
    /// OrderSearchHandler
    /// </summary>
    public class OrderSearchHandler : IOrderSearchHandler
    {
        private IDataAccessor<OrderInfo> dataAccessor { get; }
        private AbstractValidator<OrderSearchQuery> queryValidator { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderSearchHandler(IDataAccessor<OrderInfo> dataAccessor, AbstractValidator<OrderSearchQuery> queryValidator)
        {
            this.dataAccessor = dataAccessor;
            this.queryValidator = queryValidator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryValidator"></param>
        /// <param name="dataAccessor"></param>
        /// <returns></returns>
        public Validation<Exceptional<PagedOrderInfo>> HandleSearchOrderRequest(OrderSearchQuery query, AbstractValidator<OrderSearchQuery> queryValidator, IDataAccessor<OrderInfo> dataAccessor)
        {
           return ValidateQuery(query, queryValidator)
                  .Map(qry => GetPagedResults(qry, dataAccessor));
        }

        private Validation<OrderSearchQuery> ValidateQuery(OrderSearchQuery query, AbstractValidator<OrderSearchQuery> queryValidator)
        {
            var result = queryValidator.Validate(query);
            if (!result.IsValid)
                return Error(string.Join(Environment.NewLine, result.Errors));
            else return query;
        }
        
        private Exceptional<PagedOrderInfo> GetPagedResults(OrderSearchQuery query, IDataAccessor<OrderInfo> dataAccessor)
        {
            Func<OrderInfo, bool> prediate = p => query.CompletionDate.HasValue &&
                                             p.CompletionDte.Date == query.CompletionDate.Value.Date && ((query.OrderNumber.HasValue && p.OrderID == query.OrderNumber.Value) ||
                                             query.MSA.HasValue && query.Status.HasValue && query.MSA.Value == p.MSA && query.Status.Value == p.Status);

            IEnumerable<OrderInfo> pagedResults;
            int totalResults = 0;
            try
            {
                IEnumerable<OrderInfo> requestedOrders = dataAccessor.GetAllRequested(prediate); // this is a side effect....make sure this implementation is pure.
                totalResults = requestedOrders.Count();
                pagedResults = requestedOrders.OrderBy(p => p.OrderID)
                                                  .Skip(query.Page - 1 * query.PageLimit + 1)
                                                  .Take(query.PageLimit);
            }
            catch (Exception ex)
            {
                return ex;
            }

            return new PagedOrderInfo()
            {
                Orders = pagedResults,
                PageLimit = query.PageLimit,
                PageNumber = query.Page,
                IsStart = query.Page == 1,
                IsEnd = query.Page * query.PageLimit >= totalResults
            };
        }
    }
}