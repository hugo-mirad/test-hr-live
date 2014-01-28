<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveManagement.aspx.cs"
    Inherits="Attendance.LeaveManagement" %>

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

    <script type="text/javascript">
      $(window).load(function(){
       $('[rel=tooltip]').tooltip();
      });
     
     function pageLoad()
     {
       $('[rel=tooltip]').tooltip();
     }
     
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
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
    <h2 class="pageHeadding">
        Leave management
    </h2>
    <div style="display: inline-block; margin-left: 10px;">
        <asp:UpdatePanel ID="UpdateLocation" runat="server">
            <ContentTemplate>
                <b>
                    <asp:Label ID="lblGrdLocaton" runat="server" Text="Location"></asp:Label></b>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="83px"
                    Height="23px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                </h4>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upgrd" runat="server">
        <ContentTemplate>
            <div>
                <asp:Label ID="lblTotal" runat="server" Style="font-size: 11px; font-weight: bold;
                    margin-left: 10px;"></asp:Label>
                &nbsp;
            </div>
            <input style="width: 91px" id="txthdnSortOrder" type="hidden" runat="server" enableviewstate="true" />
            <input style="width: 40px" id="txthdnSortColumnId" type="hidden" runat="server" enableviewstate="true" />
            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="false" CssClass="table1"
                DataKeyNames="PaidLeaveID" Style="width: 997px;" OnRowCommand="grdUsers_RowCommand"
                OnRowDataBound="grdUsers_RowDataBound" AllowSorting="True" OnSorting="grdUsers_Sorting"
                OnRowEditing="grdUsers_RowEditing" OnRowCancelingEdit="grdUsers_RowCancelingEdit"
                OnRowUpdating="grdUsers_RowUpdating">
                <Columns>
                    <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EMPID")%>'></asp:Label>
                            <asp:HiddenField ID="hdnUserid" runat="server" Value='<%#Eval("Userid")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Firstname" HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("Firstname")+" "+Eval("lastname")%>'></asp:Label>
                            <%-- <asp:HiddenField ID="hdnPhoto" runat="server" Value='<%#Eval("photolink")%>' />--%>
                        </ItemTemplate>
                        <ItemStyle Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="JoiningDate" HeaderText="StartDt">
                        <ItemTemplate>
                            <asp:Label ID="lblStartedDate" runat="server" Text='<%# Bind("JoiningDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="TerminatedDt" HeaderText="TermDt">
                        <ItemTemplate>
                            <asp:Label ID="lblTerminatedDate" runat="server" Text='<%#Bind("TermDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="deptname" HeaderText="Department">
                        <%--  <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadDepartment" runat="server" Text="Department"></asp:LinkButton>
                            </HeaderTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="lblDept" runat="server" Text='<%#Eval("deptname")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="LeavesAvailable" HeaderText="LeavesAvailable">
                        <%--  <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadDesignation" runat="server" Text="Designation"></asp:LinkButton>
                            </HeaderTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="lblLeavesAvailable" runat="server" Text='<%#Eval("LeavesAvailable")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLeavAvailable" runat="server" Text='<%#Eval("LeavesAvailable")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="30" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="LeavesAvailableDt" HeaderText="LeavesAvailableDt">
                        <%-- <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadActive" runat="server" Text="Active"></asp:LinkButton>
                            </HeaderTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="lblLeavesAvailableDt" runat="server" Text='<%#Bind("LeavesAvailableDt","{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <%--     <EditItemTemplate>
                             <asp:TextBox ID="txtLeavAvailableDt" runat="server" Text='<%#Bind("LeavesAvailableDt","{0:MM/dd/yyyy}") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                        <ItemStyle Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="MonthlyMaxEligible" HeaderText="MonthlyMaxEligible">
                        <%-- <HeaderTemplate>
                                <asp:LinkButton ID="lblHeadActive" runat="server" Text="Active"></asp:LinkButton>
                            </HeaderTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="lblMaxEligible" runat="server" Text='<%#Eval("MonthlyMaxEligible")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMaxEligible" runat="server" Text='<%#Eval("MonthlyMaxEligible")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="30" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Notes">
                        <ItemTemplate>
                            <asp:Label ID="lblNotes" runat="server" Text='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "Notes"))%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNotes" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="140" />
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="true" ItemStyle-Width="30" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
