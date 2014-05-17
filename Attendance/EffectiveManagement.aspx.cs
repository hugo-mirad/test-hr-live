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
    public partial class EffectiveManagement : System.Web.UI.Page
    {
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

                    DateTime startdate = Convert.ToDateTime(CurentDatetime.ToString("MM/dd/yyyy")).AddDays(1 - Convert.ToDateTime(CurentDatetime.ToString("MM/dd/yyyy")).Day);
                    DateTime Enddate = startdate.AddMonths(1).AddSeconds(-1);

                    ViewState["StartMonth"] = startdate;
                    ViewState["EndMonth"] = Enddate;
                    ViewState["CurrentMonth"] = startdate;
                    if (Session["IsAdmin"].ToString() == "True")
                    {
                        ddlLocation.Enabled = true;
                    }
                    else
                    {
                        ddlLocation.Enabled = false;
                    }

                    lblDate2.Text = CurentDatetime.ToString("dddd MMMM dd yyyy, hh:mm:ss tt ");
                    lblTimeZoneName.Text = Session["TimeZoneName"].ToString().Trim();
                    lblHeadSchedule.Text = Session["ScheduleInOut"].ToString();
                    lblEmployyName.Text = Session["EmpName"].ToString().Trim();
                    Photo.Src = Session["Photo"].ToString().Trim();
                    lblLocation.Text = Session["LocationName"].ToString();
                    lblShiftName.Text = "-" + Session["ShiftName"].ToString();
                    getLocations();
                    Getdepartments();
                    GetEffectiveChangeStatus();
                    ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(Session["LocationName"].ToString().Trim()));
                    GetShifts(ddlLocation.SelectedItem.Text);
                    ddlgridShift.SelectedIndex = ddlgridShift.Items.IndexOf(ddlgridShift.Items.FindByValue(Session["ShiftID"].ToString()));
                 
                    GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlgridShift.SelectedValue), startdate, Enddate,Convert.ToInt32(ddlSelect.SelectedValue));
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        private void GetEffectiveChangeStatus()
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                DataTable dt = obj.GetEffectiveChangeStatus();
                ddlSelect.DataSource = dt;
                ddlSelect.DataTextField = "ChangeStatus";
                ddlSelect.DataValueField = "ChangeStatusID";
                ddlSelect.DataBind();
                ddlSelect.Items.Insert(0, new ListItem("ALL", "0"));


                ddlEditStatus.DataSource = dt;
                ddlEditStatus.DataTextField = "ChangeStatus";
                ddlEditStatus.DataValueField = "ChangeStatusID";
                ddlEditStatus.DataBind();
                ddlEditStatus.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
            }
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
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
        private void GetShifts(string LocationName)
        {
            Business business = new Business();
            DataSet dsShifts = business.GetShiftsByLocationName(LocationName);
            ddlgridShift.DataSource = dsShifts;
            ddlgridShift.DataTextField = "shiftname";
            ddlgridShift.DataValueField = "shiftID";
            ddlgridShift.DataBind();
            ddlgridShift.Items.Insert(0, new ListItem("ALL", "0"));

        }
        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            PayrollEditHistory obj = new PayrollEditHistory();
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblFieldname = (Label)e.Row.FindControl("lblFieldname");
                    Label lblFieldOldValue = (Label)e.Row.FindControl("lblFieldOldValue");
                    Label lblFieldNewValue = (Label)e.Row.FindControl("lblFieldNewValue");
                    lblFieldOldValue.Text = obj.GetName(lblFieldname.Text, lblFieldOldValue.Text);
                    lblFieldNewValue.Text = obj.GetName(lblFieldname.Text, lblFieldNewValue.Text);
                    if (lblFieldname.Text == "Salary")
                    {
                        lblFieldOldValue.Text = GeneralFunction.FormatCurrency(lblFieldOldValue.Text, ddlLocation.SelectedItem.Text);
                        lblFieldNewValue.Text = GeneralFunction.FormatCurrency(lblFieldNewValue.Text, ddlLocation.SelectedItem.Text);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        protected void grdUsers_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
        private void GetEffectiveData(int LocationID, int shiftID, DateTime StartDt, DateTime EndDt,int stausID)
        {


            try
            {
                EmployeeBL obj = new EmployeeBL();
                DataTable dt = obj.GetEffectivedataByLocation(LocationID, StartDt, EndDt, shiftID,stausID);
                lblMonthRep.Text = "(" + StartDt.ToString("MM/dd/yyyy") + " - " + EndDt.ToString("MM/dd/yyyy") + ")";
                if (dt.Rows.Count > 0)
                {
                    grdUsers.DataSource = dt;
                    grdUsers.DataBind();
                    lblNodata.Text = "";
                    dvNodata.Style["display"] = "none";
                }
                else
                {
                    grdUsers.DataSource = null;
                    grdUsers.DataBind();
                    lblNodata.Text = "No data found";
                    dvNodata.Style["display"] = "block";
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime Start = Convert.ToDateTime(ViewState["StartMonth"]);
                DateTime End = Convert.ToDateTime(ViewState["EndMonth"]);
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlgridShift.SelectedValue), Start, End,StatusID);
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddlgridShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
             
                DateTime Start = Convert.ToDateTime(ViewState["StartMonth"]);
                DateTime End = Convert.ToDateTime(ViewState["EndMonth"]);
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlgridShift.SelectedValue), Start, End,StatusID);
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
        private void Getdepartments()
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataTable dt = obj.GetAllDepartments();
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "Deptname";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                mdlEffective1popup.Show();
            }
            catch (Exception ex)
            {
            }

        }
        protected void ddlEffectiveField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEffectiveField.SelectedValue == "Salary")
            {
                trSalary.Style["display"] = "table-row";
                trDept.Style["display"] = "none";
            }
            else if (ddlEffectiveField.SelectedValue == "Department")
            {
                trSalary.Style["display"] = "none";
                trDept.Style["display"] = "table-row";
            }
            else
            {
                trSalary.Style["display"] = "none";
                trDept.Style["display"] = "none";
            }
        }
        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmployeeBL obj = new EmployeeBL();
            PayrollEditHistory objPay = new PayrollEditHistory();
            try
            {
                if (e.CommandName == "Effective")
                {
                    int EffectiveID = Convert.ToInt32(e.CommandArgument.ToString());
                    DataTable dt = obj.GetEmpChangesEffectiveByID(EffectiveID);
                    hdnEffectID.Value = dt.Rows[0]["EffectiveID"].ToString();
                    lblEmpName.Text = dt.Rows[0]["empname"].ToString() + " - " + dt.Rows[0]["empID"].ToString();
                    lblFieldname.Text = dt.Rows[0]["ChangeField"].ToString();
                    lblEditFieldName.Text = dt.Rows[0]["ChangeField"].ToString();
                    lblOldValue.Text = objPay.GetName(dt.Rows[0]["ChangeField"].ToString(), dt.Rows[0]["OldValue"].ToString());
                    lblEditOldValue.Text = objPay.GetName(dt.Rows[0]["ChangeField"].ToString(), dt.Rows[0]["OldValue"].ToString());
                    lblNewvalue.Text = objPay.GetName(dt.Rows[0]["ChangeField"].ToString(), dt.Rows[0]["NewValue"].ToString());
                    lblEffectiveDt.Text = Convert.ToDateTime(dt.Rows[0]["EffectiveDt"].ToString()).ToString("MM/dd/yyyy");
                    txtEffectiveDate.Text = Convert.ToDateTime(dt.Rows[0]["EffectiveDt"].ToString()).ToString("MM/dd/yyyy");
                    lblStatus.Text = dt.Rows[0]["ChangeStatus"].ToString();
                    ddlEditStatus.SelectedIndex = ddlEditStatus.Items.IndexOf(ddlEditStatus.Items.FindByValue(dt.Rows[0]["StatusID"].ToString()));
                    dvView.Style["display"] = "block";
                    dvEdit.Style["display"] = "none";
                    if (lblFieldname.Text == "Salary")
                    {
                        lblOldValue.Text = GeneralFunction.FormatCurrency(lblOldValue.Text, ddlLocation.SelectedItem.Text);
                        lblEditOldValue.Text = GeneralFunction.FormatCurrency(lblEditOldValue.Text, ddlLocation.SelectedItem.Text);
                        lblNewvalue.Text = GeneralFunction.FormatCurrency(lblNewvalue.Text, ddlLocation.SelectedItem.Text);
                    }
                    GetNamesList(lblEditFieldName.Text, dt);
                    mdlAddPopUp.Show();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void GetNamesList(string Fieldname, DataTable dt)
        {
            try
            {
                if (Fieldname == "Location")
                {
                    Attendance.BAL.Report obj = new Report();
                    DataTable ds = obj.GetLocations();

                    ddlNewValue.DataSource = ds;
                    ddlNewValue.DataTextField = "LocationName";
                    ddlNewValue.DataValueField = "LocationId";
                    ddlNewValue.DataBind();
                    ddlNewValue.SelectedIndex = ddlNewValue.Items.IndexOf(ddlNewValue.Items.FindByValue(dt.Rows[0]["NewValue"].ToString()));
                    txtEditNewValue.Visible = false;
                    ddlNewValue.Visible = true;
                }
                else if (Fieldname == "Schedule")
                {
                    Attendance.BAL.Report obj = new Attendance.BAL.Report();
                    DataTable ds = obj.GetAllScheduleTypes();


                    ddlNewValue.DataSource = ds;
                    ddlNewValue.DataValueField = "ScheduleID";
                    ddlNewValue.DataTextField = "ScheduleType";
                    ddlNewValue.DataBind();

                    ddlNewValue.SelectedIndex = ddlNewValue.Items.IndexOf(ddlNewValue.Items.FindByValue(dt.Rows[0]["NewValue"].ToString()));
                    txtEditNewValue.Visible = false;
                    ddlNewValue.Visible = true;
                }

                else if (Fieldname == "State")
                {
                    Attendance.BAL.Business obj = new Attendance.BAL.Business();
                    DataTable ds = obj.GetState();

                    ddlNewValue.DataSource = ds;
                    ddlNewValue.DataValueField = "StateID";
                    ddlNewValue.DataTextField = "StateName";
                    ddlNewValue.DataBind();

                    ddlNewValue.SelectedIndex = ddlNewValue.Items.IndexOf(ddlNewValue.Items.FindByValue(dt.Rows[0]["NewValue"].ToString()));

                    txtEditNewValue.Visible = false;
                    ddlNewValue.Visible = true;
                }
                else if (Fieldname == "Wage")
                {
                    Attendance.BAL.Report obj = new Attendance.BAL.Report();
                    DataTable ds = obj.GetAllWages();


                    ddlNewValue.DataSource = ds;
                    ddlNewValue.DataValueField = "WageID";
                    ddlNewValue.DataTextField = "WageType";
                    ddlNewValue.DataBind();

                    txtEditNewValue.Visible = false;

                    ddlNewValue.Visible = true;

                    ddlNewValue.SelectedIndex = ddlNewValue.Items.IndexOf(ddlNewValue.Items.FindByValue(dt.Rows[0]["NewValue"].ToString()));
                }
                else if (Fieldname == "Employee type")
                {
                    Attendance.BAL.Report obj = new Attendance.BAL.Report();
                    DataTable ds = obj.GetAllEmployeetypes();


                    ddlNewValue.DataSource = ds;
                    ddlNewValue.DataValueField = "EmpType";
                    ddlNewValue.DataTextField = "EmpTypeID";
                    ddlNewValue.DataBind();


                    ddlNewValue.SelectedIndex = ddlNewValue.Items.IndexOf(ddlNewValue.Items.FindByValue(dt.Rows[0]["NewValue"].ToString()));

                    txtEditNewValue.Visible = false;

                    ddlNewValue.Visible = true;
                }
                else if (Fieldname == "Shift")
                {
                    Attendance.BAL.EmployeeBL obj = new Attendance.BAL.EmployeeBL();
                    DataTable ds = obj.GetShifts();
                    DataView dv = ds.DefaultView;
                    dv.RowFilter = "locationID=" + Convert.ToInt32(dt.Rows[0]["locationID"].ToString());
                    DataTable dt1 = dv.ToTable();

                    ddlNewValue.DataSource = dt1;
                    ddlNewValue.DataTextField = "ShiftName";
                    ddlNewValue.DataValueField = "ShiftID";
                    ddlNewValue.DataBind();

                    ddlNewValue.SelectedIndex = ddlNewValue.Items.IndexOf(ddlNewValue.Items.FindByValue(dt.Rows[0]["NewValue"].ToString()));
                    txtEditNewValue.Visible = false;
                    ddlNewValue.Visible = true;
                }
                else if (Fieldname == "Filling status")
                {
                    ddlNewValue.DataSource = null;
                    ddlNewValue.DataBind();
                    ddlNewValue.Items.Insert(0, new ListItem("Single", "1"));
                    ddlNewValue.Items.Insert(1, new ListItem("Married", "2"));

                    txtEditNewValue.Visible = false;
                    ddlNewValue.Visible = true;
                    ddlNewValue.SelectedIndex = ddlNewValue.Items.IndexOf(ddlNewValue.Items.FindByValue(dt.Rows[0]["NewValue"].ToString()));
                }
                else
                {
                    txtEditNewValue.Text = dt.Rows[0]["NewValue"].ToString();
                    txtEditNewValue.Visible = true;
                    ddlNewValue.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                dvView.Style["display"] = "none";
                dvEdit.Style["display"] = "block";
            }
            catch (Exception ex)
            {
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                string oldValue = "";
                string newValue = "";
                int effectID = Convert.ToInt32(hdnEffectID.Value);
                oldValue = lblEditOldValue.Text;


                if (txtEditNewValue.Text != "")
                {
                    newValue = txtEditNewValue.Text;
                }
                else
                {
                    newValue = ddlNewValue.SelectedValue;
                }
                DateTime effectDt = txtEffectiveDate.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtEffectiveDate.Text);
                int statusID = Convert.ToInt32(ddlEditStatus.SelectedValue);
                bool bnew = obj.UpdateEfectiveChangesByID(effectDt, oldValue, statusID, newValue, effectID);
                mdlAddPopUp.Hide();

                DateTime Start = Convert.ToDateTime(ViewState["StartMonth"]);
                DateTime End = Convert.ToDateTime(ViewState["EndMonth"]);
                GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlgridShift.SelectedValue),Start, End, Convert.ToInt32(ddlSelect.SelectedValue));
            }
            catch (Exception ex)
            {
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                dvView.Style["display"] = "block";
                dvEdit.Style["display"] = "none";
            }
            catch (Exception ex)
            {
            }

        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime PrevStart = Convert.ToDateTime(ViewState["StartMonth"]).AddMonths(-1);
                DateTime PrevEnd = PrevStart.AddMonths(1).AddSeconds(-1);
                ViewState["StartMonth"] = PrevStart;
                ViewState["EndMonth"] = PrevEnd;
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlgridShift.SelectedValue), PrevStart, PrevEnd,StatusID);
                if (PrevStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentMonth"]).ToString("MM/dd/yyyy"))
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
            catch (Exception ex)
            {
            }

        }

        protected void btnCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime CurrentStart = Convert.ToDateTime(ViewState["CurrentMonth"]);
                DateTime CurrentEnd = CurrentStart.AddMonths(1).AddSeconds(-1);
                ViewState["StartMonth"] = CurrentStart;
                ViewState["EndMonth"] = CurrentEnd;
                if (CurrentStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentMonth"]).ToString("MM/dd/yyyy"))
                {
                    btnNext.CssClass = "btn btn-danger btn-small disabled";
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.CssClass = "btn btn-danger btn-small enabled";
                    btnNext.Enabled = true;
                }

                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlgridShift.SelectedValue), CurrentStart, CurrentEnd,StatusID);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime NextStart = Convert.ToDateTime(ViewState["StartMonth"]).AddMonths(1);
                DateTime NextEnd = NextStart.AddMonths(1).AddSeconds(-1);
                ViewState["StartMonth"] = NextStart;
                ViewState["EndMonth"] = NextEnd;
                if (NextStart.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentMonth"]).ToString("MM/dd/yyyy"))
                {
                    btnNext.CssClass = "btn btn-danger btn-small disabled";
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.CssClass = "btn btn-danger btn-small enabled";
                    btnNext.Enabled = true;
                }
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlgridShift.SelectedValue), NextStart, NextEnd,StatusID);
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetShifts(ddlLocation.SelectedItem.Text);
                DateTime Start = Convert.ToDateTime(ViewState["StartMonth"]);
                DateTime End = Convert.ToDateTime(ViewState["EndMonth"]);
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlgridShift.SelectedValue), Start, End,StatusID);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
