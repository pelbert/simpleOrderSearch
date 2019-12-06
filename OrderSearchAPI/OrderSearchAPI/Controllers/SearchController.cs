using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderSearchAPI.Models;

namespace OrderSearchAPI.Controllers
{
    public class SearchController : ApiController
    {

        [Route("api/GetOrderdetails")]
        [HttpGet]
        //API to get the Order details based on the input parameters
        public IHttpActionResult GetOrderdetails(DateTime CompletionDte, int Offset, int pageNo, int pageSize, int? OrderID = null, int? MSA = null, int? Status = null)
        {

            //Read the Json file that has the sample order datas and filter the results
            String sPath = HostingEnvironment.MapPath(@"/App_Data/orderInfo.json");
            using (StreamReader reader = new StreamReader(sPath))
            {
                string jsonData = reader.ReadToEnd();
                List<OrderInfo.OrderData> orderData = JsonConvert.DeserializeObject<List<OrderInfo.OrderData>>(jsonData);
                //Filter by either Order ID and completion date or MSA, Status and Completion date depending on the input
                if (OrderID != null)
                {
                    var orderInfo = orderData.Where(item => (item.OrderID == OrderID && item.CompletionDte == CompletionDte)).Skip(Offset);
                    return Ok(new PagedResult<OrderInfo.OrderData>(orderInfo, pageNo, pageSize, orderData.Count));
                }
                else
                {
                    var orderInfo = orderData.Where(item => (item.MSA == MSA && item.Status == Status) && (item.CompletionDte == CompletionDte)).Skip(Offset);
                    return Ok(new PagedResult<OrderInfo.OrderData>(orderInfo, pageNo, pageSize, orderData.Count));
                }
            }
        }

        //Create a PagedResult class to encapsulate the records as well as the paging information:
        public class PagedResult<T>
        {
            public class PagingInfo
            {
                public int PageNo { get; set; }

                public int PageSize { get; set; }

                public int PageCount { get; set; }

                public long TotalRecordCount { get; set; }

            }
            public List<T> Data { get; private set; }

            public PagingInfo Paging { get; private set; }

            public PagedResult(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
            {
                Data = new List<T>(items);
                Paging = new PagingInfo
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    TotalRecordCount = totalRecordCount,
                    PageCount = totalRecordCount > 0
                        ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                        : 0
                };
            }
        }



    }
}
