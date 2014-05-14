<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EffectiveManagement.aspx.cs"
    Inherits="Attendance.EffectiveManagement" %>

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

    <script type="text/javascript" language="javascript">
   
   $(function(){
   
        $('.popupHolder2').css('z-index','100002')
            $('.popupContent2').css('z-index','100003')
   
      $('#btnAdd').live('click', function(){
         var str = '';
         var len = 0;         
        $('#grdEfUsers input[type=checkbox]').each(function(){
            if($(this).is(':checked')){
                str += $(this).attr('LeaveID')+',';
                len++;
            }
            $('#hdnChkRecords').val(str);
            
        })
        
        if(len>0){
            $find('mdlEffective1popup').show();
        }else{
            alert('Please select an employee');           
        }
   });
   
     $('.selectAll').live('change',function(){
         if($(this).is(':checked')){
           $('#grdEfUsers input[type=checkbox]:not(.selectAll)').each(function(){
                $(this).attr('checked',true);
           })
        }else{
            $('#grdEfUsers input[type=checkbox]:not(.selectAll)').each(function(){
                $(this).removeAttr('checked');
           })
        }
    })
    

   
   });

   
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="Scrpt1" runat="server">
    </cc1:ToolkitScriptManager>
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
                                            <asp:LinkButton runat="server" ID="LinkButton1" Text="Effective Dates Management"
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
    <h2 class="pageHeadding">
        Effective management
    </h2>
    <div>
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
                    &nbsp;&nbsp; <b><b>
                        <asp:Label ID="lblGrdLocaton" runat="server" Text="Location"></asp:Label></b>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="83px"
                            Height="23px" AppendDataBoundItems="true">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp; <b>
                            <asp:Label ID="lblShift" runat="server" Text="Shift"></asp:Label></b>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlgridShift" runat="server" AutoPostBack="true" Style="width: 70px;"
                            OnSelectedIndexChanged="ddlgridShift_SelectedIndexChanged">
                        </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upef" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="lnkAddEffective" runat="server" Text="Add Effective Dates" CssClass="btn btn-danger w996"
                        Style="margin-left: 840px;" OnClick="lnkAddEffective_Click"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="upgrd" runat="server">
        <ContentTemplate>
            <div>
                <asp:Label ID="lblNodata" runat="server" Style="font-size: 11px; font-weight: bold;
                    margin-left: 10px;"></asp:Label>
                &nbsp;
            </div>
            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="false" CssClass="table1"
                Style="width: 990px;" OnRowDataBound="grdUsers_RowDataBound" AllowSorting="True"
                OnSorting="grdUsers_Sorting">
                <Columns>
                    <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>' CommandName="user"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Firstname" HeaderText="BusinessName">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("empName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="JoiningDate" HeaderText="StartDt">
                        <ItemTemplate>
                            <asp:Label ID="lblStartedDate" runat="server" Text='<%# Bind("startdate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="TerminatedDt" HeaderText="TermDt">
                        <ItemTemplate>
                            <asp:Label ID="lblTerminatedDate" runat="server" Text='<%#Bind("TermDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="DeptName" HeaderText="Department">
                        <ItemTemplate>
                            <asp:Label ID="lblDept" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Fieldname" HeaderText="Fieldname">
                        <ItemTemplate>
                            <asp:Label ID="lblFieldname" runat="server" Text='<%#Eval("FieldName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FieldValue" HeaderText="FieldValue">
                        <ItemTemplate>
                            <asp:Label ID="lblFieldValue" runat="server" Text='<%#Eval("FieldValue")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EffectiveDt" HeaderText="EffectiveDt">
                        <ItemTemplate>
                            <asp:Label ID="lblEffectiveDt" runat="server" Text='<%# Bind("EffectiveDt", "{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="Active">
                        <ItemTemplate>
                            <asp:Label ID="lblEffectiveDt" runat="server" Text='<%#Eval("IsActive")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- addpopup-->
    <cc1:ModalPopupExtender ID="mdlAddPopUp" BackgroundCssClass="popupHolder" runat="server"
        PopupControlID="AddPopUp" CancelControlID="lnkClose" TargetControlID="hdnAddpopup">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnAddpopup" runat="server" />
    <div id="AddPopUp" runat="server" class="popContent" style="width: 600px; min-height: 400px;
        display: none">
        <h2>
            Add Effective Dates
            <asp:Label ID="lbladdLoc" runat="server" Style="font-size: 17px; font-weight: bold"></asp:Label>
            <span class="close">
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
                                    <asp:Button ID="btnAdd" runat="server" Text="Add Effective" CssClass="btn btn-danger" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="scrollBlock" style="height: 300px;">
                <div class="ppHedContent">
                    <asp:UpdatePanel ID="up" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnChkRecords" runat="server" />
                            <asp:GridView ID="grdEfUsers" runat="server" AutoGenerateColumns="false" CssClass="table1"
                                Style="width: 500px; min-height: 500px;">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <input id="selectAll" class="selectAll" type="checkbox" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRecord" runat="server" LeaveID='<%#Eval("userid")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="30" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>' CommandName="user"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Firstname" HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("Firstname")+" "+Eval("lastname")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="150" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="JoiningDate" HeaderText="StartDt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartedDate" runat="server" Text='<%# Bind("startdate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="60" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TerminatedDt" HeaderText="TermDt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerminatedDate" runat="server" Text='<%#Bind("TermDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="60" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <br />
            <br />
        </div>
    </div>
    <!--add new schedule popup-->
    <cc1:ModalPopupExtender ID="mdlEffective1popup" runat="server" BackgroundCssClass="popupHolder2"
        CancelControlID="lnkScheduleClose" TargetControlID="hdnSchedulepop" PopupControlID="Schedulepop">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnSchedulepop" runat="server" />
    <div id="Schedulepop" runat="server" class="popContent popupContent2" style="width: 400px;
        display: none">
        <h2>
            Add Effective <span class="close">
                <asp:LinkButton ID="lnkScheduleClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <asp:UpdatePanel ID="updd" runat="server">
                <ContentTemplate>
                    <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                        <tr>
                            <td>
                                Field name
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEffectiveField" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEffectiveField_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                    <asp:ListItem Text="Salary" Value="Salary"></asp:ListItem>
                                    <asp:ListItem Text="Department" Value="Department">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr runat="server" id="trSalary" style="display: none;">
                            <td>
                                Value
                            </td>
                            <td>
                                <asp:TextBox ID="txtEfectiveValue" runat="server" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trDept" style="display: none;">
                            <td>
                                Department
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                      
                        <tr>
                            <td>
                            </td>
                            <td>
                                <div style="display: inline-block">
                                    <asp:UpdatePanel ID="upSch" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSchUpdate" runat="server" Text="Update" CssClass="btn btn-danger" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDepartment" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlEffectiveField" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:Button ID="btnCancelSch" runat="server" Text="Cancel" CssClass="btn" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
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
    </form>
</body>
</html>
