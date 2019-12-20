using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace tuyaConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            var exitApplicaion = false;
            var tuyaHttp = new TuyaHttpClient();
            do
            {
                MainMenu();
                var keyEntered = Console.ReadKey();
                switch (keyEntered.Key.ToString())
                {
                    case "D1":
                        Console.WriteLine("\nEnter OrderID\n");
                        var orderId = Console.ReadLine();
                        Console.WriteLine(JObject.Parse(tuyaHttp.GetRequestAsync($"/api/orders/{orderId}").GetAwaiter().GetResult()));
                        break;
                    case "D2":
                        var queryParamsDict = new Dictionary<string, string>();
                        Console.WriteLine("\nEnter MSA\n");
                        queryParamsDict.Add("msa", Console.ReadLine());
                        Console.WriteLine("\nEnter Status\n");
                        queryParamsDict.Add("status", Console.ReadLine());
                        Console.WriteLine("\nEnter Completion Date:\n");
                        queryParamsDict.Add("completionDate", Console.ReadLine());
                        Console.WriteLine(JObject.Parse(tuyaHttp.GetRequestAsync($"/api/orders/query", queryParamsDict).GetAwaiter().GetResult()));
                        break;
                    case "D3":
                        exitApplicaion = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection and try again!");
                        break;
                }
            } while (!exitApplicaion);
            Console.WriteLine("\nThanks for using the tool. Closing Session.");
        }

        private static void MainMenu()
        {
            Console.WriteLine("\n\n============== Tuya API Tool ================\n");
            Console.WriteLine("Make a selection to query orders:");
            Console.WriteLine("\t1. Find order by `OrderID`\n\t2. Find order by `MSA, Status, CompletionDate`\n\t3. Exit\n");
        }
    }

    public class TuyaHttpClient
    {
        private readonly string ApiScheme = "https";
        private readonly string ApiHost = "127.0.0.1";
        private readonly int ApiPort = 5001; 

        private string CreateUri(string path = null, Dictionary<string, string> queryParams = null)
        {
            queryParams = queryParams ?? new Dictionary<string, string>();
            var queryString = QueryHelpers.AddQueryString(path, queryParams);
            return $"{ApiScheme}://{ApiHost}:{ApiPort}{queryString}";
        }

        public async Task<string> GetRequestAsync(string path = null, Dictionary<string, string> queryParams = null)
        {
            var apiUri = CreateUri(path, queryParams);
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using var client = new HttpClient(httpClientHandler);
                var response = await client.GetAsync(apiUri);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await response.Content.ReadAsStringAsync();
                    case HttpStatusCode.NotFound:
                        return new JObject { { "error", "Order does not exist!" } }.ToString();
                    default:
                        throw new HttpRequestException($"Status Code: {response.StatusCode}\n" +
                                                       $"Response {response.Content.ReadAsStringAsync().Result}");
                };

            }
        }
    }
}
