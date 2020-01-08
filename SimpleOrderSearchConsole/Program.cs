using Newtonsoft.Json;
using SimpleOrderSearchAPI.Model;
using SimpleOrderSearchConsole.Proxy;
using System;
using System.Threading.Tasks;

namespace SimpleOrderSearchConsole
{
    class Program
    {
        public static int GetInputNumber(string text)
        {
            var invalidInput = true;
            int number = 0;
            while(invalidInput)
            {
                Console.WriteLine(text);
                try
                {
                    number = int.Parse(Console.ReadLine());
                    invalidInput = false;
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter again");
                }
            }
            return number;
        }

        public static Params GetSearchParam()
        {
            bool invalidInput = true;
            var param = new Params();

            Console.WriteLine("Please provide us your order Id and completion date time or MSA number, status number, and completion date time, so we can search for your order");
            while (invalidInput)
                {
                
                Console.WriteLine("Please enter 1 to search by order id, 2 to search by MSA number and Status number");
                var choice = Console.ReadLine();
                if (choice == "1")
                    {
                        param.OrderId = GetInputNumber("What is your orderId? please enter your order id number here.");
                        invalidInput = false;
                    }
                    else if (choice == "2")
                    {
                        param.MSA = GetInputNumber("What is your MSA? please enter MSA number here.");
                        param.Status = GetInputNumber("What is your status number? please enter your status number here.");
                        invalidInput = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter again");
                    }
                }
            invalidInput = true;
            while (invalidInput)
            {
                Console.WriteLine("What is your Completion date time? please enter a valid date time here.");
                try
                {
                    param.CompletionDte = DateTime.Parse(Console.ReadLine());
                    invalidInput = false;
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter again");
                }
            }
            return param;
        }

        public static async Task Choice(Params param)
        {
            var proxy = new SimpleOrderSearchProxy();

            var result = await proxy.GetOrder(param);
            Console.WriteLine("This is your orders");
            Console.WriteLine(JsonConvert.SerializeObject(result.Orders));
            Console.WriteLine($"current page: {result.Pagination.CurrentPage} , total pages: {result.Pagination.TotalPages}, total items: {result.Pagination.TotalItems} , Item per page: {result.Pagination.ItemPerPage}");
            Console.WriteLine("Please choose following option");
            Console.WriteLine("enter 1 : choose page number");
            Console.WriteLine("enter 2 : choose a new number items per page");
            Console.WriteLine("enter 3 : search for new orders");
            Console.WriteLine("enter 4 : exist");
        }

        static async Task Main(string[] args)
        {
            bool exist = false;
            bool invalidInput = true;
                var param = GetSearchParam();
                var proxy = new SimpleOrderSearchProxy();
                
                var result = await proxy.GetOrder(param);
                Console.WriteLine("This is your orders");
                Console.WriteLine(JsonConvert.SerializeObject(result.Orders));
                Console.WriteLine($"current page: {result.Pagination.CurrentPage} , total pages: {result.Pagination.TotalPages}, total items: {result.Pagination.TotalItems} , Item per page: {result.Pagination.ItemPerPage}");
                Console.WriteLine("Please choose following option");
                Console.WriteLine("enter 1 : choose page number");
                Console.WriteLine("enter 2 : choose a new number items per page");
                Console.WriteLine("enter 3 : search for new orders");
                Console.WriteLine("enter 4 : exist");
                
                while(invalidInput)
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            param.PageNumber = GetInputNumber("Please enter the page number");
                            await Choice(param);
                            break;
                        case "2":
                            param.ItemsPerPage = GetInputNumber("Please enter number items you want to display each page.");
                            await Choice(param);
                            break;
                        case "3":
                            param = GetSearchParam();
                            await Choice(param);
                            break;
                        case "4":
                            invalidInput = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Input please try again");
                            break;
                    }
                }
            
        }
    }
}
