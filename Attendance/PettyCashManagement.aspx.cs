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
    public partial class PettyCashManagement : System.Web.UI.Page
    {
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
                    lblLocation.Text = Session["LocationName"].ToString();
                    lblEmployyName.Text = Session["EmpName"].ToString().Trim();
                    Photo.Src = Session["Photo"].ToString().Trim();

                    GetPettyCashDetails();
                }
               
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void GetPettyCashDetails()
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataSet ds = obj.GetPettyCashDetails(Session["LocationName"].ToString());
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdPettyCashDet.DataSource = ds.Tables[0];
                        grdPettyCashDet.DataBind();

                        grdExpenseDetails.DataSource = ds.Tables[0];
                        grdExpenseDetails.DataBind();
                    }
                }
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
        protected void btnLogout_Click(object sender, EventArgs e)
        {
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

        protected void ddlIncomeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIncomeType.SelectedValue == "2")
                {
                    Cheque.Style["display"] = "table-row";
                }
                else if (ddlIncomeType.SelectedValue == "1")
                {
                    Cheque.Style["display"] = "none";
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
                Attendance.Entities.PettyCashInfo objInfo = new Attendance.Entities.PettyCashInfo();
                objInfo.AccountName = txtName.Text==""?"":GeneralFunction.ToProper(txtName.Text);
                objInfo.AmountType = ddlIncomeType.SelectedItem.Text;
                objInfo.AccountDate = txtDate.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtDate.Text);
                objInfo.InitialAmoun = txtIncome.Text;
                objInfo.FromWhom = txtFrmWhom.Text == "" ? "" : GeneralFunction.ToProper(txtFrmWhom.Text);
                objInfo.ChequeNum = txtChequeNum.Text;
                objInfo.ExpenseAmount = txtExpenseAmnt.Text;
                objInfo.ExpenseDate = txtExpenseDate.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtExpenseDate.Text);
                objInfo.Expensetype = txtExpenseType.Text;
                objInfo.ExpenseSubType = txtExpenseSubType.Text;
                objInfo.ExpenseManName = txtServiceName.Text == "" ? "" : GeneralFunction.ToProper(txtServiceName.Text);
                objInfo.BillNum = txtBillNum.Text;
                objInfo.VoucherNum = txtVoucherNum.Text;
                objInfo.Notes = txtNotes.Text == "" ? "" : GeneralFunction.ToProperNotes(txtNotes.Text);
                string Locationname = Session["LocationName"].ToString();
                int UserID = Convert.ToInt32(Session["UserID"]);


                Attendance.BAL.Report obj = new Report();
                bool bnew = obj.SavePettyCashDetails(objInfo, UserID, Locationname);

                if (bnew)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Saved successfully..');", true);
                    GetPettyCashDetails();
                
                }
              

            
            }
            catch (Exception ex)
            {

            }
        }
    }
}
