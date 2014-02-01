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
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_UpdatePasswordByUserID]", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
                // command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;
                command.Parameters.Add("@Oldpwd", SqlDbType.VarChar).Value = Oldpassword;
                command.Parameters.Add("@NewPwd", SqlDbType.VarChar).Value = Newpassword;
              //  command.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Location;
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }


        public bool UpdatePasscodeByUserID(int userid, string oldpasscode, string newpasscode)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_UpdatePassCodeByUserID]", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
                // command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;
                command.Parameters.Add("@OldPasscode", SqlDbType.VarChar).Value = oldpasscode;
                command.Parameters.Add("@NewPasscode", SqlDbType.VarChar).Value = newpasscode;
                //  command.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Location;
                command.ExecuteNonQuery();
                con.Close();
                success = true;
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


        public DataTable GetLeaveDetailsByLoction(string LocationName,DateTime Startdate,DateTime EndDate,int ApprovedStatusID)
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

        public DataTable GetEmpPaidleavesDetailsByLocation(int location)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[Usp_GetPaidLeaveDetailsByLocation]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", location));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];
        }


        public bool UpdatePaidLeaveByLeaveID(int LeaveAvail, int Maxleave, int paildLeavID,int Enterby, string notes, DateTime CurrentDt,DateTime paidLvsStartDt,string IP,int PaidLeaveUserID)
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
                command.Parameters.Add("@MaxLeave", SqlDbType.Int).Value = Maxleave;
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

        public bool SaveandGetHolidayDet(bool ISHoliday, DateTime HolidayDt, int locID, int DeptID, int userid, int EnterBy, DateTime EnterDt, string IP, string Holidayname)
        {

            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_SaveHolidayDetails]", con);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.Add(new SqlParameter("@Holidayname", Holidayname));
                command.Parameters.Add(new SqlParameter("@IsHoliday", ISHoliday));
                command.Parameters.Add(new SqlParameter("@HolidayDate", HolidayDt));
                command.Parameters.Add(new SqlParameter("@LocationID", locID));
                command.Parameters.Add(new SqlParameter("@DeptID", DeptID));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@EnteredBy", EnterBy));
                command.Parameters.Add(new SqlParameter("@EnteredDate", EnterDt));
                command.Parameters.Add(new SqlParameter("@Ipaddress", IP));
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }

            return success;
        }

        public DataTable GetHolidayDetByLoc(DateTime startDt,DateTime EndDt,int locationID)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetHolidayDet]", con);

                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", locationID));
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
    
    }
}
