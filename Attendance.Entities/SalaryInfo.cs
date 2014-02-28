using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Entities
{
    public class SalaryInfo
    {
        private float calSalary;

        public float CalSalary
        {
            get { return calSalary; }
            set { calSalary = value; }
        }
        private float bonus;
        
        public float Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

        private float incentives;

        public float Incentives
        {
            get { return incentives; }
            set { incentives = value; }
        }

        private float prevUnpaid;

        public float PrevUnpaid
        {
            get { return prevUnpaid; }
            set { prevUnpaid = value; }
        }

        private float advancePaid;

        public float AdvancePaid
        {
            get { return advancePaid; }
            set { advancePaid = value; }
        }

        private float expenses;

        public float Expenses
        {
            get { return expenses; }
            set { expenses = value; }
        }

        private float loanDeduct;

        public float LoanDeduct
        {
            get { return loanDeduct; }
            set { loanDeduct = value; }
        }

        private float totalPay;

        public float TotalPay
        {
            get { return totalPay; }
            set { totalPay = value; }
        }

        private int mnth;

        public int Mnth
        {
            get { return mnth; }
            set { mnth = value; }
        }

        private int years;

        public int Years
        {
            get { return years; }
            set { years = value; }
        }

        private int atnLogID;

        public int AtnLogID
        {
            get { return atnLogID; }
            set { atnLogID = value; }
        }
        private int enterBy;

        public int EnterBy
        {
            get { return enterBy; }
            set { enterBy = value; }
        }

        private DateTime enteredDate;

        public DateTime EnteredDate
        {
            get { return enteredDate; }
            set { enteredDate = value; }
        }

        private string internalNotes;

        public string InternalNotes
        {
            get { return internalNotes; }
            set { internalNotes = value; }
        }
    }
}
