using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Attendance.BAL;
using Attendance.Entities;
using System.Data.SqlClient;

namespace Attendance
{
    public partial class _Default : System.Web.UI.Page
    {
        //public string cn = ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString();
        //public string cn = ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString();
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
        Business business = new Business();
        int value = 0;

        Attendance.Entities.Entities entities = new Attendance.Entities.Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            comanyname.Text = CommonFiles.ComapnyName;
            hdnBversionM.Value = CommonFiles.MozillaVersion;
            hdnBversionC.Value = CommonFiles.ChromeVersion;
            HttpBrowserCapabilities objBrwInfo = Request.Browser;
            SqlDataReader dr;
            SqlCommand cmd = null;
            try
            {
              
                    if (!IsPostBack)
                    {
                        DataSet dsLocation = new DataSet();
                        String strHostName = Request.UserHostAddress.ToString();
                        string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                        dsLocation = business.GetLocationDetailsByIp(strIp);
                        if (dsLocation.Tables.Count > 0)
                        {
                            if (dsLocation.Tables[0].Rows.Count > 0)
                            {

                                int TimeZoneId = Convert.ToInt32(dsLocation.Tables[0].Rows[0]["TimeZoneId"].ToString());
                                Session["TimeZoneID"] = TimeZoneId;
                                string timezone = "";
                                if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
                                {
                                    timezone = "Eastern Standard Time";
                                }
                                else
                                {
                                    timezone = "India Standard Time";
                                }
                                DateTime ISTTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));

                                var CurentDatetime = ISTTime;

                                lblDate2.Text = CurentDatetime.ToString("dddd MMMM dd yyyy, hh:mm:ss tt ");
                                lblDate.Text = CurentDatetime.TimeOfDay.TotalSeconds.ToString();
                                Session["TimeZoneName"] = dsLocation.Tables[0].Rows[0]["TimeZoneName"].ToString();
                                lblTimeZoneName.Text = dsLocation.Tables[0].Rows[0]["TimeZoneName"].ToString();
                                lblLocation.Text = dsLocation.Tables[0].Rows[0]["LocationName"].ToString();
                                Session["LocationName"] = dsLocation.Tables[0].Rows[0]["LocationName"].ToString().Trim();

                                string LocationName = lblLocation.Text;
                                GetShifts(LocationName);
                                int shiftID = 0;
                                if (Session["ShiftID"] != null)
                                {
                                    if (Session["ShiftID"].ToString() != "")
                                    {
                                        shiftID = Convert.ToInt32(Session["ShiftID"].ToString());
                                    }
                                    else
                                    {
                                        shiftID = GetCurrentShiftBytime(CurentDatetime, LocationName);
                                    }
                                }
                                else
                                {
                                    shiftID = GetCurrentShiftBytime(CurentDatetime, LocationName);
                                }
                                Session["ShiftID"] = shiftID;
                                ddlShifts.SelectedIndex = ddlShifts.Items.IndexOf(ddlShifts.Items.FindByValue(shiftID.ToString()));                           
                                BindAttendanceData(LocationName, CurentDatetime);
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "stopLoading();", true);
                            }
                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "stopLoading();", true);
                        }
                    }
                }
           
            catch (Exception ex)
            {
            }

        }
        protected void rpLogin_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnFirstName = (HiddenField)e.Item.FindControl("hdnFirstName1");
                HiddenField hdnUserID = (HiddenField)e.Item.FindControl("hdnUserID1");
                HiddenField hdnliStartTime = (HiddenField)e.Item.FindControl("hdnStartTime2");
                HiddenField hdnliEndTime = (HiddenField)e.Item.FindControl("hdnEndTime2");
                HiddenField hdnLogin = (HiddenField)e.Item.FindControl("hdnLogin");
                Label lblBindLogin = (Label)e.Item.FindControl("hdnDeptName2");
                Label hdnDesgLogin = (Label)e.Item.FindControl("hdnDesignationLogin");
                Image imgAgent = (Image)e.Item.FindControl("imgPicture1");
                if (imgAgent.ImageUrl == "")
                {
                    imgAgent.AlternateText = hdnFirstName.Value.ToString();
                }
                else
                {
                    imgAgent.ImageUrl = @"Photos/" + imgAgent.ImageUrl;
                }
            }
        }
        protected void rplogout_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnFirstName = (HiddenField)e.Item.FindControl("hdnFirstName2");
                HiddenField hdnUserID = (HiddenField)e.Item.FindControl("hdnUserID2");
                HiddenField hdnLoginout = (HiddenField)e.Item.FindControl("hdnLoginout");
                HiddenField hdnLogout = (HiddenField)e.Item.FindControl("hdnLogout");
                HiddenField hdnloStartTime = (HiddenField)e.Item.FindControl("hdnStartTime3");
                HiddenField hdnloEndTime = (HiddenField)e.Item.FindControl("hdnEndTime3");
                Label lblBindLogout = (Label)e.Item.FindControl("hdnDeptName3");
                Label hdnDesgLogout = (Label)e.Item.FindControl("hdnDesignationLogout");
                Image imgAgent = (Image)e.Item.FindControl("imgPicture2");
                if (imgAgent.ImageUrl == "")
                {
                    imgAgent.AlternateText = hdnFirstName.Value.ToString();
                }
                else
                {
                    imgAgent.ImageUrl = @"Photos/" + imgAgent.ImageUrl;
                }
            }
        }
        public void subm_Click(object sender, EventArgs e)
        {

            string id = ""; 
            string LocationName = lblLocation.Text;
            int bnew = 0;
            entities.UserID = Convert.ToInt32(txtUserID.Text);
            string timezone = "";
            if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
            {
                timezone = "Eastern Standard Time";
            }
            else
            {
                timezone = "India Standard Time";

            }
            DateTime ISTTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));
            entities.LoginDate = ISTTime;


            if (ScResignIn.Text.ToString().Trim() == "True")
            {
                entities.LoginDate = ISTTime;
                entities.LoginNotes = txtNpte.Text;
                entities.LocationName = lblLocation.Text;
                entities.passcode = userPass.Value.ToString().Trim();

                DataSet ds = business.SaveLogDetails(entities);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = ds.Tables[0].Rows[0]["LogUserID"].ToString();

                        loginID.Text = ds.Tables[0].Rows[0]["LogUserID"].ToString();
                        logintin1.Text = ds.Tables[0].Rows[0]["Logindate"].ToString();
                        DataSet dsImages1 = new DataSet();

                        dsImages1 = business.BindLogin(LocationName, entities.LoginDate,Convert.ToInt32(ddlShifts.SelectedItem.Value));
                        rpLogin.DataSource = dsImages1;
                        rpLogin.DataBind();
                        mdlLoginpopup.Hide();
                        bnew = 1;
                    }
                    else
                    {
                        DataSet dsEmp = Session["Employee"] as DataSet;
                        DataTable dt = new DataTable();
                        DataView dv = new DataView();
                        dv = dsEmp.Tables[0].DefaultView;
                        dv.RowFilter = "userid=" + entities.UserID;
                        dt = dv.ToTable();

                        lblLIName.Text = dt.Rows[0]["FirstName"].ToString().Trim() + " " + dt.Rows[0]["LastName"].ToString().Trim();
                        lblLIError.Text = "Invalid passcode";
                        mdlLoginpopup.Show();
                        userPass.Focus();
                    }

                }
                else
                {
                    DataSet dsEmp = Session["Employee"] as DataSet;
                    DataTable dt = new DataTable();
                    DataView dv = new DataView();
                    dv = dsEmp.Tables[0].DefaultView;
                    dv.RowFilter = "userid=" + entities.UserID;
                    dt = dv.ToTable();

                    lblLIName.Text = dt.Rows[0]["FirstName"].ToString().Trim() + " " + dt.Rows[0]["LastName"].ToString().Trim();
                    lblLIError.Text = "Invalid passcode";
                    mdlLoginpopup.Show();
                    userPass.Focus();
                }
              //  BindAttendanceData(LocationName, entities.LoginDate);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "changeSuccess1(" + bnew + ");", true);
               
            }
            else
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;
                cmd = new SqlCommand("USP_ChkForLoginByDate", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurrenDate", entities.LoginDate);
                cmd.Parameters.AddWithValue("@Userid", entities.UserID);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Duplicate.Text = "true";
                    lblLIError.Text = "You are already Signed In.You cannot signed in again.";
                }
                else
                {
                    entities.LoginDate = ISTTime;
                    entities.LoginNotes = txtNpte.Text;
                    entities.LocationName = lblLocation.Text;
                    entities.passcode = userPass.Value.ToString().Trim();

                    DataSet ds = business.SaveLogDetails(entities);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            id = ds.Tables[0].Rows[0]["LogUserID"].ToString();

                            loginID.Text = ds.Tables[0].Rows[0]["LogUserID"].ToString();
                            logintin1.Text = ds.Tables[0].Rows[0]["Logindate"].ToString();
                            DataSet dsImages1 = new DataSet();

                            dsImages1 = business.BindLogin(LocationName, entities.LoginDate,Convert.ToInt32(ddlShifts.SelectedItem.Value));
                            rpLogin.DataSource = dsImages1;
                            rpLogin.DataBind();
                            mdlLoginpopup.Hide();
                            bnew = 1;
                        }
                        else
                        {
                            DataSet dsEmp = Session["Employee"] as DataSet;
                            DataTable dt = new DataTable();
                            DataView dv = new DataView();
                            dv = dsEmp.Tables[0].DefaultView;
                            dv.RowFilter = "userid=" + entities.UserID;
                            dt = dv.ToTable();

                            lblLIName.Text = dt.Rows[0]["FirstName"].ToString().Trim() + " " + dt.Rows[0]["LastName"].ToString().Trim();
                            lblLIError.Text = "Invalid passcode";
                            mdlLoginpopup.Show();
                            userPass.Focus();
                        }

                    }
                    else
                    {
                        DataSet dsEmp = Session["Employee"] as DataSet;
                        DataTable dt = new DataTable();
                        DataView dv = new DataView();
                        dv = dsEmp.Tables[0].DefaultView;
                        dv.RowFilter = "userid=" + entities.UserID;
                        dt = dv.ToTable();

                        lblLIName.Text = dt.Rows[0]["FirstName"].ToString().Trim() + " " + dt.Rows[0]["LastName"].ToString().Trim();
                        lblLIError.Text = "Invalid passcode";
                        mdlLoginpopup.Show();
                        userPass.Focus();
                    }
                   // BindAttendanceData(LocationName, entities.LoginDate);
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "changeSuccess1(" + bnew + ");", true);
                }
                dr.Close();
             }
        }
        public void logout_Click(object sender, EventArgs e)
        {

            logout.Enabled = false;
            string LocationName = lblLocation.Text;
            string logintin = ""; // logouttime = "", scdloStartTime = "", scdloEndTime = "";
            int bnew = 0;
            entities.UserID = Convert.ToInt32(txtUserID2.Text);
          
            string timezone = "";
            if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
            {
                timezone = "Eastern Standard Time";
            }
            else
            {
                timezone = "India Standard Time";

            }


            DateTime ISTTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));
            entities.LogOutDate = ISTTime;
            
            DataSet dslogout = Session["LoginEmployee"] as DataSet;
            DataTable dt = new DataTable();
            DataView dv = new DataView();
            dv = dslogout.Tables[0].DefaultView;
            dv.RowFilter = "userid=" + entities.UserID;
            dt = dv.ToTable();
            int UserLogId = Convert.ToInt32(dt.Rows[0]["LogUserID"].ToString().Trim());

            if (cn.State != ConnectionState.Open)
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            cmd = new SqlCommand("USP_ChkForLogoutByDate", cn);
            cmd.Parameters.AddWithValue("@CurrenDate", entities.LogOutDate);
            cmd.Parameters.AddWithValue("@LogUserid", UserLogId);
            cmd.CommandType = CommandType.StoredProcedure;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Duplicate.Text = "true";
                lblLOError.Text = "You are already signed out.You cannot signed out again.";
            }
            else
            {
                entities.LogOutDate = ISTTime;
                entities.LogOutNotes = txtNpte2.Text;
                entities.passcode = userPass2.Value.ToString().Trim();
                DataSet ds = business.SaveLogDetails1(entities, UserLogId);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        logintin = ds.Tables[0].Rows[0]["Logindate"].ToString();
                        logintin1.Text = ds.Tables[0].Rows[0]["logoutdate"].ToString();
                        mdlLogoutPopup.Hide();

                        DataSet dsImages2 = new DataSet();
                        dsImages2 = business.BindLogout(LocationName, entities.LogOutDate,Convert.ToInt32(ddlShifts.SelectedItem.Value));
                        rplogout.DataSource = dsImages2;
                        rplogout.DataBind();
                        bnew = 1;
                    }
                    else
                    {

                        lblLOName.Text = dt.Rows[0]["FirstName"].ToString().Trim() + " " + dt.Rows[0]["LastName"].ToString().Trim();
                        lblLOError.Text = "Invalid passcode";
                        mdlLogoutPopup.Show();
                        userPass2.Focus();
                    }

                }

                else
                {

                    lblLOName.Text = dt.Rows[0]["FirstName"].ToString().Trim() + " " + dt.Rows[0]["LastName"].ToString().Trim();
                    lblLOError.Text = "Invalid passcode";
                    mdlLogoutPopup.Show();
                    userPass2.Focus();
                }
            }
         //   BindAttendanceData(LocationName, entities.LoginDate);
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "changeSuccess2(" + bnew + ");", true);
           
        }
        protected void rpEmp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                HiddenField hdnFirstName = (HiddenField)e.Item.FindControl("hdnFirstName");
                HiddenField hdnUserID = (HiddenField)e.Item.FindControl("hdnUserID");
                HiddenField hdnscStartTime = (HiddenField)e.Item.FindControl("hdnStartTime1");
                HiddenField hdnscEndTime = (HiddenField)e.Item.FindControl("hdnEndTime1");
                Label lblBind = (Label)e.Item.FindControl("hdnDeptName1");
                Label hdnDesg = (Label)e.Item.FindControl("hdnDesignation");


                Image imgAgent = (Image)e.Item.FindControl("imgPicture");
                if (imgAgent.ImageUrl == "")
                {
                    imgAgent.AlternateText = hdnFirstName.Value.ToString();

                }
                else
                {
                    imgAgent.ImageUrl = @"Photos/" + imgAgent.ImageUrl;
                }
            }
        }
        protected void btnManager_Click(object sender, EventArgs e)
        {
            entities.EMPID = txtUserIdM.Text;
            entities.LocationName = txtLocationM.Text;
            entities.UserID = Convert.ToInt32(hdnManageUserID.Value.Trim());
            string timezone = "";
            if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
            {
                timezone = "Eastern Standard Time";
            }
            else
            {
                timezone = "India Standard Time";

            }
            DateTime ISTTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));

            Session["TodayBannerDate"] = ISTTime.ToString("MM/dd/yyyy");
            entities.passcode = txtPasswordM.Text;
            DataSet dsman = business.AuthinticateManager(entities);
            if (dsman.Tables.Count > 0)
            {
                if (dsman.Tables[0].Rows.Count > 0)
                {
                    Session["Location"] = entities.LocationName;
                    Session["UserID"] = dsman.Tables[0].Rows[0]["userid"].ToString();
                    Session["IsManage"] = dsman.Tables[0].Rows[0]["ismanage"].ToString();
                    Session["IsAdmin"] = dsman.Tables[0].Rows[0]["IsAdmin"].ToString();
                    Session["Photo"] = "~/Photos/" + dsman.Tables[0].Rows[0]["photolink"].ToString().Trim();
                    Session["EmpName"] = dsman.Tables[0].Rows[0]["firstname"].ToString().Trim() + " " + dsman.Tables[0].Rows[0]["lastname"].ToString().Trim();
                    Session["ScheduleInOut"] = dsman.Tables[0].Rows[0]["StartTime"].ToString().Trim() + "-" + dsman.Tables[0].Rows[0]["EndTime"].ToString().Trim();

                    if (Session["IsAdmin"].ToString() == "True")
                    {
                        Response.Redirect("AdminReports.aspx");

                    }
                    else if (Session["IsManage"].ToString() == "True")
                    {
                        Response.Redirect("Reports.aspx");
                    }
                    else
                    {
                        Response.Redirect("SingleReport.aspx");
                    }
                }
                else
                {
                    lblErrorM.Text = "Invalid emp id or password or location code";
                    txtUserIdM.Text = "";
                    txtUserIdM.Focus();
                }

            }
            else
            {

                lblErrorM.Text = "Invalid emp id or password or location code";
                txtUserIdM.Text = "";
                txtUserIdM.Focus();
            }




        }
        protected void lnkClose_Click(object sender, EventArgs e)
        {
            mdlLoginpopup.Hide();
        }
        protected void lnkLOClose_Click(object sender, EventArgs e)
        {
            mdlLogoutPopup.Hide();
        }
        protected void lnkMNClose_Click(object sender, EventArgs e)
        {
            mdlManagerPopup1.Hide();
        }
        protected void LeaveSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Business obj = new Business();
                string timezone = "";

                if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
                {
                    timezone = "Eastern Standard Time";
                }
                else
                {
                    timezone = "India Standard Time";

                }
                DateTime CurrentDt = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                int UserID = Convert.ToInt32(hdnLeaveUserID.Value);
                DateTime FromDt = Convert.ToDateTime(txtFromDt.Text);
                DateTime ToDt = Convert.ToDateTime(txtToDt.Text);
                string Passcode = txtLeavePassCode.Text.ToString();
                string Reason = txtReason.Text == "" ? "" : GeneralFunction.ToProperNotes(txtReason.Text);
                string EmpID = txtLeaveEmpID.Text;
                DataSet ds = obj.SaveLeaveRequestDetails(UserID, EmpID, FromDt, ToDt, CurrentDt, Reason, Passcode);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["LeaveID"].ToString() == "Applied")
                        {
                            lblLeaveError.Text = "You have already applied leave for these days";
                            lblLeaveError.Visible = true;
                            txtReason.Text = "";
                            txtFromDt.Text = "";
                            txtToDt.Text = "";
                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "showLeaveSuccess();", true);
                        }
                    }
                    else
                    {
                        lblLeaveError.Text = "Invalid employee id and passcode";
                        lblLeaveError.Visible = true;
                    }
                }
                else
                {
                    lblLeaveError.Text = "Invalid employee id and passcode";
                    lblLeaveError.Visible = true;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void GetShifts(string LocationName)
        {
         
            DataSet dsShifts = business.GetShiftsByLocationName(LocationName);
            ddlShifts.DataSource = dsShifts;
            ddlShifts.DataTextField = "shiftname";
            ddlShifts.DataValueField = "shiftID";
            ddlShifts.DataBind(); 
        }
        private int GetCurrentShiftBytime(DateTime CurentDatetime,string LocationName)
        {
            int shiftIDs=0;
            try
            {
                

                DataSet dsShifts = business.GetShiftsByLocationName(LocationName);

                for (int i = 0; i < dsShifts.Tables[0].Rows.Count; i++)
                {
                    DateTime stime = Convert.ToDateTime(dsShifts.Tables[0].Rows[i]["shiftStime"].ToString());
                    DateTime eTime = Convert.ToDateTime(dsShifts.Tables[0].Rows[i]["shiftendtime"].ToString());

                    if (stime <= CurentDatetime && eTime >= CurentDatetime)
                    {
                        shiftIDs = Convert.ToInt32(dsShifts.Tables[0].Rows[i]["shiftID"].ToString());
                      
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return shiftIDs;
        }
        protected void ddlShifts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string LocationName = Session["LocationName"].ToString();
                var timezone = "";
                if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
                {
                    timezone = "Eastern Standard Time";
                }
                else
                {
                    timezone = "India Standard Time";
                }
                DateTime ISTTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                var CurentDatetime = ISTTime;
                BindAttendanceData(LocationName, CurentDatetime);
               
            }
            catch (Exception ex)
            {
            }
        }
        private void BindAttendanceData(string LocationName, DateTime CurentDatetime)
        {
            try
            {
                int shiftID = Convert.ToInt32(ddlShifts.SelectedItem.Value);
                DataSet dsImages = new DataSet();
                dsImages = business.BindData(LocationName, CurentDatetime,shiftID);
                Session["Employee"] = dsImages;
                rpEmp.DataSource = dsImages;
                rpEmp.DataBind();

                rpLeave.DataSource = dsImages.Tables[1];
                rpLeave.DataBind();
             
                DataSet dsImages1 = new DataSet();
                dsImages1 = business.BindLogin(LocationName, CurentDatetime,shiftID);
                Session["LoginEmployee"] = dsImages1;
                rpLogin.DataSource = dsImages1;
                rpLogin.DataBind();

                DataSet dsImages2 = new DataSet();
                dsImages2 = business.BindLogout(LocationName, CurentDatetime,shiftID);
                rplogout.DataSource = dsImages2;
                rplogout.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
