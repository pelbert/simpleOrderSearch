using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using DynamicData.Binding;
using System.Collections.ObjectModel;
using SimpleOrderSearch.Model;
//using System.Reactive;
using SimpleOrderSearch.Desktop.ProxyClient;
using DynamicData;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Reactive;

namespace SimpleOrderSearch.Desktop
{
    public class MainViewModel : ReactiveValidationObject<MainViewModel>
    {
        List<string> _cursorList = new List<string>();

        public ValidationContext OrderParametersRule { get; private set; }

        public ValidationHelper CompletionDateRule { get; private set; }
        public ValidationHelper OrderNumberRule { get; private set; }
        public ValidationHelper StatusRule { get; private set; }
        public ValidationHelper MSARule { get; private set; }

        [Reactive] public string OrderNo { get; set; }
        [Reactive] public string MSA { get; set; }
        [Reactive] public string Status { get; set; }
        [Reactive] public DateTime? CompletionDate { get; set; } = DateTime.Now;

        [Reactive] public int PageSize { get; set; } = 5;
        [Reactive] public int PageNo { get; set; } = 1;
        [Reactive] public bool HasValidCriteria { get; set; } = true;
        [Reactive] public bool CanPageUp { get; set; }
        [Reactive] public bool CanPageDown { get; set; }
        [Reactive] public string ErrorMsg { get; set; }

        [Reactive] public PageInfo CurrentPageInfo { get; set; } = new PageInfo();
        private string CurrentCursor { get; set; } = string.Empty;


        public ObservableCollection<OrderInfo> Orders { get; set; } = new ObservableCollection<OrderInfo>();

        ReactiveCommand<Unit, Unit> _rxSearchCommand;
        ReactiveCommand<Unit, Unit> _rxCommandPageUp;
        ReactiveCommand<Unit, Unit> _rxCommandPageDown;
        public ReactiveCommand<Unit, Unit> RxSearchCommad => _rxSearchCommand;
        public ReactiveCommand<Unit, Unit> RxCommandPageUp => _rxCommandPageUp;
        public ReactiveCommand<Unit, Unit> RxCommandPageDown => _rxCommandPageDown;

        public MainViewModel()
        {
            this.WhenAnyValue(x => x.MSA, x => x.Status, x => x.OrderNo, x => x.CompletionDate).Do(OnNextParamter).Subscribe();
            var canSearch = this.WhenAnyValue(x => x.HasValidCriteria);

            //var canPageUp = this.WhenAnyValue(x => x.HasValidCriteria, x => x.Orders.Count, x => x.CanPageUp, x => x.PageNo).Select(x => x.Item1 && x.Item2 > 0 && !x.Item3 && x.Item4 > 0);
            //var canPageDown = this.WhenAnyValue(x => x.HasValidCriteria, x => x.Orders.Count, x => x.CanPageDown, x => x.PageNo).Select(x => x.Item1 && x.Item2 > 0 && !x.Item3 && x.Item4 > 0);

            var canPageUp = this.WhenAnyValue(x => x.CurrentPageInfo).Select(x => x.HasNextPage);
            var canPageDown = this.WhenAnyValue(x => x.CurrentPageInfo).Select(x => x.HasPreviousPage);



            _rxSearchCommand = ReactiveCommand.Create(() => OnSearch(true), canSearch);
            _rxCommandPageUp = ReactiveCommand.Create(() => OnPageUp(), canPageUp);
            _rxCommandPageDown = ReactiveCommand.Create(() => OnPageDown(), canPageDown);

            ////OrderNumberRule = this.ValidationRule(vm => vm.OrderNo,
            ////                                      orderid => 
            ////                                      {
            ////                                          var val = NumericValidation(orderid);
            ////                                          return val.isValid && val.parsedValue > 0 && !NumericValidation(this.MSA).isValid && !NumericValidation(this.Status).isValid;
            ////                                      } ,
            ////                                      orderid => $"Order Number { (string.IsNullOrWhiteSpace(orderid) ? null : orderid) } Is Invalid.");

            ////MSARule = this.ValidationRule(vm => vm.MSA,
            ////                          orderid =>
            ////                          {
            ////                              var val = NumericValidation(orderid);
            ////                              return val.isValid && val.parsedValue > 0 && !NumericValidation(this.MSA).isValid && !NumericValidation(this.Status).isValid;
            ////                          },
            ////                          orderid => $"Order Number { (string.IsNullOrWhiteSpace(orderid) ? null : orderid) } Is Invalid.");

            ////StatusRule = this.ValidationRule(vm => vm.Status,
            ////                          orderid =>
            ////                          {
            ////                              var val = NumericValidation(orderid);
            ////                              return val.isValid && val.parsedValue > 0 && !NumericValidation(this.MSA).isValid && !NumericValidation(this.Status).isValid;
            ////                          },
            ////                          orderid => $"Order Number { (string.IsNullOrWhiteSpace(orderid) ? null : orderid) } Is Invalid.");

        }

        private void OnPageDown()
        {
            PageNo -= 1;
            OnSearch(false, false);
        }

        private void OnPageUp()
        {
            PageNo += 1;
            OnSearch(false, true);
        }

        private async void OnSearch(bool isCursorReset = true, bool? isPageUp = null)
        {
            if (isCursorReset)
                _cursorList.Clear();
            else
            {
                if (isPageUp.HasValue && isPageUp.Value == false)
                {
                    CurrentCursor = _cursorList[this.PageNo];
                }
            }

            OrderSearchQuery query = new OrderSearchQuery()
            {
                CompletionDate = this.CompletionDate,
                MSA = MSA.ToNullableValue<int>(),
                OrderNumber = OrderNo.ToNullableValue<int>(),
                Page = this.PageNo,
                PageLimit = this.PageSize,
                Status = Status.ToNullableValue<int>(),
                Cursor = isCursorReset ? string.Empty : CurrentCursor,
                IsPageUp = isPageUp.HasValue == false ? (bool?)null : isPageUp.HasValue && isPageUp.Value
            };

            ////var orderResults = OrderSearchProxy.GetOrders(query);
            var results = await OrderSearchProxy.PostOrderGraphQLQuery(query);
            if (results != null)
            {
                if (results.PageInfo != null)
                {
                    results.PageInfo.HasPreviousPage = this.PageNo > 1;

                    CurrentPageInfo = results.PageInfo;

                    string cursor = results.Edges.LastOrDefault()?.Cursor ?? string.Empty;

                    CurrentCursor = cursor;

                    if (isCursorReset || (isPageUp != null && !_cursorList.Contains(cursor)))
                        _cursorList.Add(cursor);

                    Orders.Clear();

                    foreach (var node in results.Edges.Select(p => p.Node))
                    {
                        Orders.Add(node);
                    }
                }
            }

            //this.CanPageDown = orderResults.IsStart;
            //this.CanPageUp = orderResults.IsEnd;
            //Orders.Clear();

            //if (graphQlQryResults != null)
            //{
            //    graphQlQryResults.ForEach((order) => { Orders.Add(order); });
            //}

            ////if (orderResults.IsValid)
            ////{
            ////    foreach (var result in orderResults?.Orders)
            ////    {
            ////        Orders.Add(result);
            ////    }
            ////}
            ////else if (orderResults.ErrorResponse != null && orderResults.ErrorResponse.Errors.Any())
            ////{
            ////    ErrorMsg = string.Join("|", orderResults.ErrorResponse.Errors.Select(p => p.Message));
            ////}
        }

        private void OnNextParamter((string msa, string status, string orderno, DateTime? completionDate) obj)
        {
            List<string> errors = new List<string>();

            if (!obj.completionDate.HasValue)
                errors.Add("Valid CompletionDate Required.");

            var orderObj = NumericValidation(obj.orderno);
            var msaObj = NumericValidation(obj.msa);
            var statusObj = NumericValidation(obj.status);

            if (!orderObj.isValid && !msaObj.isValid && !statusObj.isValid)
                errors.Add("Valid Numeric OrderNo or Valid Numeric Status and MSA value Required.");
            else if (!orderObj.isValid)
            {
                if (msaObj.isValid && !statusObj.isValid)
                    errors.Add("Numeric Status Value Required.");
                else if (!msaObj.isValid && statusObj.isValid)
                    errors.Add("Numeric MSA Value Required.");
            }
            HasValidCriteria = !errors.Any();
            ErrorMsg = string.Join("|", errors);
        }

        private (object orgValue, int parsedValue, bool isValid) NumericValidation(object o)
        {
            int parsedValue;
            bool isValid = int.TryParse(o == null ? string.Empty : o.ToString(), out parsedValue);
            return (o, parsedValue, isValid);
        }
    }
}