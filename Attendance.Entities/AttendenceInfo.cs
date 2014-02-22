using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Entities
{
    public class AttendenceInfo
    {
        private float workingDays;

        public float WorkingDays
        {
            get { return workingDays; }
            set { workingDays = value; }
        }

        private float attendDays;

        public float AttendDays
        {
            get { return attendDays; }
            set { attendDays = value; }
        }

        private float leaves;

        public float Leaves
        {
            get { return leaves; }
            set { leaves = value; }
        }

        private float noShow;

        public float NoShow
        {
            get { return noShow; }
            set { noShow = value; }
        }

        private float paidLeaves;

        public float PaidLeaves
        {
            get { return paidLeaves; }
            set { paidLeaves = value; }
        }

        private float paidLeavesUsed;

        public float PaidLeavesUsed
        {
            get { return paidLeavesUsed; }
            set { paidLeavesUsed = value; }
        }

        private float paidLeavesBalanced;

        public float PaidLeavesBalanced
        {
            get { return paidLeavesBalanced; }
            set { paidLeavesBalanced = value; }
        }

        private float TotalCalLeaves;

        public float TotalCalLeaves1
        {
            get { return TotalCalLeaves; }
            set { TotalCalLeaves = value; }
        }

        private int enterBy;
        public int EnterBy
        {
            get { return enterBy; }
            set { enterBy = value; }
        }

        private DateTime enterDate;
        public DateTime EnterDate
        {
            get { return enterDate; }
            set { enterDate = value; }
        }

        private int mnth;

        public int Mnth
        {
            get { return mnth; }
            set { mnth = value; }
        }

        private int yr;

        public int Yr
        {
            get { return yr; }
            set { yr = value; }
        }
        private int userid;

        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }

    }
}
