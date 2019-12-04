using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SimpleOrderSearch
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        // TODO: Add your service operations here
        [OperationContract]
        OrderInfoResponse GetOrderInfos(OrderInfo orderInfo);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class OrderInfo
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public int ShipperID { get; set; }

        [DataMember]
        public int DriverID { get; set; }

        [DataMember]
        public string CompletionDte { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int MSA { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string OfferType { get; set; }
    }

    [DataContract]
    public class OrderInfoResponse
    {
        [DataMember]
        public int ReturnCode { get; set; }

        [DataMember]
        public OrderInfo[] OrderInfos { get; set; }
    }
}
