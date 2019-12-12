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
                }, "[0-9]").AsNumber());
                //dict.Add("OrderID", Console.ReadLine().asNumber());
            }
            else
            {
                dict.Add("MSA", validateData(() =>
                {
                    Console.Write("Enter the MSA (value is a number): ");
                    return Console.ReadLine();
                }, "[0-9]").AsNumber());
                Console.WriteLine();
                dict.Add("Status", validateData(() =>
                {
                    Console.Write("Enter the status (value is a number): ");
                    return Console.ReadLine();
                }, "[0-9]").AsNumber());
            }
            Console.WriteLine();
            string date = validateDate(() => validateData(() => {
                Console.WriteLine("For the completion date, first give the year (in \"yyyy-mm-dd\" format): ");
                return Console.ReadLine();
            }, @"\d{4}\-\d{2}\-\d{2}"));
            string time = validateTime(() => validateData(() => {
                Console.WriteLine("then the time (in \"hh:mm:ss\" format): ");
                return Console.ReadLine();
            }, @"[0-9]{2}\:[0-9]{2}\:[0-9]{2}"));
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
                dict.Add("page", pagenumber.AsNumber());
            }
            Console.WriteLine();
            string pagesize = validateData(() =>
            {
                Console.Write("What is the page size to start at (leave blank for all results or a default of 25 will be used if a page number was specified)?  ");
                return Console.ReadLine();
            }, "[0-9]", true);
            if (!String.IsNullOrEmpty(pagesize))
            {
                dict.Add("size", pagesize.AsNumber());
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

        private static string validateDate(Func<string> func)
        {
            string validate;
            //now we check hour, minute, and second values to see if they are kosher
            //here, we will only care about 24 hour time
            bool verified = false;
            do
            {
                validate = func();
                string[] timesplits = validate.Split('-');
                //we don't care about year because those keep going on and on, but month and day are important
                int month = int.Parse(timesplits[1]);
                int day = int.Parse(timesplits[2]);
                bool leap = (int.Parse(timesplits[0]) % 4) == 0;
                //leap years are tricky
                if(month == 2 && day > 0 && ((leap && day < 30) || day < 29))
                {
                    verified = true;
                }
                else if((month == 4 || month == 6 || month == 9 || month == 11) && day > 0 && day < 31)
                {
                    verified = true;
                }
                else if((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && day > 0 && day < 32)
                {
                    verified = true;
                }
            }
            while (!verified);
            return validate;
        }

        private static string validateTime(Func<string> func)
        {
            string validate;
            //now we check hour, minute, and second values to see if they are kosher
            //here, we will only care about 24 hour time
            bool verified = false;
            do
            {
                validate = func();
                string[] timesplits = validate.Split(':');
                if(int.Parse(timesplits[0]) < 24 && int.Parse(timesplits[1]) < 60 && int.Parse(timesplits[2]) < 60) {
                    verified = true;
                }
            }
            while (!verified);
            return validate;
        }
    }

    public static class StringExpansions
    {
        public static int AsNumber(this string s)
        {
            return int.Parse(s);
        }
    }
}
