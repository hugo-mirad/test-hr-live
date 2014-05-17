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
   
   function pageLoad() {
     
      var Currentdate=new Date($('#lblDate2').text());
     
      var mindate=(Currentdate.getMonth()+1)+"/" +Currentdate.getDate() +"/"+Currentdate.getFullYear();
  
   
     $('#txtEffectiveDate').datepicker({
            dateFormat: "mm/dd/yy",
            minDate:mindate
        });
   }

   
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="Scrpt1" runat="server" ScriptMode="Release">
    </cc1:ToolkitScriptManager>
    
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
    
    
     <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    
         <asp:UpdatePanel ID="uplvAp" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMonthRep" runat="server" CssClass="lbl"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </h2>
    <div>
        <div style="display: inline-block; margin-left: 10px;">
            <asp:UpdatePanel ID="upSelect" runat="server">
                <ContentTemplate>
                    <b>Status </b>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlSelect" runat="server" Width="105px" Height="23px" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;&nbsp; <b><b>
                        <asp:Label ID="lblGrdLocaton" runat="server" Text="Location"></asp:Label></b>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="83px"
                            Height="23px" AppendDataBoundItems="true" 
                        onselectedindexchanged="ddlLocation_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp; <b>
                            <asp:Label ID="lblShift" runat="server" Text="Shift"></asp:Label></b>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlgridShift" runat="server" AutoPostBack="true" Style="width: 70px;"
                            OnSelectedIndexChanged="ddlgridShift_SelectedIndexChanged">
                        </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
    <asp:UpdatePanel ID="upgrd" runat="server">
        <ContentTemplate>
            <div style="text-align:center;margin:180px;" runat="server" id="dvNodata"> 
                <asp:Label ID="lblNodata" runat="server" Style="font-size: 20px; font-weight: bold;
                    "></asp:Label>
                &nbsp;
            </div>
            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="false" CssClass="table1"
                Style="width: 990px;" OnRowDataBound="grdUsers_RowDataBound" AllowSorting="True"
                OnSorting="grdUsers_Sorting" OnRowCommand="grdUsers_RowCommand">
                <Columns>
                    <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblEmpID" runat="server" Text='<%#Eval("empID")%>' CommandName="Effective"
                                CommandArgument='<%#Eval("EffectiveID")%>'></asp:LinkButton>
                            <asp:HiddenField ID="hdnEffectiveID" runat="server" Value='<%#Eval("EffectiveID") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Firstname" HeaderText="BusinessName">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Fieldname" HeaderText="Fieldname">
                        <ItemTemplate>
                            <asp:Label ID="lblFieldname" runat="server" Text='<%#Eval("ChangeField")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FieldValue" HeaderText="OldValue">
                        <ItemTemplate>
                            <asp:Label ID="lblFieldOldValue" runat="server" Text='<%#Eval("OldValue")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FieldValue" HeaderText="NewValue">
                        <ItemTemplate>
                            <asp:Label ID="lblFieldNewValue" runat="server" Text='<%#Eval("NewValue")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EffectiveDt" HeaderText="EffectiveDt">
                        <ItemTemplate>
                            <asp:Label ID="lblEffectiveDt" runat="server" Text='<%# Bind("EffectiveDt", "{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ChangeStatus")%>'></asp:Label>
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
    <div id="AddPopUp" runat="server" class="popContent" style="width: 360px; height: 290px;
        display: none">
        <h2>
        <asp:UpdatePanel ID="uplbl" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblEmpName" runat="server"></asp:Label></ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner" >
        <asp:UpdatePanel ID="updv" runat="server">
        <ContentTemplate>
        <asp:HiddenField ID="hdnEffectID" runat="server" />
        <div runat="server" id="dvView">
            <table style="width: 99%;">
                <tr>
                    <td style="width: 712px;">
                        &nbsp;
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-danger" 
                                        onclick="btnEdit_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="ppHedContent">
                <div style="padding:5px;">
                    <asp:UpdatePanel ID="up" runat="server">
                        <ContentTemplate>
                            <table style="width:99%;margin-left:15px;">
                                <tr>
                                    <td style="width:30%;padding:2px;vertical-align:top">
                                        <b>Field name</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFieldname" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:30%;padding:2px;vertical-align:top">
                                       <b> Old value</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOldValue" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:30%;padding:2px;vertical-align:top">
                                       <b> New value</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNewvalue" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:30%;padding:2px;vertical-align:top">
                                        <b>Effective date</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEffectiveDt" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:30%;padding:2px;vertical-align:top">
                                        <b>Status</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
            </div>
            </div>
            <div runat="server" id="dvEdit" style="display:none;">
              <div class="ppHedContent">
                <div style="padding:5px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table style="width:99%">
                                <tr>
                                    <td style="width:30%">
                                        <b>Field name</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEditFieldName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       <b> Old value</b>
                                    </td>
                                    <td>
                                       <asp:Label ID="lblEditOldValue" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       <b> New value</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEditNewValue" runat="server"></asp:TextBox>
                                       <asp:DropDownList ID="ddlNewValue" runat="server" Visible=false></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Effective date</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEffectiveDate" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Status</b>
                                    </td>
                                    <td>
                                       <asp:DropDownList ID="ddlEditStatus" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
            </div>
              <table style="width: 99%;margin-top:-18px;">
                <tr>
                <td style="widht:33%">&nbsp;</td>
                    <td style="float:right;">
                        <div style="display: inline-block;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                                        CssClass="btn btn-danger" onclick="btnUpdate_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td>
                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" 
                            onclick="btnCancel_Click"/>
                    </td>
                </tr>
            </table>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
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
            Add Effective n></span>
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
