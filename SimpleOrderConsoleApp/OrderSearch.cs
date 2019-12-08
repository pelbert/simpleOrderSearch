using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleOrderWebApi.Models;

namespace SimpleOrderConsoleApp
{
    public class OrderSearch
    {
        HttpClient webClient = new HttpClient();
        string apiUrl;
        int Count = 0;
        public void Record()
        {
            int intOrderId = 0;
            int intMSA = 0;
            int intStatus = 0;
            //int intOffset = 0;
            //int intLimit = 0;
            DateTime Date;

        OrderNumber:
            Console.WriteLine("Please enter the order number\n(If you don't need order number as search,please press 'Enter')");
            string strOrderId = Console.ReadLine().Trim();
            if (!int.TryParse(strOrderId, out intOrderId) && strOrderId != "")
            {
                Console.WriteLine("Invalid order number");
                goto OrderNumber;
            }
            if (intOrderId == 0)
            {

            MSA:
                Console.WriteLine("Please enter the MSA number");
                string strMSA = Console.ReadLine().Trim();
                if (!int.TryParse(strMSA, out intMSA))
                {
                    Console.WriteLine("Please enter a vaild MSA number");
                    goto MSA;
                }
                if (intMSA != 0)
                {
                Status:
                    Console.WriteLine("Please enter the status number");
                    string strStatus = Console.ReadLine().Trim();
                    if (!int.TryParse(strStatus, out intStatus))
                    {
                        Console.WriteLine("Please enter a vaild status number");
                        goto Status;
                    }
                }
            }

        Datetime:
            Console.WriteLine("Please enter the confirmation date and time {MM/DD/YYY HH:MM:SS AM/PM} (Mandatory*)");
            string strDate = Console.ReadLine();
            if (!DateTime.TryParse(strDate, out Date))
            {
                Console.WriteLine("Please enter a valid datetime");
                goto Datetime;
            }

            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss" };
            var jsonDate = JsonConvert.SerializeObject(Date, settings);

            string NewDate = jsonDate.Remove(0, 1);
            NewDate = NewDate.Remove(19, 1);

            var input = new
            {
                OrderId = intOrderId,
                MSA = intMSA,
                Status = intStatus,
                Date = NewDate,
                Limit = 3,
                Offset = 0

            };
                        
            string parameter = "?obj.status=" + input.Status + "&obj.MSA=" + input.MSA + "&obj.OrderID=" + input.OrderId + "&obj.CompletionDte=" + NewDate + "&obj.Limit=" + input.Limit;
            apiUrl = "http://localhost:54861/api/Order/Search" + parameter;

            GetRecord();
            int intoffset = 0;

            if (Count >= input.Limit)
            {
                do
                {
                    Console.WriteLine("Please enter 'Next' to go for next page");
                    if (Console.ReadLine().ToLower().Trim() == "next")
                    {
                        intoffset = intoffset + 3;
                        parameter = "?obj.status=" + input.Status + "&obj.MSA=" + input.MSA + "&obj.OrderID=" + input.OrderId + "&obj.CompletionDte=" + NewDate + "&obj.Limit=" + input.Limit + "&obj.Offset=" + intoffset;
                        apiUrl = "http://localhost:54861/api/Order/Search" + parameter;
                        GetRecord();
                    }
                    else
                    {
                        break;
                    }
                }
                while ((intoffset) >= Count);
            }

        }

        public void AllRecords()
        {
            apiUrl = "http://localhost:54861/api/Order";
            GetRecord();
        }
        public void GetRecord()
        {
            IEnumerable<Order> OrderModel;
            List<Order> liOrder = new List<Order>();

            HttpResponseMessage webResponse = webClient.GetAsync(apiUrl).Result;
            OrderModel = webResponse.Content.ReadAsAsync<IEnumerable<Order>>().Result;

            if (webResponse.IsSuccessStatusCode)
            {
                if (OrderModel != null && OrderModel.GetEnumerator().MoveNext())
                {
                    Console.WriteLine("\n*********************Odrers********************\n\n");
                    foreach (Order item in OrderModel)
                    {

                        Console.WriteLine("OrderId: {0}\nCode:{1}\nShipperID:{2}\nDriverID:{3}\nCompletionDte:{4}\nStatus:{5}\nMSA:{6}\nDuration:{7}\nOfferType:{8}\n",
                                           item.OrderID, item.Code, item.ShipperID, item.DriverID, item.CompletionDte, item.Status, item.MSA, item.Duration, item.OfferType);
                        Count = Convert.ToInt32(item.PageSize);
                    }

                }
                else
                {
                    Console.WriteLine("No data");
                }
            }

            else
            {
                Console.WriteLine("Error");
            }
            Console.ReadLine();
        }
    }
}
