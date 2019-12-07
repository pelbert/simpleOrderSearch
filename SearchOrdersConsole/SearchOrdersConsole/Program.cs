using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchOrdersConsole.OrderSearchService;

namespace SearchOrdersConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OrderSearchServiceSoapClient service = new OrderSearchServiceSoapClient();
            SearchOrderViewModel model = new SearchOrderViewModel();
            Console.Write("Enter OrderId:");
            int orderId;int msa;int status;
            if (int.TryParse(Console.ReadLine(), out orderId)) { model.OrderId = orderId; }
            Console.Write("Enter MSA:");
            if (int.TryParse(Console.ReadLine(), out msa)) { model.MSA = msa; }
            Console.Write("Enter Status:");
            if (int.TryParse(Console.ReadLine(), out status)) { model.Status = status; }

            Console.Write("Enter Completion Date(YYYY-MM-DD):");
            DateTime dt = new DateTime();
            if (DateTime.TryParse(Console.ReadLine().ToString(), out dt))
            {
                model.CompletionDte = dt;
            }
            int page = 0;
            int pageSize = 25;

            if (IsValidCriteria(model))
            {
                var data = service.SearchOrder(model, page, pageSize);
                if (data?.Any() ?? false)
                {
                    Console.WriteLine("Your Orders: ");
                    data.ToList().ForEach(x =>
                    {
                        Console.WriteLine("OrderID: {0},ShipperID: {1},DriverID: {2},CompletionDte: {3},Status: {4},Code: {5},MSA: {6},Duration: {7},OfferType: {8}",
                            x.OrderId, x.ShipperId, x.DriverId, x.CompletionDte, x.Status, x.Code, x.MSA, x.Duration, x.OfferType);
                    });
                }
                else
                {
                    Console.WriteLine("No data ");
                }
            }
            else
            {
                Console.WriteLine("Please provide valid criteria");
            }
            Console.ReadKey();
        }

        private static bool IsValidCriteria(SearchOrderViewModel model)
        {
            bool bValid = false;
            if (model.CompletionDte != null)
            {
                if (model.OrderId > 0)
                {
                    bValid = true;
                }
                else if (model.MSA > 0 && model.Status > 0)
                {
                    bValid = true;
                }
            }
            return bValid;
        }
    }
}
