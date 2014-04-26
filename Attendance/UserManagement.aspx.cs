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
    public partial class UserManagement : System.Web.UI.Page
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
                    lblLocation.Text = Session["LocationName"].ToString();
                    lblEmployyName.Text = Session["EmpName"].ToString().Trim();
                    Photo.Src = Session["Photo"].ToString().Trim();
                    int sort = 1;
                    Session["SortBy"] = 1;
                    ddlSelect.SelectedIndex = 1;
                   // GetLocations();
                    GetMasterShifts(lblLocation.Text.ToString());
                    ddlShifts.SelectedIndex = ddlShifts.Items.IndexOf(ddlShifts.Items.FindByValue(Session["ShiftID"].ToString()));
                    Getdepartments();
                    GetEmployeeTypes();
                    GetShifts(Session["LocationName"].ToString().Trim());
                    ddlgridShift.SelectedIndex = ddlgridShift.Items.IndexOf(ddlgridShift.Items.FindByValue(Session["ShiftID"].ToString()));
                    GetSchedules();
                    GetAllWages();
                    GetUserDetails(sort,Convert.ToInt32(ddlgridShift.SelectedItem.Value));
                    GetStates(Session["LocationName"].ToString().Trim());
                    GetSSN();
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
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void GetSSN()
        {
            try
            {
                string locationname = Session["LocationName"].ToString().Trim();
                if (locationname.ToUpper() == "USWB" || locationname.ToUpper() == "USMP")
                {
                    SSN.Style["display"] = "table-row";
                }
                else
                {
                    SSN.Style["display"] = "none";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetUserDetails(int sort,int shiftID)
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
             
                DataTable dt = obj.GetUsers(Session["LocationName"].ToString().Trim(), sort,shiftID);
                Session["AllusersData"] = dt;
                if (dt.Rows.Count > 0)
                {
                  //  Total employee: 30 Active employee: 25 Incative employee: 5 
                    if(sort==0)
                    {
                        lblTotal.Text = "Total employee: " + dt.Rows[0]["TotalCount"].ToString() + " " + " " + " " + " " + "Active employee: " + dt.Rows[0]["ActiveCount"].ToString() +" "+" "+" "+" "+"Incative employee: " + dt.Rows[0]["InactiveCount"].ToString();
                    }
                    else if (sort ==1)
                    {
                        lblTotal.Text = "Total employee: " + dt.Rows[0]["TotalCount"].ToString() + " " + " " + " " + " " + "Active employee: " + dt.Rows[0]["ActiveCount"].ToString() +" "+" "+" "+" "+"Incative employee: " + dt.Rows[0]["InactiveCount"].ToString();
                    }
                    else
                    {
                        lblTotal.Text = "Total employee: " + dt.Rows[0]["TotalCount"].ToString() + " " + " " + " " + " " + "Active employee: " + dt.Rows[0]["ActiveCount"].ToString() +" "+" "+" "+" "+"Incative employee: " + dt.Rows[0]["InactiveCount"].ToString();
                    }
                    grdUsers.DataSource = dt;
                    grdUsers.DataBind();
                    lblGrdNodata.Text = "";
                    dvNodata.Style["display"] = "none";

                    BizUtility.GridSortInitail("Ascending", "Firstname", grdUsers, 0, dt);
                }
                else
                {
                    lblTotal.Text = "";
                    lblGrdNodata.Text = "No data found";
                    dvNodata.Style["display"] = "block";
                    grdUsers.DataSource = null;
                    grdUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkAddUser_Click(object sender, EventArgs e)
        {

            txtAddFirstName.Text = "";
            txtAddLastName.Text = "";
            txtStartDt.Text = "";
            txtBusinessEmail.Text = "";
            txtPersonalEmail.Text = "";
            txtDesignation.Text = "";
            txtEmpAddress1.Text = "";
            txtEmpAddress2.Text = "";
            txtEmpDriveLicense.Text = "";
            txtEmpMobile.Text = "";
            txtEmpPhone.Text = "";
            txtEmpSSN.Text = "";
            txtEmpZip.Text = "";
            txtCounty.Text = "";

            txtsalary.Text = "";
            rdMarried.Checked = false;
            rdMarriedSingle.Checked = false;
            rdGenderFeMale.Checked = false;
            rdGenderMale.Checked = false;
            txtDateOfBirth.Text = "";


            txtCn1Address1.Text = "";
            txtCn1Address2.Text = "";
            txtCn1Email.Text = "";
            txtCn1Name.Text = "";
            txtCn1Phone.Text = "";
            txtCn1Relation.Text = "";
            ddlCn1State.SelectedIndex = 0;
            ddlCn2State.SelectedIndex = 0;
            ddlCn3State.SelectedIndex = 0;
            txtCn1Zip.Text = "";
            txtCn2Zip.Text = "";
            txtCn3Zip.Text = "";
            txtCn2Address1.Text = "";
            txtCn2Address2.Text = "";
            txtCn2Email.Text = "";
            txtCn2Name.Text = "";
            txtCn2Phone.Text = "";
            txtCn2Relation.Text = "";

            txtCn3Address1.Text = "";
            txtCn3Address2.Text = "";
            txtCn3Email.Text = "";
            txtCn3Name.Text = "";
            txtCn3Phone.Text = "";
            txtCn3Relation.Text = "";

            ddlDeptment.SelectedIndex = 0;
            ddlEmpState.SelectedIndex = 0;
            ddlEmpType.SelectedIndex = 0;
            ddlSchedule.SelectedIndex = 0;
            ddlWagetype.SelectedIndex = 0;
            ddlDeductions.SelectedIndex = 0;



            lblError.Text = "";
            mdlAddPopUp.Show();
        }

        private void Getdepartments()
        {
            try
            {
                    Attendance.BAL.Report obj = new Report();
                    DataTable dt = obj.GetAllDepartments();
                    ddlDeptment.DataSource = dt;
                    ddlDeptment.DataTextField = "Deptname";
                    ddlDeptment.DataValueField = "DeptID";
                    ddlDeptment.DataBind();
                    ddlEditDepart.DataSource = dt;
                    ddlEditDepart.DataTextField = "Deptname";
                    ddlEditDepart.DataValueField = "DeptID";
                    ddlEditDepart.DataBind();
            }
            catch (Exception ex)
            {
            }
        }


        //private void GetLocations()
        //{
        //    try
        //    {
        //        Attendance.BAL.Report obj = new Report();
        //        DataTable dt = obj.GetLocations();
        //        ddllocation.DataSource = dt;
        //        ddllocation.DataTextField = "LocationName";
        //        ddllocation.DataValueField = "LocationId";
        //        ddllocation.DataBind();
        //         ddlEditLocation.DataSource = dt;
        //        ddlEditLocation.DataTextField = "LocationName";
        //        ddlEditLocation.DataValueField = "LocationId";
        //        ddlEditLocation.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Attendance.Entities.UserInfo objInfo = new UserInfo();
            Attendance.Entities.EmergencyContactInfo objContactInfo = new EmergencyContactInfo();
            try
            {
                Attendance.BAL.Report obj = new Report();

                objInfo.Firstname = txtAddFirstName.Text == "" ? "" : GeneralFunction.ToProper(txtAddFirstName.Text.Trim()).Trim();
                objInfo.Lastname = txtAddLastName.Text == "" ? "" : GeneralFunction.ToProper(txtAddLastName.Text.Trim()).Trim();
                string PhotoLink = "defaultUSer.jpg";

                try
                {
                    if (photoUpload.HasFile)
                    {
                        string filePhotoName = photoUpload.FileName;
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

                            photoUpload.SaveAs(FileNameSaveData);
                            Bitmap oBitmap = default(Bitmap);
                            oBitmap = new Bitmap(FileNameSaveData);
                            Graphics oGraphic = default(Graphics);

                            int newwidthimg = 140;
                            // Here create a new bitmap object of the same height and width of the image.
                            //float AspectRatio = (float)oBitmap.Size.Width / (float)oBitmap.Size.Height;
                            //int newHeight = Convert.ToInt32(newwidthimg / AspectRatio);
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

                int UserID = Convert.ToInt32(Session["UserID"]);
                //objInfo.EmpID = txtAddEmpID.Text.Trim();
                objInfo.BFirstname = txtBusinessFirst.Text == "" ? objInfo.Firstname.Trim() : GeneralFunction.ToProper(txtBusinessFirst.Text.Trim());
                objInfo.BLastname = txtBusinessLasst.Text == "" ? objInfo.Lastname.Trim() : GeneralFunction.ToProper(txtBusinessLasst.Text.Trim());
               
                objInfo.Deptname = ddlDeptment.SelectedItem.Text.ToString();
                objInfo.Designation = txtDesignation.Text == "" ? "" : GeneralFunction.ToProper(txtDesignation.Text.Trim());
                objInfo.ScheduleID = Convert.ToInt32(ddlSchedule.SelectedItem.Value);
                objInfo.ShiftID = Convert.ToInt32(ddlShift.SelectedItem.Value);
                objInfo.StartDt = txtStartDt.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtStartDt.Text);
                if (rdActiveFalse.Checked == true)
                {
                    objInfo.IsActive = false;
                }
                else
                {
                    objInfo.IsActive = true;
                }



                if (rdGenderMale.Checked == true)
                {
                    objInfo.Gender = "Male";
                }
                else
                {
                    objInfo.Gender = "Female";
                }

                if (rdMarriedSingle.Checked == true)
                {
                    objInfo.MaritalID = 1;
                }
                else
                {
                    objInfo.MaritalID = 2;
                }


                objInfo.DateOfBirth = txtDateOfBirth.Text.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtDateOfBirth.Text);
                objInfo.Phone = txtEmpPhone.Text;
                objInfo.Mobile = txtEmpMobile.Text;
                objInfo.BusinessEmail = txtBusinessEmail.Text;
                objInfo.PersonalEmail = txtPersonalEmail.Text;
                objInfo.WageID = Convert.ToInt32(ddlWagetype.SelectedItem.Value);
                objInfo.Salary = txtsalary.Text.Trim();
                objInfo.Deductions = Convert.ToInt32(ddlDeductions.SelectedItem.Value);
                objInfo.County = txtCounty.Text.Trim();

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
                    objContactInfo.Zip2= "0" + txtCn2Zip.Text.Trim();
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


                string LocationName = Session["LocationName"].ToString().Trim();
                if (LocationName.Trim().ToUpper() == "INBH" || LocationName.Trim().ToUpper() == "INDG")
                {
                    objInfo.SSN = "";
                }
                else
                {
                    objInfo.SSN = txtEmpSSN.Text.Trim();
                }
                objInfo.Address1 = GeneralFunction.ToProperNotes(txtEmpAddress1.Text.ToString().Trim());
                objInfo.Address2 = GeneralFunction.ToProperNotes(txtEmpAddress2.Text.ToString().Trim());
                objInfo.StateID = Convert.ToInt32(ddlEmpState.SelectedItem.Value);
                if (txtEmpZip.Text != "" && txtEmpZip.Text.Trim().Length == 4)
                {
                    objInfo.Zip = "0" + txtEmpZip.Text.Trim();
                }
                else
                {
                    objInfo.Zip = txtEmpZip.Text.Trim();
                }
                objInfo.DriverLicense = txtEmpDriveLicense.Text.Trim();
                objInfo.EmpTypeID = Convert.ToInt32(ddlEmpType.SelectedItem.Value);

                if (objInfo.SSN != "")
                {
                    if (obj.CheckUniqueSSN(objInfo.SSN.Trim()))
                    {

                        bool bnew = obj.AddUser(objInfo, objContactInfo, UserID, LocationName, PhotoLink);
                        if (bnew)
                        {
                            txtAddFirstName.Text = "";
                            txtAddLastName.Text = "";
                            txtBusinessEmail.Text = "";
                            txtDateOfBirth.Text = "";
                            txtDesignation.Text = "";
                            txtEmpAddress1.Text = "";
                            txtEmpAddress2.Text = "";
                            txtEmpDriveLicense.Text = "";
                            txtEmpMobile.Text = "";
                            txtEmpPhone.Text = "";
                            txtEmpSSN.Text = "";
                            txtEmpZip.Text = "";

                            txtCn1Address1.Text = "";
                            txtCn1Address2.Text = "";
                            txtCn1Email.Text = "";
                            txtCn1Name.Text = "";
                            txtCn1Phone.Text = "";
                            txtCn1Relation.Text = "";

                            txtCn2Address1.Text = "";
                            txtCn2Address2.Text = "";
                            txtCn2Email.Text = "";
                            txtCn2Name.Text = "";
                            txtCn2Phone.Text = "";
                            txtCn2Relation.Text = "";

                            txtCn3Address1.Text = "";
                            txtCn3Address2.Text = "";
                            txtCn3Email.Text = "";
                            txtCn3Name.Text = "";
                            txtCn3Phone.Text = "";
                            txtCn3Relation.Text = "";

                            ddlDeptment.SelectedIndex = 0;
                            ddlEmpState.SelectedIndex = 0;
                            ddlEmpType.SelectedIndex = 0;
                            ddlSchedule.SelectedIndex = 0;

                            mdlAddPopUp.Hide();
                            GetUserDetails(Convert.ToInt32(Session["SortBy"]),Convert.ToInt32(ddlgridShift.SelectedValue));
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Employee details are not added. SSN is already existed";
                        txtEmpSSN.Text = "";
                        txtEmpSSN.Focus();
                        mdlAddPopUp.Show();
                    }
                }
                else
                {
                     bool bnew = obj.AddUser(objInfo, objContactInfo, UserID, LocationName, PhotoLink);
                     if (bnew)
                     {
                         txtAddFirstName.Text = "";
                         txtAddLastName.Text = "";
                         txtBusinessEmail.Text = "";
                         txtDateOfBirth.Text = "";
                         txtDesignation.Text = "";
                         txtEmpAddress1.Text = "";
                         txtEmpAddress2.Text = "";
                         txtEmpDriveLicense.Text = "";
                         txtEmpMobile.Text = "";
                         txtEmpPhone.Text = "";
                         txtEmpSSN.Text = "";
                         txtEmpZip.Text = "";

                         txtCn1Address1.Text = "";
                         txtCn1Address2.Text = "";
                         txtCn1Email.Text = "";
                         txtCn1Name.Text = "";
                         txtCn1Phone.Text = "";
                         txtCn1Relation.Text = "";

                         txtCn2Address1.Text = "";
                         txtCn2Address2.Text = "";
                         txtCn2Email.Text = "";
                         txtCn2Name.Text = "";
                         txtCn2Phone.Text = "";
                         txtCn2Relation.Text = "";

                         txtCn3Address1.Text = "";
                         txtCn3Address2.Text = "";
                         txtCn3Email.Text = "";
                         txtCn3Name.Text = "";
                         txtCn3Phone.Text = "";
                         txtCn3Relation.Text = "";

                         ddlDeptment.SelectedIndex = 0;
                         ddlEmpState.SelectedIndex = 0;
                         ddlEmpType.SelectedIndex = 0;
                         ddlSchedule.SelectedIndex = 0;

                         mdlAddPopUp.Hide();
                         GetUserDetails(Convert.ToInt32(Session["SortBy"]), Convert.ToInt32(ddlgridShift.SelectedValue));
                     }
                }
            }
            catch (Exception ex)
            {
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('"+ex.ToString()+"');", true);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
          
            Attendance.Entities.UserInfo objInfo = new UserInfo();
            try
            {

                txtEditEmpID.Text = lblEditEmpID.Text;
                txtEditFirstname.Text = lblEditFirstname.Text;
                txtEditLastname.Text =lblEditLastname.Text;
                txtEditStartDate.Text = lblEditStartDate.Text;
                txtEditDesg.Text =  lblEditDesg.Text;
                //ddlEditDepart.SelectedIndex =  ;
                ddlEditDepart.SelectedIndex = ddlEditDepart.Items.IndexOf(ddlEditDepart.Items.FindByText(lblEditDepartmentName.Text.Trim()));
                if (lblActive.Text.Trim()== "Yes")
                {
                    rdEditActiveTrue.Checked = true;
                    rdEditActiveFalse.Checked = false;
                    lblEdit1TermDate.Visible = false;
                   // txtEditTermDate.Text = lblEditTermDate.Text;
                    txtEditTermDate.Visible = false;
                    lblEdit1Termreason.Visible = false;
                   // txtEdit1TermReason.Text = txtReason.Text;
                    txtEdit1TermReason.Visible = false;
                 //   txtEdit1TermReason.Enabled = true;
                }
                else
                {
                    rdEditActiveFalse.Checked = true;
                    rdEditActiveTrue.Checked = false;
                    lblEdit1TermDate.Visible = true;
                    txtEditTermDate.Text = lblEditTermDate.Text;
                    txtEditTermDate.Visible = true;
                    lblEdit1Termreason.Visible = true;
                    txtEdit1TermReason.Text = txtReason.Text;
                    txtEdit1TermReason.Visible = true;
                    txtEdit1TermReason.Enabled = true;
                }

                if (txtEditStartDate.Text == "")
                {
                    txtEditStartDate.Enabled = true;
                }
                else
                {
                    txtEditStartDate.Enabled = false;
                }
                viewPopup.Style["display"] = "none";
                Editpopup.Style["display"] = "block";
                btnEdit.Visible = false;
                btnUpdate.Visible = true;
                btnEditCancel.Visible = true;
                Image1.ImageUrl = "~/Photos/" + hdnPhotLink.Value.Trim();
     
               }

            
            catch (Exception ex)
            {

            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
          
         
        }

        

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "user")
            {
                int userid = Convert.ToInt32(e.CommandArgument);
                Session["empID"] = userid;

                Response.Redirect("EmployeDetails.aspx?Loc=" + Session["LocationName"].ToString().Trim());
            }
        }

        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblEmpLastname = (Label)e.Row.FindControl("lblEmpLastname");

                    Label lblEmpFirstname = (Label)e.Row.FindControl("lblEmpFirstname");

                    lblEmpFirstname.Text = lblEmpFirstname.Text + " " + lblEmpLastname.Text;

                    HiddenField hdnPhoto = (HiddenField)e.Row.FindControl("hdnPhoto");
                    string Photo = "/Photos/" + hdnPhoto.Value.ToString().Trim();
                    string tip = CreateSignInTable(lblEmpFirstname.Text, Photo);
                    // lblEmpFirstname.Text = GeneralFunction.WrapTextByMaxCharacters(lblEmpFirstname.Text, 20);
                    lblEmpFirstname.Attributes.Add("rel", "tooltip");
                    lblEmpFirstname.Attributes.Add("title", tip);

                    Label lblStartedDate = (Label)e.Row.FindControl("lblStartedDate");
                    lblStartedDate.Text = lblStartedDate.Text == "01/01/1900" ? "" : lblStartedDate.Text;
                  
                    Label lblTerminatedDate = (Label)e.Row.FindControl("lblTerminatedDate");
                    lblTerminatedDate.Text = lblTerminatedDate.Text == "01/01/1900" ? "" : lblTerminatedDate.Text;


                    Label lblTermReason = (Label)e.Row.FindControl("lblTermReason");
                    lblTermReason.Text = GeneralFunction.WrapTextByMaxCharacters(lblTermReason.Text, 20);


                    Label lblActvie = (Label)e.Row.FindControl("lblActvie");
                    lblActvie.Text = lblActvie.Text == "True"?"Yes":"No";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtAddFirstName.Text = "";
            txtAddLastName.Text = "";
            txtBusinessEmail.Text = "";
            txtDateOfBirth.Text = "";
            txtDesignation.Text = "";
            txtEmpAddress1.Text = "";
            txtEmpAddress2.Text = "";
            txtEmpDriveLicense.Text = "";
            txtEmpMobile.Text = "";
            txtEmpPhone.Text = "";
            txtEmpSSN.Text = "";
            txtEmpZip.Text = "";
      
            txtCn1Address1.Text = "";
            txtCn1Address2.Text = "";
            txtCn1Email.Text = "";
            txtCn1Name.Text = "";
            txtCn1Phone.Text = "";
            txtCn1Relation.Text = "";
          
            txtCn2Address1.Text = "";
            txtCn2Address2.Text = "";
            txtCn2Email.Text = "";
            txtCn2Name.Text = "";
            txtCn2Phone.Text = "";
            txtCn2Relation.Text = "";

            txtCn3Address1.Text = "";
            txtCn3Address2.Text = "";
            txtCn3Email.Text = "";
            txtCn3Name.Text = "";
            txtCn3Phone.Text = "";
            txtCn3Relation.Text = "";

            ddlDeptment.SelectedIndex = 0;
            ddlEmpState.SelectedIndex = 0;
            ddlEmpType.SelectedIndex = 0;
            ddlSchedule.SelectedIndex = 0;
           
            mdlAddPopUp.Hide();
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            viewPopup.Style["display"] = "block";
            Editpopup.Style["display"] = "none";
            btnEdit.Visible = true;
            btnUpdate.Visible = false;
            txtEdit1TermReason.Text = "";
            txtEditTermDate.Text = "";
            txtEditDesg.Text = "";
            txtEditEmpID.Text = "";
            txtEditFirstname.Text = "";
            txtEditLastname.Text = "";
            txtEditStartDate.Text = "";
            ddlEditDepart.SelectedIndex = 0;
            btnEditCancel.Visible = false;
            mdlEditPopup.Hide();
           
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }

        protected void rdEditActiveTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (rdEditActiveTrue.Checked)
            {
                lblEdit1TermDate.Visible = false;
                txtEditTermDate.Visible = false;
                lblEdit1Termreason.Visible = false;
                txtEdit1TermReason.Visible = false;
                txtEdit1TermReason.Enabled = false;
            }
        }

        protected void rdEditActiveFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (rdEditActiveFalse.Checked)
            {

                lblEdit1TermDate.Visible = true;
                txtEditTermDate.Visible = true;
                lblEdit1Termreason.Visible = true;
                txtEdit1TermReason.Visible = true;
                txtEdit1TermReason.Enabled = true;
            }
        }

        protected void lnkEditClose_Click(object sender, EventArgs e)
        {

            viewPopup.Style["display"] = "block";
            Editpopup.Style["display"] = "none";
            btnEdit.Visible = true;
            btnUpdate.Visible = false;
            btnEditCancel.Visible = false;
            txtEdit1TermReason.Text = "";
            txtEditTermDate.Text = "";
            txtEditDesg.Text = "";
            txtEditEmpID.Text = "";
            txtEditFirstname.Text = "";
            txtEditLastname.Text = "";
            txtEditStartDate.Text = "";
            ddlEditDepart.SelectedIndex = 0;
          
            mdlEditPopup.Hide();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }

        
        private string CreateSignInTable(string Employeename, string photo)
        {
            string strTransaction = string.Empty;
            strTransaction = "<table class=\"noPading\"  id=\"SalesStatus\" style=\"display: table; border-collapse:collapse;  width:100%; background-color:#FFFFFF;border:2px;border-color:Black; \">";
            strTransaction += "<tr>";
            strTransaction += "<td style=\"width:90px;\">";
            strTransaction += "<img src=" + photo + " style=\"width:70px; height:70px\" /><br>";
            strTransaction += Employeename;           
            strTransaction += "</td>";
            strTransaction += "</tr>";
            strTransaction += "</table>";

            return strTransaction;

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

        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sort=Convert.ToInt32(ddlSelect.SelectedItem.Value);
            Session["SortBy"] = sort;
            GetUserDetails(sort, Convert.ToInt32(ddlgridShift.SelectedValue));
        }

        protected void grdUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["AllusersData"] as DataTable;
             
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
              //  ddlSchedule.Items.Add(new ListItem("Select", "0"));
                
                ddlSchedule.DataBind();
                ddlSchedule.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
            }
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
                ddlEmpState.DataSource = dt;
                ddlEmpState.DataTextField = "StateName";
                ddlEmpState.DataValueField = "StateID";
                ddlEmpState.DataBind();

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
        private void GetAllWages()
        {
            try
            {
                Attendance.BAL.Report obj = new Report();
                DataTable dt = obj.GetAllWages();
                ddlWagetype.DataSource = dt;
                ddlWagetype.DataTextField = "WageType";
                ddlWagetype.DataValueField = "WageID";

                ddlWagetype.DataBind();
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
            mdlAddPopUp.Show();

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
                ddlSchedule.SelectedIndex =ddlSchedule.Items.IndexOf(ddlSchedule.Items.FindByValue(ID==""?"0":ID));
                mdlAddPopUp.Show();
                
            }
            catch (Exception ex)
            {
            }
      
        }

        protected void btnCancelSch_Click(object sender, EventArgs e)
        {

            mdlSchedulepopup.Hide();
           
        }
        private void GetShifts(string LocationName)
        {
            Business business = new Business();
            DataSet dsShifts = business.GetShiftsByLocationName(LocationName);
            ddlShift.DataSource = dsShifts;
            ddlShift.DataTextField = "shiftname";
            ddlShift.DataValueField = "shiftID";
            ddlShift.DataBind();

            ddlgridShift.DataSource = dsShifts;
            ddlgridShift.DataTextField = "shiftname";
            ddlgridShift.DataValueField = "shiftID";
            ddlgridShift.DataBind();
            ddlgridShift.Items.Insert(0, new ListItem("ALL", "0"));
          
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

        protected void ddlgridShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int sort = Convert.ToInt32(ddlSelect.SelectedItem.Value);
                Session["SortBy"] = sort;
                GetUserDetails(sort, Convert.ToInt32(ddlgridShift.SelectedValue));
            }
            catch (Exception ex)
            {
            }
        }
    }
}
