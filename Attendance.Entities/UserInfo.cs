using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Entities
{
     public class UserInfo
    {
        private string _EmpID;

        public string EmpID
        {
            get { return _EmpID; }
            set { _EmpID = value; }
        }


        private int _ShiftID;

        public int ShiftID
        {
            get { return _ShiftID; }
            set { _ShiftID = value; }
        }

        private string _Firstname;

        public string Firstname
        {
            get { return _Firstname; }
            set { _Firstname = value; }
        }
        private string _Lastname;

        public string Lastname
        {
            get { return _Lastname; }
            set { _Lastname = value; }
        }

        private string _BFirstname;

        public string BFirstname
        {
          get { return _BFirstname; }
          set { _BFirstname = value; }
        }



        private string _BLastname;

        public string BLastname
        {
            get { return _BLastname; }
            set { _BLastname = value; }
        }


        private string _Deptname;

        public string Deptname
        {
            get { return _Deptname; }
            set { _Deptname = value; }
        }

        private string _Designation;

        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        private DateTime _StartDt;

        public DateTime StartDt
        {
            get { return _StartDt; }
            set { _StartDt = value; }
        }
        private DateTime _TermDt;

        public DateTime TermDt
        {
            get { return _TermDt; }
            set { _TermDt = value; }
        }

        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        private string _TermReason;

        public string TermReason
        {
            get { return _TermReason; }
            set { _TermReason = value; }
        }

        private string _Gender;

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private DateTime _DateOfBirth;

        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }

        private int _MaritalID;

        public int MaritalID
        {
            get { return _MaritalID; }
            set { _MaritalID = value; }
        }

        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        private string _Mobile;

        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        private string _PersonalEmail;

        public string PersonalEmail
        {
            get { return _PersonalEmail; }
            set { _PersonalEmail = value; }
        }
        private string _BusinessEmail;

        public string BusinessEmail
        {
            get { return _BusinessEmail; }
            set { _BusinessEmail = value; }
        }
        private int _ScheduleID;

        public int ScheduleID
        {
            get { return _ScheduleID; }
            set { _ScheduleID = value; }
        }
        private string _Address1;

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        private string _Address2;

        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        private string _Zip;

        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private string _DriverLicense;

        public string DriverLicense
        {
            get { return _DriverLicense; }
            set { _DriverLicense = value; }
        }
        private string _SSN;

        public string SSN
        {
            get { return _SSN; }
            set { _SSN = value; }
        }
        private int _StateID;

        public int StateID
        {
            get { return _StateID; }
            set { _StateID = value; }
        }
        private int _EmpTypeID;

        public int EmpTypeID
        {
            get { return _EmpTypeID; }
            set { _EmpTypeID = value; }
        }

        private int _WageID;

        public int WageID
        {
            get { return _WageID; }
            set { _WageID = value; }
        }
        private string _Salary;

        public string Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }

        private int _Deductions;

        public int Deductions
        {
            get { return _Deductions; }
            set { _Deductions = value; }
        }

        private string _County;

        public string County
        {
            get { return _County; }
            set { _County = value; }
        }
                
     }
}
