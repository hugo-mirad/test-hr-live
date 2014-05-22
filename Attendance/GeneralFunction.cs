using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;

namespace Attendance
{
    public class GeneralFunction
    {
        public static System.DateTime GetFirstDayOfWeekDate(System.DateTime myDate)
        {
            switch (myDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    myDate = myDate.Subtract(new TimeSpan(6, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Monday:
                    myDate = myDate.Subtract(new TimeSpan(0, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Tuesday:
                    myDate = myDate.Subtract(new TimeSpan(1, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Wednesday:
                    myDate = myDate.Subtract(new TimeSpan(2, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Thursday:
                    myDate = myDate.Subtract(new TimeSpan(3, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Friday:
                    myDate = myDate.Subtract(new TimeSpan(4, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Saturday:
                    myDate = myDate.Subtract(new TimeSpan(5, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
            }
            return myDate;
        }

        public static System.DateTime GetLastDayOfWeekDate(System.DateTime myDate)
        {
            switch (myDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    myDate = myDate.Add(new TimeSpan(0, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Monday:
                    myDate = myDate.Add(new TimeSpan(6, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Tuesday:
                    myDate = myDate.Add(new TimeSpan(5, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Wednesday:
                    myDate = myDate.Add(new TimeSpan(4, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Thursday:
                    myDate = myDate.Add(new TimeSpan(3, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Friday:
                    myDate = myDate.Add(new TimeSpan(2, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case DayOfWeek.Saturday:
                    myDate = myDate.Add(new TimeSpan(1, 0, 0, 0));
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
            }
            return myDate;
        }

        public static System.DateTime GetNextDayOfWeekDate(System.DateTime myDate)
        {
            myDate = myDate.Add(new TimeSpan(1, 0, 0, 0));
            return myDate;
        }

        public static string CalNumericToTime(string timenum)
        {
            string total = "";
            try
            {
                if (timenum != "")
                {
                    string[] time1 = timenum.Split('.');
                    int Hrs = Convert.ToInt32(time1[0]);
                    Double min = Convert.ToDouble(time1[1]) * 0.006;
                    total = (Hrs + ":" + min).ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return total;
        }
        public static string ToProper(string s)
        {
            s = s.ToString().ToLower();
            string sProper = "";

            if (s.Contains('-'))
            {
                char[] seps = new char[] { '-' };
                foreach (string ss in s.ToString().Split(seps))
                {
                    if (ss.Length > 0)
                    {
                        sProper += char.ToUpper(ss[0]);
                        sProper +=
                        (ss.Substring(1, ss.Length - 1) + ' ');
                    }
                }
            }
            else
            {
                char[] seps = new char[] { ' ' };
                foreach (string ss in s.ToString().Split(seps))
                {
                    if (ss.Length > 0)
                    {
                        sProper += char.ToUpper(ss[0]);
                        sProper +=
                        (ss.Substring(1, ss.Length - 1) + ' ');
                    }

                }
            }
            return sProper;
        }

        public static string WrapTextByMaxCharacters(object objText, int intMaxChars)
        {
            string strReturnValue = "";

            if (objText != null)
            {
                if (objText.ToString().Trim() != "")
                {
                    if (objText.ToString().Trim().Length > intMaxChars)
                    {
                        strReturnValue = objText.ToString().Trim().Substring(0, intMaxChars) + "...";
                    }
                    else
                    {
                        strReturnValue = objText.ToString().Trim();
                    }
                }
            }
            return strReturnValue;
        }

        public static string ToProperNotes(string s)
        {
            string sProper = "";

            if (s != "")
            {
                s = s.ToString().ToLower();

                if (s.Length > 0)
                {
                    sProper += char.ToUpper(s[0]);
                    sProper +=
                    (s.Substring(1, s.Length - 1));
                }
            }

            return sProper;
        }

        public static string FormatUsTelephoneNo(string strPhoneNo)
        {
            string strPhoneNumber = string.Empty;
            if (strPhoneNo.ToString() != "" && strPhoneNo.ToString() != null && strPhoneNo.ToString() != " ")
            {
                strPhoneNumber = Convert.ToString(strPhoneNo);

                strPhoneNumber = string.Format("{0}-{1}-{2}", strPhoneNumber.Substring(0, 3), strPhoneNumber.Substring(3, 3), strPhoneNumber.Substring(6));
            }
            else
            {
                strPhoneNumber = "NA";
            }
            return strPhoneNumber;

        }

        public static string FormatUsSSN(string strPhoneNo)
        {
            string strPhoneNumber = string.Empty;
            if (strPhoneNo.ToString() != "" && strPhoneNo.ToString() != null && strPhoneNo.ToString() != " ")
            {
                strPhoneNumber = Convert.ToString(strPhoneNo);

                strPhoneNumber = string.Format("{0}-{1}-{2}", strPhoneNumber.Substring(0, 3), strPhoneNumber.Substring(3, 2), strPhoneNumber.Substring(5));
            }
            else
            {
                strPhoneNumber = "";
            }
            return strPhoneNumber;

        }



        public static string FormatCurrency(object strValue, string Location)
        {

            string strPrice = strValue.ToString();
            string text = "";
            decimal parsed = decimal.Parse(strPrice, CultureInfo.InvariantCulture);
            if (Location.ToUpper() == "USMP" || Location.ToUpper() == "USWB")
            {
                text = parsed.ToString("C0", new System.Globalization.CultureInfo("en-US"));
            }

            else
            {
                CultureInfo hindi = new CultureInfo("hi-IN");
                text = string.Format(hindi, "{0:C0}", parsed);
            }
            return text;
        }

        public static string ToProperTime(string obj)
        {
            string proper = "";
            if (obj != "")
            {
                string[] obj1 = obj.Split('.');
                proper = ((obj1[0].Length == 1 ? ("0" + obj1[0]).Trim() : obj1[0]).Trim() + ":" + obj1[1]).Trim();
            }
            return proper;
        }


        public static int CalNumericToint(double timeNum)
        {
            int total = 0;
            try
            {
                int i = (int)Math.Truncate(timeNum);
                int f = (int)Math.Round(100 * Math.Abs(timeNum - i));

                total = i * 60 + f;
            }
            catch (Exception ex)
            {
            }
            return total;
        }


        public static string CalDoubleToTime(double timeNum)
        {
            string total = "";
            try
            {
                int i = (int)Math.Truncate(timeNum);
                int f = (int)Math.Round(100 * Math.Abs(timeNum - i));
                double min = f * 0.6;
                f = (int)Math.Round(min);

                total = (i < 10 ? ("0" + i).ToString() : i.ToString()) + ":" + (f < 10 ? ("0" + f.ToString()) : f.ToString());
            }
            catch (Exception ex)
            {
            }
            return total;
        }

        public static string ConverttoTime(int obj)
        {
            string totalHrs = "";
            if (obj != 0)
            {
                int hrs = obj / 60;
                int minutes = obj % 60;
                string hrs1 = "";
                string min = "";
                if (minutes < 10)
                {
                    min = "0" + minutes.ToString();
                }
                else
                {
                    min = minutes.ToString();
                }

                if (hrs < 10)
                {
                    hrs1 = "0" + hrs.ToString();
                }
                else
                {
                    hrs1 = hrs.ToString();

                }
                totalHrs = hrs1.Trim() + ":" + min;

            }
            else
            {
                totalHrs = "00:00";
            }
            return totalHrs;
        }


        public string ToProperHtml(object text)
        {
            return HttpUtility.HtmlEncode(text.ToString());
        }

        public static string ToproperMinutes(string time)
        {
            string proper = "";
            if (time != "")
            {
                string[] timesplit = time.Split('.');
                int minutes = Convert.ToInt32(Convert.ToDouble(timesplit[1]) * 0.6);
                if (minutes < 10)
                {
                    proper = timesplit[0] + ":0" + minutes.ToString();
                }
                else
                {
                    proper = timesplit[0] + ":" + minutes.ToString();
                }
            }
            return proper;
        }


        //public static bool GetHoliday(DataTable dt, DateTime dayt)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}


        public static string GetColor(string name, string flag)
        {


            string c = "";
            if (name != "")
            {
                switch (name)
                {
                    case "H":
                         c=" atnHoliday ";
                         break;
                        
                    case "L":
                        if (flag == "Approved")
                        {
                            c= " atnLeave ";
                        }
                        else if(flag=="Open") 
                        {
                            c= " atnUnLeave ";
                        }
                        else if (flag == "Denied")
                        {
                            c = " atnUnLeave ";
                        }
                        break;
                    case "S":
                        c= " atnSun ";
                        break;
                 }
            }
            return c;
        }


   

    }
}
