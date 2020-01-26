using GraphQL.Client;
using GraphQL.Common.Request;
using RestSharp;
using SimpleOrderSearch.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleOrderSearch.Desktop.ProxyClient
{
    public class OrderSearchProxy
    {
        private const string uri = "https://localhost:44345/api/ordersearch";
        private const string graphQluri = "https://localhost:44345/graphql";

        public static PagedOrderInfo GetOrders(OrderSearchQuery searchQuery)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddParameter("OrderNo", searchQuery.OrderNumber);
            request.AddParameter("Status", searchQuery.Status);
            request.AddParameter("MSA", searchQuery.MSA);
            request.AddParameter("Page", searchQuery.Page);
            request.AddParameter("CompletionDate", searchQuery.CompletionDate);
            request.AddParameter("PageLimit", searchQuery.PageLimit);
            IRestResponse response = client.Execute(request);

            RestSharp.Serialization.Json.JsonDeserializer jsonDeserializer = new RestSharp.Serialization.Json.JsonDeserializer();
            var pagedOrderInfo = jsonDeserializer.Deserialize<PagedOrderInfo>(response);
            return pagedOrderInfo;
        }

        public static PagedOrderInfo PostOrdersQuery(OrderSearchQuery searchQuery)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(searchQuery);
            IRestResponse response = client.ExecuteAsPost(request, Method.POST.ToString());
            RestSharp.Serialization.Json.JsonDeserializer jsonDeserializer = new RestSharp.Serialization.Json.JsonDeserializer();
            var orders = jsonDeserializer.Deserialize<PagedOrderInfo>(response);
            return orders;
        }


        //public static async Task<List<OrderInfo>> PostOrderGraphQLQuery(OrderSearchQuery searchQuery)
        public static async Task<GraphQLResponse<OrderInfo>> PostOrderGraphQLQuery(OrderSearchQuery searchQuery)
        {
            var graphQlClient = new GraphQLClient(new Uri(graphQluri));
            var request = new GraphQLRequest
            {
                //OperationName = "OrderQuery",
                //Query = @"query OrderQuery($orderId: Int!, $msa: Int, $status: Int, $completionDate: Date) {
                //          orders(orderId: $orderId, msa: $msa, status: $status, completionDate: $completionDate) {
                //            orderID
                //            mSA
                //            status
                //            completionDte
                //            offerType
                //            driverID
                //            duration
                //            code
                //          }
                //        }",

                OperationName = "OrderQuery",
                Query = @"query OrderQuery($orderId: ID!, $msa: Int, $status: Int, $completionDate: Date, $first: Int, $after: String) {
                        ordersConnection(orderId: $orderId, msa: $msa, status: $status, completionDate: $completionDate, first:$first, after:$after, pageSize: 1) {
                    totalCount
                    edges {
                                    node {
                                    code
                        completionDte
                        driverID
                        duration
                        mSA
                        offerType
                        orderID
                        shipperID
                        status
                                }
                     cursor
                            }
                    pageInfo{
                                startCursor
                               endCursor
                      hasNextPage
                      hasPreviousPage


                    }
                        }
                    }",

                Variables = new
                {
                    orderId = searchQuery.OrderNumber.Value,
                    msa = searchQuery.MSA,
                    status = searchQuery.Status,
                    completionDate = searchQuery.CompletionDate,
                    first = searchQuery.PageLimit ,
                    after = searchQuery.IsPageUp.HasValue && searchQuery.IsPageUp.Value ? searchQuery.Cursor : string.Empty,
                    before = searchQuery.IsPageUp.HasValue && !searchQuery.IsPageUp.Value ? searchQuery.Cursor : string.Empty
                }        
            };

            var response = await graphQlClient.PostAsync(request);
            ////return response.GetDataFieldAs<List<OrderInfo>>("orders");
            return response.GetDataFieldAs<GraphQLResponse<OrderInfo>>("ordersConnection");
        }
    }
}