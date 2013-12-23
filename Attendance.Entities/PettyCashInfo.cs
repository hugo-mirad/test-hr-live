using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Entities
{
   public class PettyCashInfo
    {
        private string _AccountName;

        public string AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value; }
        }

        private DateTime _AccountDate;

        public DateTime AccountDate
        {
            get { return _AccountDate; }
            set { _AccountDate = value; }
        }

        private string _InitialAmoun;

        public string InitialAmoun
        {
            get { return _InitialAmoun; }
            set { _InitialAmoun = value; }
        }
        private string _AmountType;

        public string AmountType
        {
            get { return _AmountType; }
            set { _AmountType = value; }
        }

        private string _FromWhom;

        public string FromWhom
        {
            get { return _FromWhom; }
            set { _FromWhom = value; }
        }


        private string _ExpenseManName;

        public string ExpenseManName
        {
            get { return _ExpenseManName; }
            set { _ExpenseManName = value; }
        }

        private string _Expensetype;

        public string Expensetype
        {
            get { return _Expensetype; }
            set { _Expensetype = value; }
        }

        private string _ExpenseSubType; 

        public string ExpenseSubType
        {
            get { return _ExpenseSubType; }
            set { _ExpenseSubType = value; }
        }

        private DateTime _ExpenseDate;

        public DateTime ExpenseDate
        {
            get { return _ExpenseDate; }
            set { _ExpenseDate = value; }
        }
        private string _ExpenseAmount;

        public string ExpenseAmount
        {
            get { return _ExpenseAmount; }
            set { _ExpenseAmount = value; }
        }

        private string _BillNum;

        public string BillNum
        {
            get { return _BillNum; }
            set { _BillNum = value; }
        }

        private string _VoucherNum;

        public string VoucherNum
        {
            get { return _VoucherNum; }
            set { _VoucherNum = value; }
        }

        private string _ChequeNum;

        public string ChequeNum
        {
            get { return _ChequeNum; }
            set { _ChequeNum = value; }
        }

        private string _Notes;

        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }

    }
}
