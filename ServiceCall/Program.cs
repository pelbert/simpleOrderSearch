using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceCall
{
    class Program
    {
        async static Task Main(string[] args)
        {
            int option = 0;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Choose an option");
                Console.WriteLine("1) search by order id");
                Console.WriteLine("2) search by msa and status");
            }
            while ((!int.TryParse(Console.ReadLine(), out option)) || (option < 1 || option > 2));
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Console.WriteLine();
            if (option == 1)
            {
                dict.Add("OrderID", validateData(() =>
                {
                    Console.Write("Enter the order number: ");
                    return Console.ReadLine();
                }, "[0-9]").asNumber());
                //dict.Add("OrderID", Console.ReadLine().asNumber());
            }
            else
            {
                dict.Add("MSA", validateData(() =>
                {
                    Console.Write("Enter the MSA (value is a number): ");
                    return Console.ReadLine();
                }, "[0-9]").asNumber());
                Console.WriteLine();
                dict.Add("Status", validateData(() =>
                {
                    Console.Write("Enter the status (value is a number): ");
                    return Console.ReadLine();
                }, "[0-9]").asNumber());
            }
            Console.WriteLine();
            string date = validateData(() => {
                Console.WriteLine("For the completion date, first give the year (in \"yyyy-mm-dd\" format): ");
                return Console.ReadLine();
            }, @"\d{4}\-\d{2}\-\d{2}");
            string time = validateData(() => {
                Console.WriteLine("then the time (in \"hh:mm:ss\" format): ");
                return Console.ReadLine();
            }, @"[0-6]{2}\:[0-6]{2}\:[0-6]{2}");
            //dict.Add("CompletionDte", "2018-01-12T05:10:00");
            dict.Add("CompletionDte", $"{date}T{time}");
            Console.WriteLine();
            string pagenumber = validateData(() =>
            {
                Console.Write("What is the page number to start at (leave blank to start at the beginning)?  ");
                return Console.ReadLine();
            }, "[0-9]", true);
            if(!String.IsNullOrEmpty(pagenumber))
            {
                dict.Add("page", pagenumber.asNumber());
            }
            Console.WriteLine();
            string pagesize = validateData(() =>
            {
                Console.Write("What is the page size to start at (leave blank for all results or a default of 25 will be used if a page number was specified)?  ");
                return Console.ReadLine();
            }, "[0-9]", true);
            if (!String.IsNullOrEmpty(pagesize))
            {
                dict.Add("size", pagesize.asNumber());
            }
            Console.WriteLine();
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(dict);
                using (HttpContent content = new StringContent(json))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var result = await client.PostAsync("https://localhost:5001/api/order", content);
                    Console.WriteLine(await result.Content.ReadAsStringAsync());
                }

            }
            System.Threading.Thread.Sleep(10000);
        }

        private static string validateData(Func<string> func, string regex, bool allowBlanks = false)
        {
            string validate;
            do
            {
                validate = func();
            }
            while ((allowBlanks && !String.IsNullOrEmpty(validate)) && (!System.Text.RegularExpressions.Regex.IsMatch(validate, regex)));
            return validate;
        }
    }

    public static class StringExpansions
    {
        public static int asNumber(this string s)
        {
            return int.Parse(s);
        }
    }
}
