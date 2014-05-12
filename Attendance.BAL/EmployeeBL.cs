using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Attendance.BAL
{
    public class EmployeeBL
    {
        public bool UpdatePasswordByUserID(int userid, string Oldpassword, string Newpassword)
        {
            bool success = false;

            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_UpdatePasswordByUserID]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@Userid", userid));
                da.SelectCommand.Parameters.Add(new SqlParameter("@Oldpwd", Oldpassword));
                da.SelectCommand.Parameters.Add(new SqlParameter("@NewPwd", Newpassword));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    success = true;
                }
              

            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public bool UpdatePasscodeByUserID(int userid, string oldpasscode, string newpasscode)
        {

            bool success = false;

            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_UpdatePassCodeByUserID]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@Userid", userid));
                da.SelectCommand.Parameters.Add(new SqlParameter("@OldPasscode", oldpasscode));
                da.SelectCommand.Parameters.Add(new SqlParameter("@NewPasscode", newpasscode));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
            }
            return success;

       }
        public bool ResetPasscodeByAdmin(int userid, string ip, string newpasscode,DateTime currentDate,string EmpID)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_ResetPasscodeByAdmin]", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@changedbyid", SqlDbType.Int).Value = userid;
                command.Parameters.Add("@Empid", SqlDbType.VarChar).Value = EmpID;
                command.Parameters.Add("@ipaddress", SqlDbType.VarChar).Value = ip;
                command.Parameters.Add("@NewPasscode", SqlDbType.VarChar).Value = newpasscode;
                command.Parameters.Add("@updateDate", SqlDbType.DateTime).Value = currentDate;
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public bool ResetPassWordByAdmin(int userid, string ip, string newPwd, DateTime currentDate, string EmpID)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_ResetpasswordByAdmin]", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@changedbyid", SqlDbType.Int).Value = userid;
                command.Parameters.Add("@Empid", SqlDbType.VarChar).Value = EmpID;
                command.Parameters.Add("@ipaddress", SqlDbType.VarChar).Value = ip;
                command.Parameters.Add("@NewPwd", SqlDbType.VarChar).Value = newPwd;
                command.Parameters.Add("@updateDate", SqlDbType.DateTime).Value = currentDate;
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public DataTable GetEmployyeDetailsByUserID(int userid)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetEmployeeDetailsByUserID]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@EmpID", userid));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];
        }
        public string AddNewSchedule(string SchStart,string SchEnd,string LunchStart,string LunchEnd,bool fiveDays,bool SixDays,bool SevenDays,string IP,DateTime CurrentDt,int UserID)
        {
            bool success = false;
            string ID = "";
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_AddSchedule]", con);
                DataSet ds = new DataSet();
                da.SelectCommand.Parameters.Add(new SqlParameter("@StartTime", SchStart));
                da.SelectCommand.Parameters.Add(new SqlParameter("@EndTime", SchEnd));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LunchBreakStart", LunchStart));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LunchBreakEnd", LunchEnd));
                da.SelectCommand.Parameters.Add(new SqlParameter("@IsFiveDays", fiveDays));
                da.SelectCommand.Parameters.Add(new SqlParameter("@IsSixDays", SixDays));
                da.SelectCommand.Parameters.Add(new SqlParameter("@IsSevenDays", SevenDays));
                da.SelectCommand.Parameters.Add(new SqlParameter("@ipaddress", IP));
                da.SelectCommand.Parameters.Add(new SqlParameter("@enteredBy", UserID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@CurrentDt", CurrentDt));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ID = dt.Rows[0]["ScheduleID"].ToString();
                }

               
              
            }
            catch (Exception ex)
            {
            }
            return ID;
        }
        public DataTable GetLeaveDetailsByLoction(string LocationName,DateTime Startdate,DateTime EndDate,int ApprovedStatusID,int shiftID)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetAllLeaveRequestDetailsByLocation]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@LocatinName", LocationName));
                da.SelectCommand.Parameters.Add(new SqlParameter("@Startdate", Startdate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@Enddate", EndDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", shiftID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@ApproveStatusID", ApprovedStatusID));
           
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];
        }
        public bool UpdateLeaveRequest(int LeaveID, int ApprovedBy, int ApprovedStatus, string LeaveNotes,DateTime CurrentDt)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_UpdateLeaveRequestDetails]", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ApprovedBy", SqlDbType.Int).Value = ApprovedBy;
                command.Parameters.Add("@ApprovedDt", SqlDbType.DateTime).Value = CurrentDt;
                command.Parameters.Add("@ApprovesStatusID", SqlDbType.Int).Value = ApprovedStatus;
                command.Parameters.Add("@notes", SqlDbType.VarChar).Value = LeaveNotes;
                command.Parameters.Add("@LeaveID", SqlDbType.Int).Value = LeaveID;
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public DataTable GetEmpPaidleavesDetailsByLocation(int location,DateTime StartDt,DateTime EndDt,int shiftID)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[Usp_GetPaidLeaveDetailsByLocation]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", StartDt));
                da.SelectCommand.Parameters.Add(new SqlParameter("@EndDate", EndDt));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", location));
                da.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", shiftID));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable Usp_GetEmployeePayrollDataByLocation(int location, DateTime StartDt, DateTime EndDt,int shiftID)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[Usp_GetEmployeePayrollDataByLocation]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", StartDt));
                da.SelectCommand.Parameters.Add(new SqlParameter("@EndDate", EndDt));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", location));
                da.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", shiftID));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public bool UpdatePaidLeaveByLeaveID(int LeaveAvail,int LeavesUsed,int LeavesBalanced,int paildLeavID,int Enterby, string notes, DateTime CurrentDt,DateTime paidLvsStartDt,string IP,int PaidLeaveUserID,DateTime EnterDate)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_UpdatePaidLeavesDetials]", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@LeaveAvail", SqlDbType.Int).Value = LeaveAvail;
                command.Parameters.Add("@CurrentDt", SqlDbType.DateTime).Value = CurrentDt;
                command.Parameters.Add("@paidLvsStartDt", SqlDbType.DateTime).Value = paidLvsStartDt;
                command.Parameters.Add("@EnterDt", SqlDbType.DateTime).Value = EnterDate;
                command.Parameters.Add("@LeaveUsed", SqlDbType.Int).Value = LeavesUsed;
                command.Parameters.Add("@LeaveBalanced", SqlDbType.Int).Value = LeavesBalanced;
                command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = notes;
                command.Parameters.Add("@PaidLeaveID", SqlDbType.Int).Value = paildLeavID;
                command.Parameters.Add("@EnterBY", SqlDbType.Int).Value = Enterby;
                command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = IP;
                command.Parameters.Add("@PaidLeaveUserID", SqlDbType.Int).Value = PaidLeaveUserID;
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public DataTable GetSelectedEmpByLocDept(string LocationName, string Dept)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetSelectedEmployeeByLocDept]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@Location", LocationName));
                da.SelectCommand.Parameters.Add(new SqlParameter("@Dept", Dept));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];
        }
        public bool SaveandGetHolidayDet(bool ISHoliday, DateTime HolidayDt, int locID, int DeptID, string shiftName, int EnterBy, DateTime EnterDt, string IP, string Holidayname,bool IsDefault)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_SaveHolidayDetails]", con);//USP_UpdateHolidayDetails
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Holidayname", Holidayname));
                command.Parameters.Add(new SqlParameter("@IsHoliday", ISHoliday));
                command.Parameters.Add(new SqlParameter("@HolidayDate", HolidayDt));
                command.Parameters.Add(new SqlParameter("@LocationID", locID));
                command.Parameters.Add(new SqlParameter("@DeptID", DeptID));
                command.Parameters.Add(new SqlParameter("@shiftName", shiftName));
                command.Parameters.Add(new SqlParameter("@EnteredBy", EnterBy));
                command.Parameters.Add(new SqlParameter("@EnteredDate", EnterDt));
                command.Parameters.Add(new SqlParameter("@Ipaddress", IP));
                command.Parameters.Add(new SqlParameter("@IsDefault", IsDefault));
              //  command.Parameters.Add(new SqlParameter("@HolidayID", HolidayID));
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }

            return success;
        }

        public bool UpdateHolidayDet(bool ISHoliday, int HolidayID, int locID, int DeptID, string shiftName, int EnterBy, DateTime EnterDt, string IP)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_UpdateHolidayDetails]", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@holidayID", HolidayID));
                command.Parameters.Add(new SqlParameter("@IsHoliday", ISHoliday));
                command.Parameters.Add(new SqlParameter("@LocationID", locID));
                command.Parameters.Add(new SqlParameter("@DeptID", DeptID));
                command.Parameters.Add(new SqlParameter("@updateby", EnterBy));
                command.Parameters.Add(new SqlParameter("@updatedDate", EnterDt));
                command.Parameters.Add(new SqlParameter("@Ipaddress", IP));
                command.Parameters.Add(new SqlParameter("@shiftName", shiftName));
               
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }

            return success;
        }


        public DataTable GetHolidayDetByLoc(DateTime startDt,DateTime EndDt,int locationID,int shiftID,int DepartmentID)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetHolidayDet]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", locationID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", shiftID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@DepartmentID", DepartmentID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@StartDate", startDt));
                da.SelectCommand.Parameters.Add(new SqlParameter("@EndDate", EndDt));           
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }
        public DataTable GetLeaveRequestDetByUserID(int userid,DateTime startdate,DateTime endDate)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetLeaveRequestsByUserid]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@Startdate", startdate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@Enddate", endDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@userid", userid));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GetDepartmentByShifts(string shiftName,int locationID)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetDepartmentsByShifts]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@shiftname", shiftName));
                da.SelectCommand.Parameters.Add(new SqlParameter("@locationID", locationID));
          
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];
        }

        public DataTable GetYear()
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetYear]", con);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];
        }


        public DataTable GetDefaultHolidays(int shiftID, int locationID,int DepartmentID)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetDefaultHolidays]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", shiftID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", locationID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@DepartmentID", DepartmentID));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];
        }


        public bool SaveDefaultHolidayDet(DateTime fromdate ,DateTime todate,bool ISHoliday,int locID, int DeptID, string shiftName, int EnterBy, DateTime EnterDt, string IP, string Holidayname, bool IsDefault,string dayname)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_SaveDefaultHolidays]", con);//USP_UpdateHolidayDetails
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Holidayname", Holidayname));
                command.Parameters.Add(new SqlParameter("@dayname", dayname));
                command.Parameters.Add(new SqlParameter("@IsHoliday", ISHoliday));
                command.Parameters.Add(new SqlParameter("@Fromdate", fromdate));
                command.Parameters.Add(new SqlParameter("@todate", todate));
                command.Parameters.Add(new SqlParameter("@LocationID", locID));
                command.Parameters.Add(new SqlParameter("@DeptID", DeptID));
                command.Parameters.Add(new SqlParameter("@shiftName", shiftName));
                command.Parameters.Add(new SqlParameter("@EnteredBy", EnterBy));
                command.Parameters.Add(new SqlParameter("@EnteredDate", EnterDt));
                command.Parameters.Add(new SqlParameter("@Ipaddress", IP));
                command.Parameters.Add(new SqlParameter("@IsDefault", IsDefault));
                //  command.Parameters.Add(new SqlParameter("@HolidayID", HolidayID));
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }

            return success;
        }



        public bool UpdateDefaultHolidayDet(DateTime fromdate, DateTime todate, bool ISHoliday, string deafaultday, int locID, int DeptID, string shiftName, int EnterBy, DateTime EnterDt, string IP)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_UpdateDefaulDet]", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@fromdate", fromdate));
                command.Parameters.Add(new SqlParameter("@todate", todate));
                command.Parameters.Add(new SqlParameter("@deafaultday", deafaultday));
                command.Parameters.Add(new SqlParameter("@IsHoliday", ISHoliday));
                command.Parameters.Add(new SqlParameter("@LocationID", locID));
                command.Parameters.Add(new SqlParameter("@DeptID", DeptID));
                command.Parameters.Add(new SqlParameter("@updateby", EnterBy));
                command.Parameters.Add(new SqlParameter("@updatedDate", EnterDt));
                command.Parameters.Add(new SqlParameter("@Ipaddress", IP));
                command.Parameters.Add(new SqlParameter("@shiftName", shiftName));

                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }

            return success;
        }


        //USP_GetDefaultHolidays
    }
}
