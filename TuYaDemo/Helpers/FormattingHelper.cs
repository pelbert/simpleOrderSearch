using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuYaDemo.Helpers
{
    /// <summary>
    /// This class contains helpers to format certain data types into a format that is easily to work with
    /// </summary>
    public static class FormattingHelper
    {
        /// <summary>
        /// Converts string to DateTime object and returns the object
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string dateTime)
        {
            DateTime dt;
            if (DateTime.TryParse(dateTime, out dt))
                return dt;
            else
                return DateTime.Now;

        }
    }
}