using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;

namespace ConsoleApp
{
    class Program
    {
        public static HttpClientHandler clientHandler;
        public static HttpClient client;
        private static int resultLimit = 3;
        static async Task Main()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            client = new HttpClient(clientHandler);
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            string input = "";
            string strDate = "";
            DateTime userDate;
            bool parsed = false;
            while (true)
            {
                try
                {
                    Console.WriteLine("___________________________________________");
                    Console.WriteLine("Find Orders");
                    Console.WriteLine("Select all orders by typing 'all'");
                    Console.WriteLine("Find a single order by typing id number (ex. 37)");
                    Console.WriteLine("Type 'exit' to exit program");
                    Console.WriteLine("___________________________________________");
                    input = Console.ReadLine();
                    parsed = Int32.TryParse(input, out int id);
                    if (parsed)
                    {
                        Console.WriteLine("Enter the Complete Date for order: (ex. 2018-02-15T05:10:00)");
                        strDate = Console.ReadLine();
                        userDate = DateTime.Parse(strDate);
                        Console.WriteLine($"getting order by id: {id}");
                        HttpResponseMessage response = await client.GetAsync($"http://localhost:5000/Order/{id}");
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        if (responseBody.Length == 0)
                        {
                            Console.WriteLine($"Couldn't find order for id: {id}");
                        }
                        else
                        {
                            Order currentOrder = JsonConvert.DeserializeObject<Order>(responseBody);
                            if (DateTime.Compare(currentOrder.CompletionDte, userDate) == 0) {
                                Console.WriteLine("found your order!");
                                Console.WriteLine(responseBody);
                            } else {
                                Console.WriteLine($"Order {id} exists but the completion date wasn't correct. Try again");
                            }

                        }
                    }
                    else
                    {
                        if (input.ToLower() == "all")
                        {
                            Console.WriteLine("fetching all orders now...");
                            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/Order");
                            response.EnsureSuccessStatusCode();
                            string responseBody = await response.Content.ReadAsStringAsync();
                            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(responseBody);
                            int numOfPages = orders.Count / resultLimit;
                            int currentPage = 1;
                            int prevPage = 1;
                            Order[] currentOrders = new Order[resultLimit];
                            Console.WriteLine(orders.Count + " orders found!");
                            while (true)
                            {
                                currentOrders = orders.GetRange(currentPage - 1, resultLimit).ToArray();
                                Console.WriteLine("===============================================");
                                Console.WriteLine($"Currently on page {currentPage}");
                                Console.WriteLine($"There are {numOfPages} pages of results");
                                string strOrder = "";
                                foreach (Order order in currentOrders)
                                {
                                    strOrder = JsonConvert.SerializeObject(order);
                                    Console.WriteLine(strOrder);
                                }
                                Console.WriteLine("===============================================");
                                Console.WriteLine($"To navigate to a new page, type a number between 1 - {numOfPages}");
                                Console.WriteLine("Type 'back' to return to search");
                                Console.WriteLine("Type 'exit' to exit program");
                                prevPage = currentPage;
                                string userInput = Console.ReadLine();
                                bool isNum = Int32.TryParse(userInput, out currentPage);
                                if (isNum && currentPage >= 1 && currentPage <= numOfPages)
                                {
                                   continue;
                                } else
                                {
                                    if (userInput.ToLower() == "back")
                                    {
                                        Console.WriteLine("returning to search...");
                                        break;
                                    } else if (userInput.ToLower() == "exit") {
                                        Console.WriteLine("exiting program...");
                                        System.Environment.Exit(0); 
                                    }
                                    else
                                    {
                                        currentPage = prevPage;
                                        Console.WriteLine("Page number incorrect, try again!");
                                    }
                                }
                            }

                        }
                        else if (input.ToLower() == "exit")
                        {
                            Console.WriteLine("exiting program");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input given. Try again...");
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Couldn't find your oder. Try again!");
                }
                catch (FormatException) {
                    Console.WriteLine("'{0}' is not in an acceptable format. Try again!", strDate);
                }
            }
        }
    }
}
