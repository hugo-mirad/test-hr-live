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

                    Report obj = new Report();
                    DataSet ds=obj.GetFinalPayrollDate(Convert.ToInt32(ddlLocation.SelectedItem.Value));
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DateTime StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FinalMonth"].ToString()+ "/01/" + ds.Tables[0].Rows[0]["FinalYear"].ToString());
                            ViewState["PaidStartDate"] = StartDate.ToString("MM/dd/yyyy");
                            ViewState["CurrentStDt"] = StartDate.ToString("MM/dd/yyyy");
                            DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-1);
                            ViewState["PaidEndDate"] = EndDate.ToString("MM/dd/yyyy");
                            ViewState["CurrentEndDt"] = EndDate.ToString("MM/dd/yyyy");

                            GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value), StartDate, EndDate);
                            if (StartDate.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentStDt"]).ToString("MM/dd/yyyy"))
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
                }
            }
        }

        private void GetpaidLeavesData(int locationID, DateTime startDt, DateTime EndDt)
        {
            try
            {
                EmployeeBL obj = new EmployeeBL();
                DataTable dt = obj.GetEmpPaidleavesDetailsByLocation(locationID,startDt,EndDt);
                lblLeaveReport.Text = "(" + startDt.ToString("MM/dd/yyyy") + "-" + EndDt.ToString("MM/dd/yyyy") + ")";
                if (dt.Rows.Count > 0)
                {
                    grdUsers.DataSource = dt;
                    Session["AllPaidLeavesData"] = dt;
                    grdUsers.DataBind();
                    lblTotal.Visible = true;
                    lblTotal.Text = "Total Employee(s) :" + dt.Rows.Count;
                    lblNodata.Visible = false;
                    dvlblNodata.Style["display"] = "none";                 
                }
                else
                {
                    lblNodata.Visible = true;
                    dvlblNodata.Style["display"] = "block";
                    lblNodata.Text = "No data found";
                    grdUsers.DataSource = null;
                    grdUsers.DataBind();
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

                DateTime StartDate = Convert.ToDateTime(ViewState["PaidStartDate"]);
                DateTime EndDate = Convert.ToDateTime(ViewState["PaidEndDate"]);
                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value),StartDate,EndDate);
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
                grdUsers.Rows[e.NewEditIndex].CssClass = "edit";
                DateTime StartDate = Convert.ToDateTime(ViewState["PaidStartDate"]);
                DateTime EndDate = Convert.ToDateTime(ViewState["PaidEndDate"]);
               

                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value),StartDate,EndDate);
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
                TextBox txtLeavesUsed = (TextBox)grdUsers.Rows[e.RowIndex].FindControl("txtLeavesUsed");
                TextBox txtLeavBalanced = (TextBox)grdUsers.Rows[e.RowIndex].FindControl("txtLeavBalanced");
                //TextBox txtMAxLevAvail = (TextBox)grdUsers.Rows[e.RowIndex].FindControl("txtMaxEligible");
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

                DateTime StartDate = Convert.ToDateTime(ViewState["PaidStartDate"]);
                DateTime CurrentDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));
                int LeaveAvail =txtLevAvail.Text==""?0:Convert.ToInt32(txtLevAvail.Text);
               // int MaxLeaveAvail = Convert.ToInt32(txtMAxLevAvail.Text);
                int LeavesUsed =txtLeavesUsed.Text==""?0:Convert.ToInt32(txtLeavesUsed.Text);
                int LeavesBalanced =txtLeavBalanced.Text==""?0:Convert.ToInt32(txtLeavBalanced.Text);
                DateTime LeavesStartDt =txtLeavesStartDt.Text==""?Convert.ToDateTime("01/01/1900"): Convert.ToDateTime(txtLeavesStartDt.Text);
                string notes = txtNotes.Text.Trim() == "" ? "" : GeneralFunction.ToProperNotes(txtNotes.Text) + "<br>" + "-------------------------------------<br>" +
                               "Updated by " + Session["EmpName"].ToString().Trim() + " at " + CurrentDate + "<br>" + "***********************************" + "<br>";  
                int userid = Convert.ToInt32(Session["UserID"]);
                String strHostName = Request.UserHostAddress.ToString();
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                bool bnew = obj.UpdatePaidLeaveByLeaveID(LeaveAvail, LeavesUsed, LeavesBalanced, PaidLeaveID, userid, notes, StartDate, LeavesStartDt, strIp, PaidLeaveUserID, CurrentDate);
                grdUsers.EditIndex = -1;
                
                DateTime EndDate = Convert.ToDateTime(ViewState["PaidEndDate"]);
                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value),StartDate,EndDate);
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
                DateTime StartDate = Convert.ToDateTime(ViewState["PaidStartDate"]);
                DateTime EndDate = Convert.ToDateTime(ViewState["PaidEndDate"]);
              
                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value),StartDate,EndDate);
            }
            catch (Exception ex)
            {
            }
        }

        private string CreateSignInTable(string Employeename, string SignInNotes)
        {
            //SignInNotes = SignInNotes.Replace("<br>", Environment.NewLine);
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

        protected void grdUsers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            try
            {
                grdUsers.EditIndex = -1;

                DateTime StartDate = Convert.ToDateTime(ViewState["PaidStartDate"]);
                DateTime EndDate = Convert.ToDateTime(ViewState["PaidEndDate"]);
                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value), StartDate,EndDate);
            }
            catch (Exception ex)
            {
            }
        }



        protected void grdUsers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    GridView HeaderGrid = (GridView)sender;

                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                    TableCell header = new TableCell();
                    header.ColumnSpan = 6;
                    header.CssClass = "bR";
                    header.Text = "Employee Information";
                    header.Style["text-align"] = "center";
                    HeaderGridRow.Cells.Add(header);

                    header = new TableCell();
                    header.ColumnSpan = 7;
                   
                    header.Text = "Paid Leaves Information";
                    header.Style["text-align"] = "center";
                    HeaderGridRow.Cells.Add(header);
                    grdUsers.Controls[0].Controls.AddAt(0, HeaderGridRow);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime StartDate = Convert.ToDateTime(ViewState["PaidStartDate"]).AddMonths(-1);
                ViewState["PaidStartDate"] = StartDate;
               // ViewState["CurrentStDt"] = StartDate.ToString("MM/dd/yyyy");
                DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-1);
                ViewState["PaidEndDate"] = EndDate.ToString("MM/dd/yyyy");
               // ViewState["CurrentEndDt"] = EndDate.ToString("MM/dd/yyyy");

                if (StartDate.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentStDt"]).ToString("MM/dd/yyyy"))
                {
                    btnNext.CssClass = "btn btn-danger btn-small disabled";
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.CssClass = "btn btn-danger btn-small enabled";
                    btnNext.Enabled = true;
                }

                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value), StartDate, EndDate);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime StartDate = Convert.ToDateTime(ViewState["CurrentStDt"]);
                ViewState["PaidStartDate"] = StartDate;
              
                DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-1);
                ViewState["PaidEndDate"] = EndDate.ToString("MM/dd/yyyy");
              
                if (StartDate.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentStDt"]).ToString("MM/dd/yyyy"))
                {
                    btnNext.CssClass = "btn btn-danger btn-small disabled";
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.CssClass = "btn btn-danger btn-small enabled";
                    btnNext.Enabled = true;
                }

                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value), StartDate, EndDate);
            }
            catch (Exception ex)
            {
            }

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime StartDate = Convert.ToDateTime(ViewState["PaidStartDate"]).AddMonths(1);
                ViewState["PaidStartDate"] = StartDate;
                // ViewState["CurrentStDt"] = StartDate.ToString("MM/dd/yyyy");
                DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-1);
                ViewState["PaidEndDate"] = EndDate.ToString("MM/dd/yyyy");
                // ViewState["CurrentEndDt"] = EndDate.ToString("MM/dd/yyyy");

                if (StartDate.ToString("MM/dd/yyyy") == Convert.ToDateTime(ViewState["CurrentStDt"]).ToString("MM/dd/yyyy"))
                {
                    btnNext.CssClass = "btn btn-danger btn-small disabled";
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.CssClass = "btn btn-danger btn-small enabled";
                    btnNext.Enabled = true;
                }

                GetpaidLeavesData(Convert.ToInt32(ddlLocation.SelectedItem.Value), StartDate, EndDate);
            }
            catch (Exception ex)
            {
            }
        }
        
        }




}
