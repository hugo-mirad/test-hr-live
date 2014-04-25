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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Attendance
{
    public partial class SingleReport : System.Web.UI.Page
    {
        public GeneralFunction objFun = new GeneralFunction();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] != null)
            {
                comanyname.Text = CommonFiles.ComapnyName;
                if (!IsPostBack)
                {

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
                    hdnTodaydt.Value = CurentDatetime.ToString("MM/dd/yyyy");
                    lblTimeZoneName.Text = Session["TimeZoneName"].ToString().Trim();
                    lblHeadSchedule.Text = Session["ScheduleInOut"].ToString();
                    GetMasterShifts(Session["LocationName"].ToString());
                    ddlShifts.SelectedIndex = ddlShifts.Items.IndexOf(ddlShifts.Items.FindByValue(Session["ShiftID"].ToString()));

                    lblHeadSchedule.Text = Session["ScheduleInOut"].ToString();
                    lblLocation.Text = Session["LocationName"].ToString();
                    ViewState["Location"] = Session["LocationName"].ToString();
                    lblEmployyName.Text = Session["EmpName"].ToString().Trim();
                    Photo.Src = Session["Photo"].ToString().Trim();

                    Session["TodayDate"] = Session["TodayBannerDate"];
                   
                    DateTime TodayDate = Convert.ToDateTime(Session["TodayDate"]);
                    ViewState["MonthSatrt"] = TodayDate.AddDays(1 - TodayDate.Day);
                    ViewState["CurrentMonth"] = TodayDate.AddDays(1 - TodayDate.Day);
                    Session["TodayDate1"] = Convert.ToDateTime(Session["TodayBannerDate"]);
                    if (GeneralFunction.GetFirstDayOfWeekDate(TodayDate).ToString("MM/dd/yyyy") == GeneralFunction.GetFirstDayOfWeekDate(DateTime.Now).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }


                    DateTime StartDate = GeneralFunction.GetFirstDayOfWeekDate(TodayDate);
                    DateTime EndDate = GeneralFunction.GetLastDayOfWeekDate(TodayDate);

                    ViewState["CurrentStart"] = StartDate;
                    ViewState["CurrentEnd"] = EndDate;
                    int userid = Convert.ToInt32(Session["UserID"]);
                    string Ismanage = Session["IsManage"].ToString();
                    DataTable ds = new DataTable();
                    ddlReportType.SelectedIndex = 0;

                    ds = GetReportSingle(StartDate, EndDate, userid);
                    Session["AtnDetails"] = ds;
                    if (ds.Rows.Count > 0)
                    {
                        grdAttendanceSingle.DataSource = ds;
                        grdAttendanceSingle.DataBind();
                        dvSingle.Style["display"] = "block";
                        dvMonthrep.Style["display"] = "none";

                    }
                   
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

       
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", " $('#spinner').show();", true);
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
            txtOldpwd.Text = "";
            txtNewPwd.Text = "";
            txtConfirmPwd.Text = "";
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
                    //lblAlertName.Text="Password ";
                    //mdlSuccessfullAlert.Show();
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
        protected void lnkLeaveReq_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                DateTime startDate = Convert.ToDateTime(Session["TodayDate"]);
                DateTime endDate = startDate.AddDays(7);

                DataTable ds = obj.GetLeaveRequestDetByUserID(Convert.ToInt32(Session["UserID"]), startDate, endDate);
                grdSingleLeaveReq.DataSource = ds;
                grdSingleLeaveReq.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        protected void lnkNewLeaveReq_Click(object sender, EventArgs e)
        {
            try
            {
                txtLeaveFrom.Text = "";
                txtLeaveTo.Text = "";
                txtReason.Text = "";
                mdlNewLeaveRequest.Show();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnLeaveReqSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Business obj = new Business();
                int userid = Convert.ToInt32(Session["UserID"]);
                DateTime FromDate = Convert.ToDateTime(txtLeaveFrom.Text);
                DateTime ToDate = Convert.ToDateTime(txtLeaveTo.Text);
                string Reason = txtReason.Text == "" ? "" : GeneralFunction.ToProperNotes(txtReason.Text);

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
                DataSet ds = obj.SaveLeaveRequestDetails(userid, "", FromDate, ToDate, ISTTime, Reason, "");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["LeaveID"].ToString() == "Applied")
                        {
                            lblLeaveError.Text = "You have already applied leave for these days";
                            lblLeaveError.Visible = true;
                            txtReason.Text = "";
                            txtLeaveFrom.Text = "";
                            txtLeaveTo.Text = "";
                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Leave request updated successfully');", true);
                            mdlNewLeaveRequest.Hide();

                            DateTime startDate = Convert.ToDateTime(Session["TodayDate"]);
                            DateTime endDate = startDate.AddDays(7);

                            EmployeeBL objLeave = new EmployeeBL();
                            DataTable datas = objLeave.GetLeaveRequestDetByUserID(Convert.ToInt32(Session["UserID"]), startDate, endDate);
                            grdSingleLeaveReq.DataSource = datas;
                            grdSingleLeaveReq.DataBind();
                            objLeave = null;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void GetMasterShifts(string LocationName)
        {
            Business business = new Business();
            DataSet dsShifts = business.GetShiftsByLocationName(LocationName);
            ddlShifts.DataSource = dsShifts;
            ddlShifts.DataTextField = "shiftname";
            ddlShifts.DataValueField = "shiftID";
            ddlShifts.DataBind();
        }

        public DataTable GetReportSingle(DateTime StartDate, DateTime EndDate, int userid)
        {
            DataSet ds = new DataSet();
            DataTable dtAttandence = new DataTable();
            try
            {
                lblWeekReportheading.Text = "Weekly attendance report";
                lblWeekReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndDate.ToString("MM/dd/yyyy") + " )";
                dtAttandence.Columns.Add("EmpID", typeof(string));
                dtAttandence.Columns.Add("Empname", typeof(string));
                dtAttandence.Columns.Add("Day", typeof(string));
                dtAttandence.Columns.Add("SchIn", typeof(string));
                dtAttandence.Columns.Add("SchOut", typeof(string));
                dtAttandence.Columns.Add("SignIn", typeof(string));
                dtAttandence.Columns.Add("SignOut", typeof(string));
                dtAttandence.Columns.Add("Hrs", typeof(string));
                dtAttandence.Columns.Add("LogUserID", typeof(int));
                dtAttandence.Columns.Add("LoginNotes", typeof(string));
                dtAttandence.Columns.Add("LogoutNotes", typeof(string));
                dtAttandence.Columns.Add("LoginFlag", typeof(string));
                dtAttandence.Columns.Add("LogoutFlag", typeof(string));
                dtAttandence.Columns.Add("Multiple", typeof(string));
                dtAttandence.Columns.Add("LvStatus", typeof(string));
                dtAttandence.Rows.Add();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_Fn]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@userid", userid));
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", StartDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@EndDate", EndDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", Convert.ToInt32(ddlShifts.SelectedValue)));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables.Count > 1)
                    {
                        DataTable dt = ds.Tables[1];
                        DataView dv = dt.DefaultView;
                        DataTable dtname = new DataTable();
                        DateTime NextDate = GeneralFunction.GetNextDayOfWeekDate(StartDate);

                        DataTable dtL = ds.Tables[2];
                        DataView dvL = dtL.DefaultView;
                        DataTable dtLeave = new DataTable();

                        DataTable dtH = ds.Tables[3];
                        DataView dvH = dtH.DefaultView;
                        DataTable dtHoliday = new DataTable();

                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {

                            dv.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                            dtname = dv.ToTable();

                            dvL.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                            dtLeave = dvL.ToTable();

                            dvH.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                            dtHoliday = dvH.ToTable();

                            dtAttandence.Rows[j]["empid"] = ds.Tables[0].Rows[j]["empid"].ToString();
                            dtAttandence.Rows[j]["Empname"] = ds.Tables[0].Rows[j]["firstName"].ToString() + " " + ds.Tables[0].Rows[j]["lastname"].ToString();

                            DateTime startDate = StartDate;
                            DateTime nextdate = NextDate;

                            for (int i = 0; i < 7; i++)
                            {
                                dtAttandence.Rows[i]["Day"] = "<b>" + startDate.DayOfWeek.ToString() + "</b>" + " (" + startDate.ToString("MM/dd/yyyy") + ") ";
                                DataView dL = dtLeave.DefaultView;
                                dL.RowFilter = "Fromdate<=#" + startDate + "# and #" + startDate + "#<=Todate";
                                DataTable dtLvResult = dL.ToTable();


                                DataView dH = dtHoliday.DefaultView;
                                dH.RowFilter = "HolidayDate >= #" + startDate + "# and HolidayDate<#" + nextdate + "#";
                                DataTable dtHolResult = dH.ToTable();

                                if (dtLvResult.Rows.Count > 0)
                                {
                                    dtAttandence.Rows[i]["SignIn"] = "L";
                                    dtAttandence.Rows[i]["SignOut"] = "L";
                                    dtAttandence.Rows[i]["Hrs"] = "";
                                    dtAttandence.Rows[i]["LvStatus"] = dtLvResult.Rows[0]["ApprovedStatus"].ToString();
                                }
                                if (dtHolResult.Rows.Count > 0)
                                {
                                    if (startDate.DayOfWeek.ToString() == "Sunday")
                                    {
                                        dtAttandence.Rows[i]["SignIn"] = "S";
                                        dtAttandence.Rows[i]["SignOut"] = "S";
                                        dtAttandence.Rows[i]["Hrs"] = "";
                                    }
                                    else
                                    {
                                        dtAttandence.Rows[i]["SignIn"] = "H";
                                        dtAttandence.Rows[i]["SignOut"] = "H";
                                        dtAttandence.Rows[i]["Hrs"] = "";
                                    }
                                }

                                if (dtname.Rows.Count > 0)
                                {
                                    DataView dv1 = dtname.DefaultView;
                                    dv1.RowFilter = "Logindate >= #" + startDate + "# and Logindate<#" + nextdate + "#";
                                    DataTable dt1 = dv1.ToTable();

                                    if (dtLvResult.Rows.Count > 0)
                                    {
                                        dtAttandence.Rows[i]["SignIn"] = "L";
                                        dtAttandence.Rows[i]["SignOut"] = "L";
                                        dtAttandence.Rows[i]["Hrs"] = "";
                                        dtAttandence.Rows[i]["LvStatus"] = dtLvResult.Rows[0]["ApprovedStatus"].ToString();
                                    }

                                    if (dtHolResult.Rows.Count > 0)
                                    {
                                        if (startDate.DayOfWeek.ToString() == "Sunday")
                                        {
                                            dtAttandence.Rows[i]["SignIn"] = "S";
                                            dtAttandence.Rows[i]["SignOut"] = "S";
                                            dtAttandence.Rows[i]["Hrs"] = "";
                                        }
                                        else
                                        {
                                            dtAttandence.Rows[i]["SignIn"] = "H";
                                            dtAttandence.Rows[i]["SignOut"] = "H";
                                            dtAttandence.Rows[i]["Hrs"] = "";
                                        }
                                    }

                                    if (dt1.Rows.Count > 0)
                                    {
                                        dtAttandence.Rows[i]["SchIn"] = dt1.Rows[0]["startTime"].ToString();
                                        dtAttandence.Rows[i]["SchOut"] = dt1.Rows[0]["EndTime"].ToString();

                                        for (int k = 0; k < dt1.Rows.Count; k++)
                                        {
                                            dtAttandence.Rows[i]["SignIn"] = dt1.Rows[0]["Logindate"].ToString().Trim();
                                            dtAttandence.Rows[i]["SignOut"] = dt1.Rows[dt1.Rows.Count - 1]["Logoutdate"].ToString().Trim() == "" ? "N/A" : dt1.Rows[dt1.Rows.Count - 1]["Logoutdate"].ToString().Trim();

                                            dtAttandence.Rows[i]["LoginNotes"] = dtAttandence.Rows[i]["LoginNotes"] + "</br>" + (dt1.Rows[k]["loginnotes"].ToString() + "</br>" + dt1.Rows[k]["logoutnotes"].ToString());

                                            if (dt1.Rows.Count > 1)
                                            {
                                                dtAttandence.Rows[i]["Multiple"] = "True";
                                            }
                                            if (dt1.Rows[k]["total hours worked"].ToString() == "")
                                            {
                                                dtAttandence.Rows[i]["Hrs"] = "N/A";
                                            }
                                            else
                                            {
                                                if (dtAttandence.Rows[i]["Hrs"].ToString() == "")
                                                {
                                                    dtAttandence.Rows[i]["Hrs"] = (Convert.ToDouble(dt1.Rows[k]["total hours worked"].ToString().Trim())).ToString();
                                                }
                                                else if (dtAttandence.Rows[i]["Hrs"].ToString() == "N/A")
                                                {
                                                    dtAttandence.Rows[i]["Hrs"] = "N/A";
                                                }
                                                else
                                                {
                                                    dtAttandence.Rows[i]["Hrs"] = ((Convert.ToDouble(dtAttandence.Rows[i]["Hrs"])) + (Convert.ToDouble(dt1.Rows[k]["total hours worked"].ToString().Trim()))).ToString();
                                                }
                                            }
                                        }
                                        //dtAttandence.Rows[i]["Hrs"] = dt1.Rows[0]["total hours worked"].ToString().Trim() == "" ? "N/A" : dt1.Rows[0]["total hours worked"].ToString().Trim();
                                        dtAttandence.Rows[i]["LogUserID"] = Convert.ToInt32(dt1.Rows[0]["LogUserID"]);
                                        dtAttandence.Rows[i]["LoginFlag"] = dt1.Rows[0]["loginflag"].ToString();
                                        dtAttandence.Rows[i]["LogoutFlag"] = dt1.Rows[0]["logoutflag"].ToString();
                                    }
                                    dv1.RowFilter = null;
                                }

                              

                               
                                dvH.RowFilter = null;
                                dvL.RowFilter = null;
                                startDate = nextdate;
                                nextdate = GeneralFunction.GetNextDayOfWeekDate(nextdate);
                                dtAttandence.Rows.Add();
                            }
                        }

                        double SumHours = 0;
                        for (int i = 0; i < dtAttandence.Rows.Count; i++)
                        {
                            if (dtAttandence.Rows[i]["Hrs"].ToString() == "" || dtAttandence.Rows[i]["Hrs"].ToString() == "N/A")
                            {
                                SumHours = SumHours + 0;
                            }
                            else
                            {
                                SumHours = SumHours + Convert.ToDouble(dtAttandence.Rows[i]["Hrs"]);
                            }
                        }

                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["Day"] = "<b>Total Hours</b>";
                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["SchIn"] = "";
                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["SchOut"] = "";
                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["Day"] = "<b>Total Hours</b>";
                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["Hrs"] = SumHours == 0 ? "" : "<b>" + GeneralFunction.CalDoubleToTime(SumHours) + "</b>";
                        dtAttandence.Rows.Add();
                    }

                    lblID.Text = dtAttandence.Rows[0]["EmpID"].ToString();
                    lblName.Text = dtAttandence.Rows[0]["Empname"].ToString();
                }

            }

            catch (Exception ex)
            {
            }

            return dtAttandence;
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                EmployeeBL obj = new EmployeeBL();

                if (ddlReportType.SelectedValue == "0")
                {

                    DateTime TodayDate = Convert.ToDateTime(Session["TodayDate"]);
                    DateTime PrevWeek = TodayDate.AddDays(-7);
                    Session["TodayDate"] = PrevWeek.ToString();
                    if (GeneralFunction.GetFirstDayOfWeekDate(PrevWeek).ToString("MM/dd/yyyy") == GeneralFunction.GetFirstDayOfWeekDate(DateTime.Now).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }

                    DateTime PreWeekStart = GeneralFunction.GetFirstDayOfWeekDate(PrevWeek);
                    DateTime PreWeekEnd = GeneralFunction.GetLastDayOfWeekDate(PrevWeek);
                    DataTable ds = new DataTable();
                    ds = GetReportSingle(PreWeekStart, PreWeekEnd, userid);
                    DataTable ds1 = obj.GetLeaveRequestDetByUserID(userid, PreWeekStart, PreWeekEnd);

                    Session["AtnDetails"] = ds;
                    if (ds.Rows.Count > 0)
                    {
                        grdAttendanceSingle.DataSource = ds;
                        grdAttendanceSingle.DataBind();
                        dvSingle.Style["display"] = "block";
                        dvMonthrep.Style["display"] = "none";

                        grdSingleLeaveReq.DataSource = ds1;
                        grdSingleLeaveReq.DataBind();

                    }
                }
                else
                {
                    DateTime monthStart = Convert.ToDateTime(ViewState["MonthSatrt"]).AddMonths(-1);
                    DateTime monthEnd = monthStart.AddMonths(1).AddSeconds(-1);
                    ViewState["MonthSatrt"] = monthStart;

                    GetCalender(monthStart, monthEnd, 1);
                    DataTable ds1 = obj.GetLeaveRequestDetByUserID(userid, monthStart, monthEnd);
                    grdSingleLeaveReq.DataSource = ds1;
                    grdSingleLeaveReq.DataBind();
                    dvSingle.Style["display"] = "none";
                    dvMonthrep.Style["display"] = "block";


                    if (monthStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentMonth"]).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }
                }

            }

            catch (Exception ex)
            {
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                EmployeeBL obj = new EmployeeBL();

                if (ddlReportType.SelectedValue == "0")
                {
                    DateTime TodayDate = Convert.ToDateTime(Session["TodayDate"]);
                    DateTime NextWeek = TodayDate.AddDays(7);
                    Session["TodayDate"] = NextWeek.ToString();
                    if (GeneralFunction.GetFirstDayOfWeekDate(NextWeek).ToString("MM/dd/yyyy") == GeneralFunction.GetFirstDayOfWeekDate(DateTime.Now).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }
                    DateTime NextWeekStart = GeneralFunction.GetFirstDayOfWeekDate(NextWeek);
                    DateTime NextWeekEnd = GeneralFunction.GetLastDayOfWeekDate(NextWeek);
                    //  DataTable ds = GetReport(NextWeekStart, NextWeekEnd, userid);
                    string Ismanage = Session["IsManage"].ToString();
                    string IsAdmin = Session["IsAdmin"].ToString();
                    DataTable ds = new DataTable();
                    ds = GetReportSingle(NextWeekStart, NextWeekEnd, userid);
                    DataTable ds1 = obj.GetLeaveRequestDetByUserID(userid, NextWeekStart, NextWeekEnd);
                    Session["AtnDetails"] = ds;
                    if (ds.Rows.Count > 0)
                    {
                        grdAttendanceSingle.DataSource = ds;
                        grdAttendanceSingle.DataBind();
                        dvSingle.Style["display"] = "block";
                        dvMonthrep.Style["display"] = "none";

                        grdSingleLeaveReq.DataSource = ds1;
                        grdSingleLeaveReq.DataBind();
                    }
                }
                else
                {
                    DateTime monthStart = Convert.ToDateTime(ViewState["MonthSatrt"]).AddMonths(1);
                    DateTime monthEnd = monthStart.AddMonths(1).AddSeconds(-1);
                    ViewState["MonthSatrt"] = monthStart;

                    GetCalender(monthStart, monthEnd, 1);
                    DataTable ds1 = obj.GetLeaveRequestDetByUserID(userid, monthStart, monthEnd);
                    grdSingleLeaveReq.DataSource = ds1;
                    grdSingleLeaveReq.DataBind();
                    dvSingle.Style["display"] = "none";
                    dvMonthrep.Style["display"] = "block";


                    if (monthStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentMonth"]).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }
                }
            }

            catch (Exception ex)
            {
            }
        }

        protected void btnCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                 int userid = Convert.ToInt32(Session["UserID"]);
                EmployeeBL obj = new EmployeeBL();

                if (ddlReportType.SelectedValue == "0")
                {
                   
                    DateTime StartDate = Convert.ToDateTime(ViewState["CurrentStart"].ToString());
                    DateTime EndDate = Convert.ToDateTime(ViewState["CurrentEnd"].ToString());
                    Session["TodayDate"] = StartDate;
                    if (GeneralFunction.GetFirstDayOfWeekDate(StartDate).ToString("MM/dd/yyyy") == GeneralFunction.GetFirstDayOfWeekDate(DateTime.Now).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }

                    DataTable ds = new DataTable();
                    ds = GetReportSingle(StartDate, EndDate, userid);
                    Session["AtnDetails"] = ds;
                   
                    DataTable ds1 = obj.GetLeaveRequestDetByUserID(userid, StartDate, EndDate);
                    if (ds.Rows.Count > 0)
                    {
                        grdAttendanceSingle.DataSource = ds;
                        grdAttendanceSingle.DataBind();
                        dvSingle.Style["display"] = "block";
                        dvMonthrep.Style["display"] = "none";
                        grdSingleLeaveReq.DataSource = ds1;
                        grdSingleLeaveReq.DataBind();
                    }
                }
                else
                {
                    DateTime monthStart = Convert.ToDateTime(ViewState["CurrentMonth"]).AddMonths(1);
                    DateTime monthEnd = monthStart.AddMonths(1).AddSeconds(-1);
                    ViewState["MonthSatrt"] = monthStart;

                    GetCalender(monthStart, monthEnd, 1);
                    DataTable ds1 = obj.GetLeaveRequestDetByUserID(userid, monthStart, monthEnd);
                    grdSingleLeaveReq.DataSource = ds1;
                    grdSingleLeaveReq.DataBind();
                    dvSingle.Style["display"] = "none";
                    dvMonthrep.Style["display"] = "block";


                    if (monthStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentMonth"]).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
      
            }
        }

        protected void grdAttendanceSingle_RowCreated(object sender, GridViewRowEventArgs e)
        {
            DateTime startdate = GeneralFunction.GetFirstDayOfWeekDate(Convert.ToDateTime(Session["TodayDate"]));

            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    GridView HeaderGrid = (GridView)sender;

                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell HeaderCell = new TableCell();
                    HeaderCell.Text = lblName.Text.ToString().Trim() + " (" + lblID.Text.ToString().Trim() + ")";
                    HeaderCell.Style["text-align"] = "center";
                    HeaderCell.ColumnSpan = 7;
                    HeaderGridRow.Cells.Add(HeaderCell);

                    HeaderCell = new TableCell();

                    grdAttendanceSingle.Controls[0].Controls.AddAt(0, HeaderGridRow);

                }

            }
            catch (Exception ex)
            {
            }
        }
        protected void grdAttendanceSingle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                Report obj = new Report();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var dt = new List<Attendance.Entities.MultipleLogininfo>();
                    string s = string.Empty;
                    string sTable = string.Empty;

                    Label lblDay = (Label)e.Row.FindControl("lblDay");
                    string[] Day1 = lblDay.Text.ToString().Split('(');

                    string TodayDt = Day1[1].Substring(0, Day1[1].Length - 2);


                    Label lblScIn = (Label)e.Row.FindControl("lblScIn");
                    lblScIn.Text = lblScIn.Text.ToString().Trim() == "-" ? "" : lblScIn.Text.ToString();
                    if (lblScIn.Text.Trim() == "-")
                    {
                        lblScIn.Text = "";
                    }

                    Label lblSignIn = (Label)e.Row.FindControl("lblSignIn");
                    lblSignIn.Text = lblSignIn.Text == "" ? "" : lblSignIn.Text == "H" ? "H" : lblSignIn.Text == "L" ? "L" : lblSignIn.Text == "S" ? "S" : Convert.ToDateTime(lblSignIn.Text).ToString("hh:mm tt");

                    HiddenField hdnSignInFlag = (HiddenField)e.Row.FindControl("hdnSignInFlag");
                    if (hdnSignInFlag.Value == "True")
                    {
                        e.Row.Cells[2].CssClass += "atnEdit ";
                    }

                    HiddenField hdnSignOutFlag = (HiddenField)e.Row.FindControl("hdnSignOutFlag");
                    if (hdnSignOutFlag.Value == "True")
                    {
                        e.Row.Cells[2].CssClass += "atnEdit ";
                    }

                    Label lblSignOut = (Label)e.Row.FindControl("lblSignOut");
                    lblSignIn.Text = lblSignIn.Text == "" ? "" : lblSignIn.Text == "H" ? "H" : lblSignIn.Text == "L" ? "L" : lblSignIn.Text == "S" ? "S" : (lblSignIn.Text + " - " + (lblSignOut.Text == "" ? "" : lblSignOut.Text == "N/A" ? "N/A" : lblSignOut.Text == "L" ? "" : lblSignOut.Text == "H" ? "" : lblSignOut.Text == "S" ? "" : Convert.ToDateTime(lblSignOut.Text).ToString("hh:mm tt")));

                    HiddenField hdnSigninNotes = (HiddenField)e.Row.FindControl("hdnSigninNotes");
                    HiddenField hdnMultiple = (HiddenField)e.Row.FindControl("hdnMultiple");




                    if (hdnMultiple.Value == "True")
                    {
                        lblSignIn.CssClass += "SinglemultipleLogin ";
                        dt = obj.GetMultipleDetailsByEmpID(Convert.ToDateTime(TodayDt), lblID.Text);
                        s = CreateMultipleTable(dt);
                        dt = null;
                    }
                    sTable = CreateSignInTable(lblName.Text, (hdnSigninNotes.Value), s);
                    s = "";
                    if (sTable != "")
                    {
                        lblSignIn.Attributes.Add("rel", "tooltip");
                        lblSignIn.Attributes.Add("title", sTable);
                        e.Row.Cells[2].CssClass += "greenTag ";
                    }
                    Label lblMonHours = (Label)e.Row.FindControl("lblMonHours");
                    lblMonHours.Text = lblMonHours.Text == "N/A" ? "" : lblMonHours.Text == "" ? "" : GeneralFunction.CalDoubleToTime((Convert.ToDouble(lblMonHours.Text)));
                    if (lblMonHours.Text.Trim() == "-")
                    {
                        lblMonHours.Text = "";
                    }

                    HiddenField hdnLvStatus = (HiddenField)e.Row.FindControl("hdnLvStatus");
                    e.Row.Cells[2].CssClass += GeneralFunction.GetColor(lblSignIn.Text.Trim(), hdnLvStatus.Value.Trim());

                }
            }
            catch (Exception ex)
            {
            }

        }
        private string CreateMultipleTable(List<Attendance.Entities.MultipleLogininfo> obj)
        {
            string strTransaction = string.Empty;
            if (obj.Count > 0)
            {
                strTransaction += "<ul>";
                for (int i = 0; i < obj.Count; i++)
                {
                    strTransaction += "<li>" + (i + 1) + ". Sign in/out group : " + obj[i].LoginDate.ToString().Trim() + " - " + obj[i].LogoutDate.ToString().Trim();
                    strTransaction += "</li>";

                }

                strTransaction += "</ul>";
            }
            return strTransaction;

        }
        private string CreateSignInTable(string Employeename, string SignInNotes, string s)
        {
            SignInNotes = HttpUtility.HtmlDecode(SignInNotes).Replace("</br>", "");
            SignInNotes = HttpUtility.HtmlDecode(SignInNotes).Replace("<br/>", "");
            string strTransaction = string.Empty;
            if (SignInNotes.Trim() != "" || s.Trim() != "")
            {
                strTransaction = "<div style=\"height:143px;overflow-y:scroll;overflow-x:hidden;\">";

                strTransaction += "<table class=\"noPading\"  id=\"SalesStatus\" style=\"display: table; border-collapse:collapse;  width:100%; color:#eee; \">";
                strTransaction += "<tr id=\"CampaignsTitle1\" >";
                strTransaction += "<td align=\"center\" colspan=\"2\" >";
                strTransaction += "<b style=\"text-align:center; display:block\"  >" + Employeename + "</b>";
                strTransaction += "</td>";
                strTransaction += "</tr>";

                if (s != "")
                {
                    strTransaction += "<tr>";
                    strTransaction += "<td style=\"width:150px;\">";
                    strTransaction += "<fieldset style=\"margin:0 15px 0 0;border:#ccc 1px dashed;\"><legend>Multiple SignIn/Out(s)</legend>";
                    strTransaction += s;
                    strTransaction += "</fieldset>";
                    strTransaction += "</td>";
                    strTransaction += "</tr>";
                }
                if (SignInNotes.Trim() != "")
                {
                    strTransaction += "<tr>";
                    strTransaction += "<td style=\"width:150px;\">";
                    strTransaction += "<fieldset style=\"margin:0 15px 0 0;border:#ccc 1px dashed;\"><legend>Notes</legend>";
                    strTransaction += "<div>";
                    strTransaction += HttpUtility.HtmlDecode(SignInNotes).Replace("<br/>", "\n");
                    strTransaction += "</div>";
                    strTransaction += "</fieldset>";
                    strTransaction += "</td>";
                    strTransaction += "</tr>";
                }
                strTransaction += "</table>";
                strTransaction += "</div>";
            }
            return strTransaction;

        }

        protected void grdSingleLeaveReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblEmpFirstname = (Label)e.Row.FindControl("lblEmpFirstname");

                    Label lblNotes = (Label)e.Row.FindControl("lblNotes");

                    string sTable = CreateSignInTable(lblEmpFirstname.Text, lblNotes.Text);
                    lblNotes.Attributes.Add("rel", "tooltip");
                    lblNotes.Attributes.Add("title", sTable);

                    lblNotes.Text = GeneralFunction.WrapTextByMaxCharacters(lblNotes.Text, 20);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private string CreateSignInTable(string Employeename, string SignInNotes)
        {

            string strTransaction = string.Empty;
            if (SignInNotes.Trim() != "")
            {
                strTransaction = "<div style=\"height:143px;overflow-y:scroll;overflow-x:hidden;\">";

                strTransaction += "<table class=\"noPading\"  id=\"SalesStatus\" style=\"display: table; border-collapse:collapse;  width:100%; color:#eee; \">";
                strTransaction += "<tr id=\"CampaignsTitle1\" >";
                strTransaction += "<td align=\"center\" colspan=\"2\" >";
                strTransaction += "<b style=\"text-align:center; display:block\"  >" + Employeename + "</b>";
                strTransaction += "</td>";
                strTransaction += "</tr>";
                if (SignInNotes.Trim() != "")
                {
                    strTransaction += "<tr>";
                    strTransaction += "<td style=\"width:150px;\">";
                    strTransaction += "<fieldset style=\"margin:0 15px 0 0;border:#ccc 1px dashed;\"><legend>Notes</legend>";
                    strTransaction += "<div>";
                    strTransaction += HttpUtility.HtmlDecode(SignInNotes);
                    strTransaction += "</div>";
                    strTransaction += "</fieldset>";
                    strTransaction += "</td>";
                    strTransaction += "</tr>";
                }
                strTransaction += "</table>";
                strTransaction += "</div>";
            }
            return strTransaction;

        }

        private void GetCalender(DateTime MonthStart, DateTime MonthEnd, int locationID)
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
                DataTable dsMonth = GetMonthlyRepByuserID(MonthStart, MonthEnd, Convert.ToInt32(Session["userID"]));
                Session["MonthRep"] = dsMonth;
                grdMonthlyRep.DataSource = dtAttandence;
                grdMonthlyRep.DataBind();


            }
            catch (Exception ex)
            {
            }


        }

        private DataTable GetMonthlyRepByuserID(DateTime StartDate, DateTime EndDate, int userid)
        {
            DataSet ds = new DataSet();
            DataTable dtAttandence = new DataTable();
            try
            {
                
                lblWeekReportheading.Text = "Monthly attendance report";
                lblWeekReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndDate.ToString("MM/dd/yyyy") + " )";
                dtAttandence.Columns.Add("EmpID", typeof(string));
                dtAttandence.Columns.Add("Empname", typeof(string));
                dtAttandence.Columns.Add("Day", typeof(string));
                dtAttandence.Columns.Add("SchIn", typeof(string));
                dtAttandence.Columns.Add("SchOut", typeof(string));
                dtAttandence.Columns.Add("SignIn", typeof(string));
                dtAttandence.Columns.Add("SignOut", typeof(string));
                dtAttandence.Columns.Add("Hrs", typeof(string));
                dtAttandence.Columns.Add("LogUserID", typeof(int));
                dtAttandence.Columns.Add("LoginNotes", typeof(string));
                dtAttandence.Columns.Add("LogoutNotes", typeof(string));
                dtAttandence.Columns.Add("LoginFlag", typeof(string));
                dtAttandence.Columns.Add("LogoutFlag", typeof(string));
                dtAttandence.Columns.Add("Multiple", typeof(string));
                dtAttandence.Columns.Add("LvStatus", typeof(string));
                dtAttandence.Rows.Add();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_Fn]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@userid", userid));
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", StartDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@EndDate", EndDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@shiftID", Convert.ToInt32(ddlShifts.SelectedValue)));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables.Count > 1)
                    {
                        DataTable dt = ds.Tables[1];
                        DataView dv = dt.DefaultView;
                        DataTable dtname = new DataTable();
                        DateTime NextDate = GeneralFunction.GetNextDayOfWeekDate(StartDate);

                        DataTable dtL = ds.Tables[2];
                        DataView dvL = dtL.DefaultView;
                        DataTable dtLeave = new DataTable();

                        DataTable dtH = ds.Tables[3];
                        DataView dvH = dtH.DefaultView;
                        DataTable dtHoliday = new DataTable();

                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {

                            dv.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                            dtname = dv.ToTable();

                            dvL.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                            dtLeave = dvL.ToTable();

                            dvH.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                            dtHoliday = dvH.ToTable();

                            dtAttandence.Rows[j]["empid"] = ds.Tables[0].Rows[j]["empid"].ToString();
                            dtAttandence.Rows[j]["Empname"] = ds.Tables[0].Rows[j]["firstName"].ToString() + " " + ds.Tables[0].Rows[j]["lastname"].ToString();

                            DateTime startDate = StartDate;
                            DateTime nextdate = NextDate;

                            for (int i = 0; i < Convert.ToInt32(EndDate.Day); i++)
                            {
                                dtAttandence.Rows[i]["Day"] = startDate.ToString("MM/dd/yyyy");
                                DataView dL = dtLeave.DefaultView;
                                dL.RowFilter = "Fromdate<=#" + startDate + "# and #" + startDate + "#<=Todate";
                                DataTable dtLvResult = dL.ToTable();


                                DataView dH = dtHoliday.DefaultView;
                                dH.RowFilter = "HolidayDate >= #" + startDate + "# and HolidayDate<#" + nextdate + "#";
                                DataTable dtHolResult = dH.ToTable();

                                if (dtLvResult.Rows.Count > 0)
                                {
                                    dtAttandence.Rows[i]["SignIn"] = "L";
                                    dtAttandence.Rows[i]["SignOut"] = "L";
                                    dtAttandence.Rows[i]["Hrs"] = "";
                                    dtAttandence.Rows[i]["LvStatus"] = dtLvResult.Rows[0]["ApprovedStatus"].ToString();
                                }
                                if (dtHolResult.Rows.Count > 0)
                                {
                                    if (startDate.DayOfWeek.ToString() == "Sunday")
                                    {
                                        dtAttandence.Rows[i]["SignIn"] = "S";
                                        dtAttandence.Rows[i]["SignOut"] = "S";
                                        dtAttandence.Rows[i]["Hrs"] = "";
                                    }
                                    else
                                    {
                                        dtAttandence.Rows[i]["SignIn"] = "H";
                                        dtAttandence.Rows[i]["SignOut"] = "H";
                                        dtAttandence.Rows[i]["Hrs"] = "";
                                    }
                                }

                                if (dtname.Rows.Count > 0)
                                {
                                    DataView dv1 = dtname.DefaultView;
                                    dv1.RowFilter = "Logindate >= #" + startDate + "# and Logindate<#" + nextdate + "#";
                                    DataTable dt1 = dv1.ToTable();

                                    if (dtLvResult.Rows.Count > 0)
                                    {
                                        dtAttandence.Rows[i]["SignIn"] = "L";
                                        dtAttandence.Rows[i]["SignOut"] = "L";
                                        dtAttandence.Rows[i]["Hrs"] = "";
                                        dtAttandence.Rows[i]["LvStatus"] = dtLvResult.Rows[0]["ApprovedStatus"].ToString();
                                    }

                                    if (dtHolResult.Rows.Count > 0)
                                    {
                                        if (startDate.DayOfWeek.ToString() == "Sunday")
                                        {
                                            dtAttandence.Rows[i]["SignIn"] = "S";
                                            dtAttandence.Rows[i]["SignOut"] = "S";
                                            dtAttandence.Rows[i]["Hrs"] = "";
                                        }
                                        else
                                        {
                                            dtAttandence.Rows[i]["SignIn"] = "H";
                                            dtAttandence.Rows[i]["SignOut"] = "H";
                                            dtAttandence.Rows[i]["Hrs"] = "";
                                        }
                                    }

                                    if (dt1.Rows.Count > 0)
                                    {
                                        dtAttandence.Rows[i]["SchIn"] = dt1.Rows[0]["startTime"].ToString();
                                        dtAttandence.Rows[i]["SchOut"] = dt1.Rows[0]["EndTime"].ToString();

                                        for (int k = 0; k < dt1.Rows.Count; k++)
                                        {
                                            dtAttandence.Rows[i]["SignIn"] = dt1.Rows[0]["Logindate"].ToString().Trim();
                                            dtAttandence.Rows[i]["SignOut"] = dt1.Rows[dt1.Rows.Count - 1]["Logoutdate"].ToString().Trim() == "" ? "N/A" : dt1.Rows[dt1.Rows.Count - 1]["Logoutdate"].ToString().Trim();

                                            dtAttandence.Rows[i]["LoginNotes"] = dtAttandence.Rows[i]["LoginNotes"] + "</br>" + (dt1.Rows[k]["loginnotes"].ToString() + "</br>" + dt1.Rows[k]["logoutnotes"].ToString());

                                            if (dt1.Rows.Count > 1)
                                            {
                                                dtAttandence.Rows[i]["Multiple"] = "True";
                                            }
                                            if (dt1.Rows[k]["total hours worked"].ToString() == "")
                                            {
                                                dtAttandence.Rows[i]["Hrs"] = "N/A";
                                            }
                                            else
                                            {
                                                if (dtAttandence.Rows[i]["Hrs"].ToString() == "")
                                                {
                                                    dtAttandence.Rows[i]["Hrs"] = (Convert.ToDouble(dt1.Rows[k]["total hours worked"].ToString().Trim())).ToString();
                                                }
                                                else if (dtAttandence.Rows[i]["Hrs"].ToString() == "N/A")
                                                {
                                                    dtAttandence.Rows[i]["Hrs"] = "N/A";
                                                }
                                                else
                                                {
                                                    dtAttandence.Rows[i]["Hrs"] = ((Convert.ToDouble(dtAttandence.Rows[i]["Hrs"])) + (Convert.ToDouble(dt1.Rows[k]["total hours worked"].ToString().Trim()))).ToString();
                                                }
                                            }
                                        }
                                        //dtAttandence.Rows[i]["Hrs"] = dt1.Rows[0]["total hours worked"].ToString().Trim() == "" ? "N/A" : dt1.Rows[0]["total hours worked"].ToString().Trim();
                                        dtAttandence.Rows[i]["LogUserID"] = Convert.ToInt32(dt1.Rows[0]["LogUserID"]);
                                        dtAttandence.Rows[i]["LoginFlag"] = dt1.Rows[0]["loginflag"].ToString();
                                        dtAttandence.Rows[i]["LogoutFlag"] = dt1.Rows[0]["logoutflag"].ToString();
                                    }
                                    dv1.RowFilter = null;
                                }
                                dvH.RowFilter = null;
                                dvL.RowFilter = null;
                                startDate = nextdate;
                                nextdate = GeneralFunction.GetNextDayOfWeekDate(nextdate);
                                dtAttandence.Rows.Add();
                            }
                        }

                        double SumHours = 0;
                        for (int i = 0; i < dtAttandence.Rows.Count; i++)
                        {
                            if (dtAttandence.Rows[i]["Hrs"].ToString() == "" || dtAttandence.Rows[i]["Hrs"].ToString() == "N/A")
                            {
                                SumHours = SumHours + 0;
                            }
                            else
                            {
                                SumHours = SumHours + Convert.ToDouble(dtAttandence.Rows[i]["Hrs"]);
                            }
                        }

                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["Day"] = "<b>Total Hours</b>";
                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["SchIn"] = "";
                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["SchOut"] = "";
                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["Day"] = "<b>Total Hours</b>";
                        dtAttandence.Rows[dtAttandence.Rows.Count - 1]["Hrs"] = SumHours == 0 ? "" : "<b>" + GeneralFunction.CalDoubleToTime(SumHours) + "</b>";
                        dtAttandence.Rows.Add();
                        lblMonthHrs.InnerText="Total Hours : "+(SumHours == 0 ? "" :  GeneralFunction.CalDoubleToTime(SumHours));
                    }

                    lblID.Text = dtAttandence.Rows[0]["EmpID"].ToString();
                    lblName.Text = dtAttandence.Rows[0]["Empname"].ToString();
                    lblMonth.InnerText = StartDate.ToString("MMMM") + " " + StartDate.ToString("yyyy");
                    lblMonthEmp.InnerText = dtAttandence.Rows[0]["Empname"].ToString();
                   
                }

            }

            catch (Exception ex)
            {
            }

            return dtAttandence;

        }

        protected void grdMonthlyRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DataTable dt = Session["MonthRep"] as DataTable;
                DataTable dtfilt = new DataTable();
                DataView dv=dt.DefaultView;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblSun = (Label)e.Row.FindControl("lblSun");
                    Label lblSunSignIn = (Label)e.Row.FindControl("lblSunSignIn");
                    Label lblEmpID = (Label)e.Row.FindControl("lblEmpID");
                    Label lblSunHrs = (Label)e.Row.FindControl("lblSunHrs");
                     if (lblSun.Text != "")
                    {
                        dv.RowFilter = "Day='" + Convert.ToDateTime(lblSun.Text).ToString("MM/dd/yyyy") + "'";
                        dtfilt = dv.ToTable();
                        fillCell(lblSun,lblSunSignIn,lblSunHrs,dtfilt, 0, e);
                    }
                     Label lblMon = (Label)e.Row.FindControl("lblMon");
                     Label lblMonSignIn = (Label)e.Row.FindControl("lblMonSignIn");
                     Label lblMonHrs = (Label)e.Row.FindControl("lblMonHrs");
                     if (lblMon.Text != "")
                     {
                         dv.RowFilter = "Day='" + Convert.ToDateTime(lblMon.Text).ToString("MM/dd/yyyy") + "'";
                         dtfilt = dv.ToTable();
                         fillCell(lblMon, lblMonSignIn,lblMonHrs,dtfilt, 1, e);
                     }

                     Label lblTue = (Label)e.Row.FindControl("lblTue");
                     Label lblTueSignIn = (Label)e.Row.FindControl("lblTueSignIn");
                     Label lblTueHrs = (Label)e.Row.FindControl("lblTueHrs");
                     if (lblTue.Text != "")
                     {
                         dv.RowFilter = "Day='" + Convert.ToDateTime(lblTue.Text).ToString("MM/dd/yyyy") + "'";
                         dtfilt = dv.ToTable();
                         fillCell(lblTue, lblTueSignIn,lblTueHrs ,dtfilt, 2, e);
                     }
                     Label lblWed = (Label)e.Row.FindControl("lblWed");
                     Label lblWedSignIn = (Label)e.Row.FindControl("lblWedSignIn");
                     Label lblWedHrs = (Label)e.Row.FindControl("lblWedHrs");
                     if (lblWed.Text != "")
                     {
                         dv.RowFilter = "Day='" + Convert.ToDateTime(lblWed.Text).ToString("MM/dd/yyyy") + "'";
                         dtfilt = dv.ToTable();
                         fillCell(lblWed, lblWedSignIn,lblWedHrs,dtfilt, 3, e);
                     }
                     Label lblThu = (Label)e.Row.FindControl("lblThu");
                     Label lblThuSignIn = (Label)e.Row.FindControl("lblThuSignIn");
                     Label lblThuHrs = (Label)e.Row.FindControl("lblThuHrs");
                     if (lblThu.Text != "")
                     {
                         dv.RowFilter = "Day='" + Convert.ToDateTime(lblThu.Text).ToString("MM/dd/yyyy") + "'";
                         dtfilt = dv.ToTable();
                         fillCell(lblThu, lblThuSignIn,lblThuHrs,dtfilt, 4, e);
                     }

                     Label lblFri = (Label)e.Row.FindControl("lblFri");
                     Label lblFriSignIn = (Label)e.Row.FindControl("lblFriSignIn");
                     Label lblFriHrs = (Label)e.Row.FindControl("lblFriHrs");
                     if (lblFri.Text != "")
                     {
                         dv.RowFilter = "Day='" + Convert.ToDateTime(lblFri.Text).ToString("MM/dd/yyyy") + "'";
                         dtfilt = dv.ToTable();
                         fillCell(lblFri, lblFriSignIn,lblFriHrs,dtfilt, 5, e);
                     }

                     Label lblSat = (Label)e.Row.FindControl("lblSat");
                     Label lblSatSignIn = (Label)e.Row.FindControl("lblSatSignIn");
                     Label lblSatHrs = (Label)e.Row.FindControl("lblSatHrs");
                    
                     if (lblSat.Text != "")
                     {
                         dv.RowFilter = "Day='" + Convert.ToDateTime(lblSat.Text).ToString("MM/dd/yyyy") + "'";
                         dtfilt = dv.ToTable();
                         fillCell(lblSat, lblSatSignIn,lblSatHrs, dtfilt, 6, e);
                     }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void fillCell(Label lbl,Label lblSignIn,Label lblHrs,DataTable dtfilt, int cellNum, GridViewRowEventArgs e)
        {
            Report obj = new Report();
            string sTable = "";
            string s = "";
            var dt = new List<Attendance.Entities.MultipleLogininfo>();
            try
            {
                if (lbl.Text != "")
                {
                    if (dtfilt.Rows[0]["SignIn"].ToString() != "")
                    {
                        if (dtfilt.Rows[0]["SignIn"].ToString() == "S" || dtfilt.Rows[0]["SignIn"].ToString() == "H" || dtfilt.Rows[0]["SignIn"].ToString() == "L")
                        {
                            lblSignIn.Text = dtfilt.Rows[0]["SignIn"].ToString();
                            e.Row.Cells[cellNum].CssClass += GeneralFunction.GetColor(lblSignIn.Text, dtfilt.Rows[0]["LvStatus"].ToString().Trim());
                        }
                        else
                        {
                            if (dtfilt.Rows[0]["Multiple"].ToString() == "True")
                            {
                                lbl.CssClass += "SinglemultipleLogin ";
                                dt = obj.GetMultipleDetailsByEmpID(Convert.ToDateTime(lbl.Text), lblID.Text);
                                s = CreateMultipleTable(dt);
                                dt = null;
                            }
                            sTable = CreateSignInTable(lblName.Text, (dtfilt.Rows[0]["LoginNotes"].ToString()), s);
                            s = "";
                            if (sTable != "")
                            {
                                lbl.Attributes.Add("rel", "tooltip");
                                lbl.Attributes.Add("title", sTable);
                                e.Row.Cells[cellNum].CssClass += "greenTag ";
                            }

                            if (dtfilt.Rows[0]["LoginFlag"].ToString() == "True" || dtfilt.Rows[0]["LogoutFlag"].ToString() == "True")
                            {
                                e.Row.Cells[cellNum].CssClass += "atnEdit ";
                            }
                            lblSignIn.Text = Convert.ToDateTime(dtfilt.Rows[0]["SignIn"].ToString()).ToString("hh:mm tt") + " - " + (dtfilt.Rows[0]["SignOut"].ToString() == "N/A" ? "N/A" : Convert.ToDateTime(dtfilt.Rows[0]["SignOut"].ToString()).ToString("hh:mm tt"));
                            lblHrs.Text = (dtfilt.Rows[0]["Hrs"].ToString() == "0" ? "" : dtfilt.Rows[0]["Hrs"].ToString() == "N/A" ? "" : dtfilt.Rows[0]["Hrs"].ToString() == "NULL" ? "" : "Hrs :  " + GeneralFunction.CalDoubleToTime(Convert.ToDouble(dtfilt.Rows[0]["Hrs"].ToString())));
                        
                        }
                        
                    }
                    lbl.Text = Convert.ToDateTime(lbl.Text).ToString("dd");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                EmployeeBL obj = new EmployeeBL();
                if (ddlReportType.SelectedValue == "1")
                {

                    DateTime monthStart = Convert.ToDateTime(ViewState["MonthSatrt"]);
                    DateTime monthEnd = monthStart.AddMonths(1).AddSeconds(-1);
                   
                    GetCalender(monthStart, monthEnd, 1);
                    DataTable ds1 = obj.GetLeaveRequestDetByUserID(userid, monthStart, monthEnd);
                    grdSingleLeaveReq.DataSource = ds1;
                    grdSingleLeaveReq.DataBind();
                    dvSingle.Style["display"] = "none";
                    dvMonthrep.Style["display"] = "block";


                    if (monthStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentMonth"]).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }
                }
                else
                {
                    
                    DateTime TodayDate = Convert.ToDateTime(Session["TodayDate"]);
                    if (GeneralFunction.GetFirstDayOfWeekDate(TodayDate).ToString("MM/dd/yyyy") == GeneralFunction.GetFirstDayOfWeekDate(DateTime.Now).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;
                    }

                    DateTime Start = GeneralFunction.GetFirstDayOfWeekDate(TodayDate);
                    DateTime End = GeneralFunction.GetLastDayOfWeekDate(TodayDate);
                    DataTable ds = new DataTable();
                    ds = GetReportSingle(Start, End, userid);
                  
                    DataTable ds1 = obj.GetLeaveRequestDetByUserID(userid, Start, End);
                    Session["AtnDetails"] = ds;
                    if (ds.Rows.Count > 0)
                    {
                        grdAttendanceSingle.DataSource = ds;
                        grdAttendanceSingle.DataBind();
                        dvSingle.Style["display"] = "block";
                        dvMonthrep.Style["display"] = "none";
                        grdSingleLeaveReq.DataSource = ds1;
                        grdSingleLeaveReq.DataBind();

                    }

                }

            }
            catch (Exception ex)
            {

            }
        }


    }
}
