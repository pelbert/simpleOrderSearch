using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TuYaDemo.Helpers
{
    public static class AppSettingsHelper
    {
        public static bool IsDevelopment()
        {
            var env = Environment.ExpandEnvironmentVariables(
                    ConfigurationManager.AppSettings["Enviornment"]);
            if (env == "Dev")
            {
                return true;
            }
            else
                return false;
        }
    }
}