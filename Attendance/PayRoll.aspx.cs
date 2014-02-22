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
            if (Session["LocationName"] != null)
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
                            DateTime Count = obj.GetFreezedDate(MonthEnd, Session["LocationName"].ToString());
                            if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                            {
                                lblFreeze.Text = "";
                            }
                            else
                            {
                                lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                            }

                            DataTable dt = GetReportIndia(MonthStart, MonthEnd, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                            lblWeekPayrollReport.Text = "( " + MonthStart.ToString("MM/dd/yyyy") + " - " + MonthEnd.ToString("MM/dd/yyyy") + " )";
                            GetEditHistory(MonthStart, MonthEnd);
                            lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                            lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                            grdPayRollIndia.DataSource = dt;
                            grdPayRollIndia.DataBind();

                            grdPayRoll.DataSource = null;
                            grdPayRoll.DataBind();
                            
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


                            DataTable dt = GetReportIndia(MonthStart, MonthEnd, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                            lblWeekPayrollReport.Text = "( " + MonthStart.ToString("MM/dd/yyyy") + " - " + MonthEnd.ToString("MM/dd/yyyy") + " )";
                            GetEditHistory(MonthStart, MonthEnd);
                            lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                            lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                            grdPayRollIndia.DataSource = dt;
                            grdPayRollIndia.DataBind();

                            grdPayRoll.DataSource = null;
                            grdPayRoll.DataBind();

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
        private void GetReport(DateTime StartDate, DateTime EndTime, int userid,string Location)
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataSet ds = obj.GetPayrollReport(StartDate, EndTime, userid,Location);
                lblWeekPayrollReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndTime.ToString("MM/dd/yyyy") + " )";
                GetEditHistory(StartDate, EndTime);
                lblTotal.Text = ds.Tables[0].Rows[0]["LocDescriptiom"].ToString().Trim()==""?"Employee record count: " + ds.Tables[0].Rows.Count.ToString().Trim(): ds.Tables[0].Rows[0]["LocDescriptiom"].ToString().Trim() +"  location; Employee record count: " + ds.Tables[0].Rows.Count.ToString().Trim();
                lblReportDate.Text="Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim()+"</b>";
                grdPayRoll.DataSource = ds.Tables[0];
                grdPayRoll.DataBind();

                grdPayRollIndia.DataSource = null;
                grdPayRollIndia.DataBind();

            }
            catch (Exception ex)
            {
            }
        }
        private DataTable GetReportIndia(DateTime StartDate, DateTime EndTime,int LocationID)
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
            dtPayroll.Columns.Add("TotalPay", typeof(double));
       

            try
            {
                DataSet ds=new DataSet();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("[USP_FnAdmin]", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@LocationID", LocationID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@startdate", StartDate));
                da.SelectCommand.Parameters.Add(new SqlParameter("@EndDate", EndTime));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
                EmployeeBL obj = new EmployeeBL();
                DataTable dtPaid = obj.GetEmpPaidleavesDetailsByLocation(LocationID, StartDate.AddMonths(-1), StartDate.AddSeconds(-1));
                int days = (Convert.ToInt32(EndTime.ToString("dd"))-Convert.ToInt32(StartDate.ToString("dd")))+1;
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables.Count > 1)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            dtPayroll.Rows.Add();
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

                                if (dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString() != "" || Convert.ToDateTime(dtPaidLev.Rows[0]["PaidLeavesStartDt"]).ToString("01/01/1900") != "")
                                {
                                    if (Convert.ToDateTime(dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString()) <= StartDate.AddSeconds(-1))
                                    {
                                        int PaidLeaves = dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString() == "" ? 0 : dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString() == "NULL" ? 0 : Convert.ToInt32(dtPaidLev.Rows[0]["PaidLeavesBalanced"].ToString());
                                        dtPayroll.Rows[j]["LeavesAvailable"] = PaidLeaves + (dtPaidLev.Rows[0]["MonthlyEligible"].ToString() == "" ? 0 : dtPaidLev.Rows[0]["MonthlyEligible"].ToString() == "NULL" ? 0 : Convert.ToInt32(dtPaidLev.Rows[0]["MonthlyEligible"].ToString()));
                                        dtPayroll.Rows[j]["PaidLeaveStartDt"] = dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString() == "" ? Convert.ToDateTime("01/01/1900") : dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString() == "NULL" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dtPaidLev.Rows[0]["PaidLeavesStartDt"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                if (ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString() != "" || Convert.ToDateTime(ds.Tables[0].Rows[j]["PaidLeavesStartDt"]).ToString("01/01/1900") != "")
                                {
                                    if (Convert.ToDateTime(ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString()) <= StartDate.AddSeconds(-1))
                                    {
                                        int PaidLeaves = 0;
                                        dtPayroll.Rows[j]["LeavesAvailable"] = PaidLeaves + (ds.Tables[0].Rows[j]["MonthlyEligible"].ToString() == "" ? 0 : ds.Tables[0].Rows[j]["MonthlyEligible"].ToString() == "NULL" ? 0 : Convert.ToInt32(ds.Tables[0].Rows[j]["MonthlyEligible"].ToString()));
                                        dtPayroll.Rows[j]["PaidLeaveStartDt"] = ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString() == "" ? Convert.ToDateTime("01/01/1900") : ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString() == "NULL" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ds.Tables[0].Rows[j]["PaidLeavesStartDt"].ToString());
                                    }
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
                                                leaves += 1;
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
                                        else if(dayhrs>0.0 && dayhrs<3)
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
                                        leaves += 1;
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
                            double perdaySal = wrkDays == 0 ? 0 : (CurntSalary / Convert.ToDouble(wrkDays));
                            double paiddaysOff = perdaySal * (dtPayroll.Rows[j]["PaidLeavesUsed"].ToString() == "" ? 0 : dtPayroll.Rows[j]["PaidLeavesUsed"].ToString() == "NULL" ? 0 : Convert.ToInt32(dtPayroll.Rows[j]["PaidLeavesUsed"]));
                            double ActualSalary = perdaySal * Convert.ToInt32(dtPayroll.Rows[j]["Present"]);
                            CalSalary = paiddaysOff + ActualSalary;
                            dtPayroll.Rows[j]["CalSalary"] = CalSalary;
                            dtPayroll.Rows[j]["TotalPay"] = CalSalary;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return dtPayroll;
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
                DateTime Count = obj.GetFreezedDate(EndTime, Session["LocationName"].ToString());
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

                        txtToDate.Text = StartDate.ToString("MM/dd/yyyy");
                        txtFromDate.Text = EndTime.ToString("MM/dd/yyyy");


                        DataTable dt = GetReportIndia(StartDate, EndTime, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                        lblWeekPayrollReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndTime.ToString("MM/dd/yyyy") + " )";
                        GetEditHistory(StartDate, EndTime);
                        lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                        lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                        grdPayRollIndia.DataSource = dt;
                        grdPayRollIndia.DataBind();

                        grdPayRoll.DataSource = null;
                        grdPayRoll.DataBind();

                    }
                    else
                    {
                        txtToDate.Text = StartDate.AddDays(-1).ToString("MM/dd/yyyy");
                        txtFromDate.Text = EndTime.ToString("MM/dd/yyyy");
                        GetReport(EndTime, StartDate.AddDays(-1), 0, ddlLocation.SelectedItem.Text.Trim());
                    }

                    lblGrdLocaton.Visible = true;
                    ddlLocation.Visible = true;
                }
                else
                {
                    GetReport(StartDate, EndTime, userid, "");
                    lblGrdLocaton.Visible = false;
                    ddlLocation.Visible = false;
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
                    //rpt1.DataSource = dtChanges;
                    //rpt1.DataBind();

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
            // lblWeekPayrollReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndTime.ToString("MM/dd/yyyy") + " )";
            //grdPayRoll.DataSource = ds.Tables[0];
            //grdPayRoll.DataBind();
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
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                dvpayrollreport.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(new Rectangle(1200f,1800f));
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + "Payroll.pdf", FileMode.Create));
                Session["FilePath"] = filepath + "Payroll.pdf";
                pdfDoc.Open();
           
                iTextSharp.text.Font georgia = FontFactory.GetFont("georgia", 20f,1,new BaseColor(System.Drawing.Color.Brown));
                //georgia.Color = Color.Gray;
                Chunk beginning = new Chunk(PayrollDate, georgia);
                Phrase p1 = new Phrase(beginning);
                Phrase p2 = new Phrase();
                pdfDoc.Add(new Paragraph(p1));
                //pdfDoc.Add(new Paragraph(30f, "Created at" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + " by " + Session["EmpName"].ToString().Trim()));
                //pdfDoc.AddTitle(PayrollDate);
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                showpdf(Session["FilePath"].ToString());
                Response.End();

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
                        //lblCountyHead.Visible = false;
                        //lblCounty.Visible = false;
                        grdNewEmp.Columns[8].Visible=false;
                    }
                    else
                    {
                          grdNewEmp.Columns[8].Visible=true;
                        //lblCountyHead.Visible = true;
                        //lblCounty.Visible = true;
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

                //DateTime StartDate = Convert.ToDateTime(txtFromDate.Text);
                //DateTime EndTime = Convert.ToDateTime(txtToDate.Text);
                //ViewState["StartRptDt"] = StartDate;
                //ViewState["EndRptDt"] = EndTime;
                //Attendance.BAL.Report obj = new Report();
                //DateTime Count = obj.GetFreezedDate(EndTime, Session["LocationName"].ToString());
                //if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                //{
                //    lblFreeze.Text = "";
                //}
                //else
                //{
                //    lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                //}
                //if (Session["IsAdmin"].ToString() == "True")
                //{
                //    if (ddlLocation.SelectedItem.Text.Trim() == "INBH" || ddlLocation.SelectedItem.Text.Trim() == "INDG")
                //    {
                //        ViewState["StartRptDt"] = StartDate;
                //        ViewState["EndRptDt"] = EndTime;

                //        txtToDate.Text = EndTime.ToString("MM/dd/yyyy");
                //        txtFromDate.Text = StartDate.ToString("MM/dd/yyyy");


                //        DataTable dt = GetReportIndia(StartDate, EndTime, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                //        lblWeekPayrollReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndTime.ToString("MM/dd/yyyy") + " )";
                //        GetEditHistory(StartDate, EndTime);
                //        lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                //        lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                //        grdPayRollIndia.DataSource = dt;
                //        grdPayRollIndia.DataBind();

                //        grdPayRoll.DataSource = null;
                //        grdPayRoll.DataBind();

                //    }
                //    else
                //    {
                //        DateTime StartDate = GeneralFunction.GetFirstDayOfWeekDate(TodayDate);
                //        DateTime EndDate = StartDate.AddDays(-14);

                //        Attendance.BAL.Report obj = new Report();
                //        DateTime Count = obj.GetFreezedDate(EndDate, Session["LocationName"].ToString());
                //        if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                //        {
                //            lblFreeze.Text = "";
                //        }
                //        else
                //        {
                //            lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                //        }


                //        txtToDate.Text = StartDate.AddDays(-1).ToString("MM/dd/yyyy");
                //        txtFromDate.Text = EndDate.ToString("MM/dd/yyyy");
                //        ViewState["StartRptDt"] = EndDate;
                //        ViewState["EndRptDt"] = StartDate.AddDays(-1);
                //        GetReport(EndDate, StartDate.AddDays(-1), 0, ddlLocation.SelectedItem.Text.Trim());
                //        //GetReport(StartDate, EndTime, 0, ddlLocation.SelectedItem.Text.Trim());
                //    }

                //    lblGrdLocaton.Visible = true;
                //    ddlLocation.Visible = true;
                //}
                //else
                //{
                //    if (lblLocation.Text.Trim() == "INBH" || lblLocation.Text.Trim() == "INDG")
                //    {
                //        ViewState["StartRptDt"] = StartDate;
                //        ViewState["EndRptDt"] = EndTime;

                //        txtToDate.Text = EndTime.ToString("MM/dd/yyyy");
                //        txtFromDate.Text = StartDate.ToString("MM/dd/yyyy");


                //        DataTable dt = GetReportIndia(StartDate, EndTime, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                //        lblWeekPayrollReport.Text = "( " + StartDate.ToString("MM/dd/yyyy") + " - " + EndTime.ToString("MM/dd/yyyy") + " )";
                //        GetEditHistory(StartDate, EndTime);
                //        lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                //        lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                //        grdPayRollIndia.DataSource = dt;
                //        grdPayRollIndia.DataBind();

                //        grdPayRoll.DataSource = null;
                //        grdPayRoll.DataBind();

                //    }
                //    else
                //    {


                //        DateTime StartDate = GeneralFunction.GetFirstDayOfWeekDate(TodayDate);
                //        DateTime EndDate = StartDate.AddDays(-14);

                //        Attendance.BAL.Report obj = new Report();
                //        DateTime Count = obj.GetFreezedDate(EndDate, Session["LocationName"].ToString());
                //        if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                //        {
                //            lblFreeze.Text = "";
                //        }
                //        else
                //        {
                //            lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                //        }


                //        txtToDate.Text = StartDate.AddDays(-1).ToString("MM/dd/yyyy");
                //        txtFromDate.Text = EndDate.ToString("MM/dd/yyyy");
                //        ViewState["StartRptDt"] = EndDate;
                //        ViewState["EndRptDt"] = StartDate.AddDays(-1);
                //        GetReport(EndDate, StartDate.AddDays(-1), 0, ddlLocation.SelectedItem.Text.Trim());

                //        //GetReport(StartDate, EndTime, userid, "");
                //    }

                //    lblGrdLocaton.Visible = true;
                //    ddlLocation.Enabled = false;

                   
                   
                //}

                //// GetReport(StartDate, EndTime, userid,ddlLocation.SelectedItem.Text.Trim());
                //BindListOfNewEmployee();
                //BindListofChanges();
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
                        DateTime Count = obj.GetFreezedDate(MonthEnd, Session["LocationName"].ToString());
                        if (Count.ToString("MM/dd/yyyy") != "01/01/1900")
                        {
                            lblFreeze.Text = "";
                        }
                        else
                        {
                            lblFreeze.Text = "This is tentative attendance report.Some or part of the attendance not yet freezed";
                        }

                        DataTable dt = GetReportIndia(MonthStart, MonthEnd, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                        lblWeekPayrollReport.Text = "( " + MonthStart.ToString("MM/dd/yyyy") + " - " + MonthEnd.ToString("MM/dd/yyyy") + " )";
                        GetEditHistory(MonthStart, MonthEnd);
                        lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                        lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                        grdPayRollIndia.DataSource = dt;
                        grdPayRollIndia.DataBind();

                        grdPayRoll.DataSource = null;
                        grdPayRoll.DataBind();

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


                        DataTable dt = GetReportIndia(MonthStart, MonthEnd, Convert.ToInt32(ddlLocation.SelectedItem.Value));
                        lblWeekPayrollReport.Text = "( " + MonthStart.ToString("MM/dd/yyyy") + " - " + MonthEnd.ToString("MM/dd/yyyy") + " )";
                        GetEditHistory(MonthStart, MonthEnd);
                        lblTotal.Text = "Employee record count: " + dt.Rows.Count.ToString().Trim();
                        lblReportDate.Text = "Report generated at  <b>" + Convert.ToDateTime(lblDate2.Text).ToString("MM/dd/yyyy hh:mm:ss tt") + "</b>  by  <b>" + Session["EmpName"].ToString().Trim() + "</b>";
                        grdPayRollIndia.DataSource = dt;
                        grdPayRollIndia.DataBind();

                        grdPayRoll.DataSource = null;
                        grdPayRoll.DataBind();

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
                    HiddenField hdnUserID = (HiddenField)grdPayRollIndia.Rows[i].FindControl("hdnUserID");
                    Label lblWorkingDays = (Label)grdPayRollIndia.Rows[i].FindControl("lblWorkingDays");
                    Label lblAttendDays = (Label)grdPayRollIndia.Rows[i].FindControl("lblAttendDays");
                    Label lblLeaves = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeaves");
                    Label lblNoshow = (Label)grdPayRollIndia.Rows[i].FindControl("lblNoshow");
                    Label lblLeavesAvailable = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeavesAvailable");
                    Label lblLeavesUsed = (Label)grdPayRollIndia.Rows[i].FindControl("lblLeavesUsed");
                    Label lblPaidLeavesBalanced = (Label)grdPayRollIndia.Rows[i].FindControl("lblPaidLeavesBalanced");
                    Label lblCalLeaves = (Label)grdPayRollIndia.Rows[i].FindControl("lblCalLeaves");
                    DateTime dt = Convert.ToDateTime(ViewState["StartRptDt"]);
                    objInfo.Mnth = Convert.ToInt32(dt.ToString("MM"));
                    objInfo.Yr = Convert.ToInt32(dt.ToString("yyyy"));
                    objInfo.EnterBy=Convert.ToInt32(Session["UserID"]);
                    objInfo.EnterDate = ISTTime;
                    objInfo.TotalCalLeaves1 = lblCalLeaves.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblCalLeaves.ToString().Trim());
                    objInfo.PaidLeavesUsed = lblLeavesUsed.ToString().Trim() == "" ? 0: Convert.ToSingle(lblLeavesUsed.ToString().Trim());
                    objInfo.PaidLeavesBalanced = lblPaidLeavesBalanced.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblPaidLeavesBalanced.ToString().Trim());
                    objInfo.PaidLeaves = lblLeavesAvailable.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblLeavesAvailable.ToString().Trim());
                    objInfo.NoShow = lblNoshow.ToString().Trim() == "" ? 0 : Convert.ToSingle(lblNoshow.ToString().Trim());
                    objInfo.Leaves = lblLeaves.ToString().Trim() == "" ?0 : Convert.ToSingle(lblLeaves.ToString().Trim());
                    objInfo.WorkingDays = lblWorkingDays.ToString().Trim() == "" ? 0: Convert.ToSingle(lblWorkingDays.ToString().Trim());
                    objInfo.AttendDays = lblAttendDays.ToString().Trim() == "" ? 0: Convert.ToSingle(lblAttendDays.ToString().Trim());
                    objInfo.Userid = Convert.ToInt32(hdnUserID.Value);

                    int AtnLogID = obj.SaveAttendanceHistory(objInfo);

                    //Salary history

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
