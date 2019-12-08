using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleOrderWebApi.Models;
using System.IO;

namespace SimpleOrderWebApi.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        //Function to retrive data from Json file
        public List<Order> Orderlist()

        {
            List<Order> allOrder = new List<Order>();

            var jsonfile = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Product.json"));

            JArray jsonVal = JArray.Parse(jsonfile) as JArray;
            dynamic albums = jsonVal;

            foreach (dynamic item in albums)
            {
                Order objOrder = new Order();

                objOrder.OrderID = item["OrderID"];
                objOrder.Code = item["Code"].ToString();
                objOrder.CompletionDte = item["CompletionDte"];
                objOrder.MSA = item["MSA"];
                objOrder.Status = item["Status"];
                objOrder.ShipperID = item["ShipperID"];
                objOrder.OfferType = item["OfferType"];
                objOrder.DriverID = item["DriverID"];
                objOrder.Duration = item["Duration"];

                allOrder.Add(objOrder);

            }
            return allOrder;
        }

        [HttpGet, Route("")]
        //Get all data
        public IEnumerable<Order> GetAllOrder()
        {
            return Orderlist();
        }
        [HttpGet, Route("Search")]
        //Search : Order Number || (MSA && Status)) && CompletionDte
        public List<Order> GetSearch([FromUri]Order obj)
        {
            List<Order> listOrder = Orderlist();
            List<Order> listOrderbyparameter = new List<Order>();

            var varOrder = ((from s in listOrder
                               where (s.OrderID == obj.OrderID || (s.MSA == obj.MSA && s.Status == obj.Status))
                               select s));

            obj.PageSize = varOrder.Count();
            if (obj.Offset != 0)
            {
                varOrder = ((from s in listOrder
                             where (s.OrderID == obj.OrderID || (s.MSA == obj.MSA && s.Status == obj.Status))
                               select s).Skip(obj.Offset)).Take(obj.Limit);
            }
            else
            {
                varOrder = ((from s in listOrder
                             where (s.OrderID == obj.OrderID || (s.MSA == obj.MSA && s.Status == obj.Status))
                               select s)).Take(obj.Limit);
            }
            try
            {

                foreach (var group in varOrder)
                {
                    Order objOrder = new Order();

                    objOrder.OrderID = group.OrderID;
                    objOrder.Code = group.Code;
                    objOrder.Limit = group.Limit;
                    objOrder.MSA = group.MSA;
                    objOrder.OfferType = group.OfferType;
                    objOrder.ShipperID = group.ShipperID;
                    objOrder.Status = group.Status;
                    objOrder.CompletionDte = group.CompletionDte;
                    objOrder.DriverID = group.DriverID;
                    objOrder.Duration = group.Duration;
                    objOrder.PageSize = obj.PageSize;

                    listOrderbyparameter.Add(objOrder);

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return listOrderbyparameter;
        }
    }
}