<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolidayManagement.aspx.cs"
    Inherits="Attendance.HolidayManagement" %>

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

    <script src="js/jquery.tools.min.js" type="text/javascript"></script>

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
        .tooltip
        {
            display: none;
            background: rgba(0, 0, 0, 0) url(images/black_arrow_big.png);
            font-size: 12px;
            height: 167px;
            width: 320px;
            padding: 25px;
            color: #EEE;
        }
    </style>
    <link href="css/tipsy.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery.tipsy.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
      $(window).load(function(){
       $('[rel=tooltip]').tooltip();
        $('.tooltip2').tipsy({html: true, gravity:'sw' });
      });
     
     
     
     $(function(){
     
     
  
          $('.btnDefaultCancel').click(function(){
         
            $find('mdlDefaultmgmt').hide();
          });
       
         $('.tooltip2').tipsy({html: true, gravity:'sw' });
            $('.popupHolder2').css('z-index','100002')
            $('.popupContent2').css('z-index','100003')
     
     
            $('#btnCancel').click(function(){            
             $find('mdlHoliday').hide();
            });
            
          
              $('#btnSelectOK').click(function(){   
              var str = '';
               var len = 0;         
               
              var rowsCount = $("#grdSelectEmp tr").length;
               if(rowsCount>0)
               {
                    $('#grdSelectEmp input[type=checkbox]:not(.selectAll)').each(function(){
                     if($(this).is(':checked')){
                           str += $(this).parent().attr('leaveid')+',';
                           len++;
                        }      
                     })
                    if(len>0){
                         $('#hdnChkRecords').val(str);
                         $find('mdlEmpSelect').hide(); 
                    }else{
                         alert('Please choose ');  
                        return false;
                    }
                }
                else
                {
                 $find('mdlEmpSelect').hide(); 
                }
             });
            
            $('.selectAll').live('change',function(){
                 if($(this).is(':checked')){
                       $('#grdSelectEmp input[type=checkbox]:not(.selectAll)').each(function(){
                              $(this).attr('checked',true);
                    })
                }else{
                    $('#grdSelectEmp input[type=checkbox]:not(.selectAll)').each(function(){
                        $(this).removeAttr('checked');
                   })
                }
             })
             //rdAll rdSelected
             
             $('#rdWorkingday').live('change',function(e){
                if($(this).is(':checked'))
                {
                 $('#trHol').hide();
                }
                else{
                $('#trHol').show();
                }
             
             })
              $('#rdHoliday').live('change',function(e){
                if($(this).is(':checked'))
                {
                 $('#trHol').show();
                }
                else{
                $('#trHol').hide();
                }
             
             })
             
           
            
             $('#rdSelected').live('change',function(e){
                e.preventDefault();
                e.stopPropagation(true);
               if($('#ddlPopLoc').val()=='Select'){
                    alert('Please select location');
                    return false;
               }else if($('#ddlPopDept').val()=='Select'){
                    alert('Please select department');
                    return false;
               }else{
                    return true;
               }               
             })
     
     });
      function pageLoad(){
       $('[rel=tooltip]').tooltip();
        $('.tooltip2').tipsy({html: true, gravity:'sw' });
       if($('#scrollBlock table tr').length > 5){
            $('#scrollBlock').addClass('scrollBlock')
       }else{
            $('#scrollBlock').removeClass('scrollBlock')
       }
       
     }
      function Holidaypop(e){
      debugger
             $this = e;
             
             if($this.attr('isHoliday')!=null)
             {
              if($this.attr('isHoliday')=="true")
              {
                $("#rdHoliday").prop("checked",true);
                $("#rdWorkingday").prop("checked",false);
                $('#trHol').show();
              }
              else
              {
                 $("#rdWorkingday").prop("checked",true);
                 $("#rdHoliday").prop("checked",false);
                  $('#trHol').hide();
              }
             }
             else
             {
                  $("#rdWorkingday").prop("checked",true);
                  $("#rdHoliday").prop("checked",false);
                  $('#trHol').hide();
             }
                       
             
             $('.lblHDay').text($this.attr('currentdate'));     
             $('#hdnHolidayDt').val($this.attr('currentdate'));      
           
             $("#rdAll").prop("checked",false);
             $("#rdSelected").prop("checked",false) ;
             $('#ddlPopLoc').val('Select');
             $('#ddlPopDept').val('Select');
             if($this.attr('Hname')!=null)
             {
              $('#txtHolidayName').val($this.attr('Hname'));
             }
             else
             {
              $('#txtHolidayName').val('');
             }
             
              $find('mdlHoliday').show();              
              hideSpinner();  
              return false;      
         }
    function ValidHol()
    {
    debugger
        if(($("#rdHoliday").prop("checked")==false)&&($("#rdWorkingday").prop("checked")==false))
        {
         alert('Please choose holiday or working day');
         $('#rdHoliday').focus();                    
          return false;
        }
    
     
         if($('#ddlPopLoc').val()=='Select'){
          alert('Please select location');
          $('#ddlPopLoc').focus();
          return false;
         }
         if($('#ddlPopDept').val()=='Select'){
           alert('Please select department');
           $('#ddlPopDept').focus();
           return false;
          }
    
          
         if(($("#rdAll").prop("checked")==false)&&($("#rdSelected").prop("checked")==false))
        {
         alert('Please choose employee(s)');
         $('#rdAll').focus();
          return false;
        }
        
        if($('#trHol').css('display') != 'none')
        {
           if($('#txtHolidayName').val().length<=0)
           { 
            alert('Please enter holiday name');
            $('#txtHolidayName').focus();
             return false;
           }
       }
        return true;
    }
    
   
   
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updd"
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
    
    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upDefaultmgmt"
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
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upbtns"
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
    <%--  <div id="spinner">
        <h4>
            <div>
                Processing
                <img src="images/loading.gif" />
            </div>
        </h4>
    </div>--%>
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
                %
            </table>
        </div>
    </div>
    <!--Change password popup End-->
    <h2 class="pageHeadding">
        Holiday management
    </h2>
    <asp:UpdatePanel ID="updd" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblGrdLocaton" runat="server" Text="Location"></asp:Label></b>&nbsp;&nbsp;
            <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" Style="width: 70px;">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="margin-bottom: 6px;">
        <div style="display: inline-block; width: 1007px">
            <asp:UpdatePanel ID="upbtns" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="btn btn-danger btn-small"
                        OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnCurrent" runat="server" Text="Current" CssClass="btn btn-danger btn-small"
                        OnClick="btnCurrent_Click" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-danger btn-small"
                        OnClick="btnNext_Click" />&nbsp;
                    <div style="float:right">
                    <asp:Button ID="btnDefaultMgmt" runat="server" Text="Default holiday management" 
                            CssClass="btn-link" onclick="btnDefaultMgmt_Click"/></div>
                    <%-- <input type="button" name="btnDefaultMgmt" runat="server" value="Default holiday management" class="btn-link"  />--%>  
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <table style="width: 618px; margin: 20px auto;">
        <tr>
            <td style="vertical-align: top; width: 400px">
                <asp:UpdatePanel ID="uphol" runat="server">
                    <ContentTemplate>
                        <div class="picker">
                            <div class="header">
                                <asp:Label ID="lblMonth" runat="server" CssClass="current"></asp:Label></div>
                            <asp:GridView ID="grdholiday" runat="server" AutoGenerateColumns="false" CssClass="table1 tblCal"
                                OnRowDataBound="grdholiday_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sun">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblSun" runat="server" Text='<%#Eval("Sunday")%>' OnClientClick="return Holidaypop($(this))"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mon">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblMon" runat="server" Text='<%#Eval("Monday")%>' OnClientClick="return Holidaypop($(this))"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tue">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblTue" runat="server" Text='<%#Eval("Tuesday")%>' OnClientClick="return Holidaypop($(this))"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Wed">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblWed" runat="server" Text='<%#Eval("Wednesday")%>' OnClientClick="return Holidaypop($(this))"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Thu">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblThu" runat="server" Text='<%#Eval("Thursday")%>' OnClientClick="return Holidaypop($(this))"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fri">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblFri" runat="server" Text='<%#Eval("Friday")%>' OnClientClick="return Holidaypop($(this))"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sat">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblSat" runat="server" Text='<%#Eval("Saturday")%>' OnClientClick="return Holidaypop($(this))"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="vertical-align: top">
                <ul class="colors">
                    <li><span class="colorCodes" style="background: #F0F8FF">&nbsp;</span>Working Day</li>
                    <li><span class="colorCodes" style="background: #4FC1E9">&nbsp;</span>Holiday</li>
                    <li><span class="colorCodes" style="background: #F6F8FA">&nbsp;</span>Sunday</li>
                    <li><span class="colorCodes" style="background: #FFF9AE;">&nbsp;</span>Current Day</li>
                </ul>
            </td>
        </tr>
    </table>
    <cc1:ModalPopupExtender ID="mdlHoliday" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkHolidayClose" TargetControlID="hdnHoliday" PopupControlID="dvHoliday">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnHoliday" runat="server" />
    <div id="dvHoliday" runat="server" class="popContent" style="width: 400px; display: none">
        <h2>
            <span class="lblHDay"></span>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnHolidayDt" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkHolidayClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 95%; margin: 20px 5px; border-collapse: collapse;" class="holidayCalPo">
                <tr>
                    <td style="width: 95px;">
                        Declare as <span class="must"> *</span>
                    </td>
                    <td>
                        <asp:RadioButton ID="rdHoliday" runat="server" GroupName="Holiday" />&nbsp;Holiday
                        &nbsp; &nbsp; &nbsp;
                        <asp:RadioButton ID="rdWorkingday" runat="server" GroupName="Holiday" />&nbsp;Working
                        day
                    </td>
                </tr>
                <tr>
                    <td>
                        Location<span class="must"> *</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="upPL" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlPopLoc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPopLoc_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        Department<span class="must"> *</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlPopDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPopDept_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        Employee(s)<span class="must"> *</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="uprd" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rdAll" runat="server" GroupName="Emp" AutoPostBack="true" />&nbsp;ALL
                                &nbsp; &nbsp; &nbsp;
                                <asp:RadioButton ID="rdSelected" runat="server" GroupName="Emp" AutoPostBack="true"
                                    OnCheckedChanged="rdSelected_CheckedChanged" />&nbsp;Selected
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="trHol" runat="server">
                    <td>
                        Holiday name <span class="must"> *</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHolidayName" runat="server" MaxLength="250" Width="220"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block;">
                            <asp:UpdatePanel ID="upApply" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-danger btn-small"
                                        OnClick="btnApply_Click" OnClientClick="return ValidHol();" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCancel" runat="server" class="btn btn-small" value="Cancel" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hdnChkRecords" runat="server" />
    <cc1:ModalPopupExtender ID="mdlEmpSelect" runat="server" BackgroundCssClass="popupHolder2"
        CancelControlID="lnkSelectCancel" TargetControlID="hdnSelect" PopupControlID="dvEmpSelect">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnSelect" runat="server" />
    <div id="dvEmpSelect" runat="server" class="popContent popupContent2" style="width: 550px;
        display: none">
        <h2>
            Employee(s) <span class="close">
                <asp:LinkButton ID="lnkSelectCancel" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner" id="scrollBlock" runat="server">
            <asp:UpdatePanel ID="upSelect" runat="server">
                <ContentTemplate>
                    <div style="text-align: center; font-size: small; color: Red; height: 25px; margin-top: 41px;"
                        id="dvSelectError" runat="server">
                        <asp:Label ID="lblSelectError" runat="server"></asp:Label>
                    </div>
                    <asp:GridView ID="grdSelectEmp" runat="server" AutoGenerateColumns="false" CssClass="table1"
                        Style="width: 97%;">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="selectAll" class="selectAll" type="checkbox" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRecord" runat="server" LeaveID='<%#Eval("UserID")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="30" />
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Firstname" HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("empName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepart" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="130" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <input type="button" id="btnSelectOK" class="btn btn-danger btn-small" style="margin: 10px 20px 10px 0;
            float: right;" value="OK" runat="server" />&nbsp;
        <br />
    </div>
    
    
    
    
    
     <cc1:ModalPopupExtender ID="mdlDefaultmgmt" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkDefault" TargetControlID="hdnDefault" PopupControlID="dvDefault">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnDefault" runat="server" />
    <div id="dvDefault" runat="server" class="popContent" style="width: 400px; display: none">
        <h2>
            Default holiday management
            <span class="close">
                <asp:LinkButton ID="lnkDefault" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 95%; margin: 20px 5px; border-collapse: collapse;" class="holidayCalPo">
                <tr>
                    <td style="width: 95px;">
                        Default holiday <span class="must"> *</span>
                    </td>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                        <asp:DropDownList ID="ddlDay" runat="server" AutoPostBack="true" >
                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                        <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                        </asp:DropDownList>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        From <span class="must"> *</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlFromYear" runat="server" AutoPostBack="true" >
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        Upto<span class="must"> *</span>
                    </td>
                  
                    <td>
                          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlToYear" runat="server" AutoPostBack="true" >
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
                 <tr>
                    <td>
                        Location<span class="must"> *</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDefaultlocation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPopLoc_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        Department<span class="must"> *</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDefaultDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPopDept_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
                
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block;">
                            <asp:UpdatePanel ID="upDefaultmgmt" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Apply" CssClass="btn btn-danger btn-small"
                                        OnClientClick="return ValidDefault();" onclick="btnSubmit_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        &nbsp;&nbsp;
                        <input type="button" name="btnDefaultCancel" runat="server" class="btnDefaultCancel btn btn-small" value="Cancel" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    </form>
    
    <script type="text/javascript" language="javascript">
    function  ValidDefault()
    {
    debugger
      if($('#ddlDay').val()=='Select')
      {
       alert('Please select default holiday'); 
       $('#ddlDay').focus();
       return false;
      }
      if($('#ddlDefaultlocation').val()=='Select')
      {
       alert('Please select location'); 
       $('#ddlDefaultlocation').focus();
       
        return false;
      }
      if($('#ddlDefaultDept').val()=='Select')
      {
       alert('Please select department'); 
       $('#ddlDefaultDept').focus();
        return false;
      }
       return true;
    }
    
    </script>
    
    
</body>
</html>
