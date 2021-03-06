﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayRoll.aspx.cs" Inherits="Attendance.PayRoll"
    EnableEventValidation="false" %>

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

    <script src="js/jquery-ui.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/jquery-ui-timepicker-addon.js"></script>

    <script type="text/javascript" src="js/jquery-ui-sliderAccess.js"></script>

    <style type="text/css">
        .table1 td
        {
            border: #ccc 1px dotted;
            padding: 0px 4px;
            white-space: nowrap;
        }
        .table1 th, .table1 tr:first-child td
        {
            padding: 2px 4px;
            vertical-align: middle;
            background: #ccc;
            text-align: center;
        }
        .table1
        {
            font-size: 10px;
        }
        .table1 tr:first-child
        {
            font-weight: bold;
        }
        .table1
        {
            border: #999 1px solid;
            border-collapse: collapse;
        }
        .table1 td span
        {
            white-space: nowrap;
        }
        .bL, th.bL, td.bL
        {
            border-left: #888 2px solid;
        }
        .bT, th.bT, td.bT
        {
            border-top: #888 2px solid;
        }
        .bR, th.bR, td.bR
        {
            border-right: #888 2px solid;
        }
        .bB, th.bB, td.bB
        {
            border-bottom: #888 2px solid;
        }
    </style>

    <script type="text/javascript" language="javascript">
   
    function isNumberKeyWithDot(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;

            return true;
        }
    var curentRow = 0;
    var currentInput = '';
    
    $(window).load(function () {
        $('#lnkDwnloadPDF').css('visibility','hidden');
        $('#spinner').hide();
     
        $('#txtFromDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });

        $('#txtToDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
        
    });
        
        
         function showspinner()
        {
         $('#spinner').show();
         return true;
        }
        
        function hidespinner()
        {
         $('#spinner').hide();
         return true;
        }
        
        function addEditLabelCss()
        {
     
             $('.grdPayRollIndia .lblBonus').each(function(){
                $(this).next('input').show();
                $(this).addClass('Editlabel');
                $(this).bind();
            });            
     
            $('.grdPayRollIndia .lblIncentives').each(function(){
                $(this).next('input').show();
                $(this).addClass('Editlabel');
                $(this).bind();
            }); 
            
             $('.grdPayRollIndia .lblPrevUnpaid').each(function(){
                $(this).next('input').show();
                $(this).addClass('Editlabel');
                $(this).bind();
            }); 
            
             $('.grdPayRollIndia .lblAdvancePaid').each(function(){
                $(this).next('input').show();
                $(this).addClass('Editlabel');
                $(this).bind();
            }); 
            
            
             $('.grdPayRollIndia .lblExpenses').each(function(){
                $(this).next('input').show();
                $(this).addClass('Editlabel');
                $(this).bind();
            }); 
            
            
             $('.grdPayRollIndia .lblLoanDeduct').each(function(){
                $(this).next('input').show();
                $(this).addClass('Editlabel');
                $(this).bind();
            }); 
     
     
         
        }
        
        function delEditLabelCss()
        {

            $('.grdPayRollIndia .Editlabel').each(function(){
                $(this).next('input').hide();
                $(this).show().removeClass('Editlabel').addClass('EditlabelBk');
                $(this).unbind('click');
            });        
         
        }
        
        function pageLoad()
        {
            $('#spinner').hide();    
            
            
            //console.log($('#hdnFreeze').val());
            
            
             $('#txtFromDate').datepicker({
                dateFormat: "mm/dd/yy"
                //timeFormat:"hh:mm tt"      
            });

            $('#txtToDate').datepicker({
                dateFormat: "mm/dd/yy"
                //timeFormat:"hh:mm tt"      
            });
            
            
            if($('#hdnFreeze').val() == "false"){
            
                //addEditLabelCss();   
                
                
                $('.grdPayRollIndia .EditlabelBk').each(function(){
                    $(this).next('input').show();
                    $(this).hide().addClass('Editlabel').removeClass('EditlabelBk')
                   // $(this).bind('click');
                }); 
                
            
                $('.Editlabel').click(function(){
                    $(this).hide();
                    $(this).next().show().focus();
                })
                
                $('.editInputBlur').on('focus', function(){
                    $(this).attr('prevVal' ,$.trim($(this).val()));
                });
                
                $('.editInputBlur').blur(function(){
                    curentRow = $(this).parent().parent().index();
                   // console.log(curentRow)
                   
                   var sal = parseInt($('#grdPayRollIndia tr:eq('+curentRow+') .sal').text());
                   var total1 = 0;
                   var total2 = 0;
                   $('#grdPayRollIndia tr:eq('+curentRow+') input.add').each(function(){
                        if($.trim($(this).val()) != ''){
                            total1 += parseInt($.trim($(this).val()));
                        }
                   });
                   
                   $('#grdPayRollIndia tr:eq('+curentRow+') input.sub').each(function(){
                        if($.trim($(this).val()) != ''){
                            total2 += parseInt($.trim($(this).val()));
                        }
                   });
                   
                   
                   var gTotal = (sal+total1)-total2
                   $('#grdPayRollIndia tr:eq('+curentRow+') .totalPay').text(gTotal);
                   
                   if( $(this).attr('prevVal') != $.trim($(this).val()) && $.trim($(this).val()) != '' ){
                        $('#dvInternalNotes #txtPopNotes').val( $.trim($(this).attr('notes')));
                        $('#dvInternalNotes #lblNotesName').text($('#grdPayRollIndia tr:eq('+curentRow+') td:eq(1) span').text());
                        $find('mdlInternalNotes').show();
                        currentInput = $(this).attr('id');
                   }
                   
                   if($.trim($(this).val()) == ''){
                        $(this).attr('prevVal','').attr('notes','')
                   }
                  })
              
              }else{
                    delEditLabelCss();
              }
            
        }
        function linkdis()
        {
          
           $('#lnkDwnloadPDF').css('visibility','visible');
           return true;
        }
        
        
        function validateDate()
        {
        
        var valid=true;
          var Stdate=new Date($('#txtFromDate').val());
          var EndDate=new Date($('#txtToDate').val());
          if(Stdate>EndDate)
          {
          
            alert("Todate should be greaterthan from date");
            $('#txtToDate').val('');
            $('#txtToDate').focus();
            valid=false;
          }
          return valid;
          
        }
        
        function validFinal(){
         var confm=confirm('Once get finalized,further modifications will not allow');
         if(confm)
         {
         return true;
         }
         return false;
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
                                            <asp:LinkButton ID="lnkPayroll" runat="server" Text="Payroll Report" PostBackUrl="PayRoll.aspx"></asp:LinkButton></li>
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
                                        <li style="display:none;">
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
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        Old password<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOldpwd" runat="server" MaxLength="8" TextMode="Password"></asp:TextBox>
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
        </div>
    </div>
    <!--Change password popup End-->
    <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="up2" DisplayAfter="0">
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
    <h2 class="pageHeadding">
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblPayroll" runat="server" Text="Payroll report"></asp:Label>
                <br />
                <asp:Label ID="lblWeekPayrollReport" CssClass="lbl" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblFreeze" runat="server" ForeColor="Red" Font-Bold="false" Style="font-size: 10px;"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </h2>
    <div style="display: inline-block; margin-left: 20px;">
        <div style="display: inline-block;">
            <asp:UpdatePanel ID="up2" runat="server">
                <ContentTemplate>
                    <b>From date</b> &nbsp;<asp:TextBox ID="txtFromDate" runat="server" Width="150"></asp:TextBox>
                    &nbsp;&nbsp; <b>To date</b> &nbsp;<asp:TextBox ID="txtToDate" runat="server" Width="150"></asp:TextBox>
                    <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" OnClientClick="return validateDate();"
                        CssClass="btn btn-danger btn-small right" />
                    &nbsp; &nbsp;
                    <asp:Label ID="lblGrdLocaton" runat="server" Text="Location"></asp:Label></b>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="83px"
                        Height="23px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                       &nbsp;&nbsp; <b>
                <asp:Label ID="lblShift" runat="server" Text="Shift"></asp:Label></b>&nbsp;&nbsp;
            <asp:DropDownList ID="ddlShift" runat="server" AutoPostBack="true" 
               Style="width: 70px;" onselectedindexchanged="ddlShift_SelectedIndexChanged">
             </asp:DropDownList>
                 
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="display:inline-block;">
            <asp:UpdatePanel ID="upbnt" runat="server">
                <ContentTemplate>
                <div style="display:none;">
                    <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" CssClass="btn btn-small btn-group" />
                </div>
                    <asp:Button ID="btnPDF" runat="server" Text="DownLoadToPDF" CssClass="btn btn-small btn-success"
                        OnClick="btnPDF_Click1" OnClientClick="return linkdis();"></asp:Button>
                    <asp:Button ID="btnDoc" runat="server" Text="DownLoadToWord" CssClass="btn btn-small btn-success"
                        OnClick="btnDoc_Click1" OnClientClick="return linkdis();"></asp:Button>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="dvpayrollreport" runat="server" style="font-size: 12px; font-family: Arial;
        color: #333;">
        <table cellpadding="30" cellspacing="0">
            <tr>
                <td style="padding: 10px">
                    <asp:UpdatePanel ID="up1" runat="server">
                        <ContentTemplate>
                        
                          <div style="font-size: 20px;font-weight: bold;padding-top: 150px;text-align: center;" runat="server" id="dvNodata">
            <asp:Label ID="lblGrdNodata" runat="server"></asp:Label></div>
                            <asp:HiddenField ID="hdnPayrollPdf" runat="server" />
                            <div>
                                <asp:Label ID="lblTotal" runat="server" Style="margin-left: 10px;"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblReportDate" runat="server" Style="float: right; margin-right: 92px;"></asp:Label>
                            </div>
                            <asp:GridView runat="server" AutoGenerateColumns="false" ID="grdPayRoll" CssClass="table1"
                                OnRowDataBound="grdPayRoll_RowDataBound" BorderWidth="1" CellPadding="0" CellSpacing="0"
                                Width="800px">
                                <Columns>
                                    <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                                      <ItemTemplate>
                                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%#Eval("empid")%>' />
                                            <asp:HiddenField ID="hdnEmpuserid" runat="server" Value='<%#Eval("UserID")%>' />
                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Firstname" HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("FirstName")%>'></asp:Label>
                                            <asp:Label ID="lblEmpLastname" runat="server" Text='<%#Eval("LastName")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="JoiningDate" HeaderText="StartDt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartedDate" runat="server" Text='<%# Bind("StartDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TerminatedDt" HeaderText="TermDt">
                                           <ItemTemplate>
                                            <asp:Label ID="lblTerminatedDate" runat="server" Text='<%#Bind("TermDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="DeptName" HeaderText="Department">
                                              <ItemTemplate>
                                            <asp:Label ID="lblDept" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="EmployeeType" HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeetype" runat="server" Text='<%#Eval("MasterEmpType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Location" HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("LocationName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TotalHours" HeaderText="Hrs">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("TotalHours")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="SSN" HeaderText="SSN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrdSSN" runat="server" Text='<%#Eval("SSN")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="MaritalStatus" HeaderText="FilingStatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFilingStatus" runat="server" Text='<%#Eval("MaritalStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Isnew" HeaderText="IsNew">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsNew" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="IsChanges" HeaderText="IsChanges">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsChanges" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hdnFreeze" runat="server" Value="false" />
                            
                            <asp:GridView runat="server" AutoGenerateColumns="false" ID="grdPayRollIndia" CssClass="table1 grdPayRollIndia"
                                OnRowDataBound="grdPayRollIndia_RowDataBound" BorderWidth="1" CellPadding="0"
                                CellSpacing="0" Width="800px">
                                <Columns>
                                 <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                                      <ItemTemplate>
                                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%#Eval("empid")%>' />
                                            <asp:HiddenField ID="hdnEmpuserid" runat="server" Value='<%#Eval("UserID")%>' />
                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Firstname" HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("FirstName")+" "+ Eval("LastName")%>'></asp:Label>
                                            <asp:Label ID="lblEmpLastname" runat="server" Text='<%#Eval("LastName")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="JoiningDate" HeaderText="StartDt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartedDate" runat="server" Text='<%# Bind("StartDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TerminatedDt" HeaderText="TermDt">
                                           <ItemTemplate>
                                            <asp:Label ID="lblTerminatedDate" runat="server" Text='<%#Bind("TermDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="DeptName" HeaderText="Department">
                                              <ItemTemplate>
                                            <asp:Label ID="lblDept" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="EmployeeType" HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeetype" runat="server" Text='<%#Eval("MasterEmpType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Location" HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("LocationName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TotalHours" HeaderText="Hrs">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("TotalHours")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField SortExpression="MaritalStatus" HeaderText="FilingStatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFilingStatus" runat="server" Text='<%#Eval("MaritalStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Isnew" HeaderText="IsNew">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsNew" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="IsChanges" HeaderText="IsChanges">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsChanges" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                               </Columns>
                            </asp:GridView>
                            <table cellpadding="0" cellspacing="0" width="900">
                                <tr>
                                    <td style="height: 30px;">
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                   
                                        <b class="headding2" style="margin-bottom: 0; padding-bottom: 0">
                                            <asp:Label ID="lblNewEmp" runat="server" Style="padding: 10px;"></asp:Label></b>
                                        <asp:GridView ID="grdNewEmp" runat="server" AutoGenerateColumns="false" CssClass="table1"
                                            OnRowDataBound="grdNewEmp_RowDataBound" Width="700px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Emp ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("FirstName")%>'></asp:Label>
                                                        <asp:HiddenField ID="hdnLastname" runat="server" Value='<%#Eval("LastName")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpType" runat="server" Text='<%#Eval("MasterEmpType")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStart" runat="server" Text='<%#Eval("startdate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of birth">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBirthDate" runat="server" Text='<%# Bind("DateOfBirth", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deductions">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeductions" runat="server" Text='<%#Eval("Deductions")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address1")%>'></asp:Label>
                                                        <asp:HiddenField ID="hdnAddress2" runat="server" Value='<%#Eval("Address2")%>' />
                                                        <asp:HiddenField ID="hdnZip" runat="server" Value='<%#Eval("zip")%>' />
                                                        <asp:HiddenField ID="hdnState" runat="server" Value='<%#Eval("StateCode")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SSN">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSSN" runat="server" Text='<%#Eval("SSN")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="County">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCounty" runat="server" Text='<%#Eval("County")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px;">
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b class="headding2" style="margin-bottom: 0; padding-bottom: 0">
                                            <asp:Label ID="lblChanges" runat="server" Style="padding: 10px;"></asp:Label></b>
                                        <asp:Repeater ID="rpt1" runat="server" OnItemDataBound="rpt1_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="Agent1" style="margin-left: 10px;">
                                                    <h4>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("empname")%>'></asp:Label>&nbsp;&nbsp;
                                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpID")%>'></asp:Label>
                                                    </h4>
                                                    <table>
                                                        <tbody>
                                                            <asp:Repeater ID="repChld" runat="server" OnItemDataBound="repChld_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblPoint" runat="server"></asp:Label>
                                                                            <asp:Label ID="lblField" runat="server" Text='<%#Eval("FieldName")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblOldvalue" runat="server" Text='<%#Eval("OldValue")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblNewValue" runat="server" Text='<%#Eval("NewValue")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblChangedDt" runat="server" Text='<%#Bind("ChangeDate","{0:MM/dd/yyyy}") %>'
                                                                                Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <br />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGo" EventName="Click" />
                            
                            <asp:AsyncPostBackTrigger ControlID="btnPrint" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <!--Alert notes-->
    <cc1:ModalPopupExtender ID="mdlInternalNotes" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkInteralNotesClose" TargetControlID="hdnInternalNotes" PopupControlID="dvInternalNotes">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnInternalNotes" runat="server" />
    <div id="dvInternalNotes" runat="server" class="popContent" style="width: 350px;
        display: none">
        <h2>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblNotesName" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkINteralNotesClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td style="vertical-align: top; width: 90px;">
                        Internal notes
                        </td>
                        <td>
                        <asp:TextBox ID="txtPopNotes" runat="server" width="220" TextMode="MultiLine" Rows="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input type="button" id="btnDone" runat="server" value="Done" style="float: right;
                            margin: 5px;" />
                    </td>
                </tr>
               
            </table>
        </div>
    </div>
    <asp:UpdatePanel ID="upPayslip" runat="server">
        <ContentTemplate>
            <div>
                <asp:Repeater ID="rppayslip" runat="server" OnItemDataBound="rppayslip_ItemDataBound">
                    <ItemTemplate>
                        <div style="width: 595px; height: 842px;">
                            <h3 style="text-align: center">
                                Hugo Mirad Marketing Solutions(P) Ltd.</h3>
                            <br />
                            <br />
                            <div style="display: inline-block; width: 98%;">
                                <span style="float: left">Prepared:<asp:Label ID="lblPCurntDt" runat="server"></asp:Label>
                                </span><span style="float: right">Payroll Information:<asp:Label ID="lblPMonth" runat="server"
                                    Text="Jan'14"></asp:Label>
                                </span>
                            </div>
                            <br />
                            <table style="width: 99%">
                                <thead>
                                    <tr style="border-bottom: #999 2px solid;">
                                        <td colspan="4">
                                            <b>Employee Information</b>
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td style="width: 125px;">
                                            Name
                                        </td>
                                        <td style="width: 170px;">
                                            <b>
                                                <asp:Label runat="server" ID="lblpEmpName" Text='<%#Eval("PEmpname")%>'></asp:Label></b>
                                        </td>
                                        <td style="width: 125px;">
                                            Employee ID#
                                        </td>
                                        <td>
                                            <asp:Label ID="lblpEmpID" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Business Name
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPBusinessName" Text='<%#Eval("Empname")%>'></asp:Label>
                                        </td>
                                        <td>
                                            Date of Joining
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPDOJ" runat="server" Text='<%# Bind("Startdate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Project
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPProject"></asp:Label>
                                        </td>
                                        <td>
                                            Designation
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPDesg" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table style="width: 99%">
                                <thead>
                                    <tr style="border-bottom: #999 2px solid;">
                                        <td colspan="4">
                                            <b>Attendance Details</b>
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td style="width: 25%">
                                            Total Work Days
                                        </td>
                                        <td style="width: 25%">
                                            <b>
                                                <asp:Label runat="server" ID="lblPWkngDays" Text='<%#Eval("Workingdays")%>'></asp:Label></b>
                                        </td>
                                        <td>
                                            Days Present
                                        </td>
                                        <td style="width: 25%">
                                            <asp:Label ID="lblPPresent" runat="server" Text='<%#Eval("Present")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Off days(dates)
                                        </td>
                                        <td>
                                            Check local/online register
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Paid days off
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPPaidDaysUsed" runat="server" Text='<%#Eval("PaidLeavesUsed")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table style="width: 99%">
                                <thead>
                                    <tr style="border-bottom: #999 2px solid;">
                                        <td colspan="3">
                                            <b>Salary Details</b>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td style="width: 33%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 33%; text-align: right">
                                            Eligible Pay
                                        </td>
                                        <td style="width: 33%; text-align: right">
                                            Earned Pay
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Base
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPSalary" Text='<%#Eval("Salary")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblPCalSalary" runat="server" Text='<%#Eval("CalSalary")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Attendance Bonus
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPBonus" Text='<%#Eval("Bonus")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Transportation
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPTransport"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Food Allowance
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPFoodAllowance"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Incentives *
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPIncentives" Text='<%#Eval("Incentives")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Reimbursements **
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPReimburse"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            <b>Gross</b>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPTotalPay"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Professional Tax
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPPF" Text="100"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            <b>Net</b>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPNetPay"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table style="width: 99%">
                                <thead>
                                    <tr style="border-bottom: #999 2px solid;">
                                        <td colspan="2">
                                            <b>Summary</b>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td style="width: 49%">
                                            Current Earnings
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPCurrentEarnings"></asp:Label>
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Advance Paid
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPAdvancePaid" Text='<%#Eval("AdvancePaid")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Previously Unpaid
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPPrevUnpaid" Text='<%#Eval("PrevUnpaid")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Current Payments
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPCurrentpayments"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Remaining Balance
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="lblPRembal"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: #ccc 1px solid;">
                                        <td>
                                            Additional Incentives Paid during the month
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="Label4"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>

    <script type="text/javascript">
        $('#btnDone').click(function(){
            var text = $.trim($('#txtPopNotes').val());  
            
            if(text != ''){           
                /*
                var text2= $.trim($('#grdPayRollIndia tr:eq('+curentRow+') td:last-child input').val());
                if(text2 != ''){
                    $('#grdPayRollIndia tr:eq('+curentRow+') td:last-child input').val(text2+'<br>'+text);
                }else{
                    $('#grdPayRollIndia tr:eq('+curentRow+') td:last-child input').val(text);
                }
                */
                $('#'+currentInput).attr('notes', text);
            }
             $find('mdlInternalNotes').hide();  
            
         })
    </script>

</body>
</html>
