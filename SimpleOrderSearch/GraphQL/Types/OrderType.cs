using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using SimpleOrderSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearch.Service.GraphQL.Types
{
    /// <summary>
    /// GraphQl Order Type
    /// </summary>
    public class OrderType : ObjectGraphType<OrderInfo>
    {
        /// <summary>
        /// GraphQl OrderType over OrderInfo object.
        /// </summary>
        public OrderType()
        {
            Name = "Order";

            Field(x => x.OrderID, type: typeof(IdGraphType)).Description("Id of the order");
            Field(x => x.MSA, type: typeof(IntGraphType)).Description("MSA");
            Field(x => x.Status, type: typeof(IntGraphType)).Description("Status");
            Field(x => x.CompletionDte, type: typeof(DateGraphType)).Description("The completion date of the order");
            Field(x => x.DriverID, type: typeof(IntGraphType)).Description("Id of the driver");
            Field(x => x.Duration, type: typeof(DecimalGraphType)).Description("The duration of the order.");
            Field(x => x.OfferType, type: typeof(IntGraphType)).Description("Offer type");
            Field(x => x.ShipperID, type: typeof(IntGraphType)).Description("The shipper Id.");
            Field(x => x.Code, type: typeof(StringGraphType)).Description("code.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    //public class OrderInfoConnection : Connection<OrderType>
    //{
    //    public OrderInfoConnection()
    //    {
              
    //    }

           
    //    ////public PageInfoType()
    //    ////{
    //    ////    Field<ListGraphType<CompanyType>>(
    //    ////        "company",
    //    ////        resolve: context => context.Source.List
    //    ////    );
    //    ////    Field(xx => xx.Chars);
    //    ////    Field(xx => xx.PageCount);
    //    ////    Field(xx => xx.Size);
    //    ////    Field(xx => xx.TotalCount);
    //    ////}
    //}
}
