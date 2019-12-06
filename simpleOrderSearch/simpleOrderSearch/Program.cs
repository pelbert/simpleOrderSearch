using System;
using simpleOrderSearch.Model;
using System.Collections.Generic;
using System.Globalization;
using RestSharp;
using Newtonsoft.Json;

namespace simpleOrderSearch
{
    class Program
    {
        static void Main(string[] args)
        {
        Start:

            
            int Status = 0; int MSA = 0;
            DateTime dtCompletionDte;
            DateTime CompletionDte;
            string strStatus = "";
            string strMSA = "";
            string strCompletionDte;

            
            Console.Write("Provide the OrderID or simply press \"Enter\" key to skip: ");
            string strOrderID = Console.ReadLine();
            if (!Int32.TryParse(strOrderID, out int OrderID) && strOrderID.Trim() != "")
            {

                Console.WriteLine("The OrderID entered is invalid. Please enter a valid OrderID or simply press \"Enter\" key to skip");
                Console.WriteLine();
                goto Start;
            }
            else
            {
                if (OrderID > 0)
                {

                }
                else
                {
                StatusStep:
                    Console.WriteLine();
                    Console.Write("Provide the Status or simply press \"Enter\" key to skip: ");
                    strStatus = Console.ReadLine();
                    if (!Int32.TryParse(strStatus, out Status) && strStatus.Trim() != "")
                    {

                        Console.WriteLine("The Status entered is invalid. Please enter a valid Status or simply press \"Enter\" key to skip");
                        Console.WriteLine();
                        goto StatusStep;
                    }
                MSAStep:
                    Console.WriteLine();
                    Console.Write("Provide the MSA or simply press \"Enter\" key to skip: ");
                    strMSA = Console.ReadLine();
                    if (!Int32.TryParse(strMSA, out MSA) && strMSA.Trim() != "")
                    {

                        Console.WriteLine("The MSA entered is invalid. Please enter a valid MSA or simply press \"Enter\" key to skip");
                        Console.WriteLine();
                        goto MSAStep;
                    }
                }
            }
        DateTime:
            Console.WriteLine();
            Console.Write("Provide the Completion Date and Time in the format \"yyyy-MM-dd HH:mm:ss\": ");
            strCompletionDte = Console.ReadLine();
            if (!DateTime.TryParse(strCompletionDte, out dtCompletionDte) && strCompletionDte.Trim() != "")
            {

                Console.WriteLine("The Completion Date entered is invalid. Please enter a valid Completion Date in the format specified");
                Console.WriteLine();
                goto DateTime;
            }
            else
            {
                DateTime dtDateTime = new DateTime(dtCompletionDte.Year, dtCompletionDte.Month, dtCompletionDte.Day, dtCompletionDte.Hour, dtCompletionDte.Minute, dtCompletionDte.Second, DateTimeKind.Utc);
                string strDatetTime1 = dtDateTime.ToString("o", CultureInfo.InvariantCulture);
                string strDatetTime2 = dtDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                CompletionDte = Convert.ToDateTime(strDatetTime2);
            }
            Console.WriteLine();
            
            int Offset = 0;
            int pageNo = 2;
            int pageSize = 1;

            //Hardcoded URL for the API
            string apiUrl = "https://localhost:44328/api/GetOrderdetails";

            var client = new RestClient(apiUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            if (strOrderID.Trim() != "")
            {
                request.AddParameter("OrderID", OrderID);
            }
            if (strStatus.Trim() != "" && strMSA.Trim() != "")
            {
                request.AddParameter("Status", Status);
                request.AddParameter("MSA", MSA);
            }

            request.AddParameter("Offset", Offset);
            request.AddParameter("pageNo", pageNo);
            request.AddParameter("CompletionDte", CompletionDte);
            request.AddParameter("pageSize", pageSize);


            IRestResponse response = client.Execute(request);
            OrderInfo orderinfo = JsonConvert.DeserializeObject<OrderInfo>(response.Content.ToString());

            Console.WriteLine();
            if (orderinfo.Data.Count > 0)
            {
                foreach (var item in orderinfo.Data)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
                }
                Console.WriteLine(JsonConvert.SerializeObject(orderinfo.Paging, Formatting.Indented));

            }
            else
            {
                Console.WriteLine("No records found.");
            }
            Console.WriteLine();

        }
    }
}
