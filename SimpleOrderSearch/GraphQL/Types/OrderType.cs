using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using SimpleOrderSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types.Relay;
using GraphQL.Relay.Types;
using SimpleOrderSearch.Service.Contracts;

namespace SimpleOrderSearch.Service.GraphQL.Types
{
    /// <summary>
    /// GraphQl Order Type
    /// </summary>
    public class OrderType : ObjectGraphType<OrderInfo>
    ////public class OrderType : NodeGraphType<OrderInfo>
    {
        private readonly IDataAccessor<OrderInfo> data;

        /// <summary>
        /// GraphQl OrderType over OrderInfo object.
        /// </summary>
        public OrderType()
        ////public OrderType()
        {
            Name = "Order";

            ////Id(p => p.OrderID);
            Field(x => x.OrderID, type: typeof(IdGraphType)).Description("Id of the order");
            Field(x => x.MSA, type: typeof(IntGraphType)).Description("MSA");
            Field(x => x.Status, type: typeof(IntGraphType)).Description("Status");
            Field(x => x.CompletionDte, type: typeof(DateGraphType)).Description("The completion date of the order");
            Field(x => x.DriverID, type: typeof(IntGraphType)).Description("Id of the driver");
            Field(x => x.Duration, type: typeof(DecimalGraphType)).Description("The duration of the order.");
            Field(x => x.OfferType, type: typeof(IntGraphType)).Description("Offer type");
            Field(x => x.ShipperID, type: typeof(IntGraphType)).Description("The shipper Id.");
            Field(x => x.Code, type: typeof(StringGraphType)).Description("code.");
            //this.data = data;
        }

        ////public override OrderInfo GetById(string id)
        ////{
        ////   throw new NotImplementedException();
        ////}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public override OrderInfo GetById(string id)
        //{
        //    int parsedId;
        //    //if (int.TryParse(id, out parsedId))
        //    //    return this.data.GetById(parsedId);
        //    //else return null;

        //    return int.TryParse(id, out parsedId) ? this.data.GetById(parsedId) : null;
        //}
    }
}
