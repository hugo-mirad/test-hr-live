<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveApprovalManagement.aspx.cs"
    Inherits="Attendance.LeaveApprovalManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="css/reset.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/UI.css" />
    <link rel="stylesheet" href="css/inputs.css" type="text/css" />
    <link href="css/admin.css" rel="stylesheet" type="text/css" />
     <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="js/jquery.tools.min.js" type="text/javascript"></script>
    
     <script type="text/javascript">
      $(window).load(function(){
       $('[rel=tooltip]').tooltip();
      });
     
     function pageLoad()
     {
       $('[rel=tooltip]').tooltip();
     }
     
     </script>
     
     <style type="text/css">
      .tooltip {
        display: none;
        background: rgba(0, 0, 0, 0) url(images/black_arrow_big.png);
        font-size: 12px;
        height: 167px;
        width: 320px;
        padding: 25px;
        color: #EEE;
        }
     </style>
     
    <title>Untitled Page</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="headder">
        <a href="#" class="logo">
            <asp:Label ID="comanyname" runat="server" ForeColor="White"></asp:Label>
            <asp:Label ID="lblLocation" runat="server"></asp:Label>
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
                                            <asp:LinkButton runat="server" ID="LinkButton1" Text="Leave Approval Management" PostBackUrl="LeaveApprovalManagement.aspx"></asp:LinkButton>
                                        </li> 
                                        <li>
                                            <asp:UpdatePanel ID="ppp" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkChangepwd" Text="Change Password" OnClick="lnkChangepwd_Click"></asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </li>
                                        <li>
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkChangePasscode" Text="Change Passcode" OnClick="lnkChangePasscode_Click"></asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </li>
                                       <div style="display:none">
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkLeaveApproval" Text="Leave Approval Management"
                                                PostBackUrl="LeaveApprovalManagement.aspx"></asp:LinkButton>
                                                
                                        </li>
                                        </div>
                                    </ul>
                                </li>
                            </ul>
                            <!-- Show Clock Start  -->
                            <div class="tDate">
                                <asp:Label ID="lblDate2" runat="server" Style="display: none"></asp:Label>
                                <asp:HiddenField ID="hdnTodaydt" runat="server" />
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
                            <img src="Photos/defaultUSer.jpg" class="topPic" runat="server" id="Photo" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <h2 class="pageHeadding">
        Leave Approval management
    </h2>
    <div style="display: inline-block; margin-left: 10px;">
        <asp:UpdatePanel ID="upSelect" runat="server">
            <ContentTemplate>
                <b>Status </b>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlSelect" runat="server" Width="83px" Height="23px" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged"
                    AppendDataBoundItems="true" AutoPostBack="true">
                    <asp:ListItem Value="0">All</asp:ListItem>
                </asp:DropDownList>
                
               
                <asp:Button ID="lnkUpdate" runat="server" Text="Update" CssClass="btn btn-danger w996"  style="margin-left: 815px" OnClientClick="return validPop();"
            OnClick="lnkUpdate_Click"></asp:Button>
            
             <div style="display:none">
                <input type="button" id="lnkUpdate2" value="Update" CssClass="btn btn-danger w996" /> </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
    <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="upSelect"
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
    
      <asp:UpdateProgress ID="up189" runat="server" AssociatedUpdatePanelID="upLeaveUpdate111"
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
      <div style="margin-bottom: 6px;">
            <div style="display: inline-block; width: 1007px">
                <asp:UpdatePanel ID="upbtns" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnPrevious" runat="server" Text="Previous" 
                            CssClass="btn btn-danger btn-small" onclick="btnPrevious_Click"
                             />&nbsp;
                        <asp:Button ID="btnCurrent" runat="server" Text="Current" 
                            CssClass="btn btn-danger btn-small" onclick="btnCurrent_Click"
                           />&nbsp;
                        <asp:Button ID="btnNext" runat="server" Text="Next" 
                            CssClass="btn btn-danger btn-small" onclick="btnNext_Click"
                            />&nbsp;
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    
    <div>
        <asp:UpdatePanel ID="upgrd" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblTotal" runat="server" Style="font-size: 11px; font-weight: bold;
                        margin-left: 10px;"></asp:Label>
                    &nbsp;
                </div>
                <asp:HiddenField ID="hdnChkRecords" runat="server" />
                <input style="width: 91px" id="txthdnSortOrder" type="hidden" runat="server" enableviewstate="true" />
                <input style="width: 40px" id="txthdnSortColumnId" type="hidden" runat="server" enableviewstate="true" />
               
                
                <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="false" CssClass="table1"
                    Style="width: 990px;" OnRowCommand="grdUsers_RowCommand" OnRowDataBound="grdUsers_RowDataBound"
                    AllowSorting="True" OnSorting="grdUsers_Sorting">
                    <Columns>
                        <asp:TemplateField >
                        <HeaderTemplate>
                            <input id="selectAll" class="selectAll" type="checkbox" runat="server"  />
                        </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRecord" runat="server" LeaveID='<%#Eval("LeaveID")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="30" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>' CommandName="user"
                                    ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Firstname" HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("Firstname")+" "+Eval("lastname")%>'></asp:Label>
                                <asp:Label ID="lblEmpLastname" runat="server" Text='<%#Eval("lastname")%>' Visible="false"></asp:Label>
                                <asp:HiddenField ID="hdnPhoto" runat="server" Value='<%#Eval("photolink")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="150" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Fromdate" HeaderText="From date">
                            <ItemTemplate>
                                <asp:Label ID="lblFromdate" runat="server" Text='<%# Bind("Fromdate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Todate" HeaderText="To date">
                            <ItemTemplate>
                                <asp:Label ID="lblTodate" runat="server" Text='<%#Bind("Todate","{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Reason")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="ApprovedStatus" HeaderText="Approval status">
                            <ItemTemplate>
                                <asp:Label ID="lblApprovedStatus" runat="server" Text='<%#Eval("ApprovedStatus")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notes">
                            <ItemTemplate>
                                <asp:Label ID="lblNotes" runat="server" Text='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "Notes"))%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                 <h4 style="text-align:center"><asp:Label ID="lblError" runat="server" Visible="false" ></asp:Label></h4>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSelect" EventName="SelectedIndexChanged" />
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
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        Old password<asp:TextBox ID="txtOldpwd" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
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
    <!--Change passcode popup End-->
    <!--Leave popup start-->
    <cc1:ModalPopupExtender ID="mdlLeaveStatusUpdate" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkLeaveApprovCancel" TargetControlID="hdnLeaveApprove" PopupControlID="divLeaveApprove">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnLeaveApprove" runat="server" />
    <div id="divLeaveApprove" runat="server" class="popContent" style="width: 400px;
        display: none">
        <h2>
            Leave Approve status <span class="close">
                <asp:LinkButton ID="lnkLeaveApprovCancel" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td width"30px;">
                        Approve Status<span class="must">*</span>
                    </td>
                    <td>
                        <div style="display: inline-block; margin-left: 10px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlLeaveApprove" runat="server" 
                                        OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged" AppendDataBoundItems="true"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td width"30px;">
                        Notes
                    </td>
                     <td>
                        <asp:TextBox ID="txtLeaveNotes" runat="server" MaxLength="250" TextMode="MultiLine" Rows="5" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="upLeaveUpdate111" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnLeeaveApproveUpdate" runat="server" Text="Update" CssClass="btn btn-danger"
                                        OnClientClick="return validLeaveApprove();" 
                                        onclick="btnLeeaveApproveUpdate_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <input type="button" id="btnApproveCancel" value="Cancel" class="btn" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--Leave popup end-->
    </form>
</body>

<script type="text/javascript">


$(function(){

   $('#btnApproveCancel').click(function()
   {
      $find('mdlLeaveStatusUpdate').hide();
   });
   
   
   
   $('#lnkUpdate2').live('click', function(){
         var str = '';
         var len = 0;         
        $('#grdUsers input[type=checkbox]').each(function(){
            if($(this).is(':checked')){
                str += $(this).attr('LeaveID')+',';
                len++;
            }
            $('#hdnChkRecords').val(str);
            
        })
        
        if(len>0){
            $find('mdlLeaveStatusUpdate').show();
        }else{
            alert('Please choose ');           
        }
   });
   
    
    $('.selectAll').live('change',function(){
         if($(this).is(':checked')){
           $('#grdUsers input[type=checkbox]:not(.selectAll)').each(function(){
                $(this).attr('checked',true);
           })
        }else{
            $('#grdUsers input[type=checkbox]:not(.selectAll)').each(function(){
                $(this).removeAttr('checked');
           })
        }
    })
    

});



function validPop(){

     var str = '';
     var len = 0;  
      $('#grdUsers input[type=checkbox]:not(.selectAll)').each(function(){
        if($(this).is(':checked')){
            str += $(this).parent().attr('leaveid')+',';
            len++;
        }      
    })
    
    if(len>0){
         $('#hdnChkRecords').val(str);
        return true;
    }else{
         alert('Please choose ');  
        return false;
    }
    
   
    
}

function validLeaveApprove(){
    var valid=true;

    if($('#ddlLeaveApprove').val()==0){
        alert('Please select status');
        $('#ddlLeaveApprove').focus();
        valid=false;
    }
    return valid;
}




</script>



</html>
