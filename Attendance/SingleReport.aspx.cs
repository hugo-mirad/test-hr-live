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
                    ds = GetReportSingle(StartDate, EndDate, userid);
                    Session["AtnDetails"] = ds;
                    if (ds.Rows.Count > 0)
                    {
                        grdAttendanceSingle.DataSource = ds;
                        grdAttendanceSingle.DataBind();
                        DvSingleRep.Style["display"] = "block";
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
                Session["AtnDetails"] = ds;
                if (ds.Rows.Count > 0)
                {
                    grdAttendanceSingle.DataSource = ds;
                    grdAttendanceSingle.DataBind();
                    DvSingleRep.Style["display"] = "block";
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
                Session["AtnDetails"] = ds;
                if (ds.Rows.Count > 0)
                {
                    grdAttendanceSingle.DataSource = ds;
                    grdAttendanceSingle.DataBind();
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
                if (ds.Rows.Count > 0)
                {
                    grdAttendanceSingle.DataSource = ds;
                    grdAttendanceSingle.DataBind();
                    DvSingleRep.Style["display"] = "block";
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
    }
}
