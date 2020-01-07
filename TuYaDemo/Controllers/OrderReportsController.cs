using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TuYaDemo.Helpers;
using TuYaDemo.Models.ViewModels;
using PagedList;

namespace TuYaDemo.Controllers
{
    public class OrderReportsController : Controller
    {
        // GET: OrderReports
        public ActionResult Index(string date, string search)
        {
            if (!IsQueryValid(date, search)) return View();

            IEnumerable<OrderViewModel> orders = null;

            Uri baseAddress;

            if (AppSettingsHelper.IsDevelopment())
            {
                string host = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                Uri uri = new Uri(host);
                baseAddress = new Uri(uri.GetLeftPart(UriPartial.Authority) + "/api/Orders/");
            }
            else
            {
                baseAddress = new Uri("https://tuyademoapi20200106100020.azurewebsites.net/api/Orders/");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                //HTTP GET
                var requestUri = FormatApiRequest(date, search);
                var responseTask = client.GetAsync(requestUri);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderViewModel>>();
                    readTask.Wait();

                    orders = readTask.Result;
                }
                else //web api sent error response 
                {

                    orders = Enumerable.Empty<OrderViewModel>();
                    ModelState.AddModelError(string.Empty, "There was an error with your request. Please contact Harry about this. Sorry!");
                }

            }
            return View(orders);
        }

        public bool IsQueryValid(string date, string search)
        {

            if (!String.IsNullOrEmpty(search) && search.Trim().ToLower() == "all") return true;

            if (String.IsNullOrEmpty(search) || String.IsNullOrWhiteSpace(search)) return false;
            if (String.IsNullOrEmpty(date) || String.IsNullOrWhiteSpace(date)) return false;

            string[] terms = search.Split(' ');

            int number;

            if (terms.Count() == 1)
            {
                if (!Int32.TryParse(terms[0], out number)) return false;
            }
            else if (terms.Count() > 1 && terms.Count() < 3)
            {
                if (!Int32.TryParse(terms[0], out number)) return false;
                if (!Int32.TryParse(terms[1], out number)) return false;
            }
            else if (terms.Count() > 2)
                return false;

            DateTime dt;
            if (!DateTime.TryParse(date, out dt)) return false;

            return true;
        }

        public string FormatApiRequest(string date, string search)
        {
            if (!String.IsNullOrEmpty(search) && search.Trim().ToLower() == "all") return "GetAllOrders";
            string[] terms = search.Split(' ');
            if (terms.Count() == 1)
            {
                return "GetOrder" + "/" + search + " / " + date;
            }
            else
            {
                return "GetOrder" + "/" + terms[0] + "/" + terms[1] + "/" + date;
            }
        }




        // GET: OrderReports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderReports/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderReports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderReports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderReports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderReports/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
