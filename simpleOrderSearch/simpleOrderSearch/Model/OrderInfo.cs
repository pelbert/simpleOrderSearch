using System;
using System.Collections.Generic;
using System.Text;

namespace simpleOrderSearch.Model
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int ShipperID { get; set; }
        public int DriverID { get; set; }
        public DateTime CompletionDte { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public int MSA { get; set; }
        public string Duration { get; set; }
        public int OfferType { get; set; }
    }

    public class Paging
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalRecordCount { get; set; }
    }

    public class OrderInfo
    {
        public List<Orders> Data { get; set; }
        public Paging Paging { get; set; }
    }

}
