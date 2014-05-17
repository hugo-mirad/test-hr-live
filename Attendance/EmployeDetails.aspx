<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeDetails.aspx.cs"
    Inherits="Attendance.EmployeDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/CalControl1.ascx" TagName="calender" TagPrefix="UCL" %>
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

    <script src="js/jquery-ui.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/jquery-ui-timepicker-addon.js"></script>

    <script type="text/javascript" src="js/jquery-ui-sliderAccess.js"></script>

    <script src="js/datetimepicker_css.js" type="text/javascript"></script>

    <script>
    
        $(function(){
            $('.popupHolder2').css('z-index','100002')
            $('.popupContent2').css('z-index','100003')
        })
        
    </script>

    <script type="text/javascript">
    
    
       $(window).load(function () { 
            $('#spinner').hide();

            $('#txtEditStartDate').datepicker({
               dateFormat: "mm/dd/yy"
                 
            });
           $('#txtEditTermDate').datepicker({
            dateFormat: "mm/dd/yy"
          
            });
     
            
          });
    
    
    
        function pageLoad(){
         $('#spinner').hide();
            if($.trim($('#lblAddress').text()).charAt(0) == ','){
                $('#lblAddress').text($.trim($('#lblAddress').text()).substring(1,$.trim($('#lblAddress').text()).length-1))
            }
            $('#imgPhoto').removeAttr('style');
            $('#txtEditStartDate').datepicker({
               dateFormat: "mm/dd/yy"
            });
           $('#txtEditTermDate').datepicker({
            dateFormat: "mm/dd/yy"
  
          });  
          
             var Current=new Date($('#lblDate2').text());
             var mindt=(Current.getMonth()+1)+"/" +Current.getDate() +"/"+Current.getFullYear();
  
          
            $('#txtPerEffectiveDt').datepicker({
             dateFormat: "mm/dd/yy",
             minDate:mindt    
          });  
          
          
          $('#txtEffectDt').datepicker({
            dateFormat: "mm/dd/yy",
               minDate:mindt   
          });  
          
           $('#txtSalEffectDt').datepicker({
            dateFormat: "mm/dd/yy",
            minDate:mindt   
  
          });  
          
          
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
          
       
        }
        
         function showspinner()
        {
         $('#spinner').show();
         return true;
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

        function ValidateSalSubmit()
        {
        debugger
            var valid=true;
            if (document.getElementById('ddlEditWage').value=="0")
           {
               alert("Please select wages.");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
               document.getElementById("ddlEditWage").focus();
           }
           return valid;
        }
        
          function ValidatePersonalSalSubmit()
          {
         debugger
               var valid=true;
          if(document.getElementById('txtEmpMobile').value!="")
           {
             if(document.getElementById('txtEmpMobile').value.trim().length<10)
             {
               alert("Please enter valid mobile#.");               
               valid=false;
                document.getElementById('txtEmpMobile').value="";
                 document.getElementById('txtEmpMobile').focus();
              }
           }
           if (document.getElementById('<%= txtPersonalEmail.ClientID %>').value!="")
	        {
	            if (echeck(document.getElementById('<%= txtPersonalEmail.ClientID %>').value)==false)
	            {     
		        document.getElementById('<%= txtPersonalEmail.ClientID %>').value=""
		        
		        document.getElementById('<%= txtPersonalEmail.ClientID %>').focus();
		        
		        valid=false;
        		return valid;
        		}
	        }        
    
           if (document.getElementById('<%= txtBusinessEmail.ClientID %>').value!="")
	        {
	            if (echeck(document.getElementById('<%= txtBusinessEmail.ClientID %>').value)==false)
	            {     
		        document.getElementById('<%= txtBusinessEmail.ClientID %>').value=""
		         
		        document.getElementById('<%= txtBusinessEmail.ClientID %>').focus()
		        valid=false;
        		return valid;
        		}
	        }    
	 	        
	          if (document.getElementById('<%= txtZip.ClientID %>').value!="")
	         {
	             if(document.getElementById('txtZip').value.trim().length<4)
               {
               alert("Please enter valid zip.");               
               valid=false;
                document.getElementById('txtZip').value="";
               
               document.getElementById('txtZip').focus();
              }
	        }  
	      
	       if (document.getElementById('<%= txtSSN.ClientID %>')!=null)
	         {
	        
	          if (document.getElementById('<%= txtSSN.ClientID %>').value!="")
	          {
	             if(document.getElementById('txtSSN').value.trim().length<9)
               {
                  alert("Please enter valid ssn.");               
                  valid=false;
                  document.getElementById('txtSSN').value="";
                  $('#txtEmpSSN').closest('.ppHedContent').slideDown().prev('h4').children('span').html('-');
                  document.getElementById('txtSSN').focus();
              
                }
	          }
	        }
	  
       return valid;
       }
        
        
        function validateEmergncyubmit(){
        var valid=true;
        debugger
         if(document.getElementById('txtCn1Phone').value!="")
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
    
      if(document.getElementById('txtCn2Phone').value!="")
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
    
         if(document.getElementById('txtCn3Phone').value!="")
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
	        
	            else  if (document.getElementById('<%= txtCn1Zip.ClientID %>').value!="")
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
         if(document.getElementById('ddlShift').value=="0")
           {
               alert("Please select shift.");               
               valid=false;
               document.getElementById("ddlShift").focus();
           }
         
        else if (document.getElementById('txtEditFirstname').value=="") {
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
           
             else if (document.getElementById('ddlSchedule').value=="0")
           {
               alert("Please select schedule .");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
               document.getElementById("ddlSchedule").focus();
           }
           
               else if (document.getElementById('ddlEmpType').value=="0")
           {
               alert("Please select employee type .");               
               valid=false;
                //document.getElementById("ddlDeptment").focus="";
               document.getElementById("ddlEmpType").focus();
           }
       
          else if(document.getElementById('txtEditStartDate').value=="")
           {
               alert("Please enter start date.");               
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
           if(document.getElementById('txtEdit1TermReason')!=null)
           {
            if(document.getElementById('txtEdit1TermReason').value=="")
            {
               alert("Please enter terminate reason.");               
               valid=false;
                document.getElementById("txtEdit1TermReason").value="";
               document.getElementById("txtEdit1TermReason").focus();
             }
            
           }
           if((document.getElementById('txtEditTermDate').value!="") && (document.getElementById('txtEdit1TermReason').value!=""))
           {
            var start=document.getElementById('txtEditStartDate').value;
            var end=document.getElementById('txtEditTermDate').value;
            startdate=new Date(start);
            enddate=new Date(end);
            var today=document.getElementById('hdnToday').value;
            var todaydate=new Date(today);
                 
               if( startdate>enddate)
                { 
                 alert("Startdate should be lessthan terminate date");
                  document.getElementById("txtEditTermDate").focus();
                 valid=false;
                }
                if(enddate>todaydate)
                {
                  alert("Terminate date should not be the future date");
                  document.getElementById("txtEditTermDate").focus();
                 valid=false;
                }
           }
  
        }
      
        
        if(valid==true)
        {
        $('#spinner').show();
        }
        else
        {
         $('#spinner').hide();
        }
        return valid;
       }
        
          
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
       
       
            
            
         
        
    </script>
    
    <style>
     .table1 td,th{ font-size:12px;}
    
    </style>

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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upEditEmployee"
        DisplayAfter="0">
        <ProgressTemplate>
            <div id="spinner">
                <h4>
                    <div>
                        Processing
                        <img src="images/loading.gif" />
                    </div>
                  
                </h4>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
        DisplayAfter="0">
        <ProgressTemplate>
            <div id="spinner">
                <h4>
                    <div>
                        Processing
                        <img src="images/loading.gif" />
                    </div>
                 
                </h4>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upPerson"
        DisplayAfter="0">
        <ProgressTemplate>
            <div id="spinner">
                <h4>
                    <div>
                        Processing
                        <img src="images/loading.gif" />
                    </div>
                   
                </h4>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <div id="spinner">
                <h4>
                    <div>
                        Processing
                        <img src="images/loading.gif" />
                    </div>
                   
                </h4>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
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
                                            <asp:LinkButton runat="server" ID="lnkUserMangement" Text="Employee Management" OnClick="lnkUserMangement_Click"></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkLeaveApproval" Text="Leave Approval Management"
                                                PostBackUrl="LeaveApprovalManagement.aspx"></asp:LinkButton>
                                        </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton1" Text="Future Changes Management"
                                                    PostBackUrl="EffectiveManagement.aspx"></asp:LinkButton>
                                            </li>
                                        <li style="display: none;">
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
                                    <b>--:--:-- AM </b><strong>(<asp:Label ID="lblTimeZoneName" runat="server"></asp:Label>)</strong>
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
    <asp:UpdatePanel ID="upTotal" runat="server">
        <ContentTemplate>
            <h2 class="pageHeadding">
                <span>Employee details</span>
            </h2>
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-small btn-success"
                Style="margin-left: 1166px; display: inline-block; margin-bottom: 0px; text-decoration: none;"
                OnClick="btnBack_Click" />
            <div style="width: 98%; margin-left: 10px;">
                <div class="inner">
                    <table style="width: 100%; border-collapse: collapse">
                        <tr>
                            <td style="width: 48%; vertical-align: top;">
                                <h4 class="ppHed" style="margin-bottom: 0">
                                    Employee Details <span class="actions">
                                        <asp:Button ID="btnEmpEdit" runat="server" class="btn btn-inverse btn-small" Text="Edit"
                                            OnClick="btnEmpEdit_Click" />
                                    </span>
                                </h4>
                                <div style="border: #ccc 1px solid; height: 270px;">
                                    <div class="ppHedContent" style="margin: 10px; padding: 0">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td style="width: 80%; min-width: 390px;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                <b>EmpID</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdnUserID" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Name</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFirstname" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdnLastname" runat="server" />
                                                                <asp:HiddenField ID="hdnFirst" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Business Name</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBusinessName" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdnBusinessFirst" runat="server" />
                                                                <asp:HiddenField ID="hdnBusinessLast" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Emp Type</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEmpType" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Department</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldepartment" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Shift</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblShift" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdnShiftID" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Schedule</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSchedule" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdnScID" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Designation</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Start date</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Active</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblActive" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTextTermDt" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTermdate" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbltextTermReason" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTermReason" runat="server" Enabled="false" MaxLength="6" Visible="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="vertical-align: top; width: 130px;">
                                                    <asp:Image ID="imgPhoto" CssClass="detImg" runat="server" ImageUrl="~/Photos/defaultUSer.jpg" />
                                                    <br />
                                                    <br />
                                                    <asp:UpdatePanel ID="upreset" runat="server">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkResetPasscode" runat="server" Text="Reset Passcode" OnClick="lnkResetPasscode_Click"></asp:LinkButton>
                                                            <br />
                                                            <asp:LinkButton ID="lnkResetPassword" runat="server" Text="Reset Password" OnClick="lnkResetPassword_Click"></asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                            <td style="width: 2%;">
                                &nbsp
                            </td>
                            <td style="vertical-align: top;">
                                <h4 class="ppHed" style="margin-bottom: 0">
                                    Personal Details<span class="actions">
                                        <asp:Button ID="btnEditPersonalDet" runat="server" class="btn btn-inverse btn-small"
                                            Text="Edit" OnClick="btnEditPersonalDet_Click" />
                                    </span>
                                </h4>
                                <div style="border: #ccc 1px solid; height: 270px;">
                                    <div class="ppHedContent" style="margin: 0 10px;">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td style="width: 310px; vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 110px;">
                                                                <b>Gender</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Date of birth</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDateofBirth" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Phone#</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPhoneNum" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Mobile#</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMobileNum" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table>
                                                        <tr>
                                                            <td style="width: 110px;">
                                                                <b>Personal email</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPersonalEmail" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Business email</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBusinessemail" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Driver License#</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblLicense" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="SSN" style="display: none;">
                                                            <td>
                                                                <b>SSN</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSSN" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 80px; vertical-align: top;">
                                                                <b>Address</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 20px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <h4 class="ppHed" style="margin-bottom: 0">
                                    Salary\Tax Details <span class="actions">
                                        <asp:Button ID="btnEditSalaryDetails" runat="server" class="btn btn-inverse btn-small"
                                            Text="Edit" OnClick="btnEditSalaryDetails_Click" />
                                    </span>
                                </h4>
                                <div style="border: #ccc 1px solid;">
                                    <div class="ppHedContent" style="margin: 0 10px;">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 110px">
                                                                <b>Wages</b>
                                                            </td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="lblWageType" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 10px">
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Salary</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSalary" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 110px">
                                                                <b>Filling status</b>
                                                            </td>
                                                            <td style="width: 200px">
                                                                <asp:Label ID="lblFilling" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 10px">
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Deductions</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDeuctions" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 20px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <h4 class="ppHed" style="margin-bottom: 0">
                                    Emergency Contact Details<span class="actions">
                                        <asp:Button ID="btnEditEmergencyDet" runat="server" class="btn btn-inverse btn-small"
                                            Text="Edit" OnClick="btnEditEmergencyDet_Click" />
                                    </span>
                                </h4>
                                <div style="border: #ccc 1px solid;">
                                    <div class="ppHedContent" style="margin: 0 10px">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td style="width: 32%;">
                                                    <fieldset class="popupFieldSet">
                                                        <legend>Person 1</legend>
                                                        <table style="width: 99%; border-collapse: collapse;">
                                                            <tr>
                                                                <td style="width: 100px">
                                                                    <b>Contact Name</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn1Name" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Phone#</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn1Phone" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Email</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn1Email" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Relation</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn1Relation" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <b>Address</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn1Address" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                                <td style="width: 1%">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 33%;">
                                                    <fieldset class="popupFieldSet">
                                                        <legend>Person 2</legend>
                                                        <table style="width: 99%; border-collapse: collapse">
                                                            <tr>
                                                                <td style="width: 100px">
                                                                    <b>Contact Name</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn2Name" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Phone#</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn2Phone" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Email</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn2Email" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Relation</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn2Relation" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <b>Address</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn2Address" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                                <td style="width: 1%">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 33%;">
                                                    <fieldset class="popupFieldSet">
                                                        <legend>Person 3</legend>
                                                        <table style="width: 99%; border-collapse: collapse">
                                                            <tr>
                                                                <td style="width: 100px">
                                                                    <b>Contact Name</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn3Name" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Phone#</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn3Phone" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Email</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn3Email" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <b>Relation</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn3Relation" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <b>Address</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCn3Address" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <h4 class="ppHed" style="margin-bottom: 0">
                                    Effective changes history
                                </h4>
                                <div style="border: #ccc 1px solid; max-height:200px;">
                                    <div class="ppHedContent" style="margin: 0 10px;max-height:200px;">
                                    <div style="overflow-y:scroll;height:120px; ">
                                        <asp:GridView ID="grdEffectChanges" runat="server" AutoGenerateColumns="false" Style="min-width: 70%" CssClass="table1"
                                            OnRowDataBound="grdEffectChanges_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Field name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChangeField" runat="server" Text='<%#Eval("ChangeField")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Old value ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOldValue" runat="server" Text='<%#Eval("OldValue")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="New value">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNewValue" runat="server" Text='<%#Eval("NewValue")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChangeStatus" runat="server" Text='<%#Eval("ChangeStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Effect date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffectiveDt" runat="server" Text='<%# Bind("EffectiveDt", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entered by">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEnterBy" runat="server" Text='<%#Eval("EnterByName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entered date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChangedDt" runat="server" Text='<%# Bind("EnteredDt", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnEmpEdit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEditSalaryDetails" EventName="Click" />
            <%--  <asp:AsyncPostBackTrigger ControlID="btnEdittaxDet" EventName="Click" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnEditPersonalDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEditEmergencyDet" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
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
                                Old password
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
    <!--Edit popup-->
    <cc1:ModalPopupExtender ID="mdlEditPopup" BackgroundCssClass="popupHolder" runat="server"
        PopupControlID="DivEditpopup" CancelControlID="lnkEditClose" TargetControlID="hdnEdit">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnEdit" runat="server" />
    <div id="DivEditpopup" runat="server" class="popContent" style="width: 900px; display: none">
        <h2>
            Edit employee details<span class="close">
                <asp:LinkButton ID="lnkEditClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner" runat="server" id="Editpopup">
            <asp:UpdatePanel ID="UP" runat="server">
                <ContentTemplate>
                    <table style="width: 95%; margin: 20px auto; border-collapse: collapse;">
                        <tr>
                            <td style="width: 100px;">
                                <b>EmpID </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditEmpID" runat="server" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="hdnToday" runat="server" />
                            </td>
                            <td>
                            </td>
                            <td>
                                Shift<span class="must">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlShift" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <fieldset class="popupFieldSet">
                                    <legend>Personal</legend>
                                    <table style="width: 100%; border-collapse: collapse;">
                                        <tr>
                                            <td style="width: 100px;">
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
                                            <td style="width: 100px;">
                                                First name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditBFname" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Last name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditBLname" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">
                                Employee type<span class="must">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEmpType" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
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
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSchedule" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lnkScheduleAdd" runat="server" Text="Add new" OnClick="lnkScheduleAdd_Click"></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                            <td>
                            </td>
                            <td>
                                Designation
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditDesg" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
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
                            <td>
                            </td>
                            <td style="vertical-align: middle">
                                Photo
                            </td>
                            <td style="vertical-align: middle">
                                <table style="width: 100%; border-collapse: collapse" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="vertical-align: top; width: 220px;">
                                            <asp:FileUpload ID="photoUpload1" runat="server" Style="width: 98%;" /><br />
                                            <span style="font-size: 10px; color: GrayText">(Maximum file size 4MB)</span>
                                        </td>
                                        <td style="vertical-align: top; text-align: left;">
                                            <div class="photo" style="float: left">
                                                <asp:Image ID="EdiImg" runat="server" ImageUrl="Photos/defaultUSer.jpg" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
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
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblEdit1TermDate" runat="server" Text="Term date <span class='must'>*</span>"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditTermDate" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEdit1Termreason" runat="server" Text="Term reason <span class='must'>*</span>"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEdit1TermReason" runat="server" TextMode="MultiLine" Visible="false"
                                    MaxLength="250"></asp:TextBox>
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="vertical-align: top;">
                                <table style="width: 80%">
                                    <tr>
                                        <td style="vertical-align: top; width: 30%;">
                                            Changes to be update for
                                        </td>
                                        <td style="vertical-align: top;">
                                            <table style="width: 60%">
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        <asp:RadioButton ID="rdNow" runat="server" Checked="true" AutoPostBack="true" GroupName="Now"
                                                            OnCheckedChanged="rdNow_CheckedChanged" />
                                                        Now
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="rdFuture" runat="server" GroupName="Now" AutoPostBack="true"
                                                            OnCheckedChanged="rdFuture_CheckedChanged" />
                                                        Future
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trEffect" style="display: none;">
                                                    <td>
                                                        Effective date
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEffectDt" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                            <td style="text-align: right;">
                                <div style="display: inline-block">
                                    <asp:UpdatePanel ID="upEditEmployee" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-danger"
                                                OnClientClick="return validateEditSubmit();" OnClick="btnUpdate_Click" />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlEmpType" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlEditDepart" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlSchedule" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkEditClose" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlShift" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnEditCancel" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="rdEditActiveFalse" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rdEditActiveTrue" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rdNow" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rdFuture" EventName="CheckedChanged" />
                                            <asp:PostBackTrigger ControlID="btnUpdate" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:Button ID="btnEditCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnEditCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--Edit Salary/Tax popup start-->
    <cc1:ModalPopupExtender ID="mdlEditSalTaxPopup" BackgroundCssClass="popupHolder"
        runat="server" PopupControlID="divEditSalTax" CancelControlID="lnkEditSalClose"
        TargetControlID="hdnEditSal">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnEditSal" runat="server" />
    <div id="divEditSalTax" runat="server" class="popContent" style="width: 450px; display: none">
        <h2>
            Edit salary/tax details <span class="close">
                <asp:LinkButton ID="lnkEditSalClose" runat="server"></asp:LinkButton></span></h2>
        <div>
            <asp:UpdatePanel ID="upn" runat="server">
                <ContentTemplate>
                    <table style="width: 90%; margin: 20px auto; border-collapse: collapse;">
                        <tr>
                            <td style="width: 120px;">
                                Wages<span class="must">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEditWage" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Salary
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditSal" runat="server" MaxLength="50" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Filling status
                            </td>
                            <td>
                                <asp:RadioButton ID="rdSingle" runat="server" GroupName="Marrage" />Single &nbsp;
                                <asp:RadioButton ID="rdMarried" runat="server" GroupName="Marrage" Checked="true" />Married
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Deductions
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDeductions" runat="server" AutoPostBack="true">
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
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="vertical-align: top;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="vertical-align: top; width: 40%">
                                            Changes to be update for
                                        </td>
                                        <td style="vertical-align: top;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        <asp:RadioButton ID="rdSalNow" runat="server" Checked="true" AutoPostBack="true"
                                                            GroupName="Now" OnCheckedChanged="rdSalNow_CheckedChanged" />
                                                        Now
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="rdSalFuture" runat="server" GroupName="Now" AutoPostBack="true"
                                                            OnCheckedChanged="rdSalFuture_CheckedChanged" />
                                                        Future
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trsalEf" style="display: none;">
                                                    <td>
                                                        Effective date
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSalEffectDt" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSalEdit" runat="server" Text="Update" CssClass="btn btn-danger"
                                                OnClientClick="return ValidateSalSubmit();" OnClick="btnSalEdit_Click" />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDeductions" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlEditWage" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkEditSalClose" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalEditCancel" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="rdSingle" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rdMarried" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rdSalNow" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rdSalFuture" EventName="CheckedChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:Button ID="btnSalEditCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnSalEditCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--Edit Salary/Tax popup End-->
    <!-- EditEmp Personal Details Popup start-->
    <cc1:ModalPopupExtender ID="mdlEditPersonalDetails" BackgroundCssClass="popupHolder"
        runat="server" PopupControlID="divEditPersonal" CancelControlID="lnkEditPersonClose"
        TargetControlID="hdnpersonal">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnpersonal" runat="server" />
    <div id="divEditPersonal" runat="server" class="popContent" style="width: 450px;
        display: none">
        <h2>
            Edit Personal details<span class="close">
                <asp:LinkButton ID="lnkEditPersonClose" runat="server"></asp:LinkButton></span></h2>
        <div>
            <asp:UpdatePanel ID="upper" runat="server">
                <ContentTemplate>
                    <table style="width: 90%; margin: 20px auto; border-collapse: collapse;">
                        <tr>
                            <td style="width: 120px;">
                                Gender
                            </td>
                            <td>
                                <asp:RadioButton ID="rdMale" runat="server" GroupName="Gender" />Male
                                <asp:RadioButton ID="rdFemale" runat="server" GroupName="Gender" />Female
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Date of birth
                            </td>
                            <td>
                                <asp:TextBox ID="txtDatBirth" runat="server" Width="100"> &nbsp;</asp:TextBox>
                                <img src="images2/cal.gif" onclick="javascript:NewCssCal('txtDatBirth')" style="cursor: pointer" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mobile#
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpMobile" runat="server" onkeypress="return isNumberKey(event)"
                                    MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Phone#
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpPhone" runat="server" onkeypress="return isNumberKey(event)"
                                    MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Personal email
                            </td>
                            <td>
                                <asp:TextBox ID="txtPersonalEmail" runat="server" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Business email
                            </td>
                            <td>
                                <asp:TextBox ID="txtBusinessEmail" runat="server" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Driver license#
                            </td>
                            <td>
                                <asp:TextBox ID="txtDriving" runat="server" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Street
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress1" runat="server" MaxLength="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                City
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress2" runat="server" MaxLength="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                State
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Zip
                            </td>
                            <td>
                                <asp:TextBox ID="txtZip" runat="server" MaxLength="8" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="Tr1County" style="display: none">
                            <td>
                                County
                            </td>
                            <td>
                                <asp:TextBox ID="txtCounty" runat="server" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="SSNEdit" style="display: none">
                            <td>
                                SSN
                            </td>
                            <td>
                                <asp:TextBox ID="txtSSN" runat="server" MaxLength="9" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="divider1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="vertical-align: top;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="vertical-align: top; width: 40%">
                                            Changes to be update for
                                        </td>
                                        <td style="vertical-align: top;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        <asp:RadioButton ID="rdPerNow" runat="server" Checked="true" AutoPostBack="true"
                                                            GroupName="Now" OnCheckedChanged="rdPerNow_CheckedChanged" />
                                                        Now
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="rdPerFuture" runat="server" GroupName="Now" AutoPostBack="true"
                                                            OnCheckedChanged="rdPerFuture_CheckedChanged" />
                                                        Future
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trPerEffect" style="display: none;">
                                                    <td>
                                                        Effective date
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPerEffectiveDt" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnUpdatePersonal" runat="server" Text="Update" CssClass="btn btn-danger"
                                                OnClientClick="return ValidatePersonalSalSubmit();" OnClick="btnUpdatePersonal_Click" />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlState" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkEditPersonClose" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnPersonalEditCancel" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="rdMale" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rdFemale" EventName="CheckedChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:Button ID="btnPersonalEditCancel" runat="server" Text="Cancel" CssClass="btn"
                                    OnClick="btnPersonalEditCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- EditEmp Personal Details Popup End-->
    <!--Edit Emergency contact details start-->
    <cc1:ModalPopupExtender ID="mdlEditEmergContactDet" BackgroundCssClass="popupHolder"
        runat="server" PopupControlID="divEditEmergency" CancelControlID="lnkEditEmergClose"
        TargetControlID="hdnEmergency">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnEmergency" runat="server" />
    <div id="divEditEmergency" runat="server" class="popContent" style="width: 800px;
        display: none">
        <h2>
            Edit Emergency details <span class="close">
                <asp:LinkButton ID="lnkEditEmergClose" runat="server"></asp:LinkButton></span></h2>
        <div>
            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                <ContentTemplate>
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
                                                <asp:TextBox ID="txtCn1Email" runat="server" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Relation
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCn1Relation" runat="server" MaxLength="100"></asp:TextBox>
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
                                                <asp:TextBox ID="txtCn2Relation" runat="server" MaxLength="150"></asp:TextBox>
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
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td>
                                <div style="display: inline-block;">
                                    <asp:UpdatePanel ID="upPerson" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnUpdateEmergency" runat="server" Text="Update" CssClass="btn btn-danger"
                                                OnClick="btnUpdateEmergency_Click" OnClientClick="return validateEmergncyubmit();" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlCn1State" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkEditEmergClose" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnEditEmergCancel" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlCn2State" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlCn3State" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:Button ID="btnEditEmergCancel" CssClass="btn" runat="server" Text="Cancel" OnClick="btnEditEmergCancel_Click" />
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--Edit Emergency contact details End-->
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
    <cc1:ModalPopupExtender ID="mdlResetPasscode" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkResetPasscodeClose" TargetControlID="hdnResetPasscode" PopupControlID="Resetpasscodepopup">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnResetPasscode" runat="server" />
    <div id="Resetpasscodepopup" runat="server" class="popContent" style="width: 400px;
        display: none">
        <h2>
            <asp:UpdatePanel ID="UpdatePanelreset" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblResetPasscodeName" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkResetPasscodeClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        New passcode<span class="must">*</span>
                        <br />
                        <span style="font-size: 10px; color: GrayText">(Maximum 10 characters)</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResetNewPasscode" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm passcode<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResetConfirmPasscode" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="ResetUpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnResetPassCode" runat="server" Text="Update" CssClass="btn btn-danger"
                                        OnClientClick="return validResetPasscode();" OnClick="btnResetPassCode_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnResetCancelPasscode" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="btnResetCancelPasscode" runat="server" Text="Cancel" CssClass="btn"
                            OnClick="btnResetCancelPasscode_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="mdlResetPassword" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkResetPasswordClose" TargetControlID="hdnResetPassword" PopupControlID="ResetPasswordpopup">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnResetPassword" runat="server" />
    <div id="ResetPasswordpopup" runat="server" class="popContent" style="width: 400px;
        display: none">
        <h2>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblResetPasswordName" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkResetPasswordClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        New Password<span class="must">*</span>
                        <br />
                        <span style="font-size: 10px; color: GrayText">(Maximum 10 characters)</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResetNewPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm Password<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResetConfirmPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnResetPassword" runat="server" Text="Update" CssClass="btn btn-danger"
                                        OnClientClick="return validResetPassword();" OnClick="btnResetPassword_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnResetCancelPassword" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="btnResetCancelPassword" runat="server" Text="Cancel" CssClass="btn"
                            OnClick="btnResetCancelPassword_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>

<script type="text/javascript">
  
   function validResetPasscode()
   {
    debugger
          var valid=true;


         
           
         if (document.getElementById('txtResetNewPasscode').value=="") {
               alert("Please enter new passcode.");
               valid = false;
               document.getElementById("txtResetNewPasscode").value = "";
               document.getElementById("txtResetNewPasscode").focus();
           }

          else if (document.getElementById('txtResetNewPasscode').value.trim().length < 1) {
               alert("Please enter valid new passcode.");
               valid = false;
               document.getElementById("txtResetNewPasscode").value = "";
               document.getElementById("txtResetNewPasscode").focus();
           }

        else if(document.getElementById('txtResetConfirmPasscode').value.trim().length < 1)
           {
               alert("Please enter confirm passcode .");               
               valid=false;
                document.getElementById("txtResetConfirmPasscode").value="";
               document.getElementById("txtResetConfirmPasscode").focus();
           }
        else if (document.getElementById('txtResetConfirmPasscode').value.trim()!=document.getElementById('txtResetNewPasscode').value.trim())
           {
               alert("New passcode and confirm passcode should be match .");               
               valid=false;
               document.getElementById("txtResetConfirmPasscode").value="";
                document.getElementById("txtResetNewPasscode").value="";
               document.getElementById("txtNewPasscode").focus();
           }
  
           return valid;
   }
   
   
     function validResetPassword()
   {
    debugger
          var valid=true;
  
         if (document.getElementById('txtResetNewPassword').value=="") {
               alert("Please enter new password.");
               valid = false;
               document.getElementById("txtResetNewPassword").value = "";
               document.getElementById("txtResetNewPassword").focus();
           }

          else if (document.getElementById('txtResetNewPassword').value.trim().length < 1) {
               alert("Please enter valid new password.");
               valid = false;
               document.getElementById("txtResetNewPassword").value = "";
               document.getElementById("txtResetNewPassword").focus();
           }

        else if(document.getElementById('txtResetConfirmPassword').value.trim().length < 1)
           {
               alert("Please enter confirm password .");               
               valid=false;
                document.getElementById("txtResetConfirmPassword").value="";
               document.getElementById("txtResetConfirmPassword").focus();
           }
        else if (document.getElementById('txtResetConfirmPassword').value.trim()!=document.getElementById('txtResetNewPassword').value.trim())
           {
               alert("New passcode and confirm password should be match .");               
               valid=false;
               document.getElementById("txtResetConfirmPassword").value="";
                document.getElementById("txtResetNewPassword").value="";
               document.getElementById("txtNewPassword").focus();
           }
  
           return valid;
   }
   
  
  
</script>

</html>
