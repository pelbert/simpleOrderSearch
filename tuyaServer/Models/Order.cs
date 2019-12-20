using System.ComponentModel.DataAnnotations;

namespace tuya.Models 
{

    [Microsoft.AspNetCore.Mvc.BindProperties(SupportsGet = true)]
    public class Order
    {
       [Key]
       public int OrderID {get; set;}
       public int ShipperID {get; set;}
       public int DriverID {get; set;}
       public string CompletionDate {get; set;}
       public int Status {get; set;} 
       public string Code {get; set;}
       public int MSA {get; set;}
       public string Duration {get; set;}
       public int OfferType {get; set;}

    }
}