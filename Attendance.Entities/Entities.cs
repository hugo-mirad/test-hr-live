using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Attendance.Entities
{
    public class Entities
    {
        public int IpAddressId { get; set; }

        public string IpAddress { get; set; }

        public Boolean IsIPActive { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public int TimeZoneId { get; set; }

        public string TimeZoneCode { get; set; }

        public string TimeZoneName { get; set; }

        public string PhotoLink { get; set; }

        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LogUserID { get; set; }

        public DateTime LoginDate { get; set; }

        public DateTime LogOutDate { get; set; }

        public string LoginNotes { get; set; }

        public string LogOutNotes { get; set; }

        public DateTime StartTime { get; set; }

        public string EndTime { get; set; }

        public string passcode { get; set; }

        public string EMPID { get; set; }

        public string designation { get; set; }   
    }
}
