using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Entities
{
    public class MultipleLogininfo
    {

        private string _loginDate;

        public string LoginDate
        {
            get { return _loginDate; }
            set { _loginDate = value; }
        }

        private string _logoutDate;

        public string LogoutDate
        {
            get { return _logoutDate; }
            set { _logoutDate = value; }
        }

        private int  _Loguserid;

        public int Loguserid
        {
            get { return _Loguserid; }
            set { _Loguserid = value; }
        }

        private string _SchStart;

        public string SchStart
        {
            get { return _SchStart; }
            set { _SchStart = value; }
        }

        private string _SchEnd;

        public string SchEnd
        {
            get { return _SchEnd; }
            set { _SchEnd = value; }
        }
    }
}
