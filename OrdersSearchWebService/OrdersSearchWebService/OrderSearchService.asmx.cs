using Newtonsoft.Json;
using OrdersSearchWebService.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace OrdersSearchWebService
{
    /// <summary>
    /// Summary description for OrderSearchService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class OrderSearchService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Orders> SearchOrder(SearchOrderViewModel model, int page, int pageSize)
        {
            List<Orders> returnData = new List<Orders>();
            bool modelValidation = false;
            if (model.CompletionDte != null)
            {
                if (model.MSA > 0 && model.Status > 0)
                {
                    modelValidation = true;
                }
                else if (model.OrderId > 0)
                {
                    modelValidation = true;
                }
            }

            if (modelValidation)
            {
                var data = GetJsondata();
                if (data?.Any() ?? false)
                {
                    returnData = data
                        .Where(x => (x.OrderId == model.OrderId || (x.MSA == model.MSA && x.Status == model.Status)) && x.CompletionDte.Date == model.CompletionDte.Date)?
                        .Skip(page * pageSize).Take(pageSize).ToList();

                }                
            }
            return returnData;
        }

        private List<Orders> GetJsondata()
        {
            using (StreamReader r = new StreamReader(Server.MapPath("~/JsonData/Orders.json")))
            {
                string json = r.ReadToEnd();
                List<Orders> ro = JsonConvert.DeserializeObject<List<Orders>>(json);
                return ro;
            }
        }
    }
}
