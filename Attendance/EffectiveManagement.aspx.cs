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

                    lblDate2.Text = CurentDatetime.ToString("dddd MMMM dd yyyy, hh:mm:ss tt ");
                    lblTimeZoneName.Text = Session["TimeZoneName"].ToString().Trim();
                    lblHeadSchedule.Text = Session["ScheduleInOut"].ToString();
                    lblEmployyName.Text = Session["EmpName"].ToString().Trim();
                    Photo.Src = Session["Photo"].ToString().Trim();
                    lblLocation.Text = Session["LocationName"].ToString();
                    lblShiftName.Text = "-" + Session["ShiftName"].ToString();
                    getLocations();
                    Getdepartments();
                    ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(Session["LocationName"].ToString().Trim()));
                    GetShifts(ddlLocation.SelectedItem.Text);
                    ddlgridShift.SelectedIndex = ddlgridShift.Items.IndexOf(ddlgridShift.Items.FindByValue(Session["ShiftID"].ToString()));
                    GetEffectiveData(Convert.ToInt32(ddlLocation.SelectedValue), 0, Convert.ToDateTime("05/01/2014"), Convert.ToDateTime("05/31/2014"));
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
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
            //ddlShift.DataSource = dsShifts;
            //ddlShift.DataTextField = "shiftname";
            //ddlShift.DataValueField = "shiftID";
            //ddlShift.DataBind();
            //ddlShift.Items.Insert(0, new ListItem("Select", "0"));

            ddlgridShift.DataSource = dsShifts;
            ddlgridShift.DataTextField = "shiftname";
            ddlgridShift.DataValueField = "shiftID";
            ddlgridShift.DataBind();
            ddlgridShift.Items.Insert(0, new ListItem("ALL", "0"));

        }
   

        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdUsers_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        private void GetEffectiveData(int LocationID, int shiftID, DateTime StartDt, DateTime EndDt)
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                DataTable dt = obj.GetEffectivedataByLocation(LocationID, StartDt, EndDt, shiftID);
                if (dt.Rows.Count > 0)
                {
                    grdUsers.DataSource = dt;
                    grdUsers.DataBind();
                    lblNodata.Text = "";
                }
                else
                {
                    grdUsers.DataSource = null;
                    grdUsers.DataBind();
                    lblNodata.Text = "No data found";
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlgridShift_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkAddEffective_Click(object sender, EventArgs e)
        {
            Report obj = new Report();
            DataSet ds = obj.GetActiveUsersAdmin(Convert.ToDateTime("04/01/2014"), Convert.ToDateTime("05/31/2014"), ddlLocation.SelectedItem.Text.ToString(), 0);
            grdEfUsers.DataSource = ds;
            grdEfUsers.DataBind();
            mdlAddPopUp.Show();
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

    }
}
