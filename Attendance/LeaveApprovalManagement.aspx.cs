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
    public partial class LeaveApprovalManagement : System.Web.UI.Page
    {
        public GeneralFunction objFun = new GeneralFunction();
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
                    GetMasterShifts(lblLocation.Text.ToString());
                    ddlShifts.SelectedIndex = ddlShifts.Items.IndexOf(ddlShifts.Items.FindByValue(Session["ShiftID"].ToString()));

                    DateTime startdate = Convert.ToDateTime(CurentDatetime.ToString("MM/dd/yyyy")).AddDays(1 - Convert.ToDateTime(CurentDatetime.ToString("MM/dd/yyyy")).Day);
                    DateTime Enddate=startdate.AddMonths(1).AddSeconds(-1);

                    ViewState["StartMonth"] = startdate;
                    ViewState["EndMonth"] = Enddate;
                    ViewState["CurrentMonth"] = startdate;
                    int AppovedStatusID = 0;
                    getLocations();
                    GetShifts(lblLocation.Text.ToString());
                    ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByValue(Session["ShiftID"].ToString()));
                    if(Session["IsAdmin"].ToString()=="True")
                    {
                        ddlGrdLocation.Enabled = true;
                    }
                    else
                    {
                        ddlGrdLocation.Enabled = false;
                    }
                    ddlGrdLocation.SelectedIndex = ddlGrdLocation.Items.IndexOf(ddlGrdLocation.Items.FindByText(lblLocation.Text.ToString()));
                    GetStatus();
                    GetLeavesDetails(lblLocation.Text, startdate, Enddate, AppovedStatusID,Convert.ToInt32(ddlShift.SelectedValue));
                    if (startdate.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentMonth"]).ToString("MM/dd/yyyy"))
                    {
                        btnNext.CssClass = "btn btn-danger btn-small disabled";
                        btnNext.Enabled = false;
                    }
                    else
                    {
                        btnNext.CssClass = "btn btn-danger btn-small enabled";
                        btnNext.Enabled = true;

                    }
                    if (lblLocation.Text.Trim() == "USMP" || lblLocation.Text.Trim() == "USWB")
                    {
                        lnkLeavemangement.Enabled = false;
                        lnkLeavemangement.Style["Color"] = "Gray";
                    }
                    else
                    {
                        lnkLeavemangement.Enabled = true;
                    }
                }
            }
        }
        private void GetStatus()
        {
            try
            {
                Business obj = new Business();
                DataTable ds = obj.GetLeaveStatus();
                ddlSelect.DataSource = ds;
                ddlSelect.DataTextField = "Status";
                ddlSelect.DataValueField = "StatusID";
                ddlSelect.DataBind();

                ddlLeaveApprove.DataSource = ds;
                ddlLeaveApprove.DataTextField = "Status";
                ddlLeaveApprove.DataValueField = "StatusID";
                ddlLeaveApprove.DataBind();
             }
            catch (Exception ex)
            {
            }
        }
        private void GetLeavesDetails(string p, DateTime startdate, DateTime Enddate, int AppovedStatusID,int shiftID)
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                DataTable dt = obj.GetLeaveDetailsByLoction(p, startdate, Enddate, AppovedStatusID,shiftID);
                if (dt.Rows.Count > 0)
                {

                    grdUsers.DataSource = dt;
                    grdUsers.DataBind();
                    lblMonthRep.Text = "(" + startdate.ToString("MM/dd/yyyy") + " - " + Enddate.ToString("MM/dd/yyyy") + ")";
                    Session["AllLeaveRequestData"] = dt;
                    lblError.Visible = false;
                }
                else
                {
                    lblError.Text = "No records found";
                    lblError.Visible = true;
                   
                }

                BizUtility.GridSortInitail("Ascending", "Firstname", grdUsers, 0, dt);
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
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", " $('#spinner').show();", true);
            Session.Abandon();
            Response.Redirect("Default.aspx");
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
        protected void grdUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["AllLeaveRequestData"] as DataTable;

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
        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

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
            catch (Exception ex)
            {
            }
        }
        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetLeavesDetails(ddlGrdLocation.SelectedItem.Text.ToString().Trim(), Convert.ToDateTime(ViewState["StartMonth"]), Convert.ToDateTime(ViewState["EndMonth"]), StatusID,Convert.ToInt32(ddlShift.SelectedValue));
            }
            catch (Exception ex)
            {
            }

        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            ddlLeaveApprove.SelectedIndex = 0;
            txtLeaveNotes.Text = "";
            mdlLeaveStatusUpdate.Show();
        }
        protected void btnLeeaveApproveUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string Records = hdnChkRecords.Value.ToString();
                if (Records.Trim().Length > 0)
                {
                    string[] recordID = Records.Split(',');
                    for (int i = 0; i < recordID.Length-1; i++)
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

                    int LeaveID = Convert.ToInt32(recordID[i]);
                    string notes = txtLeaveNotes.Text.Trim() == "" ? "" : GeneralFunction.ToProperNotes(txtLeaveNotes.Text) + "<br>" + " <br>" +
                                  ddlLeaveApprove.SelectedItem.Text+" by " + Session["EmpName"].ToString().Trim() + " at " + ISTTime + "<br>" + "------------------------------------<br>";  
                        
                    int ApprovedStatusID = Convert.ToInt32(ddlLeaveApprove.SelectedValue);
                    int ApprovedBy = Convert.ToInt32(Session["UserID"]);
                    EmployeeBL obj = new EmployeeBL();
                    bool bnew = obj.UpdateLeaveRequest(LeaveID, ApprovedBy, ApprovedStatusID, notes, ISTTime);
                   }

                }
                mdlLeaveStatusUpdate.Hide();
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetLeavesDetails(ddlGrdLocation.SelectedItem.Text.ToString().Trim(), Convert.ToDateTime(ViewState["StartMonth"]), Convert.ToDateTime(ViewState["EndMonth"]), StatusID, Convert.ToInt32(ddlShift.SelectedValue));
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime PrevStart=Convert.ToDateTime(ViewState["StartMonth"]).AddMonths(-1);
                DateTime PrevEnd=PrevStart.AddMonths(1).AddSeconds(-1);
 
                ViewState["StartMonth"]=PrevStart;
                ViewState["EndMonth"]=PrevEnd;

                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetLeavesDetails(ddlGrdLocation.SelectedItem.Text.ToString().Trim(), PrevStart, PrevEnd, StatusID, Convert.ToInt32(ddlShift.SelectedValue));


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
                GetLeavesDetails(ddlGrdLocation.SelectedItem.Text.ToString().Trim(), CurrentStart, CurrentEnd, StatusID, Convert.ToInt32(ddlShift.SelectedValue));

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
                GetLeavesDetails(ddlGrdLocation.SelectedItem.Text.ToString().Trim(), NextStart, NextEnd, StatusID, Convert.ToInt32(ddlShift.SelectedValue));

            }
            catch (Exception ex)
            {

            }

        }
        private string CreateSignInTable(string Employeename, string SignInNotes)
        {
            
            string strTransaction = string.Empty;
            if (SignInNotes.Trim() != "" )
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
        private void GetMasterShifts(string LocationName)
        {
            try
            {
                Business business = new Business();
                DataSet dsShifts = business.GetShiftsByLocationName(LocationName);
                ddlShifts.DataSource = dsShifts;
                ddlShifts.DataTextField = "shiftname";
                ddlShifts.DataValueField = "shiftID";
                ddlShifts.DataBind();
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
                ddlGrdLocation.DataSource = dt;
                ddlGrdLocation.DataTextField = "LocationName";
                ddlGrdLocation.DataValueField = "LocationId";
                ddlGrdLocation.DataBind();
             
            }
            catch (Exception ex)
            {

            }
        }

        private void GetShifts(string LocationName)
        {
            Business business = new Business();
            DataSet dsShifts = business.GetShiftsByLocationName(LocationName);
            ddlShift.DataSource = dsShifts;
            ddlShift.DataTextField = "shiftname";
            ddlShift.DataValueField = "shiftID";
            ddlShift.DataBind();
            ddlShift.Items.Insert(0, new ListItem("ALL", "0"));
        }

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime Start = Convert.ToDateTime(ViewState["StartMonth"]);
                DateTime End = Convert.ToDateTime(ViewState["EndMonth"]);
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetLeavesDetails(ddlGrdLocation.SelectedItem.Text.ToString().Trim(), Start, End, StatusID, Convert.ToInt32(ddlShift.SelectedValue));

            }
            catch (Exception ex)
            {
            }

        }

        protected void ddlGrdLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetShifts(ddlGrdLocation.SelectedValue.ToString());
                DateTime Start = Convert.ToDateTime(ViewState["StartMonth"]);
                DateTime End = Convert.ToDateTime(ViewState["EndMonth"]);
                int StatusID = Convert.ToInt32(ddlSelect.SelectedValue);
                GetLeavesDetails(ddlGrdLocation.SelectedItem.Text.ToString().Trim(), Start, End, StatusID, Convert.ToInt32(ddlShift.SelectedValue));

            }
            catch (Exception ex)
            {
            }
        }

    }
}
