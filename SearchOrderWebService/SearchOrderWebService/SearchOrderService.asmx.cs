using Newtonsoft.Json;
using SearchOrderWebService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SearchOrderWebService
{
    /// <summary>
    /// Summary description for SearchOrderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SearchOrderService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public void TestSearchOrder()
        {
            var data = SearchOrder(new SearchCriteria { OrderId = 39, CompletionDte = DateTime.Parse("2018-01-31T05:10:00") });
        }
        [WebMethod]
        public List<OrderInfo> SearchOrder(SearchCriteria searchCriteria)
        {
            var dataToSend = new List<OrderInfo>();
            int page = 0;
            int pageSize = 20;
            if (searchCriteria?.IsValid() ?? false)
            {
                //var data = GetJsondata();
                StreamReader streamReader = new StreamReader(Server.MapPath("~/Data/OrderInfo.json"));
                string readData = streamReader.ReadToEnd();
                var dataSet = JsonConvert.DeserializeObject<List<OrderInfo>>(readData);
                if (dataSet.Count() > 0)
                {
                    var pageData = new List<OrderInfo>();
                    if (searchCriteria?.CompletionDte != null)
                    {
                        if (searchCriteria?.OrderId != null)
                        {
                            pageData = dataSet.Where(x => x.OrderId == searchCriteria.OrderId && x.CompletionDte.Value.Date == searchCriteria.CompletionDte.Value.Date)?.ToList();
                        }
                        else if (searchCriteria?.MSA != null && searchCriteria?.Status != null)
                        {
                            pageData = dataSet.Where(x => x.MSA == searchCriteria.MSA && x.Status == searchCriteria.Status && x.CompletionDte.Value.Date == searchCriteria.CompletionDte.Value.Date)?.ToList();
                        }
                    }
                    if (pageData?.Count() > 0)
                    {
                        dataToSend = pageData?.Skip(page * pageSize).Take(pageSize).ToList();
                    }
                }
            }
            return dataToSend;
        }
    }
}
