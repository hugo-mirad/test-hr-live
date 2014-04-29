<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUserManagement.aspx.cs"
    Inherits="Attendance.AdminUserManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <!-- <script src="js/overlibmws.js" type="text/javascript"></script>  -->
    <link rel="stylesheet" href="css/reset.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/UI.css" />
    <link rel="stylesheet" href="css/inputs.css" type="text/css" />
    <!-- <link rel="stylesheet" href="css/style.css" type="text/css" /> -->
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="css/admin.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>

    <script src="js/datetimepicker_css.js" type="text/javascript"></script>

    <script>
    
        $(function(){
            $('.popupHolder2').css('z-index','100002')
            $('.popupContent2').css('z-index','100003')
        })
        
    </script>

    <script type="text/javascript" language="javascript">
       
       
       function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        
        
          function echeck(str) {
            var at = "@"
            var dot = "."
            var lat = str.indexOf(at)
            var lstr = str.length
            var ldot = str.indexOf(dot)
            if (str.indexOf(at) == -1) {
                alert("Enter valid email")
                return false
            }

            if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
                alert("Enter valid email")
                return false
            }

            if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
                alert("Enter valid email")
                return false
            }

            if (str.indexOf(at, (lat + 1)) != -1) {
                alert("Enter valid email")
                return false
            }

            if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
                alert("Enter valid email")
                return false
            }

            if (str.indexOf(dot, (lat + 2)) == -1) {
                alert("Enter valid email")
                return false
            }

            if (str.indexOf(" ") != -1) {
                alert("Enter valid email")
                return false
            }

            return true
        }
       
       function showspinner()
        {
         $('#spinner').show();
         return true;
        }
       
       
       function validateSubmit(){
        debugger
          var valid=true;
   
         if (document.getElementById('txtAddFirstName').value=="") {
               alert("Please enter firstname.");
               valid = false;
               document.getElementById("txtAddFirstName").value = "";
                 $('#txtAddFirstName').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("txtAddFirstName").focus();
              // $('#txtAddFirstName').closest('.ppHedContent')
           }

          else if (document.getElementById('txtAddFirstName').value.trim().length < 1) {
               alert("Please enter firstname.");
               valid = false;
               document.getElementById("txtAddFirstName").value = "";
               $('#txtAddFirstName').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("txtAddFirstName").focus();
           }

        else if(document.getElementById('txtAddLastName').value.length < 1)
           {
               alert("Please enter the lastname .");               
               valid=false;
                document.getElementById("txtAddLastName").value="";
                  $('#txtAddLastName').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("txtAddLastName").focus();
           }
      
              else if (document.getElementById('ddlEmpType').value=="0")
           {
               alert("Please select employee type .");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#ddlEmpType').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("ddlEmpType").focus();
           }
           
            else if (document.getElementById('ddlSchedule').value=="0")
           {
               alert("Please select schedule .");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                     $('#ddlSchedule').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("ddlSchedule").focus();
           }
             else if (document.getElementById('ddlDeptment').value=="0")
           {
               alert("Please select department name .");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#ddlDeptment').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("ddlDeptment").focus();
           }
         else if(document.getElementById('txtStartDt').value=="")
           {
               alert("Please enter start date.");               
               valid=false;
                document.getElementById('txtStartDt').value="";
                 $('#txtStartDt').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtStartDt').focus();
           }
       else if(document.getElementById('ddlShift').value=="0")
           {
               alert("Please select shift.");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#ddlShift').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("ddlShift").focus();
           }
    
      else if (document.getElementById('ddlWagetype').value=="0")
           {
               alert("Please select wage type.");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#ddlWagetype').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("ddlWagetype").focus();
           } 
       
              else if (document.getElementById('rdMarriedSingle').checked==false && document.getElementById('rdMarried').checked==false)
           {
               alert("Please select filing status.");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#rdMarriedSingle').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("rdMarriedSingle").focus();
           } 
           
           
            else if (document.getElementById('ddlEmpType').value=="1" && ($('#lblLocation').text()!="INDG"&&$('#lblLocation').text()!="INBH"))
           {
                if(document.getElementById('txtEmpAddress1').value.trim().length<1)
               {
               alert("Please enter street.");               
               valid=false;
                document.getElementById('txtEmpAddress1').value="";
                  $('#txtEmpAddress1').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpAddress1').focus();
               }
               
               else if(document.getElementById('txtEmpAddress2').value.trim().length<1)
               {
               alert("Please enter city.");               
               valid=false;
                document.getElementById('txtEmpAddress2').value="";
                  $('#txtEmpAddress2').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpAddress2').focus();
               }
               
               else if (document.getElementById('ddlEmpState').value=="0")
              {
               alert("Please select state.");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#ddlEmpState').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("ddlEmpState").focus();
               } 
               
               
                else if(document.getElementById('txtEmpZip').value.trim().length<1)
               {
               alert("Please enter zip.");               
               valid=false;
                document.getElementById('txtEmpZip').value="";
                  $('#txtEmpZip').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpZip').focus();
               }
               
               else if (document.getElementById('ddlDeductions').value=="0")
              {
               alert("Please select deductions.");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#ddlDeductions').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("ddlDeductions").focus();
               } 
               
                 else if(document.getElementById('txtDateOfBirth').value.trim().length<1)
               {
               alert("Please enter date of birth.");               
               valid=false;
                document.getElementById('txtDateOfBirth').value="";
                  $('#txtDateOfBirth').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtDateOfBirth').focus();
               }
               
              else if (document.getElementById('<%= txtEmpSSN.ClientID %>')!=null)
	         {
	        
	          if (document.getElementById('<%= txtEmpSSN.ClientID %>').value!="")
	         {
	             if(document.getElementById('txtEmpSSN').value.trim().length<9)
               {
               alert("Please enter valid ssn.");               
               valid=false;
                document.getElementById('txtEmpSSN').value="";
                  $('#txtEmpSSN').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpSSN').focus();
              
              }
	        }
	        else
	        {
	           alert("Please enter ssn.");               
               valid=false;
                document.getElementById('txtEmpSSN').value="";
                  $('#txtEmpSSN').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpSSN').focus();
	        }
               
               
            }
           }
           
           else if (document.getElementById('ddlEmpType').value=="2" && ($('#lblLocation').text()!="INDG"&&$('#lblLocation').text()!="INBH"))
           {
           
                   if(document.getElementById('txtEmpAddress1').value.trim().length<1)
               {
               alert("Please enter street.");               
               valid=false;
                document.getElementById('txtEmpAddress1').value="";
                  $('#txtEmpAddress1').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpAddress1').focus();
               }
               
               else if(document.getElementById('txtEmpAddress2').value.trim().length<1)
               {
               alert("Please enter city.");               
               valid=false;
                document.getElementById('txtEmpAddress2').value="";
                  $('#txtEmpAddress2').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpAddress2').focus();
               }
               
               else if (document.getElementById('ddlEmpState').value=="0")
              {
               alert("Please select state.");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#ddlEmpState').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("ddlEmpState").focus();
               } 
               
               
                else if(document.getElementById('txtEmpZip').value.trim().length<1)
               {
               alert("Please enter zip.");               
               valid=false;
                document.getElementById('txtEmpZip').value="";
                  $('#txtEmpZip').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpZip').focus();
               }
           
                 else if (document.getElementById('<%= txtEmpSSN.ClientID %>')!=null)
	         {
	        
	          if (document.getElementById('<%= txtEmpSSN.ClientID %>').value!="")
	         {
	             if(document.getElementById('txtEmpSSN').value.trim().length<9)
               {
               alert("Please enter valid ssn.");               
               valid=false;
                document.getElementById('txtEmpSSN').value="";
                  $('#txtEmpSSN').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpSSN').focus();
              
              }
	        }
	        else
	        {
	           alert("Please enter ssn.");               
               valid=false;
                document.getElementById('txtEmpSSN').value="";
                  $('#txtEmpSSN').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpSSN').focus();
	        }
               
            }
           
           
           }
           
              else if (document.getElementById('rdGenderMale').checked==false && document.getElementById('rdGenderFeMale').checked==false)
           {
               alert("Please select gender.");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
                  $('#rdGenderMale').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById("rdGenderMale").focus();
           } 
       
          
    
          else if(document.getElementById('txtEmpMobile').value!="")
           {
           if(document.getElementById('txtEmpMobile').value.trim().length<10)
           {
               alert("Please enter valid mobile#.");               
               valid=false;
                document.getElementById('txtEmpMobile').value="";
                  $('#txtEmpMobile').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpMobile').focus();
               }
           }
    
    
         else if (document.getElementById('<%= txtBusinessEmail.ClientID %>').value!="")
	        {
	            if (echeck(document.getElementById('<%= txtBusinessEmail.ClientID %>').value)==false)
	            {     
		        document.getElementById('<%= txtBusinessEmail.ClientID %>').value=""
		        $('#txtBusinessEmail').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
		        document.getElementById('<%= txtBusinessEmail.ClientID %>').focus();
		        
		        valid=false;
        		return valid;
        		}
	        }        
    
          else if (document.getElementById('<%= txtPersonalEmail.ClientID %>').value!="")
	        {
	            if (echeck(document.getElementById('<%= txtPersonalEmail.ClientID %>').value)==false)
	            {     
		        document.getElementById('<%= txtPersonalEmail.ClientID %>').value=""
		         $('#txtPersonalEmail').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
		        document.getElementById('<%= txtPersonalEmail.ClientID %>').focus()
		        valid=false;
        		return valid;
        		}
	        }
        
           else if (document.getElementById('<%= txtEmpZip.ClientID %>').value!="")
	        {
	             if(document.getElementById('txtEmpZip').value.trim().length<4)
             {
               alert("Please enter valid zip.");               
               valid=false;
                document.getElementById('txtEmpZip').value="";
                  $('#txtEmpZip').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtEmpZip').focus();
              
              }
	        }   
	        
	        
	                
    
      else if(document.getElementById('txtCn1Phone').value!="")
           {
           if(document.getElementById('txtCn1Phone').value.trim().length<10)
           {
               alert("Please enter valid phonenumber.");               
               valid=false;
                document.getElementById('txtCn1Phone').value="";
                  $('#txtCn1Phone').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtCn1Phone').focus();
               }
           }
    
     else if(document.getElementById('txtCn2Phone').value!="")
           {
           if(document.getElementById('txtCn2Phone').value.trim().length<10)
           {
               alert("Please enter valid phonenumber.");               
               valid=false;
                document.getElementById('txtCn2Phone').value="";
                  $('#txtCn2Phone').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtCn2Phone').focus();
               }
           }
    
        else if(document.getElementById('txtCn3Phone').value!="")
           {
           if(document.getElementById('txtCn3Phone').value.trim().length<10)
           {
               alert("Please enter valid phonenumber.");               
               valid=false;
                document.getElementById('txtCn3Phone').value="";
                 $('#txtCn3Phone').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtCn3Phone').focus();
               }
           }
    
    
            else if (document.getElementById('<%= txtCn1Email.ClientID %>').value!="")
	        {
	            if (echeck(document.getElementById('<%= txtCn1Email.ClientID %>').value)==false)
	            {     
		        document.getElementById('<%= txtCn1Email.ClientID %>').value=""
		        $('#txtCn1Email').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
		        document.getElementById('<%= txtCn1Email.ClientID %>').focus()
		        valid=false;
        		return valid;
        		}
	        }                     
            else if (document.getElementById('<%= txtCn2Email.ClientID %>').value!="")
	        {
	        if (echeck(document.getElementById('<%= txtCn2Email.ClientID %>').value)==false)
	        {
		        document.getElementById('<%= txtCn2Email.ClientID %>').value=""
		         $('#txtCn2Email').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
		        document.getElementById('<%= txtCn2Email.ClientID %>').focus()
		        valid=false;
        		return valid;
        		}
	        }                  
	           
             else if (document.getElementById('<%= txtCn3Email.ClientID %>').value!="")
	        {
	        if (echeck(document.getElementById('<%= txtCn3Email.ClientID %>').value)==false)
	        {
		        document.getElementById('<%= txtCn3Email.ClientID %>').value=""
		         $('#txtCn3Email').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
		        document.getElementById('<%= txtCn3Email.ClientID %>').focus()
		        valid=false;
        		return valid;
        		}
	        }   
	        
	            else if (document.getElementById('<%= txtCn1Zip.ClientID %>').value!="")
	        {
	             if(document.getElementById('txtCn1Zip').value.trim().length<4)
           {
               alert("Please enter valid zip.");               
               valid=false;
                document.getElementById('txtCn1Zip').value="";
                  $('#txtCn1Zip').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtCn1Zip').focus();
              
           }
	        }    
	        
	            else if (document.getElementById('<%= txtCn2Zip.ClientID %>').value!="")
	        {
	             if(document.getElementById('txtCn2Zip').value.trim().length<4)
           {
               alert("Please enter valid zip.");               
               valid=false;
                document.getElementById('txtCn2Zip').value="";
                  $('#txtCn2Zip').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtCn2Zip').focus();
              
           }
	        }    
	        
	            else if (document.getElementById('<%= txtCn3Zip.ClientID %>').value!="")
	        {
	             if(document.getElementById('txtCn3Zip').value.trim().length<4)
           {
               alert("Please enter valid zip.");               
               valid=false;
                document.getElementById('txtCn3Zip').value="";
                  $('#txtCn3Zip').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
               document.getElementById('txtCn3Zip').focus();
              
           }
	        }    
	        
    
           return valid;
       }
    
    
    
     function validateEditSubmit() {
        debugger
          var valid=true;

              
         if (document.getElementById('txtEditFirstname').value=="") {
               alert("Please enter firstname.");
               valid = false;
               document.getElementById("txtEditFirstname").value = "";
               document.getElementById("txtEditFirstname").focus();
           }

          else if (document.getElementById('txtEditFirstname').value.trim().length < 1) {
               alert("Please enter firstname.");
               valid = false;
               document.getElementById("txtEditFirstname").value = "";
               document.getElementById("txtEditFirstname").focus();
           }



        else if(document.getElementById('txtEditLastname').value.length < 1)
           {
               alert("Please enter the lastname .");               
               valid=false;
                document.getElementById("txtEditLastname").focus="";
               document.getElementById("txtEditLastname").focus();
           }
        else if (document.getElementById('ddlEditDepart').value=="0")
           {
               alert("Please select department name .");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
               document.getElementById("ddlEditDepart").focus();
           }
       
          else if(document.getElementById('txtEditStartDate').value=="")
           {
               alert("Please enter stardate date.");               
               valid=false;
                document.getElementById("txtEditStartDate").value="";
               document.getElementById("txtEditStartDate").focus();
           }
     
     
       else if(document.getElementById('txtEditTermDate')!=null)
           {
            if(document.getElementById('txtEditTermDate').value=="")
            {
               alert("Please enter terminated date.");               
               valid=false;
                document.getElementById("txtEditTermDate").value="";
               document.getElementById("txtEditTermDate").focus();
           }
        }
           
       else if(document.getElementById('txtEdit1TermReason')!=null)
           {
            if(document.getElementById('txtEdit1TermReason').value=="")
            {
               alert("Please enter terminate reason.");               
               valid=false;
                document.getElementById("txtEdit1TermReason").value="";
               document.getElementById("txtEdit1TermReason").focus();
               }
           }
   
          else if(document.getElementById('txtEditTermDate')!=null)
           {
            var start=document.getElementById('txtEditStartDate').value;
            var end=document.getElementById('txtEditTermDate').value;
            startdate=new Date(start);
            enddate=new Date(end);     
               if( startdate>enddate)
                { 
                 alert("Startdate should be lessthan terminatedate");
                  document.getElementById("txtEdit1TermReason").focus();
                 valid=false;
                }
            }
        
       
        return valid;
       }
    
    
    
     function validPwd()
      {
       debugger
          var valid=true;


           if (document.getElementById('txtOldpwd').value.trim()=="")
           {
               alert("Please enter old password.");               
               valid=false;
               document.getElementById("txtOldpwd").value = "";
               document.getElementById("txtOldpwd").focus();
           }

        else if (document.getElementById('txtOldpwd').value.trim().length < 1)
           {
               alert("Please enter the valid old password.");               
               valid=false;
               document.getElementById("txtOldpwd").value = "";
               document.getElementById("txtOldpwd").focus();
           }
           
        else if (document.getElementById('txtNewPwd').value=="") {
               alert("Please enter new password.");
               valid = false;
               document.getElementById("txtNewPwd").value = "";
               document.getElementById("txtNewPwd").focus();
           }

          else if (document.getElementById('txtNewPwd').value.trim().length < 1) {
               alert("Please enter valid new password.");
               valid = false;
               document.getElementById("txtNewPwd").value = "";
               document.getElementById("txtNewPwd").focus();
           }



        else if(document.getElementById('txtConfirmPwd').value.trim().length < 1)
           {
               alert("Please enter confirm password .");               
               valid=false;
                document.getElementById("txtConfirmPwd").value="";
               document.getElementById("txtConfirmPwd").focus();
           }
        else if (document.getElementById('txtConfirmPwd').value.trim()!=document.getElementById('txtNewPwd').value.trim())
           {
               alert("New password and confirm password should be match .");               
               valid=false;
               document.getElementById("txtNewPwd").value="";
                document.getElementById("txtConfirmPwd").value="";
               document.getElementById("txtNewPwd").focus();
           }
  
           return valid;
       }
    
    
    
      function validPasscode()
      {
       debugger
          var valid=true;


           if (document.getElementById('txtOldpasscode').value.trim()=="")
           {
               alert("Please enter old passcode.");               
               valid=false;
               document.getElementById("txtOldpasscode").value = "";
               document.getElementById("txtOldpasscode").focus();
           }

        else if (document.getElementById('txtOldpasscode').value.trim().length < 1)
           {
               alert("Please enter the valid old passcode.");               
               valid=false;
               document.getElementById("txtOldpasscode").value = "";
               document.getElementById("txtOldpasscode").focus();
           }
           
        else if (document.getElementById('txtNewPasscode').value=="") {
               alert("Please enter new passcode.");
               valid = false;
               document.getElementById("txtNewPasscode").value = "";
               document.getElementById("txtNewPasscode").focus();
           }

          else if (document.getElementById('txtNewPasscode').value.trim().length < 1) {
               alert("Please enter valid new passcode.");
               valid = false;
               document.getElementById("txtNewPasscode").value = "";
               document.getElementById("txtNewPasscode").focus();
           }



        else if(document.getElementById('txtConfirmPasscode').value.trim().length < 1)
           {
               alert("Please enter confirm passcode .");               
               valid=false;
                document.getElementById("txtConfirmPasscode").value="";
               document.getElementById("txtConfirmPasscode").focus();
           }
        else if (document.getElementById('txtConfirmPasscode').value.trim()!=document.getElementById('txtNewPasscode').value.trim())
           {
               alert("New passcode and confirm passcode should be match .");               
               valid=false;
               document.getElementById("txtNewPasscode").value="";
                document.getElementById("txtConfirmPasscode").value="";
               document.getElementById("txtNewPasscode").focus();
           }
  
           return valid;
       }
    
    
         function validSchedules()
      {
         debugger
          var valid=true;

           var SchStart=$.trim($('#txtSheduleStart').val())
           var SchEnd=$.trim($('#txtScheduleEnd').val())
           var LunchStart=$.trim($('#txtLunchStart').val())
           var LunchEnd=$.trim($('#txtLunchEnd').val())
           
           //var str = SchStart.toString();
           SchStart = SchStart.replace(/AM/g, " AM");
           SchStart = SchStart.replace(/PM/g, " PM");
           
           //var str = SchEnd.toString();
           SchEnd = SchEnd.replace(/AM/g, " AM");
           SchEnd = SchEnd.replace(/PM/g, " PM");
           
           //var str = LunchStart.toString();
           LunchStart = LunchStart.replace(/AM/g, " AM");
           LunchStart = LunchStart.replace(/PM/g, " PM");
           
           //var str = LunchEnd.toString();
           LunchEnd = LunchEnd.replace(/AM/g, " AM");
           LunchEnd = LunchEnd.replace(/PM/g, " PM");
           
           if (document.getElementById('txtSheduleStart').value.trim()=="")
           {
               alert("Please schedule start time.");               
               valid=false;
               document.getElementById("txtSheduleStart").value = "";
               document.getElementById("txtSheduleStart").focus();
           }

        else if (document.getElementById('txtSheduleStart').value.trim().length < 1)
           {
               alert("Please enter valid schedule start time.");               
               valid=false;
               document.getElementById("txtSheduleStart").value = "";
               document.getElementById("txtSheduleStart").focus();
           }
           
            else if (document.getElementById('txtScheduleEnd').value.trim()=="")
           {
               alert("Please enter valid schedule end time.");               
               valid=false;
               document.getElementById("txtScheduleEnd").value = "";
               document.getElementById("txtScheduleEnd").focus();
           }
           
            else if (document.getElementById('txtScheduleEnd').value.trim().length < 1)
           {
               alert("Please enter valid schedule end time.");               
               valid=false;
               document.getElementById("txtScheduleEnd").value = "";
               document.getElementById("txtScheduleEnd").focus();
           }
   
        else if (document.getElementById('txtLunchStart').value.trim().length < 1) {
               alert("Please enter lunch start time.");
               valid = false;
               document.getElementById("txtLunchStart").value = "";
               document.getElementById("txtLunchStart").focus();
           }

          else if (document.getElementById('txtLunchEnd').value.trim().length < 1) {
               alert("Please enter lunch end time.");
               valid = false;
               document.getElementById("txtLunchEnd").value = "";
               document.getElementById("txtLunchEnd").focus();
           }
           
            else if (document.getElementById('rdFive').checked==false &&document.getElementById('rdSix').checked==false &&document.getElementById('rdSeven').checked==false){
               alert("Please choose days.");
               valid = false;
               //document.getElementById("rdFive").value = "";
               document.getElementById("rdFive").focus();
           }
           
           
           
         else if(Date.parse("01/01/2000 "+SchStart)>=Date.parse("01/01/2000 "+SchEnd))
         {
           alert("Schedule start time should be greater than end time");
           valid=false;
            document.getElementById("txtScheduleEnd").value = "";
            document.getElementById("txtScheduleEnd").focus();
         }
           
           
           else if(Date.parse("01/01/2000 "+LunchStart)>=Date.parse("01/01/2000 "+LunchEnd))
         {
           alert("Lunch start time should be greater than end time");
           valid=false;
            document.getElementById("txtLunchEnd").value = "";
            document.getElementById("txtLunchEnd").focus();
         }
           
            else if(!((Date.parse("01/01/2000 "+SchStart)<=Date.parse("01/01/2000 "+LunchStart))&&(Date.parse("01/01/2000 "+LunchStart)<=Date.parse("01/01/2000 "+SchEnd))))
         {
           alert("Lunch start time should be in b/w schedule start and end time");
           valid=false;
            document.getElementById("txtLunchStart").value = "";
            document.getElementById("txtLunchStart").focus();
         }
           
            else if(!((Date.parse("01/01/2000 "+SchStart)<=Date.parse("01/01/2000 "+LunchEnd))&&(Date.parse("01/01/2000 "+LunchEnd)<=Date.parse("01/01/2000 "+SchEnd))))
         {
           alert("Lunch end time should be in b/w schedule start and end time");
           valid=false;
            document.getElementById("txtLunchEnd").value = "";
            document.getElementById("txtLunchEnd").focus();
         }
           
           

           return valid;
      }
    
    
    
    
    
    
    
    
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div id="spinner">
        <h4>
            <div>
                Processing
                <img src="images/loading.gif" />
            </div>
        </h4>
    </div>
    <div>
        <div class="headder">
            <a href="#" class="logo">
                <asp:Label ID="comanyname" runat="server" ForeColor="White"></asp:Label>
                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                 <asp:Label ID="lblShiftName" runat="server"></asp:Label>
            </a>
        
            <div class="right">
                <div class="wel">
                    <table style="width: auto; margin-left: 20px; float: right; border-collapse: collapse">
                        <tr>
                            <td style="vertical-align: middle; padding-top: 3px;">
                                <div class="inOut">
                                    SCHEDULE:
                                    <asp:Label ID="lblHeadSchedule" runat="server"></asp:Label>
                                </div>
                                <b>User:</b>
                                <asp:Label ID="lblEmployyName" runat="server"></asp:Label>&nbsp;&nbsp;
                                <div class="clear h51">
                                    &nbsp;</div>
                                <asp:Button ID="btnLogout" CssClass=" btn btn-small btn-mini btn-success floatR"
                                    runat="server" Text="Logout" OnClick="btnLogout_Click" OnClientClick="return showspinner();" />
                                <ul class="menu2">
                                    <li><a href="#" class="dropdown-menu">Menu <span class="pic">&nbsp;</span> </a>
                                        <ul>
                                            <li>
                                                <asp:LinkButton ID="lnkReport" runat="server" Text="Attendance Report" OnClick="lnkReport_Click"></asp:LinkButton></li>
                                            <li>
                                                <asp:LinkButton ID="lnkPayroll" runat="server" Text="Payroll Report" PostBackUrl="Payroll.aspx"></asp:LinkButton></li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="lnkUserMangement" Text="Employee Management" PostBackUrl="AdminUserManagement.aspx"></asp:LinkButton></li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="lnkLeaveApproval" Text="Leave Approval Management"
                                                    PostBackUrl="LeaveApprovalManagement.aspx"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="lnkLeavemangement" Text="Leave Management" PostBackUrl="LeaveManagement.aspx"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="lnkHolidayManagement" Text="Holiday Management"
                                                    PostBackUrl="HolidayManagement.aspx"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:UpdatePanel ID="ppp" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkChangepwd" Text="Change Password" OnClick="lnkChangepwd_Click"></asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </li>
                                            <li>
                                                <asp:UpdatePanel ID="UpPasscode" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkChangePasscode" Text="Change Passcode" OnClick="lnkChangePasscode_Click"></asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                                <!-- Show Clock Start  -->
                                <div class="tDate">
                                    <asp:Label ID="lblDate2" runat="server" Style="display: none"></asp:Label>
                                    <%--  <asp:Label ID="lblPrevTime" runat="server" Text=""></asp:Label>--%>
                                    <span class="cDate" style="margin-bottom: 1px; margin-top: 2px; float: left; display: inline-block">
                                    </span>
                                    <div class="cTime" style="display: inline-block; float: right; margin-left: 10px;">
                                        <b>--:--:-- AM </b><strong> (<asp:Label ID="lblTimeZoneName" runat="server"></asp:Label>)</strong>
                                    </div>

                                    <script src="js/clock.js" type="text/javascript"></script>

                                </div>
                                <!-- Show Clock End  -->
                            </td>
                            <td style="vertical-align: top; width: 35px;">
                                <%-- <asp:Image ID="Image1" runat="server" ImageUrl="Photos/defaultUSer.jpg" />--%>
                                <img src="Photos/defaultUSer.jpg" class="topPic" runat="server" id="Photo" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <h2 class="pageHeadding">
            Employee management
        </h2>
        <div>
            <div style="display: inline-block;">
                <div style="display: inline-block; margin-left: 10px;">
                    <asp:UpdatePanel ID="upSelect" runat="server">
                        <ContentTemplate>
                            <b>Status </b>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlSelect" runat="server" Width="83px" Height="23px" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Value="2">All</asp:ListItem>
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div style="display: inline-block; margin-left: 10px;">
                    <asp:UpdatePanel ID="UpdateLocation" runat="server">
                        <ContentTemplate>
                            <b>
                                <asp:Label ID="lblGrdLocaton" runat="server" Text="Location"></asp:Label></b>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="83px"
                                Height="23px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp; <b>
                                <asp:Label ID="lblShift" runat="server" Text="Shift"></asp:Label></b>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlgridShift" runat="server" AutoPostBack="true" 
                                Style="width: 70px;" 
                                onselectedindexchanged="ddlgridShift_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:LinkButton ID="lnkAddUser" runat="server" Text="Add Employee" OnClick="lnkAddUser_Click"
                Style="margin-top: -36px;" CssClass="btn btn-danger w997"></asp:LinkButton>
        </div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdateLocation"
            DisplayAfter="0">
            <ProgressTemplate>
                <div id="spinner">
                    <h4>
                        <div>
                            Processing
                            <img src="images/loading.gif" />
                        </div>
                        <h4>
                        </h4>
                        <h4>
                        </h4>
                    </h4>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="upSelect"
            DisplayAfter="0">
            <ProgressTemplate>
                <div id="spinner">
                    <h4>
                        <div>
                            Processing
                            <img src="images/loading.gif" />
                        </div>
                        <h4>
                        </h4>
                        <h4>
                        </h4>
                    </h4>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1"
            DisplayAfter="0">
            <ProgressTemplate>
                <div id="spinner">
                    <h4>
                        <div>
                            Processing
                            <img src="images/loading.gif" />
                        </div>
                        <h4>
                        </h4>
                        <h4>
                        </h4>
                    </h4>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="upgrd" runat="server">
            <ContentTemplate>
            <div style="font-size: 20px;font-weight: bold;padding-top: 150px;text-align: center;" runat="server" id="dvNodata">
            <asp:Label ID="lblGrdNodata" runat="server"></asp:Label></div>
                <div>
                    <asp:Label ID="lblTotal" runat="server" Style="font-size: 11px; font-weight: bold;
                        margin-left: 10px;"></asp:Label>
                    &nbsp;
                </div>
                <input style="width: 91px" id="txthdnSortOrder" type="hidden" runat="server" enableviewstate="true" />
                <input style="width: 40px" id="txthdnSortColumnId" type="hidden" runat="server" enableviewstate="true" />
                <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="false" CssClass="table1"
                    Style="width: 997px;" OnRowCommand="grdUsers_RowCommand" OnRowDataBound="grdUsers_RowDataBound"
                    AllowSorting="True" OnSorting="grdUsers_Sorting">
                    <Columns>
                        <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                            <%--  <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadEmpID" runat="server" Text="EmpID"></asp:LinkButton>
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>' CommandName="user"
                                    CommandArgument='<%#Eval("userid")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="50" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Firstname" HeaderText="BusinessName">
                            <%--   <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadEmpFirstname" runat="server" Text="Firstname"></asp:LinkButton>
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("Firstname")%>'></asp:Label>
                                <asp:Label ID="lblEmpLastname" runat="server" Text='<%#Eval("lastname")%>' Visible="false"></asp:Label>
                                <asp:HiddenField ID="hdnPhoto" runat="server" Value='<%#Eval("photolink")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="150" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PEmpname" HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpname" runat="server" Text='<%#Eval("PEmpname")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150" />
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField SortExpression="JoiningDate" HeaderText="StartDt">
                            <%--<HeaderTemplate>
                                <asp:LinkButton ID="lblHeadStarted" runat="server" Text="StartedDt"></asp:LinkButton>
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblStartedDate" runat="server" Text='<%# Bind("JoiningDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="TerminatedDt" HeaderText="TermDt">
                            <%--   <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadTerminated" runat="server" Text="TerminatedDt"></asp:LinkButton>
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblTerminatedDate" runat="server" Text='<%#Bind("TermDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TermReason">
                            <%-- <HeaderTemplate>
                                <asp:Label ID="lblHeadTermReason" runat="server" Text="TermReason"></asp:Label>
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblTermReason" runat="server" Text='<%#Eval("TermReason")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DeptName" HeaderText="Department">
                            <%--  <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadDepartment" runat="server" Text="Department"></asp:LinkButton>
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblDept" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="designation" HeaderText="Designation">
                            <%--  <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadDesignation" runat="server" Text="Designation"></asp:LinkButton>
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("designation")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="IsActive" HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblActvie" runat="server" Text='<%#Eval("IsActive")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50" />
                        </asp:TemplateField>
                        <%--  <asp:TemplateField>
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="edit" CommandName="user" CommandArgument='<%#Eval("userid")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSelect" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!-- addpopup-->
    <cc1:ModalPopupExtender ID="mdlAddPopUp" BackgroundCssClass="popupHolder" runat="server"
        PopupControlID="AddPopUp" CancelControlID="lnkClose" TargetControlID="hdnAddpopup">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnAddpopup" runat="server" />
    <div id="AddPopUp" runat="server" class="popContent" style="width: 900px; display: none">
        <h2>
            Add employee for <asp:Label ID="lbladdLoc" runat="server" style="font-size:17px;font-weight:bold"></asp:Label> <span class="close">
                <asp:LinkButton ID="lnkClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 99%;">
                <tr>
                    <td style="width: 712px;">
                        &nbsp;
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-danger" OnClick="btnAdd_Click"
                                        OnClientClick="return validateSubmit();" />
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="ddllocation" EventName="SelectedIndexChanged" />--%>
                                    <asp:AsyncPostBackTrigger ControlID="ddlDeptment" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlEmpType" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlSchedule" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlEmpState" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlDeductions" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlWagetype" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCn1State" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCn2State" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCn3State" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlShift" EventName="SelectedIndexChanged" />
                                    <asp:PostBackTrigger ControlID="btnAdd" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="float: right; margin-right: 27px;">
                        <asp:UpdatePanel ID="uppp" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <div class="scrollBlock">
                <h4 class="ppHed acc">
                    Employee Details <span class="pls">-</span></h4>
                <div class="ppHedContent">
                    <table style="width: 92%; border-collapse: collapse; margin-left: 10px;">
                        <tr>
                            <td colspan="2">
                                <fieldset class="popupFieldSet">
                                    <legend>Personal</legend>
                                    <table style="width: 100%; border-collapse: collapse;">
                                        <tr>
                                            <td style="width: 100px">
                                                First name<span class="must">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddFirstName" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Last name<span class="must">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddLastName" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="width: 30px;">
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <fieldset class="popupFieldSet">
                                    <legend>Business</legend>
                                    <table style="width: 100%; border-collapse: collapse;">
                                        <tr>
                                            <td style="width: 100px">
                                                First name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBusinessFirst" runat="server" MaxLength="50" TabIndex="3"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                Last name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBusinessLasst" runat="server" MaxLength="50" TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 114px;">
                                Employee type<span class="must">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEmpType" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                    TabIndex="3">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 30px;">
                                &nbsp;
                            </td>
                            <td style="width: 115px;">
                                Schedule<span class="must">*</span>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSchedule" runat="server" AutoPostBack="true" TabIndex="6">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lnkScheduleAdd" runat="server" Text="Add new" OnClick="lnkScheduleAdd_Click"></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Department<span class="must">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDeptment" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                    TabIndex="5">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                                Designation
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesignation" runat="server" MaxLength="50" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Start date<span class="must">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDt" runat="server" TabIndex="7"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Shift<span class="must">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlShift" runat="server" AutoPostBack="true" >
                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle;">
                                Photo
                            </td>
                            <td style="vertical-align: middle;">
                                <asp:FileUpload runat="server" ID="photoUpload" TabIndex="10" />
                                <br />
                                <span style="font-size: 10px; color: GrayText">(Maximum file size 4MB)</span>
                            </td>
                            <td>
                            </td>
                            <td>
                                Active
                            </td>
                            <td>
                                <asp:RadioButton ID="rdActiveTrue" runat="server" GroupName="Active" Checked="true"
                                    TabIndex="8" />Yes
                                <asp:RadioButton ID="rdActiveFalse" runat="server" GroupName="Active" TabIndex="9" />No
                            </td>
                        </tr>
                    </table>
                </div>
                <h4 class="ppHed acc ">
                    Salary Details <span class="pls">+</span></h4>
                <div class="ppHedContent">
                    <table style="width: 90%; border-collapse: collapse; margin-left: 10px;">
                        <tr>
                            <td style="width: 100px">
                                Wage type<span class="must">*</span>
                            </td>
                            <td style="width: 280px">
                                <asp:DropDownList ID="ddlWagetype" runat="server" TabIndex="11" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 30px">
                            </td>
                            <td style="width: 100px">
                                Salary
                            </td>
                            <td>
                                <asp:TextBox ID="txtsalary" runat="server" onkeypress="return isNumberKey(event)"
                                    TabIndex="12"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <h4 class="ppHed acc">
                    US employee/contractor tax details <span class="pls">+</span></h4>
                <div class="ppHedContent">
                    <table style="width: 90%; border-collapse: collapse; margin-left: 10px;">
                        <tr>
                            <td style="width: 49%">
                                <table style="width: 99%; vertical-align: top;">
                                    <tr>
                                        <td style="width: 100px; vertical-align: top">
                                            Street
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpAddress1" runat="server" MaxLength="250" TabIndex="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            City
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpAddress2" runat="server" MaxLength="250" TabIndex="26"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            State
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEmpState" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                TabIndex="25">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Zip
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpZip" runat="server" MaxLength="8" onkeypress="return isNumberKey(event)"
                                                TabIndex="26"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            County
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCounty" runat="server" MaxLength="25" TabIndex="27"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 5%">
                            </td>
                            <td style="vertical-align: top;">
                                <table style="width: 99%; vertical-align: top;">
                                    <tr>
                                        <td style="width: 150px; vertical-align: top">
                                            Filling status 
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdMarriedSingle" runat="server" GroupName="MaritalStatus" Checked="true"
                                                TabIndex="15" />Single &nbsp;&nbsp;
                                            <asp:RadioButton ID="rdMarried" runat="server" GroupName="MaritalStatus" TabIndex="16" />Married
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                            Deductions
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDeductions" runat="server" AutoPostBack="true" TabIndex="17">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                            Date of birth
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDateOfBirth" runat="server" Width="190" TabIndex="20"></asp:TextBox>
                                            <img src="images2/cal.gif" onclick="javascript:NewCssCal('txtDateOfBirth')" style="cursor: pointer" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="SSN" style="display: none;">
                                        <td style="width: 100px">
                                            SSN
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpSSN" runat="server" MaxLength="9" TabIndex="31"></asp:TextBox>
                                        </td>
                                        <td style="width: 30px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 100px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <h4 class="ppHed acc">
                    Personal Details <span class="pls">+</span></h4>
                <div class="ppHedContent">
                    <table style="width: 90%; border-collapse: collapse; margin-left: 10px;">
                        <tr>
                            <td style="width: 100px">
                                Gender
                            </td>
                            <td style="width: 280px">
                                <asp:RadioButton ID="rdGenderMale" runat="server" GroupName="Gender" TabIndex="18" />Male
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="rdGenderFeMale" runat="server" GroupName="Gender" TabIndex="19" />Female
                            </td>
                            <td style="width: 30px">
                            </td>
                            <td>
                                Business email
                            </td>
                            <td>
                                <asp:TextBox ID="txtBusinessEmail" runat="server" MaxLength="100" TabIndex="23"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Phone#
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpPhone" runat="server" MaxLength="15" onkeypress="return isNumberKey(event)"
                                    TabIndex="23"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Personal email
                            </td>
                            <td>
                                <asp:TextBox ID="txtPersonalEmail" runat="server" MaxLength="100" TabIndex="24"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mobile#
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpMobile" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)"
                                    TabIndex="22"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Driver License#
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpDriveLicense" runat="server" MaxLength="30" TabIndex="30"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <h4 class="ppHed acc">
                    Emergency Contact Details <span class="pls">+</span></h4>
                <div class="ppHedContent">
                    <table style="width: 99%; border-collapse: collapse; margin-left: 10px;">
                        <tr>
                            <td style="width: 32%;">
                                <fieldset class="popupFieldSet">
                                    <legend>Person 1</legend>
                                    <table style="width: 99%; border-collapse: collapse;">
                                        <tr>
                                            <td style="width: 100px">
                                                Contact Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn1Name" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Phone#
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn1Phone" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn1Email" runat="server" MaxLength="150"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Relation
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn1Relation" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Street
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn1Address1" runat="server" MaxLength="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                City
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn1Address2" runat="server" MaxLength="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                State
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCn1State" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Zip
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn1Zip" runat="server" MaxLength="8" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="width: 2%">
                                &nbsp;
                            </td>
                            <td style="width: 32%;">
                                <fieldset class="popupFieldSet">
                                    <legend>Person 2</legend>
                                    <table style="width: 99%; border-collapse: collapse">
                                        <tr>
                                            <td style="width: 100px">
                                                Contact Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn2Name" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Phone#
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn2Phone" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn2Email" runat="server" MaxLength="150"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Relation
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn2Relation" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Street
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn2Address1" runat="server" MaxLength="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                City
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn2Address2" runat="server" MaxLength="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                State
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCn2State" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Zip
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn2Zip" runat="server" MaxLength="8" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="width: 2%">
                                &nbsp;
                            </td>
                            <td style="width: 32%;">
                                <fieldset class="popupFieldSet">
                                    <legend>Person 3</legend>
                                    <table style="width: 99%; border-collapse: collapse">
                                        <tr>
                                            <td style="width: 100px">
                                                Contact Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn3Name" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Phone#
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn3Phone" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn3Email" runat="server" MaxLength="150"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Relation
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn3Relation" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Street
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn3Address1" runat="server" MaxLength="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                City
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn3Address2" runat="server" MaxLength="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                State
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCn3State" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Zip
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn3Zip" runat="server" MaxLength="8" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <!--Edit popup-->
    <cc1:ModalPopupExtender ID="mdlEditPopup" BackgroundCssClass="popupHolder" runat="server"
        PopupControlID="DivEditpopup" CancelControlID="lnkEditClose" TargetControlID="hdnEdit">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnEdit" runat="server" />
    <div id="DivEditpopup" runat="server" class="popContent" style="width: 450px; display: none">
        <asp:UpdatePanel ID="uptbl" runat="server">
            <ContentTemplate>
                <h2>
                    <asp:Label ID="lblPopupEditHead" runat="server"></asp:Label><span class="close">
                        <asp:LinkButton ID="lnkEditClose" runat="server" OnClick="lnkEditClose_Click"></asp:LinkButton></span>
                </h2>
                <div class="inner" runat="server" id="viewPopup">
                    <table style="width: 90%; margin: 20px auto; border-collapse: collapse;">
                        <tr>
                            <td style="width: 120px;">
                                EmpID
                            </td>
                            <td>
                                <asp:Label ID="lblEditEmpID" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnUserID" runat="server" />
                                <asp:HiddenField ID="hdnPhotLink" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                First name
                            </td>
                            <td>
                                <asp:Label ID="lblEditFirstname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Last name
                            </td>
                            <td>
                                <asp:Label ID="lblEditLastname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Department
                            </td>
                            <td>
                                <asp:Label ID="lblEditDepartmentName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Designation
                            </td>
                            <td>
                                <asp:Label ID="lblEditDesg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                <asp:Label ID="lblActive" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Start date
                            </td>
                            <td>
                                <asp:Label ID="lblEditStartDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEditHeartermDt" runat="server" Text="Term date" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEditTermDate" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEditHeadTermRes" runat="server" Text="Term reason" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Enabled="false" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle">
                                Photo
                            </td>
                            <td style="vertical-align: middle">
                                <div class="photo">
                                    <asp:Image ID="imgPhoto" runat="server" ImageUrl="Photos/defaultUSer.jpg" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <div style="display: inline-block">
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-danger" OnClick="btnEdit_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="inner" runat="server" id="Editpopup" style="display: none">
                    <table style="width: 90%; margin: 20px auto; border-collapse: collapse;">
                        <tr>
                            <td style="width: 120px;">
                                EmpID
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditEmpID" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                First name<span class="must">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditFirstname" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Last name<span class="must">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditLastname" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Department<span class="must">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEditDepart" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Designation
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditDesg" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                <asp:RadioButton ID="rdEditActiveTrue" runat="server" GroupName="editActive" AutoPostBack="true"
                                    OnCheckedChanged="rdEditActiveTrue_CheckedChanged" />Yes
                                <asp:RadioButton ID="rdEditActiveFalse" runat="server" GroupName="editActive" AutoPostBack="true"
                                    OnCheckedChanged="rdEditActiveFalse_CheckedChanged" />No
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Start date<span class="must">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditStartDate" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEdit1TermDate" runat="server" Text="Term date" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditTermDate" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEdit1Termreason" runat="server" Text="Term reason" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEdit1TermReason" runat="server" TextMode="MultiLine" Visible="false"
                                    MaxLength="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle">
                                Photo
                            </td>
                            <td style="vertical-align: middle">
                                <asp:FileUpload ID="photoUpload1" runat="server" />
                                <div class="photo">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="Photos/defaultUSer.jpg" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <div style="display: inline-block">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-danger"
                                        OnClick="btnUpdate_Click" OnClientClick="return validateEditSubmit();" />
                                    <asp:Button ID="btnEditCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnEditCancel_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlEditDepart" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnEditCancel" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="rdEditActiveFalse" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="rdEditActiveTrue" EventName="CheckedChanged" />
                <asp:PostBackTrigger ControlID="btnUpdate" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!--Change password popup start-->
    <cc1:ModalPopupExtender ID="mdlChangePwd" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkPwdClose" TargetControlID="hdnChangePwd" PopupControlID="ChangePwd">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnChangePwd" runat="server" />
    <div id="ChangePwd" runat="server" class="popContent" style="width: 400px; display: none">
        <h2>
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblPwdName" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkPwdClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
        <asp:UpdatePanel ID="uppwddv" runat="server">
        <ContentTemplate>
          <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        Old password<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOldpwd" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        New password<span class="must">*</span>
                        <br />
                        <span style="font-size: 10px; color: GrayText">(Maximum 10 characters)</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewPwd" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm password<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmPwd" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnUpdatePwd" runat="server" Text="Update" CssClass="btn btn-danger"
                                        OnClientClick="return validPwd();" OnClick="btnUpdatePwd_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelPwd" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="btnCancelPwd" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancelPwd_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
          
        </div>
    </div>
    <!--Change password popup End-->
    <!--Successfull alert popup start-->
    <cc1:ModalPopupExtender ID="mdlSuccessfullAlert" runat="server" BackgroundCssClass="popupHolder"
        TargetControlID="hdnAlert" PopupControlID="Alertpopup" CancelControlID="lnkAlertClose"
        OkControlID="btnAlertOk">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnAlert" runat="server" />
    <div id="Alertpopup" runat="server" class="popContent" style="width: 350px; display: none">
        <h2>
            <span class="close">
                <asp:LinkButton ID="lnkAlertClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <h1>
                                    <asp:Label ID="lblAlertName" runat="server"></asp:Label>
                                    changed successfully...
                                </h1>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAlertOk" runat="server" Text="OK" CssClass="btn btn-small" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--Successfull alert popup End-->
    <!--Change passcode popup start-->
    <cc1:ModalPopupExtender ID="mdlChangePasscode" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkPasscodeClose" TargetControlID="hdnChangePasscode" PopupControlID="passcodepopup">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnChangePasscode" runat="server" />
    <div id="passcodepopup" runat="server" class="popContent" style="width: 400px; display: none">
        <h2>
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblPasscodeName" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkPasscodeClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
        <asp:UpdatePanel ID="uppassdv" runat="server">
        <ContentTemplate>
          <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        Old passcode<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOldpasscode" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        New passcode<span class="must">*</span>
                        <br />
                        <span style="font-size: 10px; color: GrayText">(Maximum 10 characters)</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewPasscode" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm passcode<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmPasscode" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnUpdatePassCode" runat="server" Text="Update" CssClass="btn btn-danger"
                                        OnClientClick="return validPasscode();" OnClick="btnUpdatePassCode_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelPasscode" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="btnCancelPasscode" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancelPasscode_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
          
        </div>
    </div>
    <!--Change password popup End-->
    <!--add new schedule popup-->
    <cc1:ModalPopupExtender ID="mdlSchedulepopup" runat="server" BackgroundCssClass="popupHolder2"
        CancelControlID="lnkScheduleClose" TargetControlID="hdnSchedulepop" PopupControlID="Schedulepop">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnSchedulepop" runat="server" />
    <div id="Schedulepop" runat="server" class="popContent popupContent2" style="width: 400px;
        display: none">
        <h2>
            Add Schedule <span class="close">
                <asp:LinkButton ID="lnkScheduleClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        Schedule starttime
                    </td>
                    <td>
                        <asp:TextBox ID="txtSheduleStart" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Schedule endtime
                    </td>
                    <td>
                        <asp:TextBox ID="txtScheduleEnd" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Lunch start
                    </td>
                    <td>
                        <asp:TextBox ID="txtLunchStart" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Lunch end
                    </td>
                    <td>
                        <asp:TextBox ID="txtLunchEnd" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Days
                    </td>
                    <td>
                        <asp:RadioButton ID="rdFive" runat="server" GroupName="ActiveDays" />Five&nbsp;&nbsp;
                        <asp:RadioButton ID="rdSix" runat="server" GroupName="ActiveDays" />Six &nbsp;&nbsp;
                        <asp:RadioButton ID="rdSeven" runat="server" GroupName="ActiveDays" />Seven &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="upSch" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSchUpdate" runat="server" Text="Update" CssClass="btn btn-danger"
                                        OnClientClick="return validSchedules();" OnClick="btnSchUpdate_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelSch" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="btnCancelSch" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancelSch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>

    <script src="js/jquery-ui.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/jquery-ui-timepicker-addon.js"></script>

    <script type="text/javascript" src="js/jquery-ui-sliderAccess.js"></script>

    <script type="text/javascript" language="javascript">
    
    $(window).load(function () {
    
           $('#spinner').hide();
    
        $('.ppHedContent:not(:eq(0))').hide();
        
        
        
        $('.ppHed').click(function(){
            $(this).next('div.ppHedContent').slideToggle();
        });
        
        
    
        $.widget("ui.tooltip", $.ui.tooltip, {
            options: {
                content: function () {
                    return $(this).prop('title');
                }
            }
        });

        $('[rel=tooltip]').tooltip({
            position: {
                my: "center bottom-20",
                at: "center top",
                using: function (position, feedback) {
                    $(this).css(position);
                    $("<div>")
                        .addClass("arrow")
                        .addClass(feedback.vertical)
                        .addClass(feedback.horizontal)
                        .appendTo(this);
                }
            }
        });

        //console.log('Time')
        $('#txtStartDt').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
        
        
//          $('#txtDateOfBirth').datepicker({
//            dateFormat: "mm/dd/yy"
//            //timeFormat:"hh:mm tt"      
//        });
//        
        
         $('#txtSheduleStart').timepicker({
            timeFormat:"hh:mmTT"      
        });
          $('#txtScheduleEnd').timepicker({
            timeFormat:"hh:mmTT"      
        });
          $('#txtLunchStart').timepicker({
            timeFormat:"hh:mmTT"      
        });
          $('#txtLunchEnd').timepicker({
            timeFormat:"hh:mmTT"      
        });
        
        
        
            $('#txtEditStartDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
        $('#txtEditTermDate').datepicker({
            dateFormat: "mm/dd/yy"
          
        });

    });
    // Window load Close
    
    

   



    function pageLoad() {
         $('#spinner').hide();
        $.widget("ui.tooltip", $.ui.tooltip, {
            options: {
                content: function () {
                    return $(this).prop('title');
                }
            }
        });

        $('[rel=tooltip]').tooltip({
            position: {
                my: "center bottom-20",
                at: "center top",
                using: function (position, feedback) {
                    $(this).css(position);
                    $("<div>")
                        .addClass("arrow")
                        .addClass(feedback.vertical)
                        .addClass(feedback.horizontal)
                        .appendTo(this);
                }
            }
        });

        //console.log('Time')
        $('#txtStartDt').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
        
        
//        $('#txtDateOfBirth').datepicker({
//            dateFormat: "mm/dd/yy"
//            //timeFormat:"hh:mm tt"      
//        });
            
        $('#txtEditStartDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
        
         $('#txtEditTermDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });

    }

    $(function(){
        $('.ppHed.acc').on('click', function(){
            if($(this).next('div.ppHedContent').is(':visible')){
                $(this).children('span').html('+');
            }else{
                $(this).children('span').html('-');
            }
        });
    
        
    });

/*
    $(function () {   
        
    
        $('#txtStartDt').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
           
        $('#txtEditStartDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
        $('#txtEditTermDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });

    })
    */
    </script>

</body>
</html>
