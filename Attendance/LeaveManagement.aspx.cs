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
    public partial class LeaveManagement : System.Web.UI.Page
    {
        public GeneralFunction objFun = new GeneralFunction();
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
                    getLocations();
                    ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(lblLocation.Text.Trim()));


                    if (Session["IsAdmin"].ToString() == "True")
                    {
                        ddlLocation.Enabled = true;
                    }
                    else
                    {
                        ddlLocation.Enabled = false;
                    }

                    GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value));

                }
            }
        }

        private void GetpaidLeavesData(int locationID)
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                DataTable dt = obj.GetEmpPaidleavesDetailsByLocation(locationID);
                if (dt.Rows.Count > 0)
                {
                    grdUsers.DataSource = dt;
                    Session["AllPaidLeavesData"] = dt;
                    grdUsers.DataBind();
                    lblTotal.Visible = true;
                    lblTotal.Text = "Total Employee(s) :" + dt.Rows.Count;
                }
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

        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblLeavesStartDt = (Label)e.Row.FindControl("lblLeavesStartDt");
                    lblLeavesStartDt.Text = lblLeavesStartDt.Text == "01/01/1900" ? "" : lblLeavesStartDt.Text;

                    Label lblStartedDate = (Label)e.Row.FindControl("lblStartedDate");
                    lblStartedDate.Text = lblStartedDate.Text == "01/01/1900" ? "" : lblStartedDate.Text;

                    Label lblTerminatedDate = (Label)e.Row.FindControl("lblTerminatedDate");
                    lblTerminatedDate.Text = lblTerminatedDate.Text == "01/01/1900" ? "" : lblTerminatedDate.Text;

                    Label lblNotes = (Label)e.Row.FindControl("lblNotes");
                    Label lblEmpFirstname = (Label)e.Row.FindControl("lblEmpFirstname");
                    if (lblNotes.Text.Trim() != "")
                    {
                        string sTable = sTable = CreateSignInTable(lblEmpFirstname.Text, lblNotes.Text);
                        lblNotes.Attributes.Add("rel", "tooltip");
                        lblNotes.Attributes.Add("title", sTable);
                    }
                    lblNotes.Text = GeneralFunction.WrapTextByMaxCharacters(lblNotes.Text, 20);
                }
            }
            catch (Exception ex) { }
        }

        protected void grdUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["AllPaidLeavesData"] as DataTable;

                if (dt != null)
                {
                    BizUtility.GridSort(txthdnSortOrder, e, grdUsers, 0, dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value));
            }
            catch (Exception ex)
            {
            }
        }

        protected void grdUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdUsers.EditIndex = e.NewEditIndex;
                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value));
            }
            catch (Exception ex)
            {
            }

        }

        protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                int PaidLeaveID = 0;
                if (grdUsers.DataKeys[e.RowIndex].Value.ToString().Trim()!= "")
                {
                     PaidLeaveID = Convert.ToInt32(grdUsers.DataKeys[e.RowIndex].Value);
                }
                HiddenField hdnUserid = (HiddenField)grdUsers.Rows[e.RowIndex].FindControl("hdnUserid");
                int PaidLeaveUserID = Convert.ToInt32(hdnUserid.Value);

                TextBox txtLeavesStartDt = (TextBox)grdUsers.Rows[e.RowIndex].FindControl("txtLeavesStartDt");
                TextBox txtLevAvail = (TextBox)grdUsers.Rows[e.RowIndex].FindControl("txtLeavAvailable");
                TextBox txtMAxLevAvail = (TextBox)grdUsers.Rows[e.RowIndex].FindControl("txtMaxEligible");
                TextBox txtNotes = (TextBox)grdUsers.Rows[e.RowIndex].FindControl("txtNotes");
                string timezone = "";
                if (Convert.ToInt32(Session["TimeZoneID"]) == 2)
                {
                    timezone = "Eastern Standard Time";
                }
                else
                {
                    timezone = "India Standard Time";
                }
                DateTime CurrentDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                int LeaveAvail = Convert.ToInt32(txtLevAvail.Text);
                int MaxLeaveAvail = Convert.ToInt32(txtMAxLevAvail.Text);
                DateTime LeavesStartDt =txtLeavesStartDt.Text==""?Convert.ToDateTime("01/01/1900"): Convert.ToDateTime(txtLeavesStartDt.Text);

                string notes = txtNotes.Text.Trim() == "" ? "" : GeneralFunction.ToProperNotes(txtNotes.Text) + "<br/>"+
                               "Updated by " + Session["EmpName"].ToString().Trim() + " at " + CurrentDate + "<br/>" + "***********************************" + "<br/>";  

               
                int userid = Convert.ToInt32(Session["UserID"]);

                String strHostName = Request.UserHostAddress.ToString();
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                bool bnew = obj.UpdatePaidLeaveByLeaveID(LeaveAvail, MaxLeaveAvail, PaidLeaveID, userid, notes, CurrentDate,LeavesStartDt,strIp,PaidLeaveUserID);
                grdUsers.EditIndex = -1;
                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value));
            }
            catch (Exception ex)
            {
            }
        }

        protected void grdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grdUsers.EditIndex = -1;
                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value));
            }
            catch (Exception ex)
            {
            }
        }

        private string CreateSignInTable(string Employeename, string SignInNotes)
        {
            SignInNotes = HttpUtility.HtmlDecode(SignInNotes).Replace("</br>", "\n");
            SignInNotes = HttpUtility.HtmlDecode(SignInNotes).Replace("<br/>", "\n");
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

        protected void grdUsers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            try
            {
                grdUsers.EditIndex = -1;
                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value));
            }
            catch (Exception ex)
            {
            }
        }
    }
}
