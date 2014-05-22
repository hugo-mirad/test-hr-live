using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Attendance.Entities;
using System.Net;
using System.Configuration;


namespace Attendance.BAL
{
    public class Business
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString();


        public DataSet GetLocationNameByIpAdress(int LocationId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter daLocation = new SqlDataAdapter("USP_GetLocationByIPAdress", connectionString);
                daLocation.SelectCommand.CommandType = CommandType.StoredProcedure;
                daLocation.SelectCommand.Parameters.Add(new SqlParameter("@LocationId", LocationId));
                DataSet dsLocation = new DataSet();
                daLocation.Fill(dsLocation);
                return dsLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataSet GetUTCDatetime(int TimeZoneId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter daDateTime = new SqlDataAdapter("USP_GetUTCDatetime", connectionString);
                daDateTime.SelectCommand.Parameters.Add(new SqlParameter("@TimeZoneID", TimeZoneId));
                daDateTime.SelectCommand.CommandType = CommandType.StoredProcedure;
               

                DataSet dsDateTime = new DataSet();
                daDateTime.Fill(dsDateTime);
                return dsDateTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public DataSet BindData(string LocationName,DateTime CurentDatetime,int shiftID,int offset)
        {
            try
            {
             
                DataSet dsImage = new DataSet();

                SqlDataAdapter daPhotos = new SqlDataAdapter("[USP_GetPhotoByIPAdressId1]", connectionString);
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@locationname", LocationName));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@CurrentDate", CurentDatetime));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", shiftID));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@offset", offset));
                daPhotos.SelectCommand.CommandType = CommandType.StoredProcedure;

                daPhotos.Fill(dsImage);
                return dsImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindLogin(string LocationName, DateTime CurentDatetime,int shiftID,int offset)
        {
            try
            {
                SqlDataAdapter daPhotos = new SqlDataAdapter("USP_GetPhotoByLogin1", connectionString);
                 daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@locationname", LocationName));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@CurrentDate", CurentDatetime));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", shiftID));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@offset", offset));
                daPhotos.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet dsImage = new DataSet();
                daPhotos.Fill(dsImage);
                return dsImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindLogout(string LocationName, DateTime CurentDatetime, int shiftID,int offset)
        {
            try
            {

                SqlDataAdapter daPhotos = new SqlDataAdapter("USP_GetPhotoByLogout1", connectionString);
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@locationname", LocationName));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@CurrentDate", CurentDatetime));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", shiftID));
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@offset", offset));
                daPhotos.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet dsImage = new DataSet();
                daPhotos.Fill(dsImage);
                return dsImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet SaveLogDetails(Attendance.Entities.Entities entities,int offset)
        {
            DataSet dsLoginDet = new DataSet();
            try
            {
                SqlDataAdapter daLogin = new SqlDataAdapter("[USP_AddLogin1]", connectionString);
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@UserID", entities.UserID));
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@passcode", entities.passcode));
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@LoginDate", Convert.ToDateTime((entities.LoginDate))));
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@LoginNotes", entities.LoginNotes));
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@offset", offset));
                daLogin.SelectCommand.CommandType = CommandType.StoredProcedure;
                daLogin.Fill(dsLoginDet);
                return dsLoginDet;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsLoginDet;


        }
        public DataSet SaveLogDetails1(Attendance.Entities.Entities entities, int UserLogID,int offset)
        {
            DataSet dsLogoutDet = new DataSet();
            try
            {
                SqlDataAdapter daLogout = new SqlDataAdapter("[USP_AddLogout1]", connectionString);
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@UserID", entities.UserID));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@loguserID", UserLogID));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@passcode", entities.passcode));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@LogOutDate", entities.LogOutDate));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@LogOutNotes", entities.LogOutNotes));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@offset", offset));
                daLogout.SelectCommand.CommandType = CommandType.StoredProcedure;
                daLogout.Fill(dsLogoutDet);
                return dsLogoutDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SaveManagerDetails(Attendance.Entities.Entities entities)
        {

            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("USP_AddIP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@LocationName", entities.LocationName);
                cmd.Parameters.AddWithValue("@PassCode", entities.passcode);
                cmd.Parameters.AddWithValue("@EMPID", entities.EMPID);
                cmd.Parameters.AddWithValue("@IpAddress", entities.IpAddress);
                cmd.Parameters.AddWithValue("@CurrentDate", entities.StartTime);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public void UpdateIP(Attendance.Entities.Entities entities)
        {

            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("[USP_UpdateIP]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = ();
                con.Open();
                //cmd.Parameters.AddWithValue("@FirstName",entities.FirstName);
                cmd.Parameters.AddWithValue("@LocationName", entities.LocationName);
                cmd.Parameters.AddWithValue("@CurrentDate", entities.StartTime);
                cmd.Parameters.AddWithValue("@EMPID", entities.EMPID);
                cmd.Parameters.AddWithValue("@IpAddress", entities.IpAddress);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public DataSet GetLocationDetailsByIp(string ipaddress)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter daLocation = new SqlDataAdapter("USP_GetLocationIDByIP", connectionString);
                daLocation.SelectCommand.CommandType = CommandType.StoredProcedure;
                daLocation.SelectCommand.Parameters.Add(new SqlParameter("@Ipaddress", ipaddress));
                DataSet dsLocation = new DataSet();
                daLocation.Fill(dsLocation);
                return dsLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataSet SaveLogoutDetails(Attendance.Entities.Entities objEntity, int UserLogID)
        {
            DataSet dsLogoutDet = new DataSet();
            try
            {
                SqlDataAdapter daLogout = new SqlDataAdapter("[USP_AddLogout]", connectionString);
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@UserID", objEntity.UserID));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@loguserID", UserLogID));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@LogOutDate", objEntity.LogOutDate));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@LogOutNotes", objEntity.LogOutNotes));
                daLogout.SelectCommand.CommandType = CommandType.StoredProcedure;
                daLogout.Fill(dsLogoutDet);
                return dsLogoutDet;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet AuthinticateManager(Attendance.Entities.Entities entities)
        {
            DataSet dsManagerLogin = new DataSet();
            
            try
            {
                SqlDataAdapter daManager = new SqlDataAdapter("[USP_Manager]", connectionString);
                daManager.SelectCommand.Parameters.Add(new SqlParameter("@EMPID", entities.EMPID));
                daManager.SelectCommand.Parameters.Add(new SqlParameter("@LocationName", entities.LocationName));
                daManager.SelectCommand.Parameters.Add(new SqlParameter("@PassCode", entities.passcode));
                daManager.SelectCommand.Parameters.Add(new SqlParameter("@UserID", entities.UserID));
                daManager.SelectCommand.CommandType = CommandType.StoredProcedure;
                daManager.Fill(dsManagerLogin);
                return dsManagerLogin;
      

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BindEmpData()
        {
            try
            {

               
                DataSet dsempdata = new DataSet();

                SqlDataAdapter daempData = new SqlDataAdapter("USP_GetAgentWeeklyReport", connectionString);
                //  cmd.Parameters.AddWithValue("@locationname", LocationName);
               // daempData.SelectCommand.Parameters.Add(new SqlParameter("@locationname", LocationName));
                daempData.SelectCommand.CommandType = CommandType.StoredProcedure;

                daempData.Fill(dsempdata);
                return dsempdata;

                //dlEmp.DataSource = dsImage;
                //dlEmp.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdateSignTime(string EmpID, int LoguserID, string signIntime, string notes)
         {

             bool success = false;
             try
             {

                 SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                 con.Open();
                 SqlCommand command = new SqlCommand("[USP_UpdateLogin]",con);
                 command.CommandType = CommandType.StoredProcedure;
                 command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = EmpID;
                 command.Parameters.Add("@Loguserid", SqlDbType.Int).Value = LoguserID;
                 command.Parameters.Add("@loginnotes", SqlDbType.VarChar).Value = notes;
                 command.Parameters.Add("@Logindate", SqlDbType.DateTime).Value = signIntime;
                 command.ExecuteNonQuery();
                 con.Close();
                 success = true;
             }
             catch (Exception ex)
             {
             }
             return success;
         }


         public bool UpdateSignInSignOut(string EmpID, int LoguserID, string signIntime,string signOuttime,string loginnotes,string lououtnotes)
         {

             bool success = false;
             try
             {

                 SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                 con.Open();
                 SqlCommand command = new SqlCommand("[USP_UpdateLoginLogout]", con);
                 command.CommandType = CommandType.StoredProcedure;
                 command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = EmpID;
                 command.Parameters.Add("@Loguserid", SqlDbType.Int).Value = LoguserID;
                 command.Parameters.Add("@loginnotes", SqlDbType.VarChar).Value = loginnotes;
                 command.Parameters.Add("@Logindate", SqlDbType.DateTime).Value = signIntime;
                 command.Parameters.Add("@logoutnotes", SqlDbType.VarChar).Value = lououtnotes;
                 command.Parameters.Add("@Logoutdate", SqlDbType.DateTime).Value = signOuttime;
                 command.ExecuteNonQuery();
                 con.Close();
                 success = true;
             }
             catch (Exception ex)
             {
             }
             return success;
         }


         public bool UpdateSignOutTime(int Userid, int LoguserID, string signOuttime, string notes)
         {

             bool success = false;
             try
             {

                 SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                 con.Open();
                 SqlCommand command = new SqlCommand("[USP_UpdateLogout]", con);
                 command.CommandType = CommandType.StoredProcedure;
                 command.Parameters.Add("@UserID", SqlDbType.Int).Value = Userid;
                 command.Parameters.Add("@Loguserid", SqlDbType.Int).Value = LoguserID;
                 command.Parameters.Add("@logoutnotes", SqlDbType.VarChar).Value = notes;
                 command.Parameters.Add("@Logoutdate", SqlDbType.DateTime).Value = signOuttime;
                 command.ExecuteNonQuery();
                 con.Close();
                 success = true;
             }
             catch (Exception ex)
             {
             }
             return success;
         }

         public DataTable GetState()
         {
             DataSet ds = new DataSet();

             try
             {
                 SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                 SqlCommand cmd = new SqlCommand();
                 SqlDataAdapter da = new SqlDataAdapter("USP_GetAllStates", con);

                 da.SelectCommand.CommandType = CommandType.StoredProcedure;
                 da.Fill(ds);

                 DataTable dt = ds.Tables[0];

             }
             catch (Exception ex)
             {
             }

             return ds.Tables[0];

         }

         public DataTable GetLeaveStatus()
         {
             DataSet ds = new DataSet();

             try
             {
                 SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                 SqlCommand cmd = new SqlCommand();
                 SqlDataAdapter da = new SqlDataAdapter("USP_GetLeaveStatus", con);

                 da.SelectCommand.CommandType = CommandType.StoredProcedure;
                 da.Fill(ds);

                 DataTable dt = ds.Tables[0];

             }
             catch (Exception ex)
             {
             }

             return ds.Tables[0];

         }
         public DataSet GetTimeZoneInfoByLocName(string LocationName)
         {
             try
             {
                 SqlCommand cmd = new SqlCommand();
                 SqlDataAdapter daLocation = new SqlDataAdapter("USP_TimeZoneIDByLocation", connectionString);
                 daLocation.SelectCommand.CommandType = CommandType.StoredProcedure;
                 daLocation.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", LocationName));
                 DataSet dsLocation = new DataSet();
                 daLocation.Fill(dsLocation);
                 return dsLocation;
             }
             catch (Exception ex)
             {
                 throw ex;
             }


         }


         public bool UpdateSignInSignOutDelete(int loguserId)
         {
             bool success = false;
             try
             {

                 SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                 con.Open();
                 SqlCommand command = new SqlCommand("[USP_DeleteLoginLogout]", con);
                 command.CommandType = CommandType.StoredProcedure;
                 command.Parameters.Add("@Loguserid", SqlDbType.Int).Value = loguserId;
                 command.ExecuteNonQuery();
                 con.Close();
                 success = true;
             }
             catch (Exception ex)
             {
             }
             return success;
         }

         public DataSet SaveLeaveRequestDetails(int UserID,string EmpID,DateTime FromDt,DateTime ToDt,DateTime CurrentDt,string Reason,string Passcode)
         {
             DataSet dsLoginDet = new DataSet();
             try
             {
                 SqlDataAdapter daLogin = new SqlDataAdapter("[USP_AddleaveDetails]", connectionString);
                 daLogin.SelectCommand.Parameters.Add(new SqlParameter("@UserID", UserID));
                 daLogin.SelectCommand.Parameters.Add(new SqlParameter("@Fromdate", FromDt));
                 daLogin.SelectCommand.Parameters.Add(new SqlParameter("@Todate",ToDt));
                 daLogin.SelectCommand.Parameters.Add(new SqlParameter("@RequestedDt", CurrentDt));
                 daLogin.SelectCommand.Parameters.Add(new SqlParameter("@EmpID", EmpID));
                 daLogin.SelectCommand.Parameters.Add(new SqlParameter("@PassCode", Passcode));
                 daLogin.SelectCommand.Parameters.Add(new SqlParameter("@Reason", Reason));
                 daLogin.SelectCommand.CommandType = CommandType.StoredProcedure;
                 daLogin.Fill(dsLoginDet);
                 return dsLoginDet;

             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsLoginDet;


         }


         public DataSet GetShiftsByLocationName(string LocationName)
         {
             try
             {
                 SqlCommand cmd = new SqlCommand();
                 SqlDataAdapter daLocation = new SqlDataAdapter("USP_GetShiftsByLocationName", connectionString);
                 daLocation.SelectCommand.CommandType = CommandType.StoredProcedure;
                 daLocation.SelectCommand.Parameters.Add(new SqlParameter("@locationName", LocationName));
                 DataSet dsLocation = new DataSet();
                 daLocation.Fill(dsLocation);
                 return dsLocation;
             }
             catch (Exception ex)
             {
                 throw ex;
             }


         }


         public DataSet GetShiftsByLocationIdToHoliday(int LocationID)
         {
             try
             {
                 SqlCommand cmd = new SqlCommand();
                 SqlDataAdapter daLocation = new SqlDataAdapter("USP_getshiftsByLocationID", connectionString);
                 daLocation.SelectCommand.CommandType = CommandType.StoredProcedure;
                 daLocation.SelectCommand.Parameters.Add(new SqlParameter("@locationID", LocationID));
                 DataSet dsLocation = new DataSet();
                 daLocation.Fill(dsLocation);
                 return dsLocation;
             }
             catch (Exception ex)
             {
                 throw ex;
             }


         }

         public bool UpdateChangesByEffectiveDate(DateTime EffectDt)
         {
             bool success = false;
             try
             {
                 SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                 con.Open();
                 SqlCommand command = new SqlCommand("[USP_UpdateChangesByEffectiveDate]", con);
                 command.CommandType = CommandType.StoredProcedure;
                 command.Parameters.Add(new SqlParameter("@CurrentDate", EffectDt));
                 command.ExecuteNonQuery();
                 con.Close();
                 success = true;
             }
             catch (Exception ex)
             {
             }

             return success;
         }

    }
       

    }

