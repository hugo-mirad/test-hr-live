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
using Attendance.Entities;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Attendance
{
    public partial class EmployeDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ViewState["Location"] = Request.QueryString["Loc"].ToString();

            if (Session["IsAdmin"] != null && Session["UserID"] != null)
            {
                if (ViewState["Location"] != null)
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
                        if (ViewState["Location"].ToString().ToUpper().Trim() == "USMP" || ViewState["Location"].ToString().ToUpper().Trim() == "USWB")
                        {
                            SSN.Style["display"] = "table-row";
                            SSNEdit.Style["display"] = "table-row";
                            Tr1County.Style["display"] = "table-row";
                        }
                        else
                        {
                            SSNEdit.Style["display"] = "none";
                            SSN.Style["display"] = "none";
                            Tr1County.Style["display"] = "none";
                        }
                        lblEmployyName.Text = Session["EmpName"].ToString().Trim();
                        GetStates(ViewState["Location"].ToString().Trim());
                        Photo.Src = Session["Photo"].ToString().Trim();
                        int empid = Convert.ToInt32(Session["empID"]);
                        GetEmpDet(empid);
                        GetEmployeeTypes();
                        Getdepartments();
                        GetSchedules();
                        GetAllWages();
                    }

                }
                else
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
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
  
        }

        private void GetEmpDet(int empid)
        {
            Attendance.BAL.EmployeeBL obj = new EmployeeBL();
            try
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
               
                DataTable dt = obj.GetEmployyeDetailsByUserID(empid);
               
                lblSchedule.Text = dt.Rows[0]["StartTime"].ToString() + "-" + dt.Rows[0]["EndTime"].ToString() + " " + "<b>Lunch break: </b>" + dt.Rows[0]["LunchBreakStart"].ToString() + "-" + dt.Rows[0]["LunchBreakEnd"].ToString();
                hdnScID.Value = dt.Rows[0]["ScheduleId"].ToString();
           


                hdnUserID.Value = dt.Rows[0]["UserID"].ToString();

                lblEmpID.Text = dt.Rows[0]["EmpID"].ToString();
                lblFirstname.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();

                lblBusinessName.Text = dt.Rows[0]["ProfessionalFirstName"].ToString() + " " + dt.Rows[0]["ProfessionalLastName"].ToString();


                hdnFirst.Value = dt.Rows[0]["FirstName"].ToString();
                hdnLastname.Value = dt.Rows[0]["LastName"].ToString();

                hdnBusinessFirst.Value = dt.Rows[0]["ProfessionalFirstName"].ToString();
                hdnBusinessLast.Value = dt.Rows[0]["ProfessionalLastName"].ToString();


                lblEmpType.Text = dt.Rows[0]["MasterEmpType"].ToString();
                lbldepartment.Text = dt.Rows[0]["DeptName"].ToString();
                lblDesignation.Text = dt.Rows[0]["designation"].ToString();
                lblStartDate.Text = dt.Rows[0]["startdate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["startdate"].ToString()).ToString("MM/dd/yyyy") == "01/01/1900" ? "" : Convert.ToDateTime(dt.Rows[0]["startdate"].ToString()).ToString("MM/dd/yyyy");
                lblActive.Text = dt.Rows[0]["Isactive"].ToString() == "True" ? "Yes" : "No";
                string img = dt.Rows[0]["PhotoLink"].ToString() == "" ? "defaultUSer.jpg" : dt.Rows[0]["PhotoLink"].ToString().Trim();
                imgPhoto.ImageUrl = "~/Photos/" + img;
                if (lblActive.Text == "No")
                {
                    lbltextTermReason.Visible = true;
                    lbltextTermReason.Text = "<b>Term Reason</b>";
                    lblTextTermDt.Visible = true;
                    lblTextTermDt.Text = "<b>Term Dt</b>";
                    lblTermdate.Visible = true;
                    lblTermdate.Text = dt.Rows[0]["TermDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["TermDate"].ToString()).ToString("MM/dd/yyyy") == "01/01/1900" ? "" : Convert.ToDateTime(dt.Rows[0]["TermDate"].ToString()).ToString("MM/dd/yyyy");
                    txtTermReason.Visible = true;
                    txtTermReason.Text = dt.Rows[0]["TermReason"].ToString();
                }
                else
                {
                    lbltextTermReason.Visible = false;
                    lblTextTermDt.Visible = false;
                    lblTermdate.Visible = false;
                    txtTermReason.Visible = false;
                }

                lblWageType.Text = dt.Rows[0]["WageType"].ToString();
                ViewState["Salary"] = dt.Rows[0]["Salary"].ToString() == "" ? "" : dt.Rows[0]["Salary"].ToString() == "0.00" ? "" : dt.Rows[0]["Salary"].ToString() == "NULL" ? "0.00" : dt.Rows[0]["Salary"].ToString();
                lblSalary.Text = dt.Rows[0]["Salary"].ToString() == "" ? "" : dt.Rows[0]["Salary"].ToString() == "0.00" ? "" : dt.Rows[0]["Salary"].ToString();
                lblSalary.Text = lblSalary.Text == "" ? "" : GeneralFunction.FormatCurrency(lblSalary.Text, ViewState["Location"].ToString().Trim());
                lblDeuctions.Text = dt.Rows[0]["Deductions"].ToString();
                lblGender.Text = dt.Rows[0]["gender"].ToString();
                lblFilling.Text = dt.Rows[0]["MaritalStatus"].ToString();
              
               
                lblDateofBirth.Text = dt.Rows[0]["dateofbirth"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["dateofbirth"].ToString().Trim()).ToString("MM/dd/yyyy") == "01/01/1900" ? "" : Convert.ToDateTime(dt.Rows[0]["dateofbirth"].ToString()).ToString("MM/dd/yyyy");
                lblPhoneNum.Text = dt.Rows[0]["phoneNum"].ToString() == "" ? "" : GeneralFunction.FormatUsTelephoneNo(dt.Rows[0]["phoneNum"].ToString());
                lblMobileNum.Text = dt.Rows[0]["mobileNum"].ToString() == "" ? "" : GeneralFunction.FormatUsTelephoneNo(dt.Rows[0]["mobileNum"].ToString());
                lblBusinessemail.Text = dt.Rows[0]["businessEmail"].ToString();
                lblPersonalEmail.Text = dt.Rows[0]["personalEmail"].ToString();
                lblSSN.Text = GeneralFunction.FormatUsSSN(dt.Rows[0]["ssn"].ToString());

                lblAddress.Text = dt.Rows[0]["Address1"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["Address1"].ToString());
                txtAddress1.Text = dt.Rows[0]["Address1"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["Address1"].ToString());

                lblAddress.Text = lblAddress.Text + (dt.Rows[0]["Address2"].ToString() == "" ? "" : "<br/>" + GeneralFunction.ToProperNotes(dt.Rows[0]["Address2"].ToString()));
                txtAddress2.Text = dt.Rows[0]["Address2"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["Address2"].ToString());
                lblAddress.Text = lblAddress.Text + (dt.Rows[0]["StateCode"].ToString() == "UN" ? "" : "<br/>" + dt.Rows[0]["StateCode"].ToString());
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt.Rows[0]["StateID"].ToString()));
                lblAddress.Text = lblAddress.Text + (dt.Rows[0]["zip"].ToString() == "" ? "" : " " + dt.Rows[0]["zip"].ToString());
                txtZip.Text = dt.Rows[0]["zip"].ToString() == "" ? "" : dt.Rows[0]["zip"].ToString();
                lblLicense.Text = dt.Rows[0]["drivingLicenceNum"].ToString();

                lblCn1Address.Text = dt.Rows[0]["P1Address1"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P1Address1"].ToString());
                txtCn1Address1.Text = dt.Rows[0]["P1Address1"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P1Address1"].ToString());
                lblCn1Address.Text = lblCn1Address.Text + (lblCn1Address.Text != "" ? (dt.Rows[0]["P1Address2"].ToString() == "" ? "" : "<br/>" + GeneralFunction.ToProperNotes(dt.Rows[0]["P1Address2"].ToString())) : (dt.Rows[0]["P1Address2"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P1Address2"].ToString())));
                txtCn1Address2.Text = dt.Rows[0]["P1Address2"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P1Address2"].ToString());


                lblCn1Address.Text = lblCn1Address.Text + (lblCn1Address.Text != "" ? (dt.Rows[0]["state1"].ToString() == "UN" ? "" : "<br/>" + dt.Rows[0]["state1"].ToString()) : dt.Rows[0]["state1"].ToString() == "UN" ? "" : dt.Rows[0]["state1"].ToString());
                ddlCn1State.SelectedIndex = ddlCn1State.Items.IndexOf(ddlCn1State.Items.FindByValue(dt.Rows[0]["stateID1"].ToString()));
                lblCn1Address.Text = lblCn1Address.Text + (lblCn1Address.Text != "" ? (dt.Rows[0]["zip1"].ToString() == "" ? "" : " " + dt.Rows[0]["zip1"].ToString()) : (dt.Rows[0]["zip1"].ToString() == "" ? "" : dt.Rows[0]["zip1"].ToString()));
                txtCn1Zip.Text = dt.Rows[0]["zip1"].ToString();


                lblCn1Name.Text = dt.Rows[0]["Person1"].ToString();
                txtCn1Name.Text = dt.Rows[0]["Person1"].ToString();
                lblCn1Phone.Text = dt.Rows[0]["phone1"].ToString() == "" ? "" : GeneralFunction.FormatUsTelephoneNo(dt.Rows[0]["phone1"].ToString());
                txtCn1Phone.Text = lblCn1Phone.Text.Replace("-", "").Trim();
                lblCn1Email.Text = dt.Rows[0]["email1"].ToString();
                txtCn1Email.Text = dt.Rows[0]["email1"].ToString();
                lblCn1Relation.Text = dt.Rows[0]["relation1"].ToString();
                txtCn1Relation.Text = dt.Rows[0]["relation1"].ToString();

                lblCn2Address.Text = dt.Rows[0]["P2Address1"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P2Address1"].ToString());
                txtCn2Address1.Text = dt.Rows[0]["P2Address1"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P2Address1"].ToString());
                lblCn2Address.Text = lblCn2Address.Text + (lblCn2Address.Text != "" ? (dt.Rows[0]["P2Address2"].ToString() == "" ? "" : "<br/>" +GeneralFunction.ToProperNotes(dt.Rows[0]["P2Address2"].ToString())) : (dt.Rows[0]["P2Address2"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P2Address2"].ToString())));
                txtCn2Address2.Text = dt.Rows[0]["P2Address2"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P2Address2"].ToString());
                ddlCn2State.SelectedIndex = ddlCn1State.Items.IndexOf(ddlCn2State.Items.FindByValue(dt.Rows[0]["stateID2"].ToString()));
                lblCn2Address.Text = lblCn2Address.Text + (lblCn2Address.Text != "" ? (dt.Rows[0]["state2"].ToString() == "UN" ? "" : "<br/>" + dt.Rows[0]["state2"].ToString()) : dt.Rows[0]["state2"].ToString() == "UN" ? "" : dt.Rows[0]["state2"].ToString());
                lblCn2Address.Text = lblCn2Address.Text + (lblCn2Address.Text != "" ? (dt.Rows[0]["zip2"].ToString() == "" ? "" : " " + dt.Rows[0]["zip2"].ToString()) : (dt.Rows[0]["zip2"].ToString() == "" ? "" : dt.Rows[0]["zip2"].ToString()));
                txtCn2Zip.Text = dt.Rows[0]["zip2"].ToString();


                lblCn2Name.Text = dt.Rows[0]["Person2"].ToString();
                txtCn2Name.Text = dt.Rows[0]["Person2"].ToString();
                lblCn2Phone.Text = dt.Rows[0]["phone2"].ToString() == "" ? "" : GeneralFunction.FormatUsTelephoneNo(dt.Rows[0]["phone2"].ToString());
                txtCn2Phone.Text = lblCn2Phone.Text.Replace("-", "").Trim();
                lblCn2Relation.Text = dt.Rows[0]["relation2"].ToString();
                txtCn2Relation.Text = dt.Rows[0]["relation2"].ToString();
                lblCn2Email.Text = dt.Rows[0]["email2"].ToString();
                txtCn2Email.Text = dt.Rows[0]["email2"].ToString();


                lblCn3Address.Text = dt.Rows[0]["P3Address1"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P3Address1"].ToString());
                txtCn3Address1.Text = dt.Rows[0]["P3Address1"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P3Address1"].ToString());
                lblCn3Address.Text = lblCn3Address.Text + (lblCn2Address.Text != "" ? (dt.Rows[0]["P3Address2"].ToString() == "" ? "" : "<br/>" +GeneralFunction.ToProperNotes(dt.Rows[0]["P3Address2"].ToString())) : (dt.Rows[0]["P3Address2"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P3Address2"].ToString())));
                txtCn3Address2.Text = dt.Rows[0]["P3Address2"].ToString() == "" ? "" : GeneralFunction.ToProperNotes(dt.Rows[0]["P3Address2"].ToString());

                lblCn3Address.Text = lblCn3Address.Text + (lblCn2Address.Text != "" ? (dt.Rows[0]["state3"].ToString() == "UN" ? "" : "<br/>" + dt.Rows[0]["state3"].ToString()) : dt.Rows[0]["state3"].ToString() == "UN" ? "" : dt.Rows[0]["state3"].ToString());
                ddlCn3State.SelectedIndex = ddlCn1State.Items.IndexOf(ddlCn3State.Items.FindByValue(dt.Rows[0]["stateID3"].ToString()));
                lblCn3Address.Text = lblCn3Address.Text + (lblCn2Address.Text != "" ? (dt.Rows[0]["zip3"].ToString() == "" ? "" : " " + dt.Rows[0]["zip3"].ToString()) : (dt.Rows[0]["zip3"].ToString() == "" ? "" : dt.Rows[0]["zip3"].ToString()));
                txtCn2Zip.Text = dt.Rows[0]["zip3"].ToString();

                lblCn3Name.Text = dt.Rows[0]["Person3"].ToString();
                txtCn3Name.Text = dt.Rows[0]["Person3"].ToString();
                txtCn3Phone.Text = dt.Rows[0]["phone3"].ToString();
                lblCn3Phone.Text = dt.Rows[0]["phone3"].ToString() == "" ? "" : GeneralFunction.FormatUsTelephoneNo(dt.Rows[0]["phone3"].ToString());
                lblCn3Relation.Text = dt.Rows[0]["relation3"].ToString();
                txtCn3Relation.Text = dt.Rows[0]["relation3"].ToString();
                lblCn3Email.Text = dt.Rows[0]["email3"].ToString();
                txtCn3Email.Text = dt.Rows[0]["email3"].ToString();
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
                //string location = Session["LocationName"].ToString();
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
               // string location = Session["LocationName"].ToString();
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

        protected void btnEmpEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtEditEmpID.Text = lblEmpID.Text.Trim();
                txtEditFirstname.Text = hdnFirst.Value.Trim();
                txtEditLastname.Text = hdnLastname.Value.Trim();

                txtEditBFname.Text = hdnBusinessFirst.Value.Trim();
                txtEditBLname.Text = hdnBusinessLast.Value.Trim();

                ddlEmpType.SelectedIndex = ddlEmpType.Items.IndexOf(ddlEmpType.Items.FindByText(lblEmpType.Text.Trim()));
                ddlSchedule.SelectedIndex = ddlSchedule.Items.IndexOf(ddlSchedule.Items.FindByValue(hdnScID.Value.Trim()));
                ddlEditDepart.SelectedIndex = ddlEditDepart.Items.IndexOf(ddlEditDepart.Items.FindByText(lbldepartment.Text.Trim()));
                txtEditDesg.Text = lblDesignation.Text.Trim();
                if (lblActive.Text == "Yes")
                {
                    rdEditActiveTrue.Checked = true;
                    rdEditActiveFalse.Checked = false;
                }
                else
                {
                    rdEditActiveTrue.Checked = false;
                    rdEditActiveFalse.Checked = true;
                    txtEditTermDate.Visible = true;
                    txtEdit1TermReason.Visible = true;
                    lblEdit1TermDate.Visible = true;
                    lblEdit1Termreason.Visible = true;
                    txtEditTermDate.Text = lblTermdate.Text.Trim();
                    txtEdit1TermReason.Text = txtTermReason.Text.Trim();
                }
                txtEditStartDate.Text = lblStartDate.Text.Trim();
                if (txtEditStartDate.Text == "")
                {
                    txtEditStartDate.Enabled = true;
                }
                else
                {
                    txtEditStartDate.Enabled = false;
                }
                EdiImg.ImageUrl = imgPhoto.ImageUrl;
                mdlEditPopup.Show();
            }
            catch (Exception ex)
            {
            }
        }

        private void GetEmployeeTypes()
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataTable dt = obj.GetAllEmployeetypes();
                ddlEmpType.DataSource = dt;
                ddlEmpType.DataTextField = "EmpType";
                ddlEmpType.DataValueField = "EmpTypeID";

                ddlEmpType.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        private void GetSchedules()
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataTable dt = obj.GetAllScheduleTypes();
                ddlSchedule.DataSource = dt;
                ddlSchedule.DataTextField = "ScheduleType";
                ddlSchedule.DataValueField = "ScheduleID";
                ddlSchedule.DataBind();
                ddlSchedule.Items.Insert(0, new ListItem("Select", "0"));

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
                ddlEditDepart.DataSource = dt;
                ddlEditDepart.DataTextField = "Deptname";
                ddlEditDepart.DataValueField = "DeptID";

                ddlEditDepart.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        protected void rdEditActiveTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (rdEditActiveTrue.Checked == true)
            {
                txtEditTermDate.Visible = false;
                txtEdit1TermReason.Visible = false;
                lblEdit1TermDate.Visible = false;
                lblEdit1Termreason.Visible = false;
            }

        }

        protected void rdEditActiveFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (rdEditActiveFalse.Checked == true)
            {
                txtEditTermDate.Visible = true;
                txtEdit1TermReason.Visible = true;
                lblEdit1TermDate.Visible = true;
                lblEdit1Termreason.Visible = true;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Attendance.Entities.UserInfo objInfo = new UserInfo();
            string timezone = "";

            try
            {
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
                hdnToday.Value = ISTTime.ToString("MM/dd/yyyy");


                btnUpdate.Attributes.Add("Enabled", "False");

                int UserID = Convert.ToInt32(Session["UserID"]);
                int EmployeeID = Convert.ToInt32(hdnUserID.Value);

                objInfo.Firstname = txtEditFirstname.Text == "" ? "" : GeneralFunction.ToProper(txtEditFirstname.Text.Trim());
                objInfo.Lastname = txtEditLastname.Text == "" ? "" : GeneralFunction.ToProper(txtEditLastname.Text.Trim());

                objInfo.BLastname = txtEditBLname.Text == "" ? txtEditFirstname.Text.Trim() : GeneralFunction.ToProper(txtEditBLname.Text.Trim());
                objInfo.BFirstname = txtEditBFname.Text == "" ? txtEditLastname.Text.Trim() : GeneralFunction.ToProper(txtEditBFname.Text.Trim());
                
                
                
                string PhotoLink = imgPhoto.ImageUrl.Replace("~/Photos/", "").Trim();
                try
                {
                    if (photoUpload1.HasFile)
                    {
                        string filePhotoName = photoUpload1.FileName;
                        string Photoextension = System.IO.Path.GetExtension(filePhotoName);
                        if (Photoextension == ".jpg" || Photoextension == ".png" || Photoextension == ".jpeg" || Photoextension == ".JPG" || Photoextension == ".PNG" || Photoextension == ".JPEG")
                        {
                            string SaveFileLoc = Server.MapPath("~/Photos/");
                            if (System.IO.Directory.Exists(SaveFileLoc) == false)
                            {
                                System.IO.Directory.CreateDirectory(SaveFileLoc);
                            }

                            string FileNameSaveData = SaveFileLoc + (objInfo.Firstname.Trim() + objInfo.Lastname.Trim()).Trim() + ".jpeg";
                            PhotoLink = (objInfo.Firstname.Trim() + objInfo.Lastname.Trim()).Trim() + ".jpeg";


                            photoUpload1.SaveAs(FileNameSaveData);
                            Bitmap oBitmap = default(Bitmap);
                            oBitmap = new Bitmap(FileNameSaveData);
                            Graphics oGraphic = default(Graphics);

                            int newwidthimg = 140;
                            // Here create a new bitmap object of the same height and width of the image.
                            // float AspectRatio = (float)oBitmap.Size.Width / (float)oBitmap.Size.Height;

                            int newHeight = 140;
                            Bitmap bmpNew = new Bitmap(newwidthimg, newHeight);
                            oGraphic = Graphics.FromImage(bmpNew);

                            oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                            oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                            oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;


                            oGraphic.DrawImage(oBitmap, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), 0, 0, oBitmap.Width, oBitmap.Height, GraphicsUnit.Pixel);
                            // Release the lock on the image file. Of course,
                            // image from the image file is existing in Graphics object
                            oBitmap.Dispose();
                            oBitmap = bmpNew;

                            //SolidBrush oBrush = new SolidBrush(Color.Black);
                            //Font ofont = new Font("Arial", 8);
                            //oGraphic.DrawString("Some text to write", ofont, oBrush, 10, 10);
                            //oGraphic.Dispose();
                            //ofont.Dispose();
                            //oBrush.Dispose();
                            oBitmap.Save(FileNameSaveData, ImageFormat.Jpeg);
                            oBitmap.Dispose();
                       }
                    }
                }
                catch (Exception ex)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('File access denied');", true);
                }

                //objInfo.Lastname = GeneralFunction.ToProper(txtEditLastname.Text.Trim());
                objInfo.ScheduleID = Convert.ToInt32(ddlSchedule.SelectedItem.Value);
                objInfo.EmpTypeID = Convert.ToInt32(ddlEmpType.SelectedItem.Value);
                objInfo.Deptname = ddlEditDepart.SelectedItem.Text.ToString();
                objInfo.Designation = GeneralFunction.ToProper(txtEditDesg.Text.Trim());
                objInfo.StartDt = txtEditStartDate.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtEditStartDate.Text);
                objInfo.TermDt = txtEditTermDate.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtEditTermDate.Text);
                objInfo.TermReason = txtEdit1TermReason.Text.Trim();
                if (rdEditActiveFalse.Checked == true)
                {
                    objInfo.IsActive = false;
                }
                else
                {
                    objInfo.IsActive = true;
                    objInfo.TermDt = Convert.ToDateTime("01/01/1900");
                    objInfo.TermReason = "";
                }

                Attendance.BAL.Report obj = new Report();
                String strHostName = Request.UserHostAddress.ToString();
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                bool bnew = obj.UpdateUser(objInfo, EmployeeID, UserID, PhotoLink, strIp);
                if (bnew)
                {
                    btnUpdate.Enabled = true;
                    btnEditCancel.Enabled = true;
                    mdlEditPopup.Hide();
                    txtEdit1TermReason.Text = "";
                    txtEditTermDate.Text = "";
                    txtEditDesg.Text = "";
                    txtEditEmpID.Text = "";
                    txtEditFirstname.Text = "";
                    txtEditLastname.Text = "";
                    txtEditStartDate.Text = "";
                    ddlEditDepart.SelectedIndex = 0;
                    ddlEmpType.SelectedIndex = 0;
                    ddlSchedule.SelectedIndex = 0;
                    GetEmpDet(EmployeeID);

                }
            }


            catch (Exception ex)
            {

            }
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            mdlEditPopup.Hide();
            txtEdit1TermReason.Text = "";
            txtEditTermDate.Text = "";
            txtEditDesg.Text = "";
            txtEditEmpID.Text = "";
            txtEditFirstname.Text = "";
            txtEditLastname.Text = "";
            txtEditStartDate.Text = "";
            ddlEditDepart.SelectedIndex = 0;
            ddlEmpType.SelectedIndex = 0;
            ddlSchedule.SelectedIndex = 0;
        }

        protected void btnEditSalaryDetails_Click(object sender, EventArgs e)
        {

            ddlEditWage.SelectedIndex = ddlEditWage.Items.IndexOf(ddlEditWage.Items.FindByText(lblWageType.Text.Trim()));
            txtEditSal.Text = ViewState["Salary"].ToString().Trim();
            ddlDeductions.SelectedIndex = ddlDeductions.Items.IndexOf(ddlDeductions.Items.FindByText(lblDeuctions.Text.Trim()));
            if (lblFilling.Text == "Single")
            {
                rdSingle.Checked = true;
                rdMarried.Checked = false;
            }
            else if (lblFilling.Text == "Married")
            {
                rdSingle.Checked = false;
                rdMarried.Checked = true;
            }
            else
            {
                rdSingle.Checked = false;
                rdMarried.Checked = false;
            }

            mdlEditSalTaxPopup.Show();
        }

        protected void btnSalEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Attendance.Entities.UserInfo objInfo = new UserInfo();
                objInfo.WageID = Convert.ToInt32(ddlEditWage.SelectedIndex);
                objInfo.Deductions = Convert.ToInt32(ddlDeductions.SelectedIndex);
                objInfo.Salary = txtEditSal.Text.Trim();

                if (rdSingle.Checked)
                {
                    objInfo.MaritalID = 1;
                }
                else
                {
                    objInfo.MaritalID = 2;
                }

                int UserID = Convert.ToInt32(Session["UserID"]);
                int EmployeeID = Convert.ToInt32(hdnUserID.Value);
                String strHostName = Request.UserHostAddress.ToString();
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();

                Attendance.BAL.Report obj = new Report();
                bool bnew = obj.UpdateUserSalTaxDetails(objInfo, EmployeeID, UserID, strIp);
                ddlDeductions.SelectedIndex = 0;
                txtEditSal.Text = "";
                ddlEditWage.SelectedIndex = 0;
                rdSingle.Checked = true;
                //Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                GetEmpDet(EmployeeID);
                mdlEditSalTaxPopup.Hide();


            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSalEditCancel_Click(object sender, EventArgs e)
        {
            ddlDeductions.SelectedIndex = 0;
            txtEditSal.Text = "";
            ddlEditWage.SelectedIndex = 0;
            rdSingle.Checked = true;
            mdlEditSalTaxPopup.Hide();

        }

        private void GetAllWages()
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataTable dt = obj.GetAllWages();
                ddlEditWage.DataSource = dt;
                ddlEditWage.DataTextField = "WageType";
                ddlEditWage.DataValueField = "WageID";

                ddlEditWage.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnEdittaxDet_Click(object sender, EventArgs e)
        {

            ddlEditWage.SelectedIndex = ddlEditWage.Items.IndexOf(ddlEditWage.Items.FindByText(lblWageType.Text.Trim()));
            txtEditSal.Text = ViewState["Salary"].ToString().Trim();
            ddlDeductions.SelectedIndex = ddlDeductions.Items.IndexOf(ddlDeductions.Items.FindByText(lblDeuctions.Text.Trim()));
            if (lblFilling.Text == "Single")
            {
                rdSingle.Checked = true;
                rdMarried.Checked = false;
            }
            else if (lblFilling.Text == "Married")
            {
                rdSingle.Checked = false;
                rdMarried.Checked = true;
            }
            else
            {
                rdSingle.Checked = false;
                rdMarried.Checked = false;
            }
            mdlEditSalTaxPopup.Show();
        }

        protected void btnEditPersonalDet_Click(object sender, EventArgs e)
        {

            if (lblGender.Text == "Male")
            {
                rdMale.Checked = true;
            }
            else if (lblGender.Text == "Female")
            {
                rdFemale.Checked = true;
            }
            else
            {
                rdMale.Checked = false;
                rdFemale.Checked = false;
            }
            txtDatBirth.Text = lblDateofBirth.Text == "" ? "" : Convert.ToDateTime(lblDateofBirth.Text).ToString("MM-dd-yyyy");
            txtEmpMobile.Text = lblMobileNum.Text.Replace("-", "").Trim();
            txtEmpPhone.Text = lblPhoneNum.Text.Replace("-", "").Trim();
            txtPersonalEmail.Text = lblPersonalEmail.Text;
            txtBusinessEmail.Text = lblBusinessemail.Text;
            txtDriving.Text = lblLicense.Text;
            txtSSN.Text = lblSSN.Text;
            mdlEditPersonalDetails.Show();
        }

        protected void btnEditEmergencyDet_Click(object sender, EventArgs e)
        {
            mdlEditEmergContactDet.Show();
        }

        protected void btnEditEmergCancel_Click(object sender, EventArgs e)
        {
            mdlEditEmergContactDet.Hide();
        }

        protected void btnUpdateEmergency_Click(object sender, EventArgs e)
        {
            try
            {
                Attendance.Entities.EmergencyContactInfo objContactInfo = new EmergencyContactInfo();
                int UserID = Convert.ToInt32(Session["UserID"]);
                int EmployeeID = Convert.ToInt32(hdnUserID.Value);
                String strHostName = Request.UserHostAddress.ToString();
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();

                objContactInfo.Person1 = txtCn1Name.Text.ToString() == "" ? "" : GeneralFunction.ToProper(txtCn1Name.Text.ToString().Trim());
                objContactInfo.P1Address1 = GeneralFunction.ToProperNotes(txtCn1Address1.Text.ToString().Trim());
                objContactInfo.P1Address2 = GeneralFunction.ToProperNotes(txtCn1Address2.Text.ToString().Trim());
                objContactInfo.Phone1 = txtCn1Phone.Text.ToString().Trim();
                objContactInfo.Relation1 = GeneralFunction.ToProperNotes(txtCn1Relation.Text.ToString().Trim());
                objContactInfo.Email1 = txtCn1Email.Text.ToString().Trim();
                objContactInfo.StateID1 = Convert.ToInt32(ddlCn1State.SelectedItem.Value);
                // objContactInfo.Zip1 = txtCn1Zip.Text.Trim();

                if (txtCn1Zip.Text != "" && txtCn1Zip.Text.Trim().Length == 4)
                {
                    objContactInfo.Zip1 = "0" + txtCn1Zip.Text.Trim();
                }
                else
                {
                    objContactInfo.Zip1 = txtCn1Zip.Text.Trim();
                }


                objContactInfo.Person2 = txtCn2Name.Text.ToString() == "" ? "" : GeneralFunction.ToProper(txtCn2Name.Text.ToString().Trim());
                objContactInfo.P2Address1 = GeneralFunction.ToProperNotes((txtCn2Address1.Text.ToString().Trim()));
                objContactInfo.P2Address2 = GeneralFunction.ToProperNotes(txtCn2Address2.Text.ToString().Trim());
                objContactInfo.Phone2 = txtCn2Phone.Text.ToString().Trim();
                objContactInfo.Relation2 = GeneralFunction.ToProperNotes(txtCn2Relation.Text.ToString().Trim());
                objContactInfo.Email2 = txtCn2Email.Text.ToString().Trim();
                objContactInfo.StateID2 = Convert.ToInt32(ddlCn2State.SelectedItem.Value);
                if (txtCn2Zip.Text != "" && txtCn2Zip.Text.Trim().Length == 4)
                {
                    objContactInfo.Zip2 = "0" + txtCn2Zip.Text.Trim();
                }
                else
                {
                    objContactInfo.Zip2 = txtCn2Zip.Text.Trim();
                }


                objContactInfo.Person3 = txtCn3Name.Text.ToString() == "" ? "" : GeneralFunction.ToProper(txtCn3Name.Text.ToString().Trim());
                objContactInfo.P3Address1 = GeneralFunction.ToProperNotes(txtCn3Address1.Text.ToString().Trim());
                objContactInfo.P3Address2 = GeneralFunction.ToProperNotes(txtCn3Address2.Text.ToString().Trim());
                objContactInfo.Phone3 = txtCn3Phone.Text.ToString().Trim();
                objContactInfo.Relation3 = GeneralFunction.ToProperNotes(txtCn3Relation.Text.ToString().Trim());
                objContactInfo.Email3 = txtCn3Email.Text.ToString().Trim();
                objContactInfo.StateID3 = Convert.ToInt32(ddlCn3State.SelectedItem.Value);
                if (txtCn3Zip.Text != "" && txtCn3Zip.Text.Trim().Length == 4)
                {
                    objContactInfo.Zip3 = "0" + txtCn3Zip.Text.Trim();
                }
                else
                {
                    objContactInfo.Zip3 = txtCn3Zip.Text.Trim();
                }

                Attendance.BAL.Report obj = new Report();
                obj.UpdateEmergencyDetails(objContactInfo, UserID, EmployeeID, strIp);
                //Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                GetEmpDet(EmployeeID);
                mdlEditEmergContactDet.Hide();


            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdatePersonal_Click(object sender, EventArgs e)
        {
            try
            {
                Attendance.Entities.UserInfo objInfo = new UserInfo();
                int UserID = Convert.ToInt32(Session["UserID"]);
                int EmployeeID = Convert.ToInt32(hdnUserID.Value);
                String strHostName = Request.UserHostAddress.ToString();
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                if (rdMale.Checked == true)
                {
                    objInfo.Gender = "Male";
                }
                else
                {
                    objInfo.Gender = "Female";
                }


                objInfo.DateOfBirth = txtDatBirth.Text.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(Convert.ToDateTime(txtDatBirth.Text.Trim()).ToString("MM/dd/yyyy"));
                objInfo.Phone = txtEmpPhone.Text;
                objInfo.Mobile = txtEmpMobile.Text;
                objInfo.BusinessEmail = txtBusinessEmail.Text;
                objInfo.PersonalEmail = txtPersonalEmail.Text;
                string LocationName = ViewState["Location"].ToString().Trim();
                if (LocationName.Trim().ToUpper() == "INBH" || LocationName.Trim().ToUpper() == "INDG")
                {
                    objInfo.SSN = "";
                }
                else
                {
                    objInfo.SSN = txtSSN.Text.Trim();
                }
                objInfo.Address1 = GeneralFunction.ToProperNotes(txtAddress1.Text.ToString().Trim());
                objInfo.Address2 = GeneralFunction.ToProperNotes(txtAddress2.Text.ToString().Trim());
                objInfo.StateID = Convert.ToInt32(ddlState.SelectedItem.Value);
                objInfo.County = txtCounty.Text;
                if (txtZip.Text != "" && txtZip.Text.Trim().Length == 4)
                {
                    objInfo.Zip = "0" + txtZip.Text.Trim();
                }
                else
                {
                    objInfo.Zip = txtZip.Text.Trim();
                }
                objInfo.DriverLicense = txtDriving.Text.Trim();

                Attendance.BAL.Report obj = new Report();
                obj.UpdatePersonalDetails(objInfo, UserID, EmployeeID, strIp);
                GetEmpDet(EmployeeID);
                mdlEditPersonalDetails.Hide();


            }
            catch (Exception ex)
            {
            }
        }

        protected void btnPersonalEditCancel_Click(object sender, EventArgs e)
        {
            mdlEditPersonalDetails.Hide();
        }
        private void GetStates(string Location)
        {
            try
            {
                int locationid = 0;
                if (Location.Trim() == "INDG" || Location.Trim() == "INBH")
                {
                    locationid = 1;
                }
                else
                {
                    locationid = 2;
                }
                Attendance.BAL.Report obj = new Report();
                DataTable dt = obj.GetAllStates(locationid);
                ddlState.DataSource = dt;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateID";
                ddlState.DataBind();

                ddlCn1State.DataSource = dt;
                ddlCn1State.DataTextField = "StateName";
                ddlCn1State.DataValueField = "StateID";
                ddlCn1State.DataBind();

                ddlCn2State.DataSource = dt;
                ddlCn2State.DataTextField = "StateName";
                ddlCn2State.DataValueField = "StateID";
                ddlCn2State.DataBind();

                ddlCn3State.DataSource = dt;
                ddlCn3State.DataTextField = "StateName";
                ddlCn3State.DataValueField = "StateID";
                ddlCn3State.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
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


        protected void lnkScheduleAdd_Click(object sender, EventArgs e)
        {
            mdlEditPopup.Show();

            txtScheduleEnd.Text = "";
            txtSheduleStart.Text = "";
            txtLunchEnd.Text = "";
            txtLunchStart.Text = "";
            rdFive.Checked = false;
            rdSix.Checked = false;
            rdSeven.Checked = false;
            mdlSchedulepopup.Show();


        }

        protected void btnSchUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string SchStartTime = txtSheduleStart.Text.ToString().Trim();
                string SchEndTime = txtScheduleEnd.Text.ToString().Trim();
                string LunchStartime = txtLunchStart.Text.ToString().Trim();
                string LunchEndtime = txtLunchEnd.Text.ToString().Trim();
                bool Fivedays = false;
                bool Sixdays = false;
                bool Sevendays = false;
                if (rdFive.Checked)
                {
                    Fivedays = true;
                }
                else if (rdSix.Checked)
                {
                    Sixdays = true;
                }
                else if (rdSeven.Checked)
                {
                    Sevendays = true;
                }

                int UserID = Convert.ToInt32(Session["UserID"]);

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

                String strHostName = Request.UserHostAddress.ToString();
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();

                EmployeeBL obj = new EmployeeBL();
                string ID = obj.AddNewSchedule(SchStartTime, SchEndTime, LunchStartime, LunchEndtime, Fivedays, Sixdays, Sevendays, strIp, ISTTime, UserID);
                mdlSchedulepopup.Hide();
                GetSchedules();
                ddlSchedule.SelectedIndex = ddlSchedule.Items.IndexOf(ddlSchedule.Items.FindByValue(ID == "" ? "0" : ID));
                mdlEditPopup.Show();

            }
            catch (Exception ex)
            {
            }

        }

        protected void btnCancelSch_Click(object sender, EventArgs e)
        {

            mdlSchedulepopup.Hide();

        }

        protected void lnkResetPasscode_Click(object sender, EventArgs e)
        {
            txtResetNewPasscode.Text = "";
            txtResetConfirmPasscode.Text = "";
            lblResetPasscodeName.Text = lblFirstname.Text.Trim();
            mdlResetPasscode.Show();
        }

        protected void btnResetCancelPasscode_Click(object sender, EventArgs e)
        {
            txtResetNewPasscode.Text = "";
            txtResetConfirmPasscode.Text = "";
            mdlResetPasscode.Hide();
        }

        protected void btnResetPassCode_Click(object sender, EventArgs e)
        {
            try
            {
                string passcode = txtResetNewPasscode.Text.Trim();
                string EmpID = lblEmpID.Text.Trim();
                String strHostName = Request.UserHostAddress.ToString();
                int userid = Convert.ToInt32(Session["UserID"]);
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
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


                EmployeeBL obj = new EmployeeBL();
                bool bnew=obj.ResetPasscodeByAdmin(userid,strIp,passcode,CurentDatetime,EmpID);
                mdlResetPasscode.Hide();


            }
            catch (Exception ex)
            {

            }

        }


        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string password = txtResetNewPassword.Text.Trim();
                string EmpID = lblEmpID.Text.Trim();
                String strHostName = Request.UserHostAddress.ToString();
                int userid = Convert.ToInt32(Session["UserID"]);
                string strIp = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
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


                EmployeeBL obj = new EmployeeBL();
                bool bnew = obj.ResetPassWordByAdmin(userid, strIp, password, CurentDatetime, EmpID);
                mdlResetPassword.Hide();


            }
            catch (Exception ex)
            {

            }

        }


        protected void lnkResetPassword_Click(object sender, EventArgs e)
        {
            txtResetNewPassword.Text = "";
            txtResetConfirmPassword.Text = "";
            lblResetPasswordName.Text = lblFirstname.Text.Trim();
            mdlResetPassword.Show();
        }

        protected void btnResetCancelPassword_Click(object sender, EventArgs e)
        {
            txtResetNewPassword.Text = "";
            txtResetConfirmPassword.Text = "";
            mdlResetPassword.Hide();
        }

    }
}
