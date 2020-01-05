using GraphQL.Types;
using SimpleOrderSearch.Model;
using SimpleOrderSearch.Service.Contracts;
using SimpleOrderSearch.Service.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types.Relay.DataObjects;
using GraphQL.Relay.Types;

namespace SimpleOrderSearch.Service.GraphQL.Query
{
    /// <summary>
    /// encapsulates querying for an order using graphql.
    /// </summary>
    public class OrderGraphQLQuery : ObjectGraphType
    {
        private const string OrderIdArgName = "orderId";
        private const string MsaArgName = "msa";
        private const string StatusArgName = "status";
        private const string CompletionDateArgName = "completionDate";
        private const string FirstArgName = "first";
        private const string cursorArgName = "cursor";
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="dataAccessor"></param>
        public OrderGraphQLQuery(IDataAccessor<OrderInfo> dataAccessor)
        {
            Field<ListGraphType<OrderType>>("Orders", arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = OrderIdArgName, Description = "OrderId" },
                new QueryArgument<IntGraphType> { Name = MsaArgName, Description = "MSA" },
                new QueryArgument<IntGraphType> { Name = StatusArgName, Description = "Status" },
                new QueryArgument<DateGraphType> { Name = CompletionDateArgName, Description = "CompletionDate" },
                new QueryArgument<GuidGraphType> { Name = cursorArgName, Description = "Cursor" },
                new QueryArgument<IntGraphType> { Name = FirstArgName, Description = "First order to return." }), resolve: ctx =>
                {
                    var completionDate = ctx.GetArgument<DateTime?>(CompletionDateArgName);
                    var msa = ctx.GetArgument<int?>(MsaArgName);
                    var status = ctx.GetArgument<int?>(StatusArgName);
                    var orderId = ctx.GetArgument<int?>(OrderIdArgName);

                    Func<OrderInfo, bool> predicate = p => completionDate.HasValue &&
                                          p.CompletionDte.Date == completionDate.Value.Date && ((orderId.HasValue && p.OrderID == orderId.Value) ||
                                          msa.HasValue && status.HasValue && msa.Value == p.MSA && status.Value == p.Status);

                    return dataAccessor.GetAllRequested(predicate);
                });

            Connection<OrderType>().Name("OrdersConnection")
                                  .Bidirectional()
                                  .Argument<IdGraphType>(name: OrderIdArgName, description: "OrderId")
                                  .Argument<IntGraphType>(name: MsaArgName, description: "MSA")
                                  .Argument<IntGraphType>(name: StatusArgName, description: "Status")
                                  .Argument<DateGraphType>(name: CompletionDateArgName, description: "Completion Date")
                                  .Argument<IntGraphType>(name: "first", description: "first")
                                  .Argument<IntGraphType>(name: "after", description: "after")
                                  .Argument<IntGraphType>(name: "pageSize", description: "page size")
                                  ////.Argument<GuidGraphType>(name: cursorArgName, description: "cursors of orders")
                                  .Resolve(ctx =>
                                  {
                                      var f = ctx.First;
                                      var a = ctx.After;
                                      var c = ctx.PageSize;
                                      var src = ctx.Source;
                                      var tc = ctx.TotalCount;
                                      var sz = ctx.PageSize;

                                      var completionDate = ctx.GetArgument<DateTime?>(CompletionDateArgName);
                                      var msa = ctx.GetArgument<int?>(MsaArgName);
                                      var status = ctx.GetArgument<int?>(StatusArgName);
                                      var orderId = ctx.GetArgument<int?>(OrderIdArgName);

                                      Func<OrderInfo, bool> predicate = p => completionDate.HasValue &&
                                                            p.CompletionDte.Date == completionDate.Value.Date && ((orderId.HasValue && p.OrderID == orderId.Value) ||
                                                            msa.HasValue && status.HasValue && msa.Value == p.MSA && status.Value == p.Status);
                                      // return dataAccessor.GetAllRequested(predicate);
                                      var results = dataAccessor.GetAllRequested(predicate).OrderBy(p => p.OrderID);
                                      //.SkipWhile(p => p.OrderID != ctx.First.Value);
                                      return ConnectionUtils.ToConnection(results, ctx);
                                  });
        }
    }
}
