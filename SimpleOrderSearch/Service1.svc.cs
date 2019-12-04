using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SimpleOrderSearch
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        #region Constant definitions

        private const int c_errCompletionDteMissing = -1;

        #endregion

        public OrderInfoResponse GetOrderInfos(OrderInfo orderInfo)
        {
            var response = new OrderInfoResponse();

            // perform basic parameter validation...
            if (string.IsNullOrWhiteSpace(orderInfo.CompletionDte))
            {
                response.ReturnCode = c_errCompletionDteMissing;
            }
            else
            {
                using (StreamReader r = new StreamReader(@"C:\temp\orderInfo.json"))
                {
                    string json = r.ReadToEnd();
                    List<OrderInfo> dataItems = JsonConvert.DeserializeObject<List<OrderInfo>>(json);

                    // Select matching items
                    var results =
                        from p in dataItems
                        where p.CompletionDte == orderInfo.CompletionDte
                            && (p.OrderID == orderInfo.OrderID || (
                            p.MSA == orderInfo.MSA && p.Status == orderInfo.Status))
                        select p;

                    response.ReturnCode = 0;
                    response.OrderInfos = results.ToArray();
                }
            }

            return response;
        }
    }
}
