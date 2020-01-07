using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuYaDemo.Models
{
    /// <summary>
    /// Information regarding a tuya order that has been placed
    /// </summary>
    public class OrderInfo
    {
        /// <summary>
        /// The unique ID of the order 
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// The unique ID of the shipper
        /// </summary>
        public int ShipperID { get; set; }

        /// <summary>
        /// The unique ID of the driver
        /// </summary>
        public int DriverID { get; set; }

        /// <summary>
        /// The date and time of the order completion
        /// </summary>
        public DateTime CompletionDte { get; set; }

        /// <summary>
        /// The status of the order
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// The order code
        /// </summary>|
        public string Code { get; set; }

        /// <summary>
        /// Master Service Agreement type? (Actually not sure what this is...)
        /// </summary>
        public int MSA { get; set; }

        /// <summary>
        /// The amount of time in minutes the order took to complete
        /// </summary>
        public decimal Duration { get; set; }

        /// <summary>
        /// The offer type
        /// </summary>
        public int OfferType { get; set; }
    }
}