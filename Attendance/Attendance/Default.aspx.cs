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

                        //  lblTimeZoneName.Text = dsLocation.Tables[0].Rows[0]["TimeZoneName"].ToString();
                        Session["TimeZoneName"] = dsLocation.Tables[0].Rows[0]["TimeZoneName"].ToString();
                        lblTimeZoneName.Text = dsLocation.Tables[0].Rows[0]["TimeZoneName"].ToString();


                        lblLocation.Text = dsLocation.Tables[0].Rows[0]["LocationName"].ToString();

                        Session["LocationName"] = dsLocation.Tables[0].Rows[0]["LocationName"].ToString().Trim();


                        string LocationName = lblLocation.Text;
                        DataSet dsImages = new DataSet();
                        dsImages = business.BindData(LocationName, CurentDatetime);
                        Session["Employee"] = dsImages;
                        rpEmp.DataSource = dsImages;
                        rpEmp.DataBind();

                        DataSet dsImages1 = new DataSet();
                        dsImages1 = business.BindLogin(LocationName, CurentDatetime);
                        Session["LoginEmployee"] = dsImages1;
                        rpLogin.DataSource = dsImages1;
                        rpLogin.DataBind();

                        DataSet dsImages2 = new DataSet();
                        dsImages2 = business.BindLogout(LocationName, CurentDatetime);
                        rplogout.DataSource = dsImages2;
                        rplogout.DataBind();

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

            string id = ""; string LocationName = lblLocation.Text;
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

                        dsImages1 = business.BindLogin(LocationName, entities.LoginDate);
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
                        // userPass.Value = "";
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
                    // userPass.Value = "";
                    userPass.Focus();
                }


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

                            dsImages1 = business.BindLogin(LocationName, entities.LoginDate);
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
                            // userPass.Value = "";
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
                        // userPass.Value = "";
                        userPass.Focus();
                    }


                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "changeSuccess1(" + bnew + ");", true);


                }
                dr.Close();
                // Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
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

            if (cn.State != ConnectionState.Open)
                cn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;


            cmd = new SqlCommand("USP_ChkForLogoutByDate", cn);
            cmd.Parameters.AddWithValue("@CurrenDate", entities.LogOutDate);
            cmd.Parameters.AddWithValue("@Userid", entities.UserID);
            cmd.CommandType = CommandType.StoredProcedure;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //mdlLoginpopup.Hide();
                Duplicate.Text = "true";
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Duplicate();", true);
                lblLOError.Text = "You are already signed out.You cannot signed out again.";


            }
            else
            {
                entities.LogOutDate = ISTTime;
                entities.LogOutNotes = txtNpte2.Text;
                entities.passcode = userPass2.Value.ToString().Trim();

                DataSet dslogout = Session["LoginEmployee"] as DataSet;
                // DataSet dsEmp = Session["Employee"] as DataSet;
                DataTable dt = new DataTable();
                DataView dv = new DataView();
                dv = dslogout.Tables[0].DefaultView;
                dv.RowFilter = "userid=" + entities.UserID;
                dt = dv.ToTable();
                //int UserLogId = Convert.ToInt32(hdnLogoutUserID.Value);
                int UserLogId = Convert.ToInt32(dt.Rows[0]["LogUserID"].ToString().Trim());
                DataSet ds = business.SaveLogDetails1(entities, UserLogId);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        logintin = ds.Tables[0].Rows[0]["Logindate"].ToString();
                        //   logouttime = ds.Tables[0].Rows[0]["logoutdate"].ToString();
                        logintin1.Text = ds.Tables[0].Rows[0]["logoutdate"].ToString();
                        // ScLOStartTime.Text = ds.Tables[0].Rows[0]["StartTime"].ToString();
                        // ScLOEndTime.Text = ds.Tables[0].Rows[0]["EndTime"].ToString();
                        mdlLogoutPopup.Hide();

                        DataSet dsImages2 = new DataSet();
                        dsImages2 = business.BindLogout(LocationName, entities.LogOutDate);
                        rplogout.DataSource = dsImages2;
                        rplogout.DataBind();
                        bnew = 1;
                    }
                    else
                    {


                        lblLOName.Text = dt.Rows[0]["FirstName"].ToString().Trim() + " " + dt.Rows[0]["LastName"].ToString().Trim();
                        lblLOError.Text = "Invalid passcode";
                        mdlLogoutPopup.Show();
                        // userPass.Value = "";
                        userPass2.Focus();
                    }

                }

                else
                {

                    lblLOName.Text = dt.Rows[0]["FirstName"].ToString().Trim() + " " + dt.Rows[0]["LastName"].ToString().Trim();
                    lblLOError.Text = "Invalid passcode";
                    mdlLogoutPopup.Show();
                    // userPass.Value = "";
                    userPass2.Focus();
                }
            }


            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "changeSuccess2(" + bnew + ");", true);
            //Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
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
                    else
                    {
                        Response.Redirect("Reports.aspx");
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
    }
}
