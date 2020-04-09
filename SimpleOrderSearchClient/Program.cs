using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SimpleOrderSearchClient
{
    class Program
    {
        static string ApiUrl = "http://localhost:6627/api/Order";
        static RestClient Client = new RestClient(ApiUrl);

        static void Main(string[] args)
        {
            MenuMain();
        }

        static void MenuMain()
        {
            string mainChoice;

            do
            {
                Console.WriteLine("[Main Menu]");
                Console.WriteLine("'o' to search by OrderID");
                Console.WriteLine("'m' to search by {MSA, Status, CompletionDte}");
                Console.WriteLine("'q' to quit");
                Console.Write("your choice: ");

                mainChoice = Console.ReadLine();
                Console.WriteLine();

                if (mainChoice == "o")
                {
                    SearchByOrderId();
                }
                else if (mainChoice == "m")
                {
                    SearchByMsaStatusCompletionDte();
                }

                Console.WriteLine();
            } while (mainChoice != "q");
        }

        static void SearchByOrderId()
        {
            Console.WriteLine("[Searching By OrderID]");

            long orderID;
            if (!getUserLong("OrderID", out orderID))
            {
                return;
            }

            var request = new RestRequest(Method.GET);
            request.AddParameter("orderID", orderID);

            var response = Client.Execute(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine("  success!");
                var orders = JsonConvert.DeserializeObject<List<object>>(response.Content);
                Console.WriteLine(JsonConvert.SerializeObject(orders, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("  error!");
                Console.WriteLine($"  Status Code: {response.StatusCode}");
                Console.WriteLine($"  Status Description: {response.StatusDescription}");
                Console.WriteLine($"  Error Msg: {response.ErrorMessage}");
            }
        }

        static void SearchByMsaStatusCompletionDte()
        {
            Console.WriteLine("[Searching By {MSA, Status, CompleteDte}]");
            long msa;
            long status;
            DateTime completionDte;

            if (!getUserLong("MSA", out msa)
                || !getUserLong("Status", out status)
                || !getUserDateTime("CompletionDte", out completionDte))
            {
                return;
            }

            int page = 0;

            while (displaySearchResults(msa, status, completionDte, page))
            {
                page++;
                Console.Write("want next page (y/n)? ");

                if (Console.ReadLine() != "y")
                {
                    break;
                }
            }
        }

        static bool displaySearchResults(long msa, long status, DateTime completionDte, int page)
        {
            var request = new RestRequest(Method.GET);
            request.AddParameter("msa", msa);
            request.AddParameter("status", status);
            request.AddParameter("completionDte", completionDte);
            request.AddParameter("page", page);

            var response = Client.Execute(request);
            bool gotOrders = false;

            if (response.IsSuccessful)
            {
                Console.WriteLine("  success!");
                var orders = JsonConvert.DeserializeObject<List<object>>(response.Content);
                Console.WriteLine(JsonConvert.SerializeObject(orders, Formatting.Indented));

                gotOrders = orders.Count > 0;
            }
            else
            {
                Console.WriteLine("  error!");
                Console.WriteLine($"  Status Code: {response.StatusCode}");
                Console.WriteLine($"  Status Description: {response.StatusDescription}");
                Console.WriteLine($"  Error Msg: {response.ErrorMessage}");
            }

            return gotOrders;
        }

        static bool getUserLong(string valName, out long val)
        {
            Console.Write($"enter {valName}: ");
            string valStr = Console.ReadLine();

            if (!long.TryParse(valStr, out val))
            {
                Console.WriteLine($"invalid {valName}, returning to previous menu");
                return false;
            }

            return true;
        }

        static bool getUserDateTime(string valName, out DateTime val)
        {
            Console.Write($"enter {valName}: ");
            string valStr = Console.ReadLine();

            if (!DateTime.TryParse(valStr, out val))
            {
                Console.WriteLine($"invalid {valName}, returning to previous menu");
                return false;
            }

            return true;
        }

    }
}
