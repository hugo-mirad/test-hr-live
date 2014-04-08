using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;

namespace Attendance.BAL
{
    public class Report
    {
        public DataTable GetReport(DateTime StartDate,DateTime EndDate)
        {
            DataSet ds = new DataSet();
           
            try
            {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter("[USP_GetDetails]", con);
            da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", StartDate));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", EndDate));
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(ds);

            DataTable dt=ds.Tables[0];
              
            }
            catch(Exception ex)
            {
            }

            return ds.Tables[0];


        }
        public List<Attendance.Entities.MultipleLogininfo> GetMultipleDetailsByEmpID(DateTime StartDate, string empID)
        {
            List<Attendance.Entities.MultipleLogininfo> obj = new List<Attendance.Entities.MultipleLogininfo>();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand("[USP_GetMultipleLoginByempID]", con);
                con.Open();
                cmd.Parameters.Add(new SqlParameter("@TdyDt", StartDate));
                cmd.Parameters.Add(new SqlParameter("@empid", empID));
                cmd.CommandType=CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Attendance.Entities.MultipleLogininfo objInfo = new Attendance.Entities.MultipleLogininfo();
                    objInfo.LoginDate = dr["Logindate"].ToString() == "" ? "" : dr["Logindate"].ToString() == "NULL" ? "" : Convert.ToDateTime(dr["Logindate"].ToString()).ToString("MM/dd/yyyy")=="01/01/1900"?"":Convert.ToDateTime(dr["Logindate"].ToString()).ToString("hh:mm tt");
                    objInfo.LogoutDate = dr["Logoutdate"].ToString() == "" ? "" : dr["Logoutdate"].ToString() == "NULL" ? "" : Convert.ToDateTime(dr["Logoutdate"].ToString()).ToString("MM/dd/yyyy") == "01/01/1900" ? "" : Convert.ToDateTime(dr["Logoutdate"].ToString()).ToString("hh:mm tt"); ;
                    objInfo.Loguserid = Convert.ToInt32(dr["LogUserID"]);
                    objInfo.SchStart = dr["startTime"].ToString();
                    objInfo.SchEnd = dr["EndTime"].ToString();
                    obj.Add(objInfo);
                }
               con.Close();

               

            }
            catch (Exception ex)
            {
            }

            return obj;


        }

        public DataTable GetUsers(string Locationname,int sort)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetAllUsersData]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", Locationname));
                da.SelectCommand.Parameters.Add(new SqlParameter("@Bit", sort));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];


        }

        public DataTable GetAllDepartments()
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetAllDepartments]", con);
               // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", Locationname));
                // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", EndDate));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];


        }

        public DataTable GetLocations()
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetLocation]", con);
                // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", Locationname));
                // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", EndDate));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];


        }
        public bool AddUser(Attendance.Entities.UserInfo objInfo, Attendance.Entities.EmergencyContactInfo objContactInfo,int EnteredBy, string Locationname, string PhotoLink)
        {
            bool success=false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_INSERT]", con);
                command.CommandType = CommandType.StoredProcedure;

                //command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = objInfo.Firstname;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = objInfo.Lastname;
                command.Parameters.Add("@ProfessionalFirstName", SqlDbType.VarChar).Value = objInfo.BFirstname;
                command.Parameters.Add("@ProfessionalLastName", SqlDbType.VarChar).Value = objInfo.BLastname;

                command.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = objInfo.StartDt;
                command.Parameters.Add("@DeptName", SqlDbType.VarChar).Value = objInfo.Deptname;
                command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = objInfo.Designation;

                command.Parameters.Add("@Location", SqlDbType.VarChar).Value = Locationname;
                command.Parameters.Add("@EnteredBy", SqlDbType.Int).Value = EnteredBy;
                command.Parameters.Add("@scheduleID", SqlDbType.Int).Value = objInfo.ScheduleID;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = objInfo.IsActive;

                command.Parameters.Add("@photoLink", SqlDbType.VarChar).Value = PhotoLink;
                command.Parameters.Add("@EmpTypeID", SqlDbType.Int).Value = objInfo.EmpTypeID;
                //command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;
                command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = objInfo.Gender;

                command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = objInfo.Phone;
                command.Parameters.Add("@DateofBirth", SqlDbType.DateTime).Value = objInfo.DateOfBirth;
                command.Parameters.Add("@mobileNum", SqlDbType.VarChar).Value = objInfo.Mobile;

                command.Parameters.Add("@BusinessEmail", SqlDbType.VarChar).Value = objInfo.BusinessEmail;
                command.Parameters.Add("@PersonalEmail", SqlDbType.VarChar).Value = objInfo.PersonalEmail;
                command.Parameters.Add("@MaritalStatusID", SqlDbType.Int).Value = objInfo.MaritalID;

                command.Parameters.Add("@County", SqlDbType.VarChar).Value = objInfo.County;
                command.Parameters.Add("@WageID", SqlDbType.Int).Value = objInfo.WageID;
                command.Parameters.Add("@Deductions", SqlDbType.Int).Value = objInfo.Deductions;

                command.Parameters.Add("@Salary", SqlDbType.VarChar).Value = objInfo.Salary;
                command.Parameters.Add("@Person1", SqlDbType.VarChar).Value = objContactInfo.Person1;
                command.Parameters.Add("@P1Address1", SqlDbType.VarChar).Value = objContactInfo.P1Address1;

                command.Parameters.Add("@P1Address2", SqlDbType.VarChar).Value = objContactInfo.P1Address2;
                command.Parameters.Add("@phone1", SqlDbType.VarChar).Value = objContactInfo.Phone1;
                command.Parameters.Add("@relation1", SqlDbType.VarChar).Value = objContactInfo.Relation1;

                command.Parameters.Add("@person2", SqlDbType.VarChar).Value = objContactInfo.Person2;
                command.Parameters.Add("@p2Address1", SqlDbType.VarChar).Value = objContactInfo.P2Address1;
                command.Parameters.Add("@p2Address2", SqlDbType.VarChar).Value = objContactInfo.P2Address2;

                command.Parameters.Add("@phone2", SqlDbType.VarChar).Value = objContactInfo.Phone2;
                command.Parameters.Add("@relation2", SqlDbType.VarChar).Value = objContactInfo.Relation2;            
                command.Parameters.Add("@email2", SqlDbType.VarChar).Value = objContactInfo.Email2;
                command.Parameters.Add("@email1", SqlDbType.VarChar).Value = objContactInfo.Email1;

                command.Parameters.Add("@email3", SqlDbType.VarChar).Value = objContactInfo.Email3;          
                command.Parameters.Add("@StateID1", SqlDbType.Int).Value = objContactInfo.StateID1;
                command.Parameters.Add("@StateID2", SqlDbType.Int).Value = objContactInfo.StateID2;
                command.Parameters.Add("@StateID3", SqlDbType.Int).Value = objContactInfo.StateID3;

                command.Parameters.Add("@Zip1", SqlDbType.VarChar).Value = objContactInfo.Zip1;
                command.Parameters.Add("@Zip2", SqlDbType.VarChar).Value = objContactInfo.Zip2;
                command.Parameters.Add("@Zip3", SqlDbType.VarChar).Value = objContactInfo.Zip3;

                command.Parameters.Add("@person3", SqlDbType.VarChar).Value = objContactInfo.Person3;
                command.Parameters.Add("@p3Address1", SqlDbType.VarChar).Value = objContactInfo.P3Address1;
                command.Parameters.Add("@p3Address2", SqlDbType.VarChar).Value = objContactInfo.P3Address2;

                command.Parameters.Add("@phone3", SqlDbType.VarChar).Value = objContactInfo.Phone3;
                command.Parameters.Add("@relation3", SqlDbType.VarChar).Value = objContactInfo.Relation3;
                command.Parameters.Add("@StateID", SqlDbType.Int).Value = objInfo.StateID;

                command.Parameters.Add("@Address1", SqlDbType.VarChar).Value = objInfo.Address1;
                command.Parameters.Add("@Address2", SqlDbType.VarChar).Value = objInfo.Address2;
                command.Parameters.Add("@zip", SqlDbType.VarChar).Value = objInfo.Zip;

                command.Parameters.Add("@SSN", SqlDbType.VarChar).Value = objInfo.SSN;
                command.Parameters.Add("@drivingLicenceNum", SqlDbType.VarChar).Value = objInfo.DriverLicense;
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public bool UpdateUser(Attendance.Entities.UserInfo objInfo, int EmployeeID, int EnteredBy,string PhotoLink,string Ipaddress)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_UpdateUserDetails]", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = EmployeeID;
               // command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = objInfo.Firstname;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = objInfo.Lastname;
                command.Parameters.Add("@ProfessionalFirstName", SqlDbType.VarChar).Value = objInfo.BFirstname;
                command.Parameters.Add("@ProfessionalLastName", SqlDbType.VarChar).Value = objInfo.BLastname;
                command.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = objInfo.StartDt;
                command.Parameters.Add("@TermReason", SqlDbType.VarChar).Value = objInfo.TermReason;
                command.Parameters.Add("@TermDate", SqlDbType.DateTime).Value = objInfo.TermDt;
                command.Parameters.Add("@DeptName", SqlDbType.VarChar).Value = objInfo.Deptname;
                command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = objInfo.Designation;
                command.Parameters.Add("@ScheduleID", SqlDbType.Int).Value = objInfo.ScheduleID;
                command.Parameters.Add("@EmpTypeID", SqlDbType.Int).Value = objInfo.EmpTypeID;
                command.Parameters.Add("@EnteredBy", SqlDbType.Int).Value = EnteredBy;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = objInfo.IsActive;
                command.Parameters.Add("@photoLink", SqlDbType.VarChar).Value = PhotoLink;
                command.Parameters.Add("@Ipaddress", SqlDbType.VarChar).Value = Ipaddress;
                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public bool UpdateUserSalTaxDetails(Attendance.Entities.UserInfo objInfo, int EmployeeID, int EnteredBy, string Ipaddress)
        {
            bool success = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                con.Open();
                SqlCommand command = new SqlCommand("[USP_updateSalTaxDetails]", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = EmployeeID;
                // command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;
                command.Parameters.Add("@WageID", SqlDbType.Int).Value = objInfo.WageID;
                command.Parameters.Add("@salary", SqlDbType.VarChar).Value = objInfo.Salary;
                command.Parameters.Add("@Deductions", SqlDbType.Int).Value = objInfo.Deductions;
                command.Parameters.Add("@MaritalID", SqlDbType.Int).Value = objInfo.MaritalID;
                command.Parameters.Add("@EnteredBy", SqlDbType.Int).Value = EnteredBy;
                command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Ipaddress;



                command.ExecuteNonQuery();
                con.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public DataSet GetWeeklyReport(DateTime startdate, DateTime EndDate, int userid, string LocationName)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_weekly]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", startdate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@endDate", EndDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@userid", userid));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationName", LocationName));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
               
            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        public DataSet GetWeeklyReportAdmin(DateTime startdate, DateTime EndDate,string LocationName)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_weeklyAdmin]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", startdate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@endDate", EndDate));
                //da.SelectCommand.Parameters.Add(new SqlParameter("@userid", userid));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationName", LocationName));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        public DataSet GetActiveUsersAdmin(DateTime startdate, DateTime EndDate,string LocationName)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetActiveUsersToAdmin]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", startdate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@endDate", EndDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationName", LocationName));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        public DataSet GetActiveUsers(DateTime startdate, DateTime EndDate, int userid, string LocationName)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetActiveUsers]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", startdate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@endDate", EndDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@userid", userid));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationName", LocationName));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        public DataSet GetPayrollReport(DateTime startdate, DateTime EndDate, int userid, string LocationName)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetPayrollData]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", startdate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@endDate", EndDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@userid", userid));
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationName", LocationName));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

            }
            catch (Exception ex)
            {
            }
            return ds;
        }
            public DataSet GetPayrollEdithistory(DateTime startdate, DateTime EndDate)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetEditHistory]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", startdate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@endDate", EndDate));
               // da.SelectCommand.Parameters.Add(new SqlParameter("@userid", userid));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

            }
            catch (Exception ex)
            {
            }
            return ds;
        }
            public DataTable GetAllScheduleTypes()
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_GetAllSchduleTypes]", con);
               // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", Locationname));
                // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", EndDate));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
            }

            return ds.Tables[0];


        }
            public DataTable GetAllEmployeetypes()
            {
                DataSet ds = new DataSet();

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_MasterEmpType]", con);
                    // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", Locationname));
                    // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", EndDate));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);

                    DataTable dt = ds.Tables[0];

                }
                catch (Exception ex)
                {
                }

                return ds.Tables[0];


            }
            public DataTable GetAllWages()
            {
                DataSet ds = new DataSet();

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_GetAllWages]", con);
                    // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", Locationname));
                    // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", EndDate));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);

                    DataTable dt = ds.Tables[0];

                }
                catch (Exception ex)
                {
                }

                return ds.Tables[0];


            }
            public DataTable GetAllStates(int LocationID)
            {
                DataSet ds = new DataSet();

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("USP_GEtAllStatesByLocationID", con);
                    da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", LocationID));
                    // da.SelectCommand.Parameters.Add(new SqlParameter("@Locationname", EndDate));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);

                    DataTable dt = ds.Tables[0];

                }
                catch (Exception ex)
                {
                }

                return ds.Tables[0];


            }
            public bool CheckUniqueSSN(string SSN)
            {
                bool success = false;

                try
                {
                    DataTable dt = new DataTable();

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("USP_CheckUniqueSSN", con);
                    da.SelectCommand.Parameters.Add(new SqlParameter("@SSN", SSN));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        int cnt = Convert.ToInt32(dt.Rows[0]["cnt"]);
                        if (cnt == 0)
                        {
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return success;

            }
            public bool UpdatePersonalDetails(Attendance.Entities.UserInfo objInfo,int EnteredBy, int userID,string ipaddress)
            {
                bool success = false;
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    con.Open();
                    SqlCommand command = new SqlCommand("[USP_UpdatePersonalDetailsByUserID]", con);
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;

                    command.Parameters.Add("@EnterBY", SqlDbType.Int).Value = EnteredBy;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                    command.Parameters.Add("@Ipaddress", SqlDbType.VarChar).Value = ipaddress;
                    command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = objInfo.Gender;
                    command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = objInfo.Phone;
                    command.Parameters.Add("@DateofBirth", SqlDbType.DateTime).Value = objInfo.DateOfBirth;
                    command.Parameters.Add("@mobileNum", SqlDbType.VarChar).Value = objInfo.Mobile;
                    command.Parameters.Add("@businessEmail", SqlDbType.VarChar).Value = objInfo.BusinessEmail;
                    command.Parameters.Add("@personalEmail", SqlDbType.VarChar).Value = objInfo.PersonalEmail;

                    command.Parameters.Add("@County", SqlDbType.VarChar).Value = objInfo.County;
                    command.Parameters.Add("@StateID", SqlDbType.Int).Value = objInfo.StateID;
                    command.Parameters.Add("@Address1", SqlDbType.VarChar).Value = objInfo.Address1;
                    command.Parameters.Add("@Address2", SqlDbType.VarChar).Value = objInfo.Address2;
                    command.Parameters.Add("@zip", SqlDbType.VarChar).Value = objInfo.Zip;
                    command.Parameters.Add("@SSN", SqlDbType.VarChar).Value = objInfo.SSN;
                    command.Parameters.Add("@drivingLicenceNum", SqlDbType.VarChar).Value = objInfo.DriverLicense;


                    command.ExecuteNonQuery();
                    con.Close();
                    success = true;
                }
                catch (Exception ex)
                {
                }
                return success;
            }
            public bool UpdateEmergencyDetails(Attendance.Entities.EmergencyContactInfo objContactInfo, int EnteredBy,int userid, string ipaddress)
            {
                bool success = false;
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    con.Open();
                    SqlCommand command = new SqlCommand("[USP_UpdateEmergencyContactDetails]", con);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@EnterBy", SqlDbType.Int).Value = EnteredBy;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = userid;
                    command.Parameters.Add("@ipaddress", SqlDbType.VarChar).Value = ipaddress;
                
                    command.Parameters.Add("@Person1", SqlDbType.VarChar).Value = objContactInfo.Person1;
                    command.Parameters.Add("@P1Address1", SqlDbType.VarChar).Value = objContactInfo.P1Address1;
                    command.Parameters.Add("@P1Address2", SqlDbType.VarChar).Value = objContactInfo.P1Address2;
                    command.Parameters.Add("@phone1", SqlDbType.VarChar).Value = objContactInfo.Phone1;
                    command.Parameters.Add("@relation1", SqlDbType.VarChar).Value = objContactInfo.Relation1;


                    command.Parameters.Add("@person2", SqlDbType.VarChar).Value = objContactInfo.Person2;
                    command.Parameters.Add("@p2Address1", SqlDbType.VarChar).Value = objContactInfo.P2Address1;
                    command.Parameters.Add("@p2Address2", SqlDbType.VarChar).Value = objContactInfo.P2Address2;
                    command.Parameters.Add("@phone2", SqlDbType.VarChar).Value = objContactInfo.Phone2;
                    command.Parameters.Add("@relation2", SqlDbType.VarChar).Value = objContactInfo.Relation2;

                    command.Parameters.Add("@email2", SqlDbType.VarChar).Value = objContactInfo.Email2;
                    command.Parameters.Add("@email1", SqlDbType.VarChar).Value = objContactInfo.Email1;
                    command.Parameters.Add("@email3", SqlDbType.VarChar).Value = objContactInfo.Email3;

                    command.Parameters.Add("@StateID1", SqlDbType.Int).Value = objContactInfo.StateID1;
                    command.Parameters.Add("@StateID2", SqlDbType.Int).Value = objContactInfo.StateID2;
                    command.Parameters.Add("@StateID3", SqlDbType.Int).Value = objContactInfo.StateID3;

                    command.Parameters.Add("@Zip1", SqlDbType.VarChar).Value = objContactInfo.Zip1;
                    command.Parameters.Add("@Zip2", SqlDbType.VarChar).Value = objContactInfo.Zip2;
                    command.Parameters.Add("@Zip3", SqlDbType.VarChar).Value = objContactInfo.Zip3;


                    command.Parameters.Add("@person3", SqlDbType.VarChar).Value = objContactInfo.Person3;
                    command.Parameters.Add("@p3Address1", SqlDbType.VarChar).Value = objContactInfo.P3Address1;
                    command.Parameters.Add("@p3Address2", SqlDbType.VarChar).Value = objContactInfo.P3Address2;
                    command.Parameters.Add("@phone3", SqlDbType.VarChar).Value = objContactInfo.Phone3;
                    command.Parameters.Add("@relation3", SqlDbType.VarChar).Value = objContactInfo.Relation3;

                 
                    command.ExecuteNonQuery();
                    con.Close();
                    success = true;
                }
                catch (Exception ex)
                {
                }
                return success;
            }
            public bool UpdateFreeze(int userid, string Location,DateTime FreezeDate)
            {
                bool success = false;
                 try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    con.Open();
                    SqlCommand command = new SqlCommand("[UP_UpdateFreezeByLocation]", con);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Locationname", SqlDbType.VarChar).Value = Location;
                    command.Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = FreezeDate;
                    command.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
                    command.ExecuteNonQuery();
                    con.Close();
                    success = true;
                }
               
                catch (Exception ex)
                {
                }
                return success;
            }
            public DateTime GetFreezedDate(DateTime FreezedDate, string LocationName)
            {
                DataSet ds = new DataSet();
                DateTime Count = Convert.ToDateTime("01/01/1900"); 
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_GetFreezedDate]", con);
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Currentdate", FreezedDate));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@LocationName", LocationName));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Count =Convert.ToDateTime(ds.Tables[0].Rows[0]["FreezeDate"].ToString());
                        }
                    }


                }
                catch (Exception ex)
                {
                }
                return Count;
            }
            public bool SavePettyCashDetails(Attendance.Entities.PettyCashInfo objInfo,int EnteredBy, string Locationname)
            {
                bool success = false;
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    con.Open();
                    SqlCommand command = new SqlCommand("[USP_PettycashDetails]", con);
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;
                    command.Parameters.Add("@AccountHolderName", SqlDbType.VarChar).Value = objInfo.AccountName;
                    command.Parameters.Add("@IncomeType", SqlDbType.VarChar).Value = objInfo.AmountType;
                    command.Parameters.Add("@AcountDate", SqlDbType.DateTime).Value = objInfo.AccountDate;
                    command.Parameters.Add("@BillNo", SqlDbType.VarChar).Value = objInfo.BillNum;
                    command.Parameters.Add("@chequeNo", SqlDbType.VarChar).Value = objInfo.ChequeNum;
                    command.Parameters.Add("@locationname", SqlDbType.VarChar).Value = Locationname;
                    command.Parameters.Add("@EnteredBy", SqlDbType.Int).Value = EnteredBy;
                    command.Parameters.Add("@expenditureAmount", SqlDbType.VarChar).Value = objInfo.ExpenseAmount;
                    command.Parameters.Add("@Expdate", SqlDbType.DateTime).Value = objInfo.ExpenseDate;
                   
                    //command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = objInfo.EmpID;
                    command.Parameters.Add("@ServicemanName", SqlDbType.VarChar).Value = objInfo.ExpenseManName;
                    command.Parameters.Add("@expenditureSubtype", SqlDbType.VarChar).Value = objInfo.ExpenseSubType;
                    command.Parameters.Add("@expenditureTpe", SqlDbType.VarChar).Value = objInfo.Expensetype;
                    command.Parameters.Add("@FromWhom", SqlDbType.VarChar).Value = objInfo.FromWhom;
                    command.Parameters.Add("@Income", SqlDbType.VarChar).Value = objInfo.InitialAmoun;
                    command.Parameters.Add("@expenditureNotes", SqlDbType.VarChar).Value = objInfo.Notes;
                    command.Parameters.Add("@VoucherNo", SqlDbType.VarChar).Value = objInfo.VoucherNum;
                    
                    command.ExecuteNonQuery();
                    con.Close();
                    success = true;
                }
                catch (Exception ex)
                {
                }
                return success;
            }
            public DataSet GetPettyCashDetails(string LocationName)
            {
                DataSet ds = new DataSet();

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_GetPettycashDetails]", con);

                    da.SelectCommand.Parameters.Add(new SqlParameter("@locationname", LocationName));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);

                }
                catch (Exception ex)
                {
                }
                return ds;
            }
            public int SaveAttendanceHistory(Attendance.Entities.AttendenceInfo objInfo)
            {
                DataSet ds = new DataSet();
                int AtnLogID = 0;
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_SaveEmpAttendanceHistory]", con);

                    da.SelectCommand.Parameters.Add(new SqlParameter("@userid", objInfo.Userid));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Wkgdays", objInfo.WorkingDays));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Atndays", objInfo.AttendDays));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Leaves", objInfo.Leaves));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Noshow", objInfo.NoShow));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@PaidLeaves", objInfo.PaidLeaves));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@PaidLeavesUsed", objInfo.PaidLeavesUsed));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@PaidLeavesBalanced", objInfo.PaidLeavesBalanced));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@TotalCalLeaves", objInfo.TotalCalLeaves1));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@EnteredDate", objInfo.EnterDate));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@EnterBy", objInfo.EnterBy));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@month", objInfo.Mnth));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@year", objInfo.Yr));


                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            AtnLogID = Convert.ToInt32(ds.Tables[0].Rows[0]["AtnLogID"].ToString());
                        }
                    }


                }
                catch (Exception ex)
                {
                }
                return AtnLogID;
            }
            public bool SaveSalaryHistory(Attendance.Entities.SalaryInfo objInfo)
            {
                DataSet ds = new DataSet();
                bool success = false;
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_SaveEmpSalaryHistory]", con);

                    da.SelectCommand.Parameters.Add(new SqlParameter("@AtnLogID", objInfo.AtnLogID));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@CalSalary", objInfo.CalSalary));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Bonus", objInfo.Bonus));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Incentives", objInfo.Incentives));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@PrevUnpaid", objInfo.PrevUnpaid));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Advancepaid", objInfo.AdvancePaid));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@ExpensesRecieved", objInfo.Expenses));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@LoanDeduct", objInfo.LoanDeduct));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@TotalPay", objInfo.TotalPay));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@EnteredDate", objInfo.EnteredDate));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@EnterBy", objInfo.EnterBy));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Month", objInfo.Mnth));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Year", objInfo.Years));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@InternalNotes", objInfo.InternalNotes));


                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            success = true;
                        }
                    }


                }
                catch (Exception ex)
                {
                }
                return success;
            }
            public bool FinalizePayrollReport(int LocationID, DateTime startDate,int enterBy)
            {
                
                    bool success = false;
                    try
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                        con.Open();
                        SqlCommand command = new SqlCommand("[USP_UpdateFinalizePayrollData]", con);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@locationID", SqlDbType.Int).Value = LocationID;
                        command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                        command.Parameters.Add("@enterBy", SqlDbType.Int).Value = enterBy;

                        command.ExecuteNonQuery();
                        con.Close();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                    }
                    return success;
                }
            public bool UpdatePaidLeavesDetAfterFinalPayroll(Attendance.Entities.AttendenceInfo objInfo, DateTime CurrentDt,string ip)
            {

                bool success = false;
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    con.Open();
                    SqlCommand command = new SqlCommand("[USP_SavePaidLeaveDetAfterFinalPayroll]", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ip;
                    command.Parameters.Add("@LeaveAvail", SqlDbType.Int).Value = objInfo.PaidLeaves;
                    command.Parameters.Add("@CurrentDt", SqlDbType.DateTime).Value = CurrentDt;
                    command.Parameters.Add("@LeaveUsed", SqlDbType.Int).Value = objInfo.PaidLeavesUsed;
                    command.Parameters.Add("@LeaveBalanced", SqlDbType.Int).Value = objInfo.PaidLeavesBalanced;
                    command.Parameters.Add("@EnterBY", SqlDbType.Int).Value = objInfo.EnterBy ;
                    command.Parameters.Add("@PaidLeaveUserID", SqlDbType.Int).Value = objInfo.Userid;
                   // command.Parameters.Add("@EnteredBy", SqlDbType.Int).Value = objInfo.EnterBy;

                    command.Parameters.Add("@EnterDt", SqlDbType.DateTime).Value = objInfo.EnterDate;
                    command.ExecuteNonQuery();
                    con.Close();
                    success = true;
                }
                catch (Exception ex)
                {
                }
                return success;
            }
            public bool GetFinalPayrollDate(DateTime MonthStart, int LocationID)
            {
                bool Count = false;
                DataSet ds = new DataSet();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_GetFinalPayrollDate]", con);
                    da.SelectCommand.Parameters.Add(new SqlParameter("@startDate", MonthStart));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", LocationID));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Count = true;
                        }
                    }


                }
                catch (Exception ex)
                {
                }
                return Count;
            }
            public DataSet GetFinalPayrollDate(int locationID)
            {
                DataSet ds = new DataSet();

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_GetFinalpayrollDateToLvsmgmt]", con);

                    da.SelectCommand.Parameters.Add(new SqlParameter("@locationID", locationID));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);

                }
                catch (Exception ex)
                {
                }
                return ds;
            }
            public DataSet GetEmpScheduleDet(int locationID,int scheduleType,DateTime startDate,DateTime endDate)
            {
                DataSet ds = new DataSet();

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter("[USP_GetEmpScheduleDet]", con);
                    da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", locationID));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", startDate));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@EndDate", endDate));
                    da.SelectCommand.Parameters.Add(new SqlParameter("@ScheduleType", scheduleType));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);

                }
                catch (Exception ex)
                {
                }
                return ds;
            }
    }
}
