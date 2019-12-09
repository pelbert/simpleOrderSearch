using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOrderConsoleApp
{
    class Home
    {
        static void Main(string[] args)
        {
            Home pro = new Home();

            while (true)
            {
                pro.Choice();
                Console.WriteLine("Do you want to continue?(yes/no)");
                string ans = Console.ReadLine().ToLower().Trim();
                if (ans != "yes")
                    break;
            }

        }
        public void Choice()
        {
            OrderSearch objProduct = new OrderSearch();
            Console.WriteLine("\n********** Welcome to Sample Order Search **********\n");
            Console.WriteLine("Please select option\n1. List all orders\n2. Search order by criteria (Format: Order Number || (MSA && Status)) && CompletionDte)\n3. Exit");
            try
            {
                int i = Convert.ToInt32(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        objProduct.AllRecords();
                        break;
                    case 2:
                        objProduct.Record();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid option");
            }
        }


    }
}
