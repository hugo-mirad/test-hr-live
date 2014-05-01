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

namespace Attendance
{
    public partial class HolidayManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] != null && Session["UserID"]!=null)
            {

                if (!IsPostBack)
                {
                    comanyname.Text = CommonFiles.ComapnyName;
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
                    lblTimeZoneName.Text = Session["TimeZoneName"].ToString().Trim();
                    lblHeadSchedule.Text = Session["ScheduleInOut"].ToString();
                    lblEmployyName.Text = Session["EmpName"].ToString().Trim();
                    Photo.Src = Session["Photo"].ToString().Trim();
                    lblLocation.Text = Session["LocationName"].ToString();
                    lblShiftName.Text = "-" + Session["ShiftName"].ToString();
                    
                    DateTime TodayDate = Convert.ToDateTime(Session["TodayBannerDate"]);
                    Getdepartments();
                    GetYear();
                    getLocations();
                    ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(lblLocation.Text.Trim()));

                    DateTime CurrentDate = Convert.ToDateTime(ISTTime.ToString("MM/dd/yyyy"));
                    DateTime MonthStart = CurrentDate.AddDays(1 - CurrentDate.Day);
                    DateTime MonthEnd = MonthStart.AddMonths(1).AddSeconds(-1);
                    int locationID = Convert.ToInt32(ddlLocation.SelectedItem.Value);
                    Session["HCurrentDay"] = TodayDate;

                    //if (lblLocation.Text.Trim() == "USMP" || lblLocation.Text.Trim() == "USWB")
                    //{
                    //    lnkLeavemangement.Enabled = false;
                    //    lnkLeavemangement.Style["Color"] = "Gray";
                    //}
                    //else
                    //{
                    //    lnkLeavemangement.Enabled = true;
                    //}
                    if (Session["IsAdmin"].ToString() == "True")
                    {
                        ddlLocation.Enabled = true;
                    }
                    else
                    {
                        ddlLocation.Enabled = false;
                    }

                    Session["MonthHolStart"] = MonthStart;
                    Session["MonthHolEnd"] = MonthEnd;
                    Session["CurntHolStart"] = MonthStart;
                    Session["CurntHolEnd"] = MonthEnd;
                    GetCalender(MonthStart, MonthEnd,locationID);
                  
                }
            }
        }
        private void GetCalender(DateTime MonthStart, DateTime MonthEnd,int locationID)
        {
            EmployeeBL obj = new EmployeeBL();
            DataTable dtAttandence = new DataTable();
            try
            {
                DataTable dt = obj.GetHolidayDetByLoc(MonthStart, MonthEnd, locationID);
                Session["HolidayMgmt"] = dt;
                dtAttandence.Columns.Add("Sunday", typeof(string));
                dtAttandence.Columns.Add("Monday", typeof(string));
                dtAttandence.Columns.Add("Tuesday", typeof(string));
                dtAttandence.Columns.Add("Wednesday", typeof(string));
                dtAttandence.Columns.Add("Thursday", typeof(string));
                dtAttandence.Columns.Add("Friday", typeof(string));
                dtAttandence.Columns.Add("Saturday", typeof(string));
              
                DateTime current = Convert.ToDateTime("01/01/1900");
                bool first = true;
                DayOfWeek GetDay = Convert.ToDateTime(MonthStart).DayOfWeek;
                int days = 1;

                for (int j = 0; j < 7; j++)
                {
                    dtAttandence.Rows.Add();
                    if (first)
                    {
                        first = false;
                        switch (GetDay)
                        {
                            case DayOfWeek.Sunday:
                                current = MonthStart;
                                dtAttandence.Rows[j]["Sunday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Monday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Tuesday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Wednesday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Thursday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Friday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Saturday"] = current;
                                days++;
                                break;
                            case DayOfWeek.Monday:
                                current = MonthStart;
                                dtAttandence.Rows[j]["Monday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Tuesday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Wednesday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Thursday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Friday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Saturday"] = current;
                                days++;
                                break;
                            case DayOfWeek.Tuesday:
                                current = MonthStart;
                                dtAttandence.Rows[j]["Tuesday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Wednesday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Thursday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Friday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Saturday"] = current;
                                days++;
                                break; // TODO: might not be correct. Was : Exit Select
                            case DayOfWeek.Wednesday:
                                current = MonthStart;
                                dtAttandence.Rows[j]["Wednesday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Thursday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Friday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Saturday"] = current;
                                days++;
                                break;
                            case DayOfWeek.Thursday:
                                current = MonthStart;
                                dtAttandence.Rows[j]["Thursday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Friday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Saturday"] = current;
                                days++;
                                break;
                            case DayOfWeek.Friday:
                                current = MonthStart;
                                dtAttandence.Rows[j]["Friday"] = current;
                                days++;
                                current = current.AddDays(1);
                                dtAttandence.Rows[j]["Saturday"] = current;
                                days++;
                                break;
                            case DayOfWeek.Saturday:
                                current = MonthStart;
                                dtAttandence.Rows[j]["Saturday"] = current;
                                days++;
                                break;
                        }

                    }
                    else
                    {
                        if (days <= Convert.ToInt32(MonthEnd.ToString("dd")))
                        {
                            current = current.AddDays(1);
                            dtAttandence.Rows[j]["Sunday"] = current;
                                days++;
                        }
                        if (days <= Convert.ToInt32(MonthEnd.ToString("dd")))
                        {
                            current = current.AddDays(1);
                            dtAttandence.Rows[j]["Monday"] = current;
                                days++;
                        }
                        if (days <= Convert.ToInt32(MonthEnd.ToString("dd")))
                        {
                            current = current.AddDays(1);
                            dtAttandence.Rows[j]["Tuesday"] = current;
                                days++;
                        }
                        if (days <= Convert.ToInt32(MonthEnd.ToString("dd")))
                        {
                            current = current.AddDays(1);
                            dtAttandence.Rows[j]["Wednesday"] = current;
                                days++;
                        }
                        if (days <= Convert.ToInt32(MonthEnd.ToString("dd")))
                        {
                            current = current.AddDays(1);
                            dtAttandence.Rows[j]["Thursday"] = current;
                                days++;
                        }
                        if (days <= Convert.ToInt32(MonthEnd.ToString("dd")))
                        {
                            current = current.AddDays(1);
                            dtAttandence.Rows[j]["Friday"] = current;
                                days++;
                        }
                        if (days <= Convert.ToInt32(MonthEnd.ToString("dd")))
                        {
                            current = current.AddDays(1);
                            dtAttandence.Rows[j]["Saturday"] = current;
                                days++;
                        }

                        if (days >= Convert.ToInt32(MonthEnd.ToString("dd")))
                        {
                            j = 6;
                        }
                    }

                }
                lblMonth.Text = MonthStart.ToString("MMMM") + " " + MonthStart.ToString("yyyy");
                grdholiday.DataSource = dtAttandence;
                grdholiday.DataBind();
            }
            catch (Exception ex)
            {
            }
          
 
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
        protected void lnkChangepwd_Click(object sender, EventArgs e)
        {
            try
            {
                txtOldpwd.Text = "";
                txtNewPwd.Text = "";
                lblPwdName.Text = Session["EmpName"].ToString().Trim();
                txtConfirmPwd.Text = "";
                mdlChangePwd.Show();
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCancelPwd_Click(object sender, EventArgs e)
        {
            mdlChangePwd.Hide();
        }
        protected void btnUpdatePwd_Click(object sender, EventArgs e)
        {
            try
            {
                string location = Session["LocationName"].ToString();
                string EmpName = Session["EmpName"].ToString().Trim();
                int userid = Convert.ToInt32(Session["UserID"]);
                string oldPwd = txtOldpwd.Text.Trim();
                string NewPwd = txtNewPwd.Text.Trim();

                Attendance.BAL.EmployeeBL obj = new EmployeeBL();
                bool bnew = obj.UpdatePasswordByUserID(userid, oldPwd, NewPwd);
                if (bnew)
                {
                    txtOldpwd.Text = "";
                    txtNewPwd.Text = "";
                    txtConfirmPwd.Text = "";
                    mdlChangePwd.Hide();
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Password changed successfully..');", true);
                }
                else
                {
                    txtOldpwd.Text = "";
                    txtNewPwd.Text = "";
                    txtConfirmPwd.Text = "";
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Invalid userid and password..');", true);
                    txtOldpwd.Focus();
                }

            }
            catch (Exception ex)
            {
            }
        }
        protected void btnUpdatePassCode_Click(object sender, EventArgs e)
        {
            try
            {
                string location = Session["LocationName"].ToString();
                string EmpName = Session["EmpName"].ToString().Trim();
                int userid = Convert.ToInt32(Session["UserID"]);
                string oldPasscode = txtOldpasscode.Text.Trim();
                string NewPasscode = txtNewPasscode.Text.Trim();

                Attendance.BAL.EmployeeBL obj = new EmployeeBL();
                bool bnew = obj.UpdatePasscodeByUserID(userid, oldPasscode, NewPasscode);
                if (bnew)
                {
                    txtOldpasscode.Text = "";
                    txtNewPasscode.Text = "";
                    txtConfirmPasscode.Text = "";
                    mdlChangePasscode.Hide();
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Passcode changed successfully..');", true);
                }
                else
                {
                    txtOldpasscode.Text = "";
                    txtNewPasscode.Text = "";
                    txtConfirmPasscode.Text = "";
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Invalid userid and old passcode..');", true);
                    txtOldpasscode.Focus();
                }

            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCancelPasscode_Click(object sender, EventArgs e)
        {
            txtOldpasscode.Text = "";
            txtNewPasscode.Text = "";
            txtConfirmPasscode.Text = "";
            mdlChangePasscode.Hide();
        }
        protected void lnkChangePasscode_Click(object sender, EventArgs e)
        {
            try
            {

                txtOldpasscode.Text = "";
                txtNewPasscode.Text = "";
                lblPasscodeName.Text = Session["EmpName"].ToString().Trim();
                txtConfirmPasscode.Text = "";
                mdlChangePasscode.Show();
            }
            catch (Exception ex)
            {
            }
        }
        protected void lnkUserMangement_Click(object sender, EventArgs e)
        {
            if (Session["IsAdmin"].ToString() == "True")
            {
                Response.Redirect("AdminUserManagement.aspx");
            }
            else
            {
                Response.Redirect("UserManagement.aspx");
            }
        }
        protected void lnkReport_Click(object sender, EventArgs e)
        {
            if (Session["IsAdmin"].ToString() == "True")
            {
                Response.Redirect("AdminReports.aspx");
            }
            else
            {
                Response.Redirect("Reports.aspx");
            }
        }
        protected void ddlPopDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (rdSelected.Checked)
                {
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Getdepartments()
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataTable dt = obj.GetAllDepartments();
                ddlPopDept.DataSource = dt;
                ddlPopDept.DataTextField = "Deptname";
                ddlPopDept.DataValueField = "DeptID";
                ddlPopDept.DataBind();
                ddlPopDept.Items.Insert(0, new ListItem("Select", "Select"));
                ddlPopDept.Items.Insert(1, new ListItem("ALL", "0"));

                ddlDefaultDept.DataSource = dt;
                ddlDefaultDept.DataTextField = "Deptname";
                ddlDefaultDept.DataValueField = "DeptID";
                ddlDefaultDept.DataBind();
                ddlDefaultDept.Items.Insert(0, new ListItem("Select", "Select"));
                ddlDefaultDept.Items.Insert(1, new ListItem("ALL", "0"));
              
            }
            catch (Exception ex)
            {
            }
        }
        private void getLocations()
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataTable dt = obj.GetLocations();
                ddlLocation.DataSource = dt;
                ddlLocation.DataTextField = "LocationName";
                ddlLocation.DataValueField = "LocationId";
                ddlLocation.DataBind();
             //   ddlLocation.Items.Insert(0, new ListItem("Select", "0"));

                ddlPopLoc.DataSource = dt;
                ddlPopLoc.DataTextField = "LocationName";
                ddlPopLoc.DataValueField = "LocationId";
                ddlPopLoc.DataBind();
                ddlPopLoc.Items.Insert(0, new ListItem("Select", "Select"));
                ddlPopLoc.Items.Insert(1, new ListItem("ALL", "0"));

                ddlDefaultlocation.DataSource = dt;
                ddlDefaultlocation.DataTextField = "LocationName";
                ddlDefaultlocation.DataValueField = "LocationId";
                ddlDefaultlocation.DataBind();
                ddlDefaultlocation.Items.Insert(0, new ListItem("Select", "Select"));
                ddlDefaultlocation.Items.Insert(1, new ListItem("ALL", "0"));

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime HolidayDt = Convert.ToDateTime(hdnHolidayDt.Value);
                string Holidayname =GeneralFunction.ToProper(txtHolidayName.Text.Trim());
                bool IsHoliday = true;
                if (rdWorkingday.Checked)
                {
                    IsHoliday = false;
                }
                int DeptID = 0;
                if (ddlPopDept.SelectedItem.Value != "Select")
                {
                    DeptID = Convert.ToInt32(ddlPopDept.SelectedItem.Value);
                }
                int LocationID = 0;
                if (ddlPopLoc.SelectedItem.Value != "Select")
                {
                    LocationID = Convert.ToInt32(ddlPopLoc.SelectedItem.Value);
                }
                int EnterBy = Convert.ToInt32(Session["UserID"]);

                string timezone = "";
                if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
                {
                    timezone = "Eastern Standard Time";
                }
                else
                {
                    timezone = "India Standard Time";

                }
                DateTime Enterdate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                String strHostName = Request.UserHostAddress.ToString();
                string IpAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();

                EmployeeBL obj = new EmployeeBL();
                if (rdAll.Checked)
                {
                    int ForAll = 0;
                    bool bnew = obj.SaveandGetHolidayDet(IsHoliday, HolidayDt, LocationID, DeptID, ForAll, EnterBy, Enterdate, IpAddress, Holidayname,false);
                    
                }
                else if (rdSelected.Checked)
                {
                    string Records = hdnChkRecords.Value.ToString().Trim();
                    if (Records != "")
                    {
                        string[] selectedRec = Records.Split(',');
                        for (int i = 0; i < selectedRec.Length - 1; i++)
                        {
                            int SelectID = Convert.ToInt32(selectedRec[i].Trim());
                            bool bnew = obj.SaveandGetHolidayDet(IsHoliday, HolidayDt, LocationID, DeptID, SelectID, EnterBy, Enterdate, IpAddress, Holidayname,false);
                        }
                    }
                }
                GetCalender(Convert.ToDateTime(Session["MonthHolStart"]),Convert.ToDateTime(Session["MonthHolEnd"]),Convert.ToInt32(ddlLocation.SelectedItem.Value));
                mdlHoliday.Hide();
            }
            catch (Exception ex)
            {
            }
        }
        protected void rdSelected_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdSelected.Checked)
                {
                    EmployeeBL obj = new EmployeeBL();
                    string Location = ddlPopLoc.SelectedItem.Value;
                    string Dept = ddlPopDept.SelectedItem.Text;
                    DataTable dt = obj.GetSelectedEmpByLocDept(Location, Dept);
                    if (dt.Rows.Count > 0)
                    {
                        grdSelectEmp.DataSource = dt;
                        grdSelectEmp.DataBind();
                        dvSelectError.Visible = false;
                        lblSelectError.Text = "";
                    }
                    else
                    {
                        dvSelectError.Visible = true;
                        lblSelectError.Text = "No Employee(s) found";
                        grdSelectEmp.DataSource = null;
                        grdSelectEmp.DataBind();
                    }
                    mdlEmpSelect.Show();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }

        }
        protected void grdholiday_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DataTable dt = Session["HolidayMgmt"] as DataTable;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataView dv = dt.DefaultView;
                    LinkButton lblSun = (LinkButton)e.Row.FindControl("lblSun");
                    if (lblSun.Text.Trim() != "")
                    {
                        if (Convert.ToDateTime(Session["HCurrentDay"]).ToString("ddd") == "Sun" && Convert.ToDateTime(Session["HCurrentDay"]).ToString("MM/dd/yyyy") == Convert.ToDateTime(lblSun.Text.Trim()).ToString("MM/dd/yyyy"))
                        {
                            lblSun.CssClass = "currentDate";
                        }

                        dv.RowFilter = "HolidayDate='" +Convert.ToDateTime(lblSun.Text).ToString("MM/dd/yyyy") + "'";
                        DataTable dtDay = dv.ToTable();
                        dv.RowFilter = null;
                        if (dtDay.Rows.Count > 0 )  
                        {
                            if (dtDay.Rows[0]["IsDefault"].ToString().Trim() == "True")
                            {
                                lblSun.CssClass += " DefaultHoliday";
                            }
                            else
                            {
                                string cs = lblSun.CssClass == "" ? "holiday tooltip2" : "holiday tooltip2";
                                string HName = dtDay.Rows[0]["Holidayname"].ToString().Trim();
                                lblSun.Attributes.Add("class", cs);
                                lblSun.Attributes.Add("title", HName);
                                lblSun.Attributes.Add("Hname", HName);
                            }
                            lblSun.Attributes.Add("isHoliday", "true");
                        }
                       
                        lblSun.Attributes.Add("currentdate", Convert.ToDateTime(lblSun.Text).ToString("MM/dd/yyyy"));
                        lblSun.Text = Convert.ToDateTime(lblSun.Text).ToString("dd");
                    }
                    LinkButton lblMon = (LinkButton)e.Row.FindControl("lblMon");
                    if (lblMon.Text.Trim() != "")
                    {

                        if (Convert.ToDateTime(Session["HCurrentDay"]).ToString("ddd") == "Mon" && Convert.ToDateTime(Session["HCurrentDay"]).ToString("MM/dd/yyyy") == Convert.ToDateTime(lblMon.Text.Trim()).ToString("MM/dd/yyyy"))
                        {
                            lblMon.CssClass = "currentDate";
                        }
                        dv.RowFilter = "HolidayDate='" + Convert.ToDateTime(lblMon.Text).ToString("MM/dd/yyyy") + "'";
                        DataTable dtDay = dv.ToTable();
                        dv.RowFilter = null;
                        if (dtDay.Rows.Count > 0)
                        {
                            if (dtDay.Rows[0]["IsDefault"].ToString().Trim() == "True")
                            {
                                lblMon.CssClass += " DefaultHoliday";
                            }
                            else
                            {
                                string cs = lblMon.CssClass == "" ? "holiday tooltip2" : "holiday tooltip2";
                                string HName = dtDay.Rows[0]["Holidayname"].ToString().Trim();
                                lblMon.Attributes.Add("class", cs);
                                lblMon.Attributes.Add("title", HName);
                                lblMon.Attributes.Add("Hname", HName);
                            }
                            lblMon.Attributes.Add("isHoliday", "true");
                        }
                        lblMon.Attributes.Add("currentdate", Convert.ToDateTime(lblMon.Text).ToString("MM/dd/yyyy"));
                        lblMon.Text = Convert.ToDateTime(lblMon.Text).ToString("dd");
                    }

                    LinkButton lblTue = (LinkButton)e.Row.FindControl("lblTue");
                    if (lblTue.Text.Trim() != "")
                    {
                        if (Convert.ToDateTime(Session["HCurrentDay"]).ToString("ddd") == "Tue" && Convert.ToDateTime(Session["HCurrentDay"]).ToString("MM/dd/yyyy") == Convert.ToDateTime(lblTue.Text.Trim()).ToString("MM/dd/yyyy"))
                        {
                            lblTue.CssClass = "currentDate";
                        }

                        dv.RowFilter = "HolidayDate='" + Convert.ToDateTime(lblTue.Text).ToString("MM/dd/yyyy") + "'";
                        DataTable dtDay = dv.ToTable();
                        dv.RowFilter = null;
                        if (dtDay.Rows.Count > 0)
                        {
                            if (dtDay.Rows[0]["IsDefault"].ToString().Trim() == "True")
                            {
                                lblTue.CssClass += " DefaultHoliday";
                            }
                            else
                            {

                                string HName = dtDay.Rows[0]["Holidayname"].ToString().Trim();
                                lblTue.Attributes.Add("class", "holiday tooltip2");
                                lblTue.Attributes.Add("title", HName);
                                lblTue.Attributes.Add("Hname", HName);
                            }
                            lblTue.Attributes.Add("isHoliday", "true");
                        }
                        lblTue.Attributes.Add("currentdate", Convert.ToDateTime(lblTue.Text).ToString("MM/dd/yyyy"));
                        lblTue.Text = Convert.ToDateTime(lblTue.Text).ToString("dd");
                    }

                    LinkButton lblWed = (LinkButton)e.Row.FindControl("lblWed");
                    if (lblWed.Text.Trim() != "")
                    {
                        if (Convert.ToDateTime(Session["HCurrentDay"]).ToString("ddd") == "Wed" && Convert.ToDateTime(Session["HCurrentDay"]).ToString("MM/dd/yyyy") == Convert.ToDateTime(lblWed.Text.Trim()).ToString("MM/dd/yyyy"))
                        {
                            lblWed.CssClass = "currentDate";
                        }


                        dv.RowFilter = "HolidayDate='" + Convert.ToDateTime(lblWed.Text).ToString("MM/dd/yyyy") + "'";
                        DataTable dtDay = dv.ToTable();
                        dv.RowFilter = null;
                        if (dtDay.Rows.Count > 0)
                        {
                            if (dtDay.Rows[0]["IsDefault"].ToString().Trim() == "True")
                            {
                                lblWed.CssClass += " DefaultHoliday";
                            }
                            else
                            {

                                string HName = dtDay.Rows[0]["Holidayname"].ToString().Trim();
                                lblWed.Attributes.Add("class", "holiday tooltip2");
                                lblWed.Attributes.Add("title", HName);
                                lblWed.Attributes.Add("Hname", HName);
                            }
                            lblWed.Attributes.Add("isHoliday", "true");
                        }
                        lblWed.Attributes.Add("currentdate", Convert.ToDateTime(lblWed.Text).ToString("MM/dd/yyyy"));
                        lblWed.Text = Convert.ToDateTime(lblWed.Text).ToString("dd");
                    }

                    LinkButton lblThu = (LinkButton)e.Row.FindControl("lblThu");
                    if (lblThu.Text.Trim() != "")
                    {
                        if (Convert.ToDateTime(Session["HCurrentDay"]).ToString("ddd") == "Thu" && Convert.ToDateTime(Session["HCurrentDay"]).ToString("MM/dd/yyyy") == Convert.ToDateTime(lblThu.Text.Trim()).ToString("MM/dd/yyyy"))
                        {
                            lblThu.CssClass = "currentDate";
                        }

                        dv.RowFilter = "HolidayDate='" + Convert.ToDateTime(lblThu.Text).ToString("MM/dd/yyyy") + "'";
                        DataTable dtDay = dv.ToTable();
                        dv.RowFilter = null;
                        if (dtDay.Rows.Count > 0)
                        {
                            if (dtDay.Rows[0]["IsDefault"].ToString().Trim() == "True")
                            {
                                lblThu.CssClass += " DefaultHoliday";
                            }
                            else
                            {
                                string HName = dtDay.Rows[0]["Holidayname"].ToString().Trim();
                                lblThu.Attributes.Add("class", "holiday tooltip2");
                                lblThu.Attributes.Add("title", HName);
                                lblThu.Attributes.Add("Hname", HName);
                            }
                            lblThu.Attributes.Add("isHoliday", "true");
                        }
                        lblThu.Attributes.Add("currentdate", Convert.ToDateTime(lblThu.Text).ToString("MM/dd/yyyy"));
                        lblThu.Text = Convert.ToDateTime(lblThu.Text).ToString("dd");
                    }

                    LinkButton lblFri = (LinkButton)e.Row.FindControl("lblFri");
                    if (lblFri.Text.Trim() != "")
                    {
                        if (Convert.ToDateTime(Session["HCurrentDay"]).ToString("ddd") == "Fri" && Convert.ToDateTime(Session["HCurrentDay"]).ToString("MM/dd/yyyy") == Convert.ToDateTime(lblFri.Text.Trim()).ToString("MM/dd/yyyy"))
                        {
                            lblFri.CssClass = "currentDate";
                        }


                        dv.RowFilter = "HolidayDate='" + Convert.ToDateTime(lblFri.Text).ToString("MM/dd/yyyy") + "'";
                        DataTable dtDay = dv.ToTable();
                        dv.RowFilter = null;
                        if (dtDay.Rows.Count > 0)
                        {
                            if (dtDay.Rows[0]["IsDefault"].ToString().Trim() == "True")
                            {
                                lblFri.CssClass += " DefaultHoliday";
                            }
                            else
                            {
                                string HName = dtDay.Rows[0]["Holidayname"].ToString().Trim();
                                lblFri.Attributes.Add("class", "holiday tooltip2");
                                lblFri.Attributes.Add("title", HName);
                                lblFri.Attributes.Add("Hname", HName);
                            }
                            lblFri.Attributes.Add("isHoliday", "true");
                        }
                        lblFri.Attributes.Add("currentdate", Convert.ToDateTime(lblFri.Text).ToString("MM/dd/yyyy"));
                        lblFri.Text = Convert.ToDateTime(lblFri.Text).ToString("dd");
                    }

                    LinkButton lblSat = (LinkButton)e.Row.FindControl("lblSat");
                    if (lblSat.Text.Trim() != "")
                    {
                        if (Convert.ToDateTime(Session["HCurrentDay"]).ToString("ddd") == "Sat" && Convert.ToDateTime(Session["HCurrentDay"]).ToString("MM/dd/yyyy") == Convert.ToDateTime(lblSat.Text.Trim()).ToString("MM/dd/yyyy"))
                        {
                            lblSat.CssClass = "currentDate";
                        }

                        dv.RowFilter = "HolidayDate='" + Convert.ToDateTime(lblSat.Text).ToString("MM/dd/yyyy") + "'";
                        DataTable dtDay = dv.ToTable();
                        dv.RowFilter = null;
                        if (dtDay.Rows.Count > 0)
                        {
                            if (dtDay.Rows[0]["IsDefault"].ToString().Trim() == "True")
                            {
                                lblSat.CssClass += " DefaultHoliday";
                            }
                            else
                            {
                                string HName = dtDay.Rows[0]["Holidayname"].ToString().Trim();
                                lblSat.Attributes.Add("class", "holiday tooltip2");
                                lblSat.Attributes.Add("title", HName);
                                lblSat.Attributes.Add("Hname", HName);
                            }
                            lblSat.Attributes.Add("isHoliday", "true");
                        }
                        lblSat.Attributes.Add("currentdate", Convert.ToDateTime(lblSat.Text).ToString("MM/dd/yyyy"));
                        lblSat.Text = Convert.ToDateTime(lblSat.Text).ToString("dd");
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime MonthStart = Convert.ToDateTime(Session["MonthHolStart"]);
                DateTime PrevMnthStart = MonthStart.AddMonths(-1);
                DateTime PrevMnthEnd = PrevMnthStart.AddMonths(1).AddSeconds(-1);
                Session["MonthHolStart"] = PrevMnthStart;
                Session["MonthHolEnd"] = PrevMnthEnd;
                int location = Convert.ToInt32(ddlLocation.SelectedItem.Value);
                //if (PrevMnthStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(Session["CurntHolStart"]).ToString("MM/dd/yyyy"))
                //{
                //    btnNext.CssClass = "btn btn-danger btn-small disabled";
                //    btnNext.Enabled = false;
                //}
                //else
                //{
                //    btnNext.CssClass = "btn btn-danger btn-small enabled";
                //    btnNext.Enabled = true;
                //}


                GetCalender(PrevMnthStart, PrevMnthEnd, location);


            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCurrent_Click(object sender, EventArgs e)
        {
            try
            {
             //   DateTime MonthStart = Convert.ToDateTime(Session["MonthHolStart"]);
                DateTime CrtMnthStart = Convert.ToDateTime(Session["CurntHolStart"]);
                DateTime CrtMnthEnd = Convert.ToDateTime( Session["CurntHolEnd"]);
                Session["MonthHolStart"] = CrtMnthStart;
                Session["MonthHolEnd"] = CrtMnthEnd;
                int location = Convert.ToInt32(ddlLocation.SelectedItem.Value);
                //if (CrtMnthStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(Session["CurntHolStart"]).ToString("MM/dd/yyyy"))
                //{
                //    btnNext.CssClass = "btn btn-danger btn-small disabled";
                //    btnNext.Enabled = false;
                //}
                //else
                //{
                //    btnNext.CssClass = "btn btn-danger btn-small enabled";
                //    btnNext.Enabled = true;
                //}
                GetCalender(CrtMnthStart, CrtMnthEnd, location);
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime MonthStart = Convert.ToDateTime(Session["MonthHolStart"]);
                DateTime NxtMnthStart = MonthStart.AddMonths(1);
                DateTime NxtMnthEnd = NxtMnthStart.AddMonths(1).AddSeconds(-1);
                Session["MonthHolStart"] = NxtMnthStart;
                Session["MonthHolEnd"] = NxtMnthEnd;
                int location = Convert.ToInt32(ddlLocation.SelectedItem.Value);
                //if (NxtMnthStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(Session["CurntHolStart"]).ToString("MM/dd/yyyy"))
                //{
                //    btnNext.CssClass = "btn btn-danger btn-small disabled";
                //    btnNext.Enabled = false;
                //}
                //else
                //{
                //    btnNext.CssClass = "btn btn-danger btn-small enabled";
                //    btnNext.Enabled = true;
                //}


                GetCalender(NxtMnthStart, NxtMnthEnd, location);

            }
            catch (Exception ex)
            {
            }
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int location = Convert.ToInt32(ddlLocation.SelectedItem.Value);
                DateTime Start=Convert.ToDateTime(Session["MonthHolStart"]);
                DateTime EndDt =Convert.ToDateTime(Session["MonthHolEnd"]) ;
                GetCalender(Start, EndDt, location);
                   
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddlPopLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rdAll.Checked = false;
                rdSelected.Checked = false;
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddlPopDept_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                rdAll.Checked = false;
                rdSelected.Checked = false;
            }
            catch (Exception ex)
            {
            }
        }
        private void GetYear()
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                ddlFromYear.DataSource = obj.GetYear();
                ddlFromYear.DataTextField = "year";
                ddlFromYear.DataValueField = "year";
                ddlFromYear.DataBind();
                ddlFromYear.Items.Insert(0, new ListItem("Select", "Select"));
                ddlFromYear.SelectedIndex = ddlFromYear.Items.IndexOf(ddlFromYear.Items.FindByText(System.DateTime.Now.ToString("yyyy")));


                ddlToYear.DataSource = obj.GetYear();
                ddlToYear.DataTextField = "year";
                ddlToYear.DataValueField = "year";
                ddlToYear.DataBind();
                ddlToYear.Items.Insert(0, new ListItem("Select", "Select"));
                ddlToYear.SelectedIndex = ddlToYear.Items.IndexOf(ddlToYear.Items.FindByText(System.DateTime.Now.ToString("yyyy")));
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string DefaultDay = ddlDay.SelectedItem.Text;
                int fromYr=Convert.ToInt32(ddlFromYear.SelectedItem.Text.ToString().Trim());
                int toYr=Convert.ToInt32(ddlToYear.SelectedItem.Text.ToString().Trim());
                int LocationID = Convert.ToInt32(ddlDefaultlocation.SelectedItem.Value);
                int DeptID = Convert.ToInt32(ddlDefaultDept.SelectedItem.Value);

                DateTime startDate = new DateTime(fromYr, 1, 1);
                DateTime endDate = new DateTime(toYr, 12, 31);

                int EnterBy = Convert.ToInt32(Session["UserID"]);

                string timezone = "";
                if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
                {
                    timezone = "Eastern Standard Time";
                }
                else
                {
                    timezone = "India Standard Time";

                }
                DateTime Enterdate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                String strHostName = Request.UserHostAddress.ToString();
                string IpAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();


                TimeSpan diff = endDate - startDate;
                int days = diff.Days;
                EmployeeBL obj = new EmployeeBL();
                for (var i = 0; i <= days; i++)
                {
                    var testDate = startDate.AddDays(i);
                    if (testDate.DayOfWeek.ToString()==DefaultDay)
                    {
                        bool bnew = obj.SaveandGetHolidayDet(true, testDate, LocationID, DeptID, 0, EnterBy, Enterdate, IpAddress, "Default",true);
                    
                    }
                }
                mdlDefaultmgmt.Hide();
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Default holidays updated successfully..');", true);
                GetCalender(Convert.ToDateTime(Session["MonthHolStart"]), Convert.ToDateTime(Session["MonthHolEnd"]), Convert.ToInt32(ddlLocation.SelectedItem.Value));
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnDefaultMgmt_Click(object sender, EventArgs e)
        {
            try
            {
                ddlDay.SelectedIndex = 0;
                ddlDefaultlocation.SelectedIndex = 0;
                ddlDefaultDept.SelectedIndex = 0;
                ddlFromYear.SelectedIndex = ddlFromYear.Items.IndexOf(ddlFromYear.Items.FindByText(System.DateTime.Now.ToString("yyyy")));
                ddlToYear.SelectedIndex = ddlToYear.Items.IndexOf(ddlToYear.Items.FindByText(System.DateTime.Now.ToString("yyyy")));
                mdlDefaultmgmt.Show();
            }
            catch (Exception ex)
            {
            }   
        }
     
    }
}
