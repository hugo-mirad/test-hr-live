<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VariableSchdeulemgmt.aspx.cs"
    Inherits="Attendance.VariableSchdeulemgmt" %>

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

    <script type="text/javascript" language="javascript">
    $(function(){  
            $('.chkAll').live('change',function(){
                if($('#dvVariable').css('display')=="block")
                 {
                  if($(this).is(':checked')){
                   $('#grdVariable input[type=checkbox]:not(.chkAll)').each(function(){
                        $(this).attr('checked',true);
                   })
                   }else{
                    $('#grdVariable input[type=checkbox]:not(.chkAll)').each(function(){
                        $(this).removeAttr('checked');
                   })
                 }
                }
                else if($('#dvDefault').css('display')=="block"){
                  if($(this).is(':checked')){
                    $('#grdDefault input[type=checkbox]:not(.chkAll)').each(function(){
                        $(this).attr('checked',true);
                   })
                   }else{
                    $('#grdDefault input[type=checkbox]:not(.chkAll)').each(function(){
                        $(this).removeAttr('checked');
                   })
                  }
                   
                }
            })
    })
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <%-- <div id="spinner">
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
    <h2 class="pageHeadding">
        Variable schedule management
    </h2>
    <div style="display: inline-block;">
        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>
                <b>Schdeule type</b> &nbsp;<asp:DropDownList ID="ddlScheduleType" runat="server"
                    AutoPostBack="true" Width="83px" Height="23px" OnSelectedIndexChanged="ddlScheduleType_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Default" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Variable" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblGrdLocaton" runat="server" Text="Location"></asp:Label></b>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="83px"
                    Height="23px" AppendDataBoundItems="true">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="upgd" runat="server">
        <ContentTemplate>
            <div id="dvVariable" style="width: 90%" runat="server">
                <asp:Button ID="btnChangeVariable" runat="server" Text="Change variable schedule"
                    CssClass="btn btn-small btn-danger" Style="float: right; margin-right: 218px;" />
                <br />
                <br />
                <asp:GridView runat="server" AutoGenerateColumns="false" ID="grdVariable" CssClass="table1"
                    BorderWidth="1" CellPadding="0" CellSpacing="0" Width="800px">
                    <Columns>
                        <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnEmpuserid" runat="server" Value='<%#Eval("UserID")%>' />
                                <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Firstname" HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("FirstName")+" "+Eval("LastName")%>'></asp:Label>
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
                        <asp:TemplateField SortExpression="Schedule" HeaderText="Schedule">
                            <ItemTemplate>
                                <asp:Label ID="lblVarSchedule" runat="server" Text='<%#Eval("Schedule")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="LunchBreak" HeaderText="Lunch break">
                            <ItemTemplate>
                                <asp:Label ID="lblVarLunch" runat="server" Text='<%#Eval("LunchBreak")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Fromdate" HeaderText="From date">
                            <ItemTemplate>
                                <asp:Label ID="lblVarFrom" runat="server" Text='<%#Bind("VschFromDt", "{0:MM/dd/yyyy}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Todate" HeaderText="To date">
                            <ItemTemplate>
                                <asp:Label ID="lblVarTo" runat="server" Text='<%#Bind("VschToDt", "{0:MM/dd/yyyy}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="dvDefault" runat="server" style="display: none;">
                <asp:Button ID="btnAddVarSchedule" runat="server" Text="Add variable schedule" CssClass="btn btn-small btn-danger"
                    Style="float: right; margin-right: 218px;" />
                <br />
                <br />
                <asp:GridView runat="server" AutoGenerateColumns="false" ID="grdDefault" CssClass="table1"
                    BorderWidth="1" CellPadding="0" CellSpacing="0" Width="800px">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" id="chkAll" runat="server" name="chkAll" class="chkAll" />All
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnEmpuserid" runat="server" Value='<%#Eval("UserID")%>' />
                                <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Firstname" HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("FirstName")+" "+Eval("LastName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="JoiningDate" HeaderText="StartDt">
                            <ItemTemplate>
                                <asp:Label ID="lblStartedDate" runat="server" Text='<%#Bind("StartDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="TerminatedDt" HeaderText="TermDt">
                            <ItemTemplate>
                                <asp:Label ID="lblTerminatedDate" runat="server" Text='<%#Bind("TermDate","{0:MM/dd/yyyy}")%>'></asp:Label>
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
                        <asp:TemplateField SortExpression="Schedule" HeaderText="Schedule">
                            <ItemTemplate>
                                <asp:Label ID="lblVarSchedule" runat="server" Text='<%#Eval("Schedule")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="LunchBreak" HeaderText="Lunch break">
                            <ItemTemplate>
                                <asp:Label ID="lblVarLunch" runat="server" Text='<%#Eval("LunchBreak")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlScheduleType" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <!----------Popups------------->
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
    
    <!--add variable schedule management start-->
    <cc1:ModalPopupExtender ID="mdlAddVariableSch" runat="server" BackgroundCssClass="popupHolder"
        CancelControlID="lnkAddClose" TargetControlID="hdnAddVariable" PopupControlID="addVariableSchedule">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnAddVariable" runat="server" />
    <div id="addVariableSchedule" runat="server" class="popContent" style="width: 400px; display: none">
        <h2>
            Add variable schedule
            <span class="close">
            <asp:LinkButton ID="lnkAddClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        From<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="8" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                       Upto<span class="must">*</span>
                                             
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUpto" runat="server">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="One week" Value="1week"></asp:ListItem>
                        <asp:ListItem Text="Two weeks" Value="1week"></asp:ListItem>
                        <asp:ListItem Text="One week" Value="1week"></asp:ListItem>
                        <asp:ListItem Text="One week" Value="1week"></asp:ListItem>
                        <asp:ListItem Text="One week" Value="1week"></asp:ListItem>
                        <asp:ListItem Text="One week" Value="1week"></asp:ListItem>
                        <asp:ListItem Text="One week" Value="1week"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm password<span class="must">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="Button1" runat="server" Text="Update" CssClass="btn btn-danger"
                                        OnClientClick="return validPwd();" OnClick="btnUpdatePwd_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelPwd" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancelPwd_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    
    <!-- add variable schedule management stop-->
    
    </form>
</body>
</html>
