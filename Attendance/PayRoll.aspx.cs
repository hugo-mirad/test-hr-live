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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Net.Mail;
using System.Data.SqlClient;
namespace Attendance
{
    public partial class PayRoll : System.Web.UI.Page
    {
        DataTable dtChanges = new DataTable();
        DataTable dtNewEmp = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] != null && Session["UserID"] != null)
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
                    DateTime TodayDate = Convert.ToDateTime(Session["TodayBannerDate"]);
                    int userid = Convert.ToInt32(Session["UserID"]);

                    getLocations();
                    ddlLocation.SelectedIndex = (ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(Session["LocationName"].ToString())));


                    if (Session["IsAdmin"].ToString() == "True")
                    {

                        if (ddlLocation.SelectedItem.Text.Trim() == "INBH" || ddlLocation.SelectedItem.Text.Trim() == "INDG")
                        {
                            DateTime CurrentDate = Convert.ToDateTime(ISTTime.ToString("MM/dd/yyyy"));
                            CurrentDate = CurrentDate.AddMonths(-1);
                            DateTime MonthStart = CurrentDate.AddDays(1 - CurrentDate.Day);
                            DateTime MonthEnd = MonthStart.AddMonths(1).AddSeconds(-1);

                            txtToDate.Text = MonthEnd.ToString("MM/dd/yyyy");
                            txtFromDate.Text = MonthStart.ToString("MM/dd/yyyy");

                            ViewState["StartRptDt"] = MonthStart;
                            ViewState["EndRptDt"] = MonthEnd;


                            Attendance.BAL.Report obj = new Report();
                            DateTime Count = obj.GetFreezedDate(MonthEnd, ddlLocation.SelectedItem.Text.Trim());
                            if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                            {
                                lblFreeze.Text = "";
                            }
                            else
                            {
                                lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                            }

                            bool final = obj.GetFinalPayrollDate(MonthStart,Convert.ToInt32(ddlLocation.SelectedItem.Value));

                            if (final)
                            {
                                btnFinal.CssClass = "btn btn-small btn-warning disabled";
                                btnSave.CssClass = "btn btn-small btn-warning disabled";
                                btnFinal.Enabled = false;
                                btnSave.Enabled = false;
                                hdnFreeze.Value = "true"; ;
                               // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "delEditLabelCss();", true);
                            }
                            else
                            {
                                btnFinal.CssClass = "btn btn-small btn-warning";
                                btnSave.CssClass = "btn btn-small btn-warning";
                                btnFinal.Enabled = true;
                                btnSave.Enabled = true;
                                hdnFreeze.Value = "false";
                               // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "addEditLabelCss();", true);
                            }

                            DataTable dt = GetReportIndia(MonthStart, MonthEnd, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                            lblWeekPayrollReport.Text = "( " + MonthStart.ToString("MM/dd/yyyy") + " - " + MonthEnd.ToString("MM/dd/yyyy") + " )";
                            GetEditHistory(MonthStart, MonthEnd);
                            lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                            lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                            grdPayRollIndia.DataSource = dt;
                            grdPayRollIndia.DataBind();
                            Session["Indiapayroll"] = (DataTable)grdPayRollIndia.DataSource;


                            grdPayRoll.DataSource = null;
                            grdPayRoll.DataBind();
                            btnSave.Visible = true;
                            btnFinal.Visible = true;
                            btnPrint.Visible = true;
                        }
                        else
                        {
                            DateTime StartDate = GeneralFunction.GetFirstDayOfWeekDate(TodayDate);
                            DateTime EndDate = StartDate.AddDays(-14);

                            Attendance.BAL.Report obj = new Report();
                            DateTime Count = obj.GetFreezedDate(EndDate, ddlLocation.SelectedItem.Text.Trim());
                            if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                            {
                                lblFreeze.Text = "";
                            }
                            else
                            {
                                lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                            }


                            txtToDate.Text = StartDate.AddDays(-1).ToString("MM/dd/yyyy");
                            txtFromDate.Text = EndDate.ToString("MM/dd/yyyy");
                            ViewState["StartRptDt"] = EndDate;
                            ViewState["EndRptDt"] = StartDate.AddDays(-1);
                            GetReport(EndDate, StartDate.AddDays(-1), 0, ddlLocation.SelectedItem.Text.Trim());
                            btnSave.Visible = false;
                            btnFinal.Visible = false;
                            btnPrint.Visible = false;
                            rppayslip.DataSource = null;
                            rppayslip.DataBind();
                        }
                        
                        lblGrdLocaton.Visible = true;
                        ddlLocation.Visible = true;
                    }
                    else
                    {
                        if (ddlLocation.SelectedItem.Text.Trim() == "INBH" || ddlLocation.SelectedItem.Text.Trim() == "INDG")
                        {
                            DateTime CurrentDate = Convert.ToDateTime(ISTTime.ToString("MM/dd/yyyy"));
                            CurrentDate = CurrentDate.AddMonths(-1);
                            DateTime MonthStart = CurrentDate.AddDays(1 - CurrentDate.Day);
                            DateTime MonthEnd = MonthStart.AddMonths(1).AddSeconds(-1);
                            txtToDate.Text = MonthEnd.ToString("MM/dd/yyyy");
                            txtFromDate.Text = MonthStart.ToString("MM/dd/yyyy");
                            ViewState["StartRptDt"] = MonthStart;
                            ViewState["EndRptDt"] = MonthEnd;
                            Attendance.BAL.Report obj = new Report();
                            DateTime Count = obj.GetFreezedDate(MonthEnd, Session["LocationName"].ToString());
                            if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                            {
                                lblFreeze.Text = "";
                            }
                            else
                            {
                                lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                            }
                            bool final = obj.GetFinalPayrollDate(MonthStart, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                            if (final)
                            {
                                btnFinal.CssClass = "btn btn-small btn-warning disabled";
                                btnSave.CssClass = "btn btn-small btn-warning disabled";
                                btnFinal.Enabled = false;
                                btnSave.Enabled = false;
                                hdnFreeze.Value = "true"; ;
                                // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "delEditLabelCss();", true);
                            }
                            else
                            {
                                btnFinal.CssClass = "btn btn-small btn-warning";
                                btnSave.CssClass = "btn btn-small btn-warning";
                                btnFinal.Enabled = true;
                                btnSave.Enabled = true;
                                hdnFreeze.Value = "false";
                                // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "addEditLabelCss();", true);
                            }

                            DataTable dt = GetReportIndia(MonthStart, MonthEnd, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                            lblWeekPayrollReport.Text = "( " + MonthStart.ToString("MM/dd/yyyy") + " - " + MonthEnd.ToString("MM/dd/yyyy") + " )";
                            GetEditHistory(MonthStart, MonthEnd);
                            lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                            lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                            grdPayRollIndia.DataSource = dt;
                            grdPayRollIndia.DataBind();
                            Session["Indiapayroll"] = (DataTable)grdPayRollIndia.DataSource;
                            grdPayRoll.DataSource = null;
                            grdPayRoll.DataBind();
                            btnSave.Visible = true;
                            btnFinal.Visible = true;
                            btnPrint.Visible = true;
                        }
                        else
                        {
                            DateTime StartDate = GeneralFunction.GetFirstDayOfWeekDate(TodayDate);
                            DateTime EndDate = StartDate.AddDays(-14);
                            Attendance.BAL.Report obj = new Report();
                            DateTime Count = obj.GetFreezedDate(EndDate, Session["LocationName"].ToString());
                            if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                            {
                                lblFreeze.Text = "";
                            }
                            else
                            {
                                lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                            }
                            txtToDate.Text = StartDate.AddDays(-1).ToString("MM/dd/yyyy");
                            txtFromDate.Text = EndDate.ToString("MM/dd/yyyy");
                            ViewState["StartRptDt"] = EndDate;
                            ViewState["EndRptDt"] = StartDate.AddDays(-1);
                            GetReport(EndDate, StartDate.AddDays(-1), 0, ddlLocation.SelectedItem.Text.Trim());
                            btnSave.Visible = false;
                            btnFinal.Visible = false;
                            btnPrint.Visible = false;
                            rppayslip.DataSource = null;
                            rppayslip.DataBind();
                        }
                        lblGrdLocaton.Visible = true;
                        ddlLocation.Enabled = false;
                    }
                    BindListOfNewEmployee();
                    BindListofChanges();
                 }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        private void BindListOfNewEmployee()
        {
            DataTable dt = dtNewEmp;
            if (dt.Rows.Count > 0)
            {
                // divNewEmp.Style["display"] = "block";
                //divNewEmp.Visible = true;
                lblNewEmp.Text = "New employee(s) data :";
                grdNewEmp.DataSource = dt;
                grdNewEmp.DataBind();
            }
            else
            {
                //divNewEmp.Style["display"] = "none";
                lblNewEmp.Text = "";
                grdNewEmp.DataSource = null;
                grdNewEmp.DataBind();
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
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                DateTime StartDate = Convert.ToDateTime(txtFromDate.Text);
                DateTime EndTime = Convert.ToDateTime(txtToDate.Text);
                ViewState["StartRptDt"] = StartDate;
                ViewState["EndRptDt"] = EndTime;
                Attendance.BAL.Report obj = new Report();
                DateTime Count = obj.GetFreezedDate(EndTime, ddlLocation.SelectedItem.Text.ToString());
                if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                {
                    lblFreeze.Text = "";
                }
                else
                {
                    lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                }
                if (Session["IsAdmin"].ToString() == "True")
                {
                    if (ddlLocation.SelectedItem.Text.Trim() == "INBH" || ddlLocation.SelectedItem.Text.Trim() == "INDG")
                    {
                        txtFromDate.Text = StartDate.ToString("MM/dd/yyyy");
                        txtToDate.Text = EndTime.ToString("MM/dd/yyyy");
                        bool final = obj.GetFinalPayrollDate(StartDate, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                        if (final)
                        {
                            btnFinal.CssClass = "btn btn-small btn-warning disabled";
                            btnSave.CssClass = "btn btn-small btn-warning disabled";
                            btnFinal.Enabled = false;
                            btnSave.Enabled = false;
                            hdnFreeze.Value = "true"; ;
                            // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "delEditLabelCss();", true);
                        }
                        else
                        {
                            btnFinal.CssClass = "btn btn-small btn-warning";
                            btnSave.CssClass = "btn btn-small btn-warning";
                            btnFinal.Enabled = true;
                            btnSave.Enabled = true;
                            hdnFreeze.Value = "false";
                            // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "addEditLabelCss();", true);
                        }
                        DataTable dt = GetReportIndia(StartDate, EndTime, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                        lblWeekPayrollReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndTime.ToString("MM/dd/yyyy") + " )";
                        GetEditHistory(StartDate, EndTime);
                        lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                        lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                        grdPayRollIndia.DataSource = dt;
                        grdPayRollIndia.DataBind();
                        Session["Indiapayroll"] = (DataTable)grdPayRollIndia.DataSource;
                        grdPayRoll.DataSource = null;
                        grdPayRoll.DataBind();
                        btnSave.Visible = true;
                        btnFinal.Visible = true;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        txtFromDate.Text = StartDate.AddDays(-1).ToString("MM/dd/yyyy");
                        txtToDate.Text = EndTime.ToString("MM/dd/yyyy");
                        GetReport(EndTime, StartDate.AddDays(-1), 0, ddlLocation.SelectedItem.Text.Trim());
                        btnSave.Visible = false;
                        btnFinal.Visible = false;
                        btnPrint.Visible = false;
                        rppayslip.DataSource = null;
                        rppayslip.DataBind();
                    }
                    lblGrdLocaton.Visible = true;
                    ddlLocation.Visible = true;
                }
                else
                {
                    GetReport(StartDate, EndTime, userid, "");
                    lblGrdLocaton.Visible = false;
                    ddlLocation.Visible = false;
                    btnSave.Visible = false;
                    btnFinal.Visible = false;
                    btnPrint.Visible = false;
                    rppayslip.DataSource = null;
                    rppayslip.DataBind();
                }
              // GetReport(StartDate, EndTime, userid,ddlLocation.SelectedItem.Text.Trim());
                BindListOfNewEmployee();
                BindListofChanges();
            }
            catch (Exception ex)
            {
            }
        } 
        private void BindListofChanges()
        {
            try
            {
                if (dtChanges.Rows.Count > 0)
                {
                    //dvChanges.Style["display"] = "block";
                    lblChanges.Text = "List of Changes during this period :";
                    DataTable dt = dtChanges;
                    DataView dv = dt.DefaultView;
                    DataTable dtDistinctID = dt.DefaultView.ToTable(true, "EMPID", "empname");
                    rpt1.DataSource = dtDistinctID;
                    rpt1.DataBind();
                }
                else
                {
                    //dtChanges.Clear();
                    // dvChanges.Style["display"] = "none";
                    lblChanges.Text = "";
                    rpt1.DataSource = dtChanges;
                    rpt1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void grdPayRoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsEdit = Session["EditHistory"] as DataSet;
            DataTable dt = dsEdit.Tables[0];
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblEmpFirstname = (Label)e.Row.FindControl("lblEmpFirstname");
                    Label lblEmpLastname = (Label)e.Row.FindControl("lblEmpLastname");

                    Label lblEmpID = (Label)e.Row.FindControl("lblEmpID");
                    lblEmpFirstname.Text = lblEmpFirstname.Text + " " + lblEmpLastname.Text;

                    Label lblStartedDate = (Label)e.Row.FindControl("lblStartedDate");
                    lblStartedDate.Text = lblStartedDate.Text == "01/01/1900" ? "" : (lblStartedDate.Text);

                    Label lblTerminatedDate = (Label)e.Row.FindControl("lblTerminatedDate");
                    lblTerminatedDate.Text = lblTerminatedDate.Text == "01/01/1900" ? "" : lblTerminatedDate.Text;

                    Label lblgrdSSN = (Label)e.Row.FindControl("lblgrdSSN");
                    lblgrdSSN.Text = lblgrdSSN.Text == "" ? "" : GeneralFunction.FormatUsSSN(lblgrdSSN.Text);

                    DateTime Start = Convert.ToDateTime(ViewState["StartRptDt"]);
                    DateTime End = Convert.ToDateTime(ViewState["EndRptDt"]);

                    if (lblStartedDate.Text != "" && (Convert.ToDateTime(lblStartedDate.Text) >= Start && Convert.ToDateTime(lblStartedDate.Text) <= End))
                    {
                        HiddenField hdnEmpuserid = (HiddenField)e.Row.FindControl("hdnEmpuserid");
                        Label lblIsNew = (Label)e.Row.FindControl("lblIsNew");
                        lblIsNew.Text = "Yes";
                        CreateNewEmployeeList(Convert.ToInt32(hdnEmpuserid.Value));
                    }
                    else
                    {
                        HiddenField hdnEmpuserid = (HiddenField)e.Row.FindControl("hdnEmpuserid");
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = "RecordID=" + Convert.ToInt32(hdnEmpuserid.Value);
                        DataTable dt1 = dv.ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            Label lblIsChanges = (Label)e.Row.FindControl("lblIsChanges");
                            lblIsChanges.Text = "Yes";
                            CreateListOFChanges(lblEmpFirstname.Text, hdnEmpuserid.Value, dt1, lblEmpID.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void CreateNewEmployeeList(int EmpID)
        {
            try
            {
                Attendance.BAL.EmployeeBL obj = new EmployeeBL();
                DataTable dt = obj.GetEmployyeDetailsByUserID(EmpID);
                dtNewEmp.Merge(dt);
            }
            catch (Exception ex)
            {

            }
        }
        private void CreateListOFChanges(string EmpName, string userid, DataTable dt, string EmpID)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    DataTable dnew = new DataTable();
                    dtChanges.Merge(dt);
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void GetEditHistory(DateTime startDate, DateTime Enddate)
        {
            Attendance.BAL.Report obj = new Report();
            DataSet ds = obj.GetPayrollEdithistory(startDate, Enddate);
            Session["EditHistory"] = ds;
        }
        protected void rpt1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    Label lblEmpID = (Label)e.Item.FindControl("lblEmpID");
                    Repeater repChld = (Repeater)e.Item.FindControl("repChld");
                    DataView dv = dtChanges.DefaultView;
                    dv.RowFilter = "empid='" + lblEmpID.Text + "'";
                    repChld.DataSource = dv.ToTable();
                    repChld.DataBind();
                }

            }
            catch (Exception ex)
            {

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
        protected void rptNewEmp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    Label lblEmpName = (Label)e.Item.FindControl("lblEmpName");
            //    HiddenField hdnLastname = (HiddenField)e.Item.FindControl("hdnLastname");

            //    lblEmpName.Text = lblEmpName.Text + " " + hdnLastname.Value.ToString().Trim();

            //    Label lblAddress = (Label)e.Item.FindControl("lblAddress");
            //    HiddenField hdnAddress2 = (HiddenField)e.Item.FindControl("hdnAddress2");             
            //    HiddenField hdnZip = (HiddenField)e.Item.FindControl("hdnZip");
            //    HiddenField hdnState = (HiddenField)e.Item.FindControl("hdnState");

            //    Label lblSSN = (Label)e.Item.FindControl("lblSSN");
            //    Label lblHeadSSN = (Label)e.Item.FindControl("lblHeadSSN");
            //    if (lblSSN.Text.Trim() == "")
            //    {
            //        lblHeadSSN.Visible = false;
            //        lblSSN.Visible = false;

            //    }
            //    else
            //    {
            //        lblHeadSSN.Visible = true;
            //        lblSSN.Visible = true;
            //        lblSSN.Text = GeneralFunction.FormatUsSSN(lblSSN.Text);
            //    }
            //    Label lblCounty = (Label)e.Item.FindControl("lblCounty");

            //    Label lblCountyHead = (Label)e.Item.FindControl("lblCountyHead");
            //    if (lblCounty.Text.Trim() == "")
            //    {
            //        lblCountyHead.Visible = false;
            //        lblCounty.Visible = false;
            //    }
            //    else
            //    {
            //        lblCountyHead.Visible = true;
            //        lblCounty.Visible = true;
            //    }

            //    Label lblStart = (Label)e.Item.FindControl("lblStart");
            //    lblStart.Text = Convert.ToDateTime(lblStart.Text).ToString("MM/dd/yyyy");

            //    if (hdnAddress2.Value != "")
            //    {
            //        lblAddress.Text = lblAddress.Text + "</br>" + hdnAddress2.Value;
            //    }
            //    if ((hdnState.Value != "") && (hdnState.Value != "UN"))
            //    {
            //        lblAddress.Text = lblAddress.Text + "</br>" + hdnState.Value;
            //        if (hdnZip.Value != "")
            //        {
            //            lblAddress.Text = lblAddress.Text + ", " + hdnZip.Value;
            //        }
            //    }
            //    else
            //    {
            //        if (hdnZip.Value != "")
            //        {
            //            lblAddress.Text = lblAddress.Text + "</br> " + hdnZip.Value;
            //        }
            //    }

            //    lblAddress.Text = lblAddress.Text == "" ? "N/A" : lblAddress.Text;
            //    Label lblDeductions = (Label)e.Item.FindControl("lblDeductions");
            //    lblDeductions.Text = lblDeductions.Text == "0" ? "N/A" : lblDeductions.Text;
            //}
        }
        protected void repChld_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                PayrollEditHistory obj = new PayrollEditHistory();
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblField = (Label)e.Item.FindControl("lblField");
                    Label lblOldvalue = (Label)e.Item.FindControl("lblOldvalue");
                    Label lblNewValue = (Label)e.Item.FindControl("lblNewValue");
                    Label lblChangedDt = (Label)e.Item.FindControl("lblChangedDt");
                    string point = obj.ChanngeHistory(lblField.Text, lblOldvalue.Text, lblNewValue.Text, lblChangedDt.Text, Session["LocationName"].ToString().Trim());
                    if (point != "")
                    {
                        Label lblPoint = (Label)e.Item.FindControl("lblPoint");
                        lblPoint.Text = "&#8226; &nbsp;"+point;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnPDF_Click1(object sender, EventArgs e)
        {
            try
            {
                //lnkDwnloadPDF.Enabled = true;
                string CurrentDate = Convert.ToDateTime(lblDate2.Text).ToString("MM-dd-yyyy") + Convert.ToDateTime(lblDate2.Text).ToString("hhmmsstt");
                string Location = Session["LocationName"].ToString();
                string PayrollDate = "PAYROLL REPORT " + lblWeekPayrollReport.Text;
                string filepath = Server.MapPath("~/Payroll/" + Location + "/" + CurrentDate + "/");
                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);
                }

                var pdfDoc = new Document(PageSize.A3);
                PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + "Payroll.pdf", FileMode.Create));
                Session["FilePath"] = filepath + "Payroll.pdf";
                pdfDoc.Open();
                var table1 = new PdfPTable(15);
                PdfPCell cell = new PdfPCell();
                // table1.DefaultCell.Border = Rectangle.NO_BORDER;


                table1.WidthPercentage = 100;
                table1.HorizontalAlignment = Element.ALIGN_CENTER;

                iTextSharp.text.Font fntTableFont1 = new Font(Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font fntTableFont = new Font(Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font fntTableHeading = new Font(Font.FontFamily.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD, BaseColor.MAGENTA);

                if (ddlLocation.SelectedItem.Text.ToString() == "INDG" || ddlLocation.SelectedItem.Text.ToString() == "INBH")
                {
                    DataTable dt = GeneralFunction.GetDataTable(grdPayRollIndia);

                    PdfPCell CellZero = new PdfPCell(new Phrase("Employee Information", fntTableFont1));
                    CellZero.Colspan = 8;
                    CellZero.Border = 0;
                    CellZero.BackgroundColor = BaseColor.GRAY;
                    CellZero.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(CellZero);

                    PdfPCell CellZero2 = new PdfPCell(new Phrase("Attendance Info", fntTableFont1));
                    CellZero2.Colspan = 2;
                    CellZero2.Border = 0;
                    CellZero2.BackgroundColor = BaseColor.GRAY;
                    CellZero2.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(CellZero2);


                    PdfPCell CellZero3 = new PdfPCell(new Phrase("Paid days Info", fntTableFont1));
                    CellZero2.Colspan = 2;
                    CellZero2.Border = 0;
                    CellZero2.BackgroundColor = BaseColor.GRAY;
                    CellZero2.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(CellZero2);

                    CellZero2 = new PdfPCell(new Phrase("Salary Info", fntTableFont1));
                    CellZero2.Colspan = 3;
                    CellZero2.Border = 0;
                    CellZero2.BackgroundColor = BaseColor.GRAY;
                    CellZero2.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(CellZero2);
                    PdfPCell[] cel = new PdfPCell[15];
                    PdfPRow row = new PdfPRow(cel);

                    cel[0] = new PdfPCell(new Phrase("EmpID", fntTableFont1));
                    cel[1] = new PdfPCell(new Phrase("Name", fntTableFont1));
                    cel[2] = new PdfPCell(new Phrase("StartDt", fntTableFont1));
                    cel[3] = new PdfPCell(new Phrase("TermDt", fntTableFont1));
                    cel[4] = new PdfPCell(new Phrase("Department", fntTableFont1));
                    cel[5] = new PdfPCell(new Phrase("Type", fntTableFont1));
                    cel[6] = new PdfPCell(new Phrase("Location", fntTableFont1));
                    cel[7] = new PdfPCell(new Phrase("Salary", fntTableFont1));
                    cel[8] = new PdfPCell(new Phrase("Working days", fntTableFont1));
                    cel[9] = new PdfPCell(new Phrase("Attend days", fntTableFont1));
                    cel[10] = new PdfPCell(new Phrase("Used", fntTableFont1));
                    cel[11] = new PdfPCell(new Phrase("Balanced", fntTableFont1));
                    cel[12] = new PdfPCell(new Phrase("Salary", fntTableFont1));
                    cel[13] = new PdfPCell(new Phrase("Is New", fntTableFont1));
                    cel[14] = new PdfPCell(new Phrase("Is Changes", fntTableFont1));

                    table1.Rows.Add(row);

                    for (int i = 0; i < grdPayRollIndia.Rows.Count; i++)
                    {
                        cel[0] = new PdfPCell(new Phrase(grdPayRollIndia.Rows[i].Cells[0].Text.ToString(), fntTableFont));
                        cel[1] = new PdfPCell(new Phrase(dt.Rows[i]["PEmpname"].ToString(), fntTableFont));

                        string stDt = dt.Rows[i]["StartDate"].ToString() == "NULL" ? "" : Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()).ToString("MM/dd/yyyy") == "01/01/1990" ? "" : Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()).ToString("MM/dd/yyyy");
                        string TrDt = dt.Rows[i]["TermDate"].ToString() == "NULL" ? "" : Convert.ToDateTime(dt.Rows[i]["TermDate"].ToString()).ToString("MM/dd/yyyy") == "01/01/1990" ? "" : Convert.ToDateTime(dt.Rows[i]["TermDate"].ToString()).ToString("MM/dd/yyyy");

                        cel[2] = new PdfPCell(new Phrase(stDt, fntTableFont));
                        cel[3] = new PdfPCell(new Phrase(TrDt, fntTableFont));
                        cel[4] = new PdfPCell(new Phrase(dt.Rows[i]["DeptName"].ToString(), fntTableFont));
                        cel[5] = new PdfPCell(new Phrase(dt.Rows[i]["MasterEmpType"].ToString(), fntTableFont));
                        cel[6] = new PdfPCell(new Phrase(dt.Rows[i]["LocationName"].ToString(), fntTableFont));
                        cel[7] = new PdfPCell(new Phrase(dt.Rows[i]["Salary"].ToString(), fntTableFont));
                        cel[8] = new PdfPCell(new Phrase(dt.Rows[i]["Workingdays"].ToString(), fntTableFont));
                        cel[9] = new PdfPCell(new Phrase(dt.Rows[i]["Present"].ToString(), fntTableFont));
                        cel[10] = new PdfPCell(new Phrase(dt.Rows[i]["PaidLeavesUsed"].ToString(), fntTableFont));
                        cel[11] = new PdfPCell(new Phrase(dt.Rows[i]["PaidLeavesBalanced"].ToString(), fntTableFont));
                        cel[12] = new PdfPCell(new Phrase(dt.Rows[i]["TotalPay"].ToString(), fntTableFont));
                        cel[13] = new PdfPCell(new Phrase(dt.Rows[i]["empid"].ToString(), fntTableFont));
                        cel[14] = new PdfPCell(new Phrase(dt.Rows[i]["empid"].ToString(), fntTableFont));
                        table1.Rows.Add(row);
                    }
                    pdfDoc.Add(table1);
                    table1.Rows.Clear();
                    table1.WidthPercentage = 100;
                    pdfDoc.Close();
                    Response.Write(pdfDoc);
                    showpdf(Session["FilePath"].ToString());
                    Response.End();
                }





                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);
                //dvpayrollreport.RenderControl(hw);
                //StringReader sr = new StringReader(sw.ToString());
                //Document pdfDoc = new Document(new Rectangle(1200f, 1800f));
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + "Payroll.pdf", FileMode.Create));
                //Session["FilePath"] = filepath + "Payroll.pdf";
                //pdfDoc.Open();

                //iTextSharp.text.Font georgia = FontFactory.GetFont("georgia", 20f, 1, new BaseColor(System.Drawing.Color.Brown));
                ////georgia.Color = Color.Gray;
                //Chunk beginning = new Chunk(PayrollDate, georgia);
                //Phrase p1 = new Phrase(beginning);
                //Phrase p2 = new Phrase();
                //pdfDoc.Add(new Paragraph(p1));
                ////pdfDoc.Add(new Paragraph(30f, "Created at" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + " by " + Session["EmpName"].ToString().Trim()));
                ////pdfDoc.AddTitle(PayrollDate);
                //htmlparser.Parse(sr);
                //pdfDoc.Close();
                //Response.Write(pdfDoc);
                //showpdf(Session["FilePath"].ToString());
                //Response.End();

            }
            catch (Exception ex)
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void showpdf(string filepath)
        {
            try
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "hidespinner();", true);
                Response.Redirect("PayRollDownLoad.aspx");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnDoc_Click1(object sender, EventArgs e)
        {
            try
            {

                //lnkDwnloadPDF.Enabled = true;
                string CurrentDate = Convert.ToDateTime(lblDate2.Text).ToString("MM-dd-yyyy") + Convert.ToDateTime(lblDate2.Text).ToString("hhmmsstt");
                string Location = Session["LocationName"].ToString();
                string PayrollDate = "PAYROLL REPORT ";
                //string filepath = Server.MapPath("~/Payroll/" + Location + "/" + CurrentDate + "/");
                //if (!System.IO.Directory.Exists(filepath))
                //{
                //    System.IO.Directory.CreateDirectory(filepath);
                //}
                
                Response.ClearContent();
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}",CurrentDate+".doc"));
                Response.Charset = "";
                Response.ContentType = "application/ms-word";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dvpayrollreport.RenderControl(htw);
                Response.Write("<h2 style=\"font-size:12px; font-weight:normal; font-family:\"Century Gothic\">"+PayrollDate+"</br>"+lblWeekPayrollReport.Text+"</h2>");
                Response.Write(sw.ToString());

                Response.End();

            }
            catch (Exception ex)
            {
            }
        }
        protected void grdNewEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblEmpName = (Label)e.Row.FindControl("lblEmpName");
                    HiddenField hdnLastname = (HiddenField)e.Row.FindControl("hdnLastname");

                    lblEmpName.Text = lblEmpName.Text + " " + hdnLastname.Value.ToString().Trim();

                    Label lblAddress = (Label)e.Row.FindControl("lblAddress");
                    HiddenField hdnAddress2 = (HiddenField)e.Row.FindControl("hdnAddress2");
                    HiddenField hdnZip = (HiddenField)e.Row.FindControl("hdnZip");
                    HiddenField hdnState = (HiddenField)e.Row.FindControl("hdnState");
                    Label lblSSN = (Label)e.Row.FindControl("lblSSN");           
                    if (lblSSN.Text.Trim() == "")
                    {
                       grdNewEmp.Columns[7].Visible=false;
                    }
                    else
                    {
                       grdNewEmp.Columns[8].Visible=true;
                       lblSSN.Text = GeneralFunction.FormatUsSSN(lblSSN.Text);
                    }
                    Label lblCounty = (Label)e.Row.FindControl("lblCounty");

                   // Label lblCountyHead = (Label)e.Row.FindControl("lblCountyHead");
                    if (lblCounty.Text.Trim() == "")
                    {
                       grdNewEmp.Columns[8].Visible=false;
                    }
                    else
                    {
                          grdNewEmp.Columns[8].Visible=true;
                    }

                    Label lblStart = (Label)e.Row.FindControl("lblStart");
                    lblStart.Text = Convert.ToDateTime(lblStart.Text).ToString("MM/dd/yyyy");

                    if (hdnAddress2.Value != "")
                    {
                        lblAddress.Text = lblAddress.Text + ",</br>" + hdnAddress2.Value;
                    }
                    if ((hdnState.Value != "") && (hdnState.Value != "UN"))
                    {
                        lblAddress.Text = lblAddress.Text + ",</br>" + hdnState.Value;
                        if (hdnZip.Value != "")
                        {
                            lblAddress.Text = lblAddress.Text + " " + hdnZip.Value+".";
                        }
                    }
                    else
                    {
                        if (hdnZip.Value != "")
                        {
                            lblAddress.Text = lblAddress.Text + ",</br> " + hdnZip.Value;
                        }
                    }

                    lblAddress.Text = lblAddress.Text == "" ? "N/A" : lblAddress.Text;
                    Label lblDeductions = (Label)e.Row.FindControl("lblDeductions");
                    lblDeductions.Text = lblDeductions.Text == "0" ? "N/A" : lblDeductions.Text;

                    Label lblBirthDate = (Label)e.Row.FindControl("lblBirthDate");
                    if (lblBirthDate.Text.Trim() == "01/01/1900")
                    {
                        lblBirthDate.Text = "";
                    }
                }
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
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int userid = Convert.ToInt32(Session["UserID"]);
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
                DateTime TodayDate = Convert.ToDateTime(Session["TodayBannerDate"]);

                if (Session["IsAdmin"].ToString() == "True")
                {

                    if (ddlLocation.SelectedItem.Text.Trim() == "INBH" || ddlLocation.SelectedItem.Text.Trim() == "INDG")
                    {
                        DateTime CurrentDate = Convert.ToDateTime(ISTTime.ToString("MM/dd/yyyy"));
                        CurrentDate = CurrentDate.AddMonths(-1);
                        DateTime MonthStart = CurrentDate.AddDays(1 - CurrentDate.Day);
                        DateTime MonthEnd = MonthStart.AddMonths(1).AddSeconds(-1);

                        txtToDate.Text = MonthEnd.ToString("MM/dd/yyyy");
                        txtFromDate.Text = MonthStart.ToString("MM/dd/yyyy");

                        ViewState["StartRptDt"] = MonthStart;
                        ViewState["EndRptDt"] = MonthEnd;


                        Attendance.BAL.Report obj = new Report();
                        DateTime Count = obj.GetFreezedDate(MonthEnd, ddlLocation.SelectedItem.Text.Trim());
                        if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                        {
                            lblFreeze.Text = "";
                        }
                        else
                        {
                            lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                        }

                        bool final = obj.GetFinalPayrollDate(MonthStart, Convert.ToInt32(ddlLocation.SelectedItem.Value));

                        if (final)
                        {
                            btnFinal.CssClass = "btn btn-small btn-warning disabled";
                            btnSave.CssClass = "btn btn-small btn-warning disabled";
                            btnFinal.Enabled = false;
                            btnSave.Enabled = false;
                            hdnFreeze.Value = "true"; ;
                            // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "delEditLabelCss();", true);
                        }
                        else
                        {
                            btnFinal.CssClass = "btn btn-small btn-warning";
                            btnSave.CssClass = "btn btn-small btn-warning";
                            btnFinal.Enabled = true;
                            btnSave.Enabled = true;
                            hdnFreeze.Value = "false";
                            // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "addEditLabelCss();", true);
                        }
                        DataTable dt = GetReportIndia(MonthStart, MonthEnd, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                        lblWeekPayrollReport.Text = "( " + MonthStart.ToString("MM/dd/yyyy") + " - " + MonthEnd.ToString("MM/dd/yyyy") + " )";
                        GetEditHistory(MonthStart, MonthEnd);
                        lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                        lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                        grdPayRollIndia.DataSource = dt;
                        grdPayRollIndia.DataBind();
                        Session["Indiapayroll"] = (DataTable)grdPayRollIndia.DataSource;

                        grdPayRoll.DataSource = null;
                        grdPayRoll.DataBind();
                        btnSave.Visible = true;
                        btnFinal.Visible = true;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        DateTime StartDate = GeneralFunction.GetFirstDayOfWeekDate(TodayDate);
                        DateTime EndDate = StartDate.AddDays(-14);

                        Attendance.BAL.Report obj = new Report();
                        DateTime Count = obj.GetFreezedDate(EndDate, Session["LocationName"].ToString());
                        if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                        {
                            lblFreeze.Text = "";
                        }
                        else
                        {
                            lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                        }
                        txtToDate.Text = StartDate.AddDays(-1).ToString("MM/dd/yyyy");
                        txtFromDate.Text = EndDate.ToString("MM/dd/yyyy");
                        ViewState["StartRptDt"] = EndDate;
                        ViewState["EndRptDt"] = StartDate.AddDays(-1);
                        GetReport(EndDate, StartDate.AddDays(-1), 0, ddlLocation.SelectedItem.Text.Trim());
                        btnSave.Visible = false;
                        btnFinal.Visible = false;
                        btnPrint.Visible = false;
                        rppayslip.DataSource = null;
                        rppayslip.DataBind();
                    }

                    lblGrdLocaton.Visible = true;
                    ddlLocation.Visible = true;
                }
                else
                {
                    if (ddlLocation.SelectedItem.Text.Trim() == "INBH" || ddlLocation.SelectedItem.Text.Trim() == "INDG")
                    {
                        DateTime CurrentDate = Convert.ToDateTime(ISTTime.ToString("MM/dd/yyyy"));
                        CurrentDate = CurrentDate.AddMonths(-1);
                        DateTime MonthStart = CurrentDate.AddDays(1 - CurrentDate.Day);
                        DateTime MonthEnd = MonthStart.AddMonths(1).AddSeconds(-1);

                        txtToDate.Text = MonthEnd.ToString("MM/dd/yyyy");
                        txtFromDate.Text = MonthStart.ToString("MM/dd/yyyy");

                        ViewState["StartRptDt"] = MonthStart;
                        ViewState["EndRptDt"] = MonthEnd;

                        Attendance.BAL.Report obj = new Report();
                        DateTime Count = obj.GetFreezedDate(MonthEnd, Session["LocationName"].ToString());
                        if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                        {
                            lblFreeze.Text = "";
                        }
                        else
                        {
                            lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                        }

                        bool final = obj.GetFinalPayrollDate(MonthStart, Convert.ToInt32(ddlLocation.SelectedItem.Value));

                        if (final)
                        {
                            btnFinal.CssClass = "btn btn-small btn-warning disabled";
                            btnSave.CssClass = "btn btn-small btn-warning disabled";
                            btnFinal.Enabled = false;
                            btnSave.Enabled = false;
                            hdnFreeze.Value = "true"; ;
                            // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "delEditLabelCss();", true);
                        }
                        else
                        {
                            btnFinal.CssClass = "btn btn-small btn-warning";
                            btnSave.CssClass = "btn btn-small btn-warning";
                            btnFinal.Enabled = true;
                            btnSave.Enabled = true;
                            hdnFreeze.Value = "false";
                            // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "addEditLabelCss();", true);
                        }


                        DataTable dt = GetReportIndia(MonthStart, MonthEnd, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                        lblWeekPayrollReport.Text = "( " + MonthStart.ToString("MM/dd/yyyy") + " - " + MonthEnd.ToString("MM/dd/yyyy") + " )";
                        GetEditHistory(MonthStart, MonthEnd);
                        lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                        lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                        grdPayRollIndia.DataSource = dt;
                        grdPayRollIndia.DataBind();
                        Session["Indiapayroll"] = (DataTable)grdPayRollIndia.DataSource;

                        grdPayRoll.DataSource = null;
                        grdPayRoll.DataBind();
                        btnSave.Visible = true;
                        btnFinal.Visible = true;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        DateTime StartDate = GeneralFunction.GetFirstDayOfWeekDate(TodayDate);
                        DateTime EndDate = StartDate.AddDays(-14);

                        Attendance.BAL.Report obj = new Report();
                        DateTime Count = obj.GetFreezedDate(EndDate, Session["LocationName"].ToString());
                        if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                        {
                            lblFreeze.Text = "";
                        }
                        else
                        {
                            lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                        }


                        txtToDate.Text = StartDate.AddDays(-1).ToString("MM/dd/yyyy");
                        txtFromDate.Text = EndDate.ToString("MM/dd/yyyy");
                        ViewState["StartRptDt"] = EndDate;
                        ViewState["EndRptDt"] = StartDate.AddDays(-1);
                        GetReport(EndDate, StartDate.AddDays(-1), 0, ddlLocation.SelectedItem.Text.Trim());
                        btnSave.Visible = false;
                        btnFinal.Visible = false;
                        btnPrint.Visible = false;
                        rppayslip.DataSource = null;
                        rppayslip.DataBind();
                    }

                    lblGrdLocaton.Visible = true;
                    ddlLocation.Enabled = false;

                }
                BindListOfNewEmployee();
                BindListofChanges();

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
            }
            catch (Exception ex)
            {

            }
        }
        protected void grdPayRollIndia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsEdit = Session["EditHistory"] as DataSet;
            DataTable dt = dsEdit.Tables[0];
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblEmpID = (Label)e.Row.FindControl("lblEmpID");
                    Label lblEmpFirstname = (Label)e.Row.FindControl("lblEmpFirstname");
                    Label lblStartedDate = (Label)e.Row.FindControl("lblStartedDate");
                    lblStartedDate.Text = lblStartedDate.Text == "01/01/1900" ? "" : (lblStartedDate.Text);
                    Label lblTerminatedDate = (Label)e.Row.FindControl("lblTerminatedDate");
                    lblTerminatedDate.Text = lblTerminatedDate.Text == "01/01/1900" ? "" : lblTerminatedDate.Text;
                    DateTime Start = Convert.ToDateTime(ViewState["StartRptDt"]);
                    DateTime End = Convert.ToDateTime(ViewState["EndRptDt"]);
                    Label lblSalary = (Label)e.Row.FindControl("lblSalary");
                    Label lblLeavesAvailable = (Label)e.Row.FindControl("lblLeavesAvailable");
                    Label lblLeaves = (Label)e.Row.FindControl("lblLeaves");

                    if (lblStartedDate.Text != "" && (Convert.ToDateTime(lblStartedDate.Text) >= Start && Convert.ToDateTime(lblStartedDate.Text) <= End))
                    {
                        HiddenField hdnEmpuserid = (HiddenField)e.Row.FindControl("hdnEmpuserid");
                        Label lblIsNew = (Label)e.Row.FindControl("lblIsNew");
                        lblIsNew.Text = "Yes";
                        CreateNewEmployeeList(Convert.ToInt32(hdnEmpuserid.Value));
                    }
                    else
                    {
                        HiddenField hdnEmpuserid = (HiddenField)e.Row.FindControl("hdnEmpuserid");
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = "RecordID=" + Convert.ToInt32(hdnEmpuserid.Value);
                        DataTable dt1 = dv.ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            Label lblIsChanges = (Label)e.Row.FindControl("lblIsChanges");
                            lblIsChanges.Text = "Yes";
                            CreateListOFChanges(lblEmpFirstname.Text, hdnEmpuserid.Value, dt1, lblEmpID.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(ViewState["StartRptDt"]);
                DateTime EndDate = Convert.ToDateTime(ViewState["EndRptDt"]);
                Report obj = new Report();
                for (int i = 0; i < grdPayRollIndia.Rows.Count; i++)
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

                    //Atndence history
                    Attendance.Entities.AttendenceInfo objInfo = new Attendance.Entities.AttendenceInfo();
                    HiddenField hdnEmpuserid = (HiddenField)grdPayRollIndia.Rows[i].FindControl("hdnEmpuserid");
                    Label lblWorkingDays = (Label)grdPayRollIndia.Rows[i].FindControl("lblWorkingDays");
                    Label lblAttendDays = (Label)grdPayRollIndia.Rows[i].FindControl("lblAttendDays");
                    Label lblLeaves = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeaves");
                    Label lblNoshow = (Label)grdPayRollIndia.Rows[i].FindControl("lblNoshow");
                    Label lblLeavesAvailable = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeavesAvailable");
                    Label lblLeavesUsed = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeavesUsed");
                    Label lblPaidLeavesBalanced = (Label)grdPayRollIndia.Rows[i].FindControl("lblPaidLeavesBalanced");
                    Label lblCalLeaves = (Label)grdPayRollIndia.Rows[i].FindControl("lblCalLeaves");
                   
                    objInfo.Mnth = Convert.ToInt32(dt.ToString("MM"));
                    objInfo.Yr = Convert.ToInt32(dt.ToString("yyyy"));
                    objInfo.EnterBy=Convert.ToInt32(Session["UserID"]);
                    objInfo.EnterDate = ISTTime;
                    objInfo.TotalCalLeaves1 = lblCalLeaves.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblCalLeaves.Text.ToString().Trim());
                    objInfo.PaidLeavesUsed = lblLeavesUsed.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblLeavesUsed.Text.ToString().Trim());
                    objInfo.PaidLeavesBalanced = lblPaidLeavesBalanced.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblPaidLeavesBalanced.Text.ToString().Trim());
                    objInfo.PaidLeaves = lblLeavesAvailable.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblLeavesAvailable.Text.ToString().Trim());
                    objInfo.NoShow = lblNoshow.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblNoshow.Text.ToString().Trim());
                    objInfo.Leaves = lblLeaves.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblLeaves.Text.ToString().Trim());
                    objInfo.WorkingDays = lblWorkingDays.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblWorkingDays.Text.ToString().Trim());
                    objInfo.AttendDays = lblAttendDays.Text.ToString().Trim() == "" ? 0: Convert.ToSingle(lblAttendDays.Text.ToString().Trim());
                    objInfo.Userid = Convert.ToInt32(hdnEmpuserid.Value);

                    int AtnLogID = obj.SaveAttendanceHistory(objInfo);

                    //Salary history

                    Label lblCalSalary = (Label)grdPayRollIndia.Rows[i].FindControl("lblCalSalary");
                    TextBox txtBonus = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtBonus");
                    TextBox txtIncentives = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtIncentives");
                    TextBox txtPrevUnpaid = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtPrevUnpaid");
                    TextBox txtAdvancePaid = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtAdvancePaid");
                    TextBox txtExpenses = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtExpenses");
                    TextBox txtLoanDeduct = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtLoanDeduct");
                    Label lblTotal = (Label)grdPayRollIndia.Rows[i].FindControl("lblTotal");


                    Attendance.Entities.SalaryInfo objSal = new Attendance.Entities.SalaryInfo();
                    objSal.AtnLogID = AtnLogID;
                    objSal.CalSalary = lblCalSalary.Text.Trim() == "" ? 0 : Convert.ToSingle(lblCalSalary.Text.Trim());
                    objSal.Bonus = txtBonus.Text.Trim() == "" ? 0 : Convert.ToSingle(txtBonus.Text.Trim());
                    objSal.Incentives = txtIncentives.Text.Trim() == "" ? 0 : Convert.ToSingle(txtIncentives.Text.Trim());
                    objSal.PrevUnpaid = txtPrevUnpaid.Text.Trim() == "" ? 0 : Convert.ToSingle(txtPrevUnpaid.Text.Trim());
                    objSal.AdvancePaid = txtAdvancePaid.Text.Trim() == "" ? 0 : Convert.ToSingle(txtAdvancePaid.Text.Trim());
                    objSal.Expenses = txtExpenses.Text.Trim() == "" ? 0 : Convert.ToSingle(txtExpenses.Text.Trim());
                    objSal.LoanDeduct = txtLoanDeduct.Text.Trim() == "" ? 0 : Convert.ToSingle(txtLoanDeduct.Text.Trim());
                    objSal.TotalPay = lblTotal.Text.Trim() == "" ? 0 : Convert.ToSingle(lblTotal.Text.Trim());
                    objSal.Mnth = objInfo.Mnth;
                    objSal.Years = objInfo.Yr;
                    objSal.EnterBy = objInfo.EnterBy;
                    objSal.EnteredDate = objInfo.EnterDate;

                    string notes = "";
                    if (txtBonus.Attributes["notes"] != null)
                    {
                        if (txtBonus.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes = txtBonus.Attributes["notes"].ToString().Trim();
                            notes += "<br>....................<br> Bonus enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    if (txtIncentives.Attributes["notes"] != null)
                    {
                        if (txtIncentives.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtIncentives.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Incentives enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    if (txtPrevUnpaid.Attributes["notes"] != null)
                    {
                        if (txtPrevUnpaid.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtPrevUnpaid.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Previous unpaid enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    if (txtAdvancePaid.Attributes["notes"] != null)
                    {
                        if (txtAdvancePaid.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtAdvancePaid.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Advance paid enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    if (txtExpenses.Attributes["notes"] != null)
                    {
                        if (txtExpenses.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtExpenses.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Expenses enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }

                    if (txtLoanDeduct.Attributes["notes"] != null)
                    {
                        if (txtLoanDeduct.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtLoanDeduct.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Loan deduction enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    objSal.InternalNotes = notes;
                    bool bnew = obj.SaveSalaryHistory(objSal);
                }

                bool final = obj.GetFinalPayrollDate(dt, Convert.ToInt32(ddlLocation.SelectedItem.Value));

                if (final)
                {
                    btnFinal.CssClass = "btn btn-small btn-warning disabled";
                    btnSave.CssClass = "btn btn-small btn-warning disabled";
                    btnFinal.Enabled = false;
                    btnSave.Enabled = false;
                    hdnFreeze.Value = "true"; ;
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "delEditLabelCss();", true);
                }
                else
                {
                    btnFinal.CssClass = "btn btn-small btn-warning";
                    btnSave.CssClass = "btn btn-small btn-warning";
                    btnFinal.Enabled = true;
                    btnSave.Enabled = true;
                    hdnFreeze.Value = "false";
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "addEditLabelCss();", true);
                }

                DataTable dst=GetReportIndia(dt, EndDate, Convert.ToInt32(ddlLocation.SelectedValue));
                lblWeekPayrollReport.Text = "( " + dt.ToString("MM/dd/yyyy") + " - " + EndDate.ToString("MM/dd/yyyy") + " )";
                GetEditHistory(dt, EndDate);
              //  lblTotal.Text = "Employee record count: " + dst.Rows.Count.ToString().Trim();
                lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                grdPayRollIndia.DataSource = dst;
                grdPayRollIndia.DataBind();
                Session["Indiapayroll"] = (DataTable)grdPayRollIndia.DataSource;

                grdPayRoll.DataSource = null;
                grdPayRoll.DataBind();
                btnSave.Visible = true;
                btnFinal.Visible = true;
                btnPrint.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnFinal_Click(object sender, EventArgs e)
        {
            try
            {
                Report obj = new Report();
                DateTime dt = Convert.ToDateTime(ViewState["StartRptDt"]);
                DateTime EndDate = Convert.ToDateTime(ViewState["EndRptDt"]);
                int locationID = Convert.ToInt32(ddlLocation.SelectedValue);
                for (int i = 0; i < grdPayRollIndia.Rows.Count; i++)
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

                    //Atndence history
                    Attendance.Entities.AttendenceInfo objInfo = new Attendance.Entities.AttendenceInfo();
                    HiddenField hdnEmpuserid = (HiddenField)grdPayRollIndia.Rows[i].FindControl("hdnEmpuserid");
                    Label lblWorkingDays = (Label)grdPayRollIndia.Rows[i].FindControl("lblWorkingDays");
                    Label lblAttendDays = (Label)grdPayRollIndia.Rows[i].FindControl("lblAttendDays");
                    Label lblLeaves = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeaves");
                    Label lblNoshow = (Label)grdPayRollIndia.Rows[i].FindControl("lblNoshow");
                    Label lblLeavesAvailable = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeavesAvailable");
                    Label lblLeavesUsed = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeavesUsed");
                    Label lblPaidLeavesBalanced = (Label)grdPayRollIndia.Rows[i].FindControl("lblPaidLeavesBalanced");
                    Label lblCalLeaves = (Label)grdPayRollIndia.Rows[i].FindControl("lblCalLeaves");
                   
                    objInfo.Mnth = Convert.ToInt32(dt.ToString("MM"));
                    objInfo.Yr = Convert.ToInt32(dt.ToString("yyyy"));
                    objInfo.EnterBy = Convert.ToInt32(Session["UserID"]);
                    objInfo.EnterDate = ISTTime;
                    objInfo.TotalCalLeaves1 = lblCalLeaves.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblCalLeaves.Text.ToString().Trim());
                    objInfo.PaidLeavesUsed = lblLeavesUsed.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblLeavesUsed.Text.ToString().Trim());
                    objInfo.PaidLeavesBalanced = lblPaidLeavesBalanced.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblPaidLeavesBalanced.Text.ToString().Trim());
                    objInfo.PaidLeaves = lblLeavesAvailable.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblLeavesAvailable.Text.ToString().Trim());
                    objInfo.NoShow = lblNoshow.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblNoshow.Text.ToString().Trim());
                    objInfo.Leaves = lblLeaves.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblLeaves.Text.ToString().Trim());
                    objInfo.WorkingDays = lblWorkingDays.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblWorkingDays.Text.ToString().Trim());
                    objInfo.AttendDays = lblAttendDays.Text.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblAttendDays.Text.ToString().Trim());
                    objInfo.Userid = Convert.ToInt32(hdnEmpuserid.Value);

                    int AtnLogID = obj.SaveAttendanceHistory(objInfo);

                    String strHostName = Request.UserHostAddress.ToString();
                    string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                    bool bnewp = obj.UpdatePaidLeavesDetAfterFinalPayroll(objInfo, dt, strIp);
                    //Salary history

                    Label lblCalSalary = (Label)grdPayRollIndia.Rows[i].FindControl("lblCalSalary");
                    TextBox txtBonus = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtBonus");
                    TextBox txtIncentives = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtIncentives");
                    TextBox txtPrevUnpaid = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtPrevUnpaid");
                    TextBox txtAdvancePaid = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtAdvancePaid");
                    TextBox txtExpenses = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtExpenses");
                    TextBox txtLoanDeduct = (TextBox)grdPayRollIndia.Rows[i].FindControl("txtLoanDeduct");
                    Label lblTotal = (Label)grdPayRollIndia.Rows[i].FindControl("lblTotal");

                    Attendance.Entities.SalaryInfo objSal = new Attendance.Entities.SalaryInfo();
                    objSal.AtnLogID = AtnLogID;
                    objSal.CalSalary = lblCalSalary.Text.Trim() == "" ? 0 : Convert.ToSingle(lblCalSalary.Text.Trim());
                    objSal.Bonus = txtBonus.Text.Trim() == "" ? 0 : Convert.ToSingle(txtBonus.Text.Trim());
                    objSal.Incentives = txtIncentives.Text.Trim() == "" ? 0 : Convert.ToSingle(txtIncentives.Text.Trim());
                    objSal.PrevUnpaid = txtPrevUnpaid.Text.Trim() == "" ? 0 : Convert.ToSingle(txtPrevUnpaid.Text.Trim());
                    objSal.AdvancePaid = txtAdvancePaid.Text.Trim() == "" ? 0 : Convert.ToSingle(txtAdvancePaid.Text.Trim());
                    objSal.Expenses = txtExpenses.Text.Trim() == "" ? 0 : Convert.ToSingle(txtExpenses.Text.Trim());
                    objSal.LoanDeduct = txtLoanDeduct.Text.Trim() == "" ? 0 : Convert.ToSingle(txtLoanDeduct.Text.Trim());
                    objSal.TotalPay = lblTotal.Text.Trim() == "" ? 0 : Convert.ToSingle(lblTotal.Text.Trim());
                    objSal.Mnth = objInfo.Mnth;
                    objSal.Years = objInfo.Yr;
                    objSal.EnterBy = objInfo.EnterBy;
                    objSal.EnteredDate = objInfo.EnterDate;

                    string notes = "";
                    if (txtBonus.Attributes["notes"] != null)
                    {
                        if (txtBonus.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes = txtBonus.Attributes["notes"].ToString().Trim();
                            notes += "<br>....................<br> Bonus enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    if (txtIncentives.Attributes["notes"] != null)
                    {
                        if (txtIncentives.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtIncentives.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Incentives enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    if (txtPrevUnpaid.Attributes["notes"] != null)
                    {
                        if (txtPrevUnpaid.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtPrevUnpaid.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Previous unpaid enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    if (txtAdvancePaid.Attributes["notes"] != null)
                    {
                        if (txtAdvancePaid.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtAdvancePaid.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Advance paid enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    if (txtExpenses.Attributes["notes"] != null)
                    {
                        if (txtExpenses.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtExpenses.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Expenses enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }

                    if (txtLoanDeduct.Attributes["notes"] != null)
                    {
                        if (txtLoanDeduct.Attributes["notes"].ToString().Trim() != "")
                        {
                            notes += txtLoanDeduct.Attributes["notes"].ToString();
                            notes += "<br>....................<br> Loan deduction enter by" + lblEmployyName.Text.Trim() + " at " + objSal.EnteredDate + "<br>";
                        }
                    }
                    objSal.InternalNotes = notes;
                    bool bnew = obj.SaveSalaryHistory(objSal);

                }
                bool bnewf = obj.FinalizePayrollReport(locationID, dt, Convert.ToInt32(Session["UserID"]));
                bool final = obj.GetFinalPayrollDate(dt, Convert.ToInt32(ddlLocation.SelectedItem.Value));

                if (final)
                {
                    btnFinal.CssClass = "btn btn-small btn-warning disabled";
                    btnSave.CssClass = "btn btn-small btn-warning disabled";
                    btnFinal.Enabled = false;
                    btnSave.Enabled = false;
                    hdnFreeze.Value = "true"; ;
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "delEditLabelCss();", true);
                }
                else
                {
                    btnFinal.CssClass = "btn btn-small btn-warning";
                    btnSave.CssClass = "btn btn-small btn-warning";
                    btnFinal.Enabled = true;
                    btnSave.Enabled = true;
                    hdnFreeze.Value = "false";
                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "addEditLabelCss();", true);
                }

                DataTable dst = GetReportIndia(dt, EndDate, Convert.ToInt32(ddlLocation.SelectedValue));
                lblWeekPayrollReport.Text = "( " + dt.ToString("MM/dd/yyyy") + " - " + EndDate.ToString("MM/dd/yyyy") + " )";
                GetEditHistory(dt, EndDate);
             
                //lblTotal.Text = "Employee record count: " + dst.Rows.Count.ToString().Trim();
                lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                grdPayRollIndia.DataSource = dst;
                grdPayRollIndia.DataBind();
                Session["Indiapayroll"] = (DataTable)grdPayRollIndia.DataSource;

                btnSave.Visible = true;
                btnFinal.Visible = true;
                btnPrint.Visible = true;
                grdPayRoll.DataSource = null;
                grdPayRoll.DataBind();

                //DataTable xmlDt = (DataTable)grdPayRollIndia.DataSource;
                //xmlDt.TableName = "Payslip";
                //xmlDt.WriteXml("/myxml.xml", true);

            }
            catch (Exception ex)
            {

            }
        }
        private void GetReport(DateTime StartDate, DateTime EndTime, int userid, string Location)
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataSet ds = obj.GetPayrollReport(StartDate, EndTime, userid, Location);
                lblWeekPayrollReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndTime.ToString("MM/dd/yyyy") + " )";
                GetEditHistory(StartDate, EndTime);
                lblTotal.Text = ds.Tables[0].Rows[0]["LocDescriptiom"].ToString().Trim() == "" ? "Employee record count: " + ds.Tables[0].Rows.Count.ToString().Trim() : ds.Tables[0].Rows[0]["LocDescriptiom"].ToString().Trim() + "  location; Employee record count: " + ds.Tables[0].Rows.Count.ToString().Trim();
                lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                grdPayRoll.DataSource = ds.Tables[0];
                grdPayRoll.DataBind();

                grdPayRollIndia.DataSource = null;
                grdPayRollIndia.DataBind();
                Session["Indiapayroll"] = (DataTable)grdPayRollIndia.DataSource;
            }
            catch (Exception ex)
            {
            }
        }
        private DataTable GetReportIndia(DateTime StartDate, DateTime EndTime, int LocationID)
        {

            DataTable dtPayroll = new DataTable();
            dtPayroll.Columns.Add("empid", typeof(string));
            dtPayroll.Columns.Add("Empname", typeof(string));
            dtPayroll.Columns.Add("PEmpname", typeof(string));
            dtPayroll.Columns.Add("Termdate", typeof(DateTime));
            dtPayroll.Columns.Add("Startdate", typeof(DateTime));
            dtPayroll.Columns.Add("DeptName", typeof(string));
            dtPayroll.Columns.Add("LocationName", typeof(string));
            dtPayroll.Columns.Add("MasterEmpType", typeof(string));
            dtPayroll.Columns.Add("LocDescriptiom", typeof(string));

            dtPayroll.Columns.Add("userid", typeof(string));
            dtPayroll.Columns.Add("Workingdays", typeof(int));
            dtPayroll.Columns.Add("Present", typeof(double));
            dtPayroll.Columns.Add("paidLeaves", typeof(int));
            dtPayroll.Columns.Add("Leaves", typeof(int));
            dtPayroll.Columns.Add("Holidays", typeof(int));
            dtPayroll.Columns.Add("LeavesAvailable", typeof(int));
            dtPayroll.Columns.Add("PaidLeaveStartDt", typeof(DateTime));
            dtPayroll.Columns.Add("PaidLeavesBalanced", typeof(int));
            dtPayroll.Columns.Add("PaidLeavesUsed", typeof(int));
            dtPayroll.Columns.Add("CalLeaves", typeof(int));
            dtPayroll.Columns.Add("Salary", typeof(double));
            dtPayroll.Columns.Add("Noshow", typeof(int));

            dtPayroll.Columns.Add("CalSalary", typeof(double));
            dtPayroll.Columns.Add("Bonus", typeof(double));
            dtPayroll.Columns.Add("Incentives", typeof(double));
            dtPayroll.Columns.Add("PrevUnpaid", typeof(double));
            dtPayroll.Columns.Add("AdvancePaid", typeof(double));
            dtPayroll.Columns.Add("Expenses", typeof(double));
            dtPayroll.Columns.Add("LoanDeduct", typeof(double));
            dtPayroll.Columns.Add("TotalPay", typeof(double));
            try
            {
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_FnAdmin]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", LocationID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", StartDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@EndDate", EndTime));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                EmployeeBL obj = new EmployeeBL();

                // DataTable dtPaid = GetPayrollDataByLoc(LocationID, StartDate.AddMonths(-1), StartDate.AddSeconds(-1));
                DataTable dtPaid1 = obj.Usp_GetEmployeePayrollDataByLocation(LocationID, StartDate, EndTime);
                DataTable dtPaid = obj.GetEmpPaidleavesDetailsByLocation(LocationID, StartDate.AddMonths(-1), StartDate.AddSeconds(-1));
                int days = (Convert.ToInt32(EndTime.ToString("dd")) - Convert.ToInt32(StartDate.ToString("dd"))) + 1;
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables.Count > 1)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            dtPayroll.Rows.Add();
                            if (dtPaid1.Rows.Count > 0)
                            {

                                DataView dvP = dtPaid1.DefaultView;
                                DataTable dtPaidLev = new DataTable();
                                dvP.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                                dtPaidLev = dvP.ToTable();
                                dtPayroll.Rows[j]["userid"] = ds.Tables[0].Rows[j]["userid"].ToString();
                                dtPayroll.Rows[j]["empid"] = ds.Tables[0].Rows[j]["empid"].ToString();
                                dtPayroll.Rows[j]["Empname"] = ds.Tables[0].Rows[j]["firstName"].ToString() + " " + ds.Tables[0].Rows[j]["lastname"].ToString();
                                dtPayroll.Rows[j]["PEmpname"] = ds.Tables[0].Rows[j]["PfirstName"].ToString() + " " + ds.Tables[0].Rows[j]["Plastname"].ToString();
                                dtPayroll.Rows[j]["Termdate"] = ds.Tables[0].Rows[j]["Termdate"].ToString() == "NULL" ? Convert.ToDateTime("01/01/1900") : ds.Tables[0].Rows[j]["Termdate"].ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[j]["Termdate"]).ToString("MM/dd/yyyy"));
                                dtPayroll.Rows[j]["Startdate"] = ds.Tables[0].Rows[j]["Startdate"].ToString() == "NULL" ? Convert.ToDateTime("01/01/1900") : ds.Tables[0].Rows[j]["Startdate"].ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[j]["Startdate"]).ToString("MM/dd/yyyy"));
                                dtPayroll.Rows[j]["DeptName"] = ds.Tables[0].Rows[j]["DeptName"].ToString() == "NULL" ? "" : ds.Tables[0].Rows[j]["DeptName"].ToString();
                                dtPayroll.Rows[j]["LocationName"] = ds.Tables[0].Rows[j]["LocationName"].ToString() == "NULL" ? "" : ds.Tables[0].Rows[j]["LocationName"].ToString();
                                dtPayroll.Rows[j]["MasterEmpType"] = ds.Tables[0].Rows[j]["EmpType"].ToString() == "NULL" ? "" : ds.Tables[0].Rows[j]["EmpType"].ToString();
                                dtPayroll.Rows[j]["LocDescriptiom"] = ds.Tables[0].Rows[j]["LocDescriptiom"].ToString() == "NULL" ? "" : ds.Tables[0].Rows[j]["LocDescriptiom"].ToString();
                                dtPayroll.Rows[j]["Salary"] = ds.Tables[0].Rows[j]["Salary"].ToString() == "" ? 0.0 : ds.Tables[0].Rows[j]["Salary"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(ds.Tables[0].Rows[j]["Salary"].ToString());
                                dtPayroll.Rows[j]["Workingdays"] = dtPaidLev.Rows[0]["WkngDays"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["WkngDays"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["WkngDays"].ToString());
                                dtPayroll.Rows[j]["Present"] = dtPaidLev.Rows[0]["Atndays"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["Atndays"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["Atndays"].ToString());
                                dtPayroll.Rows[j]["Leaves"] = dtPaidLev.Rows[0]["Leaves"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["Leaves"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["Leaves"].ToString());
                                dtPayroll.Rows[j]["Noshow"] = dtPaidLev.Rows[0]["Noshow"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["Noshow"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["Noshow"].ToString());
                                dtPayroll.Rows[j]["LeavesAvailable"] = dtPaidLev.Rows[0]["PaidLeaves"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["PaidLeaves"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["PaidLeaves"].ToString());
                                dtPayroll.Rows[j]["PaidLeavesUsed"] = dtPaidLev.Rows[0]["PaidLeavesUsed"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["PaidLeavesUsed"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["PaidLeavesUsed"].ToString());
                                dtPayroll.Rows[j]["PaidLeavesBalanced"] = dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString());
                                dtPayroll.Rows[j]["CalLeaves"] = dtPaidLev.Rows[0]["TotalCalLeaves"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["TotalCalLeaves"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["TotalCalLeaves"].ToString());
                                dtPayroll.Rows[j]["CalSalary"] = dtPaidLev.Rows[0]["CalSalary"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["CalSalary"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["CalSalary"].ToString());
                                dtPayroll.Rows[j]["Bonus"] = dtPaidLev.Rows[0]["Bonus"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["Bonus"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["Bonus"].ToString());
                                dtPayroll.Rows[j]["Incentives"] = dtPaidLev.Rows[0]["Incentives"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["Incentives"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["Incentives"].ToString());
                                dtPayroll.Rows[j]["PrevUnpaid"] = dtPaidLev.Rows[0]["PrevUnpaid"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["PrevUnpaid"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["PrevUnpaid"].ToString());
                                dtPayroll.Rows[j]["AdvancePaid"] = dtPaidLev.Rows[0]["Advancepaid"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["Advancepaid"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["Advancepaid"].ToString());
                                dtPayroll.Rows[j]["Expenses"] = dtPaidLev.Rows[0]["ExpensesRecieved"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["ExpensesRecieved"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["ExpensesRecieved"].ToString());
                                dtPayroll.Rows[j]["LoanDeduct"] = dtPaidLev.Rows[0]["LoanDeduct"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["LoanDeduct"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["LoanDeduct"].ToString());
                                dtPayroll.Rows[j]["TotalPay"] = dtPaidLev.Rows[0]["TotalPay"].ToString() == "" ? 0.0 : dtPaidLev.Rows[0]["TotalPay"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(dtPaidLev.Rows[0]["TotalPay"].ToString());
                            }
                            else
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

                                DataView dvP = dtPaid.DefaultView;
                                DataTable dtPaidLev = new DataTable();

                                dv.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                                dtname = dv.ToTable();

                                dvL.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                                dtLeave = dvL.ToTable();

                                dvH.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                                dtHoliday = dvH.ToTable();

                                dvP.RowFilter = "empid='" + ds.Tables[0].Rows[j]["empid"].ToString() + "'";
                                dtPaidLev = dvP.ToTable();

                                int holidays = 0;
                                int leaves = 0;
                                double present = 0.0;
                                int noshow = 0;
                                dtPayroll.Rows[j]["userid"] = ds.Tables[0].Rows[j]["userid"].ToString();
                                dtPayroll.Rows[j]["empid"] = ds.Tables[0].Rows[j]["empid"].ToString();
                                dtPayroll.Rows[j]["Empname"] = ds.Tables[0].Rows[j]["firstName"].ToString() + " " + ds.Tables[0].Rows[j]["lastname"].ToString();
                                dtPayroll.Rows[j]["PEmpname"] = ds.Tables[0].Rows[j]["PfirstName"].ToString() + " " + ds.Tables[0].Rows[j]["Plastname"].ToString();
                                dtPayroll.Rows[j]["Termdate"] = ds.Tables[0].Rows[j]["Termdate"].ToString() == "NULL" ? Convert.ToDateTime("01/01/1900") : ds.Tables[0].Rows[j]["Termdate"].ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[j]["Termdate"]).ToString("MM/dd/yyyy"));
                                dtPayroll.Rows[j]["Startdate"] = ds.Tables[0].Rows[j]["Startdate"].ToString() == "NULL" ? Convert.ToDateTime("01/01/1900") : ds.Tables[0].Rows[j]["Startdate"].ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[j]["Startdate"]).ToString("MM/dd/yyyy"));
                                dtPayroll.Rows[j]["DeptName"] = ds.Tables[0].Rows[j]["DeptName"].ToString() == "NULL" ? "" : ds.Tables[0].Rows[j]["DeptName"].ToString();
                                dtPayroll.Rows[j]["LocationName"] = ds.Tables[0].Rows[j]["LocationName"].ToString() == "NULL" ? "" : ds.Tables[0].Rows[j]["LocationName"].ToString();
                                dtPayroll.Rows[j]["MasterEmpType"] = ds.Tables[0].Rows[j]["EmpType"].ToString() == "NULL" ? "" : ds.Tables[0].Rows[j]["EmpType"].ToString();
                                dtPayroll.Rows[j]["LocDescriptiom"] = ds.Tables[0].Rows[j]["LocDescriptiom"].ToString() == "NULL" ? "" : ds.Tables[0].Rows[j]["LocDescriptiom"].ToString();
                                dtPayroll.Rows[j]["Salary"] = ds.Tables[0].Rows[j]["Salary"].ToString() == "" ? 0.0 : ds.Tables[0].Rows[j]["Salary"].ToString() == "NULL" ? 0.0 : Convert.ToDouble(ds.Tables[0].Rows[j]["Salary"].ToString());
                                dtPayroll.Rows[j]["LeavesAvailable"] = 0;
                                if (dtPaidLev.Rows.Count > 0)
                                {
                                    if (dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString()!="NULL" && dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString() != "" && Convert.ToDateTime(dtPaidLev.Rows[0]["PaidLeavesStartDt"]).ToString("MM/dd/yyyy") != "01/01/1900")
                                    {
                                        if (Convert.ToDateTime(dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString()) <= StartDate.AddSeconds(-1))
                                        {
                                            int PaidLeaves = dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString() == "" ? 0 : dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString() == "NULL" ? 0 : Convert.ToInt32(dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString());
                                            dtPayroll.Rows[j]["LeavesAvailable"] = PaidLeaves + (dtPaidLev.Rows[0]["MonthlyEligible"].ToString() == "" ? 0 : dtPaidLev.Rows[0]["MonthlyEligible"].ToString() == "NULL" ? 0 : Convert.ToInt32(dtPaidLev.Rows[0]["MonthlyEligible"].ToString()));
                                            dtPayroll.Rows[j]["PaidLeaveStartDt"] = dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString() == "" ? Convert.ToDateTime("01/01/1900") : dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString() == "NULL" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString());
                                        }
                                        else
                                        {
                                            dtPayroll.Rows[j]["LeavesAvailable"] = "0";
                                        }
                                    }
                                    else
                                    {
                                        dtPayroll.Rows[j]["LeavesAvailable"] = "0";
                                    }
                                }
                                else
                                {
                                    if (ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString() != "NULL" && ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString() != "" && Convert.ToDateTime(ds.Tables[0].Rows[j]["PaidLeavesStartDt"]).ToString("MM/dd/yyyy") != "01/01/1900")
                                    {
                                        if (Convert.ToDateTime(ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString()) <= StartDate.AddSeconds(-1))
                                        {
                                            int PaidLeaves = 0;
                                            dtPayroll.Rows[j]["LeavesAvailable"] = PaidLeaves + (ds.Tables[0].Rows[j]["MonthlyEligible"].ToString() == "" ? 0 : ds.Tables[0].Rows[j]["MonthlyEligible"].ToString() == "NULL" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[j]["MonthlyEligible"].ToString()));
                                            dtPayroll.Rows[j]["PaidLeaveStartDt"] = ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString() == "" ? Convert.ToDateTime("01/01/1900") : ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString() == "NULL" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString());
                                        }
                                        else
                                        {
                                            dtPayroll.Rows[j]["LeavesAvailable"] = "0";
                                        }
                                    }
                                    else
                                    {
                                        dtPayroll.Rows[j]["LeavesAvailable"] = "0";
                                    }
                                }
                                if (dtname.Rows.Count > 0)
                                {
                                    DateTime startDate = StartDate;
                                    DateTime nextdate = NextDate;

                                    for (int i = 0; i < days; i++)
                                    {
                                        DataView dv1 = dtname.DefaultView;
                                        dv1.RowFilter = "Logindate >= #" + startDate + "# and Logindate<#" + nextdate + "#";
                                        DataTable dt1 = dv1.ToTable();

                                        DataView dL = dtLeave.DefaultView;
                                        dL.RowFilter = "Fromdate<=#" + startDate + "# and #" + startDate + "#<=Todate";
                                        DataTable dtLvResult = dL.ToTable();

                                        DataView dH = dtHoliday.DefaultView;
                                        dH.RowFilter = "HolidayDate >= #" + startDate + "# and HolidayDate<#" + nextdate + "#";
                                        DataTable dtHolResult = dH.ToTable();
                             
                                        if (dtHolResult.Rows.Count > 0)
                                        {
                                            holidays += 1;
                                        }
                                        if (dt1.Rows.Count > 0)
                                        {
                                            double dayhrs = 0.0;
                                            for (int k = 0; k < dt1.Rows.Count; k++)
                                            {
                                                if (dt1.Rows[k]["logoutdate"].ToString().Trim() != "")
                                                {
                                                    if (dt1.Rows[k]["total hours worked"].ToString() == "")
                                                    {
                                                        dayhrs += 0.0;
                                                    }
                                                    else
                                                    {
                                                        dayhrs += Convert.ToDouble(dt1.Rows[k]["total hours worked"].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    noshow += 1;
                                                }
                                            }
                                            if (dayhrs > 5)
                                            {
                                                present += 1;
                                            }
                                            else if (dayhrs > 3 && dayhrs < 5)
                                            {
                                                present += 0.5;
                                            }
                                            else if (dayhrs > 0.0 && dayhrs < 3)
                                            {
                                                leaves += 1;
                                            }
                                        }
                                        else if (dtHolResult.Rows.Count <= 0 && dtLvResult.Rows.Count > 0)
                                        {
                                            leaves += 1;
                                        }
                                        else if (dtHolResult.Rows.Count <= 0 && dtLvResult.Rows.Count <= 0 && dt1.Rows.Count <= 0)
                                        {
                                            noshow += 1;
                                        }
                                        else
                                        {
                                            noshow += 1;
                                        }
                                        dL.RowFilter = null;
                                        dH.RowFilter = null;
                                        dv1.RowFilter = null;
                                        startDate = nextdate;
                                        nextdate = GeneralFunction.GetNextDayOfWeekDate(nextdate);
                                    }
                                }
                                else if (dtHoliday.Rows.Count > 0)
                                {
                                    DateTime startDate = StartDate;
                                    DateTime nextdate = NextDate;
                                    for (int i = 0; i < days; i++)
                                    {
                                        DataView dH = dtHoliday.DefaultView;
                                        dH.RowFilter = "HolidayDate >= #" + startDate + "# and HolidayDate<#" + nextdate + "#";
                                        DataTable dtHolResult = dH.ToTable();
                                        DataView dL = dtLeave.DefaultView;
                                        dL.RowFilter = "Fromdate<=#" + startDate + "# and #" + startDate + "#<=Todate";
                                        DataTable dtLvResult = dL.ToTable();
                                        if (dtHolResult.Rows.Count > 0)
                                        {
                                            holidays += 1;
                                        }
                                        else if (dtLvResult.Rows.Count > 0)
                                        {
                                            leaves += 1;
                                        }
                                        else
                                        {
                                            noshow += 1;
                                        }
                                        dL.RowFilter = null;
                                        dH.RowFilter = null;
                                        startDate = nextdate;
                                        nextdate = GeneralFunction.GetNextDayOfWeekDate(nextdate);
                                    }
                                }
                                dtPayroll.Rows[j]["Present"] = present;
                                dtPayroll.Rows[j]["Workingdays"] = days - holidays;
                                dtPayroll.Rows[j]["Leaves"] = leaves;
                                dtPayroll.Rows[j]["Holidays"] = holidays;
                                if (Convert.ToInt32(dtPayroll.Rows[j]["LeavesAvailable"]) > Convert.ToInt32(dtPayroll.Rows[j]["leaves"]))
                                {
                                    dtPayroll.Rows[j]["PaidLeavesBalanced"] = Convert.ToInt32(dtPayroll.Rows[j]["LeavesAvailable"]) - Convert.ToInt32(dtPayroll.Rows[j]["leaves"]);
                                    dtPayroll.Rows[j]["PaidLeavesUsed"] = Convert.ToInt32(dtPayroll.Rows[j]["LeavesAvailable"]) - Convert.ToInt32(dtPayroll.Rows[j]["PaidLeavesBalanced"]);
                                    dtPayroll.Rows[j]["CalLeaves"] = 0;
                                }
                                else if (Convert.ToInt32(dtPayroll.Rows[j]["leaves"]) > Convert.ToInt32(dtPayroll.Rows[j]["LeavesAvailable"]))
                                {
                                    dtPayroll.Rows[j]["CalLeaves"] = Convert.ToInt32(dtPayroll.Rows[j]["leaves"]) - Convert.ToInt32(dtPayroll.Rows[j]["LeavesAvailable"]);
                                    dtPayroll.Rows[j]["PaidLeavesUsed"] = Convert.ToInt32(dtPayroll.Rows[j]["leaves"]) - Convert.ToInt32(dtPayroll.Rows[j]["CalLeaves"]);
                                    dtPayroll.Rows[j]["PaidLeavesBalanced"] = 0;
                                }
                                else
                                {
                                    dtPayroll.Rows[j]["PaidLeavesBalanced"] = Convert.ToInt32(dtPayroll.Rows[j]["LeavesAvailable"]) - Convert.ToInt32(dtPayroll.Rows[j]["leaves"]);
                                    dtPayroll.Rows[j]["PaidLeavesUsed"] = Convert.ToInt32(dtPayroll.Rows[j]["LeavesAvailable"]) - Convert.ToInt32(dtPayroll.Rows[j]["PaidLeavesBalanced"]);
                                    dtPayroll.Rows[j]["CalLeaves"] = Convert.ToInt32(dtPayroll.Rows[j]["LeavesAvailable"]) - Convert.ToInt32(dtPayroll.Rows[j]["leaves"]);
                                }
                                //salary calculation  CalSalary  CalLeaves
                                double CurntSalary = Convert.ToDouble(dtPayroll.Rows[j]["Salary"]);
                                int wrkDays = Convert.ToInt32(dtPayroll.Rows[j]["Workingdays"]);
                                double CalSalary = 0.0;
                                if (Convert.ToInt32(dtPayroll.Rows[j]["Present"]) > 0)
                                {
                                    double perdaySal = wrkDays == 0 ? 0 : Math.Round(CurntSalary / Convert.ToDouble(wrkDays));
                                    double paiddaysOff = perdaySal * (dtPayroll.Rows[j]["PaidLeavesUsed"].ToString() == "" ? 0 : dtPayroll.Rows[j]["PaidLeavesUsed"].ToString() == "NULL" ? 0 : Convert.ToInt32(dtPayroll.Rows[j]["PaidLeavesUsed"]));
                                    double ActualSalary = perdaySal * Convert.ToInt32(dtPayroll.Rows[j]["Present"]);
                                    CalSalary = paiddaysOff + ActualSalary;
                                    dtPayroll.Rows[j]["CalSalary"] = CalSalary;
                                    dtPayroll.Rows[j]["TotalPay"] = CalSalary;
                                }
                                else
                                {
                                    dtPayroll.Rows[j]["CalSalary"] = 0.0;
                                    dtPayroll.Rows[j]["TotalPay"] = 0.0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dtPayroll;
        }
        protected void grdPayRollIndia_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    GridView HeaderGrid = (GridView)sender;

                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell HeaderCell = new TableCell();
                    HeaderCell.Text = "Employee Info";
                    HeaderCell.Style["text-align"] = "center";
                    HeaderCell.ColumnSpan = 8;
                    HeaderCell.CssClass = "bL bR bT";
                    HeaderGridRow.Cells.Add(HeaderCell);

                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Attendence Info";
                    HeaderCell.Style["text-align"] = "center";
                    HeaderCell.ColumnSpan = 4;
                    HeaderCell.CssClass = "bR bT";
                    HeaderGridRow.Cells.Add(HeaderCell);

                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Paid leaves Info";
                    HeaderCell.Style["text-align"] = "center";
                    HeaderCell.ColumnSpan = 4;
                    HeaderCell.CssClass = "bR bT";
                    HeaderGridRow.Cells.Add(HeaderCell);

                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Salary Info";
                    HeaderCell.Style["text-align"] = "center";
                    HeaderCell.ColumnSpan = 8;
                    HeaderCell.CssClass = "bR bT";
                    HeaderGridRow.Cells.Add(HeaderCell);

                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.Style["text-align"] = "center";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.CssClass = "bR bT";
                    HeaderGridRow.Cells.Add(HeaderCell);

                    grdPayRollIndia.Controls[0].Controls.AddAt(0, HeaderGridRow);
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable xmlDt = Session["Indiapayroll"] as DataTable;
                //rppayslip.DataSource = xmlDt;
                //rppayslip.DataBind();
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

                string CurrentDate = Convert.ToDateTime(lblDate2.Text).ToString("MM-dd-yyyy") + Convert.ToDateTime(lblDate2.Text).ToString("hhmmsstt");


                string filepath = Server.MapPath("~/Payroll/Payslips" + ddlLocation.SelectedItem.Value + "/" + CurrentDate + "/");
                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);
                }
                var pdfDoc = new Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + "Payroll.pdf", FileMode.Create));
                Session["FilePath"] = filepath + "Payroll.pdf";
                pdfDoc.Open();
                var table1 = new PdfPTable(4);
                PdfPCell cell = new PdfPCell();
                // table1.DefaultCell.Border = Rectangle.NO_BORDER;


                table1.WidthPercentage = 90;
                table1.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.SpacingAfter = 15;
                


                //adding some rows 
                for (int i = 0; i < xmlDt.Rows.Count; i++)
                {

                    // cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    iTextSharp.text.Font fntTableFont1 = new Font(Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    iTextSharp.text.Font fntTableFont = new Font(Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                   
                    //iTextSharp.text.Font fntTableFont2 = FontFactory.GetFont("Times New Roman", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    //iTextSharp.text.Font fntTableFont3 = FontFactory.GetFont("Times New Roman", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                    


                    PdfPCell CellZero = new PdfPCell(new Phrase(new Phrase(("Hugo Mirad Marketing Solutions(P) Ltd."), fntTableFont1)));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    CellZero.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(new Phrase(("Hyderabad"), fntTableFont1)));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    CellZero.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    CellZero.Rowspan = 3;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(("Prepared:" + ISTTime.ToString("MMMM") + " " + ISTTime.ToString("dd") + " " + ISTTime.ToString("yyyy")), fntTableFont));
                    CellZero.Colspan = 2;
                    CellZero.Border = 0;
                   // CellZero.AddElement(new Phrase(("Prepared:" + ISTTime.ToString("MMM") + " " + ISTTime.ToString("dd") + " " + ISTTime.ToString("yyyy")), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);


                    string s = Convert.ToDateTime(txtFromDate.Text.ToString()).ToString("MMM") + "'" + Convert.ToDateTime(txtFromDate.Text.ToString()).ToString("yy");
                    CellZero = new PdfPCell(new Phrase(("Payroll Information:" + s), fntTableFont1));
                    CellZero.Colspan = 2;
                    CellZero.Border = 0;
                    //CellZero.AddElement(new Phrase(("Payroll Information:" + s), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Employee Information", fntTableFont1));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1.5F;
                    CellZero.HorizontalAlignment = Element.ALIGN_MIDDLE; 
                    //  CellZero.AddElement(new Phrase("Salary Details", fntTableFont));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Name", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;                  
                   // CellZero.AddElement(new Phrase("Name", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["PEmpname"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                 //   CellZero.AddElement(new Phrase(xmlDt.Rows[i]["PEmpname"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Employee ID#", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase("EmployeeID#", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["empid"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase(xmlDt.Rows[i]["empid"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Business Name", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase("Business Name", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["Empname"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(xmlDt.Rows[i]["Empname"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Date of Joining", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase("Date of Joining", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(Convert.ToDateTime(xmlDt.Rows[i]["Startdate"]).ToString("dd/MM/yyyy"), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase(Convert.ToDateTime(xmlDt.Rows[i]["Startdate"]).ToString("dd/MM/yyyy"), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Project", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                  //  CellZero.AddElement(new Phrase("Project", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Designation", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase("Designation", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["DeptName"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(xmlDt.Rows[i]["DeptName"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Attendance Details", fntTableFont1));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1.5F;
                   // CellZero.AddElement(new Phrase("Attendance Details", fntTableFont));
                    CellZero.HorizontalAlignment = Element.ALIGN_MIDDLE;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Total work days", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase("Total work days", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["Workingdays"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(xmlDt.Rows[i]["Workingdays"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Days Present", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                  //  CellZero.AddElement(new Phrase("Days Present", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["Present"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase(xmlDt.Rows[i]["Present"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Off days(Dates)", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase("Off days(Dates)", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Check local/online register", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase("Check local/online register", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Paid days off", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase("Paid days off", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["PaidLeavesUsed"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                  //  CellZero.AddElement(new Phrase(xmlDt.Rows[i]["PaidLeavesUsed"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Salary Details", fntTableFont1));
                    CellZero.Colspan = 3;
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1.5F;
                  //  CellZero.AddElement(new Phrase("Salary Details", fntTableFont));
                    CellZero.HorizontalAlignment = Element.ALIGN_MIDDLE;               
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Eligible Pay", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase("Eligible Pay", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Earned Pay", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;                 
                    //CellZero.AddElement(new Phrase("Earned Pay", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Base", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 1;
                   // CellZero.AddElement(new Phrase("Base", fntTableFont2));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["Salary"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;                 
                    //CellZero.AddElement(new Phrase(xmlDt.Rows[i]["Salary"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["CalSalary"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;                  
                    //CellZero.AddElement(new Phrase(xmlDt.Rows[i]["CalSalary"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Attendance Bonus", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                   // CellZero.AddElement(new Phrase("Attendance Bonus", fntTableFont2));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["Bonus"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(xmlDt.Rows[i]["Bonus"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Transportation", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                    //CellZero.AddElement(new Phrase("Transportation", fntTableFont2));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Food Allowance", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan =2;
                  //  CellZero.AddElement(new Phrase("Food Allowance", fntTableFont2));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Incentives *", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                  //  CellZero.AddElement(new Phrase("Incentives *", fntTableFont2));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["Incentives"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(xmlDt.Rows[i]["Incentives"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Reimbursements **", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                  //  CellZero.AddElement(new Phrase("Reimbursements **", fntTableFont2));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Gross", fntTableFont1));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                   // CellZero.AddElement(new Phrase("Gross", fntTableFont3));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase("", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Professional Tax", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                   // CellZero.AddElement(new Phrase("Professional Tax", fntTableFont2));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("100", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                  //  CellZero.AddElement(new Phrase("100", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Net", fntTableFont1));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    CellZero.Colspan = 2;
                   // CellZero.AddElement(new Phrase("Net", fntTableFont2));
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Colspan = 4;
                    CellZero.Border = 0;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Summary", fntTableFont1));
                    CellZero.Colspan = 3;
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1.5F;
                    //CellZero.AddElement(new Phrase("Summary", fntTableFont));
                    CellZero.HorizontalAlignment = Element.ALIGN_MIDDLE;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Current Earnings", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.Colspan = 2;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase("Current Earnings", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;    
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Advance Paid", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.Colspan = 2;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase("Advance Paid", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["AdvancePaid"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                  //  CellZero.AddElement(new Phrase(xmlDt.Rows[i]["AdvancePaid"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                      CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Previously Unpaid", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.Colspan = 2;
                    CellZero.BorderWidthBottom = 1;
                  //  CellZero.AddElement(new Phrase("Previously Unpaid", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(xmlDt.Rows[i]["PrevUnpaid"].ToString(), fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(xmlDt.Rows[i]["PrevUnpaid"].ToString(), fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                      CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Current Payments", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.Colspan = 2;
                    CellZero.BorderWidthBottom = 1;
                  //  CellZero.AddElement(new Phrase("Current Payments", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                  //  CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Remaining Balance", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.Colspan = 2;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase("Remaining Balance", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase("Additional Incentives Paid during the month", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.Colspan = 2;
                    CellZero.BorderWidthBottom = 1;
                    //CellZero.AddElement(new Phrase("Additional Incentives Paid during the month", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" ", fntTableFont));
                    CellZero.Border = 0;
                    CellZero.BorderWidthBottom = 1;
                   // CellZero.AddElement(new Phrase(" ", fntTableFont2));
                    CellZero.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table1.AddCell(CellZero);

                    CellZero = new PdfPCell(new Phrase(" "));
                    CellZero.Border = 0;
                    CellZero.Colspan = 1;
                    table1.AddCell(CellZero);

                    pdfDoc.Add(table1);
                    pdfDoc.NewPage();
                    table1.Rows.Clear();
                    table1.WidthPercentage = 100;
                }
                pdfDoc.Close();
                Response.Write(pdfDoc);
                showpdf(Session["FilePath"].ToString());
                Response.End();

            }
            catch (Exception ex)
            {
            }
        }
        protected void rppayslip_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
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

                    Label lblPCurntDt = (Label)e.Item.FindControl("lblPCurntDt");
                    lblPCurntDt.Text = CurentDatetime.ToString("MMM dd yyyy");
                    Label lblPMonth = (Label)e.Item.FindControl("lblPMonth");
                    lblPMonth.Text = Convert.ToDateTime(txtFromDate.Text.ToString()).ToString("MMM") + "'" + Convert.ToDateTime(txtFromDate.Text.ToString()).ToString("yyyy");
                    Label lblPSalary = (Label)e.Item.FindControl("lblPSalary");
                    Label lblPCalSalary = (Label)e.Item.FindControl("lblPCalSalary");
                    Label lblPBonus = (Label)e.Item.FindControl("lblPBonus");
                    Label lblPIncentives = (Label)e.Item.FindControl("lblPIncentives");
                    Label lblPTotalPay = (Label)e.Item.FindControl("lblPTotalPay");
                    Label lblPNetPay = (Label)e.Item.FindControl("lblPNetPay");
                    Label lblPPF = (Label)e.Item.FindControl("lblPPF");
                    Label lblPTransport = (Label)e.Item.FindControl("lblPTransport");
                    Label lblPFoodAllowance = (Label)e.Item.FindControl("lblPFoodAllowance");
                    Label lblPReimburse = (Label)e.Item.FindControl("lblPReimburse");
                    Double salary = lblPSalary.Text == "" ? 0.0 : lblPSalary.Text == "NULL" ? 0.0 : Convert.ToDouble(lblPSalary.Text);
                    Double CalSalary = lblPCalSalary.Text == "" ? 0.0 : lblPCalSalary.Text == "NULL" ? 0.0 : Convert.ToDouble(lblPCalSalary.Text);
                    Double Bonus = lblPBonus.Text == "" ? 0.0 : lblPBonus.Text == "NULL" ? 0.0 : Convert.ToDouble(lblPBonus.Text);
                    Double Incentives = lblPIncentives.Text == "" ? 0.0 : lblPIncentives.Text == "NULL" ? 0.0 : Convert.ToDouble(lblPIncentives.Text);
                    Double ProfTax = lblPPF.Text == "" ? 0.0 : lblPPF.Text == "NULL" ? 0.0 : Convert.ToDouble(lblPPF.Text);
                    Double Reimburse = lblPReimburse.Text == "" ? 0.0 : lblPReimburse.Text == "NULL" ? 0.0 : Convert.ToDouble(lblPReimburse.Text);
                    Double FoodAllowance = lblPFoodAllowance.Text == "" ? 0.0 : lblPFoodAllowance.Text == "NULL" ? 0.0 : Convert.ToDouble(lblPFoodAllowance.Text);
                    Double Transport = lblPTransport.Text == "" ? 0.0 : lblPTransport.Text == "NULL" ? 0.0 : Convert.ToDouble(lblPTransport.Text);
                    Double GrossSal = CalSalary + Bonus + Incentives + Reimburse + FoodAllowance + Transport;
                    lblPTotalPay.Text = GrossSal.ToString() == "" ? "0" : GrossSal.ToString()=="NULL" ?"0":GrossSal.ToString();
                    Double NetPay = GrossSal - ProfTax;
                    lblPNetPay.Text=NetPay.ToString()==""?"0":NetPay.ToString()=="NULL"?"0":NetPay.ToString();
                }
            }
            catch (Exception ex)
            {
            }

        }

        private static DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                Label lbl = (Label)gvDepartments.Rows[i].FindControl("lblEmpID");
                dr[0] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblEmpFirstname");
                dr[1] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblStartedDate");
                dr[2] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblTerminatedDate");
                dr[3] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblDept");
                dr[0] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblEmpID");
                dr[0] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblEmpID");
                dr[0] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblEmpID");
                dr[0] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblEmpID");
                dr[0] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblEmpID");
                dr[0] = lbl.Text.ToString();

                lbl = (Label)gvDepartments.Rows[i].FindControl("lblEmpID");
                dr[0] = lbl.Text.ToString();

                
                    
              
                dt.Rows.Add(dr);
            }
            return dt;
        }

    }
}
