using FluentValidation;
using LaYumba.Functional;
using SimpleOrderSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearch.Service.Contracts
{
    public interface IOrderSearchHandler
    {
        Validation<Exceptional<PagedOrderInfo>> HandleSearchOrderRequest(OrderSearchQuery query, AbstractValidator<OrderSearchQuery> queryValidator, IDataAccessor<OrderInfo> dataAccessor);

    }
}