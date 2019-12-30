using FluentValidation;
using SimpleOrderSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearch.Service.Validators
{
    public class OrderSearchQueryValidator : AbstractValidator<OrderSearchQuery>
    {
        public OrderSearchQueryValidator()
        {
            // CompletionDate set as top level validation rule. If completion date validation fails the dependent validations won't run.
            this.RuleFor(query => query.CompletionDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEqual(DateTime.MinValue)
                .DependentRules(() => 
                {
                    RuleFor(q => q).Must(HaveValidQuery).WithMessage(q => $"Invalid Order Number ({GetValue(q.OrderNumber)}) or Invalid MSA ({GetValue(q.MSA)}) and Status ({GetValue(q.Status)}) combination");
                });
        }

        private string GetValue(object obj) => obj == null ? "null" : obj.ToString();

        private bool HaveValidQuery(OrderSearchQuery query)
        {
            bool isValid = HaveValidOrderNumber(query) || (HaveValidMSA(query) && HaveValidStatus(query));

            return isValid;
        }

        private bool HaveValidOrderNumber(OrderSearchQuery arg)
        {
            bool isValid = arg.OrderNumber.HasValue && arg.OrderNumber.Value > 0;

            return isValid;
        }

        private bool HaveValidMSA(OrderSearchQuery qry)
        {
            bool isValid =  qry == null ? false : qry.MSA.HasValue;
            return isValid;
        }

        private bool HaveValidStatus(OrderSearchQuery qry)
        {
            bool isValid = qry == null ? false : qry.Status.HasValue;
            return isValid;
        }
    }
}
