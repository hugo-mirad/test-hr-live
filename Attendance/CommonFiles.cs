using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance
{
    public class CommonFiles
    {
        public static string ChromeVersion = System.Configuration.ConfigurationManager.AppSettings["ChromeVersion"].ToString();
        public static string MozillaVersion = System.Configuration.ConfigurationManager.AppSettings["MozillaVersion"].ToString();
        public static string ComapnyName = System.Configuration.ConfigurationManager.AppSettings["ComapnyName"].ToString();
        public const string StrAscending = "Ascending";
        public const string StrDescending = "Descending";
        public const string StrFormatException = "Parameter ArrayList empty";
    }
}
