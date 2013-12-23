<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PettyCashManagement.aspx.cs"
    Inherits="Attendance.PettyCashManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
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
    </style>

    <script type="text/javascript">
      function pageLoad()
        {
            $('#spinner').hide();            
                
             $('#txtDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
            $('#txtExpenseDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
        
        
        }
   $(window).load(function () {
    
         $('#txtDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
        
        $('#txtExpenseDate').datepicker({
            dateFormat: "mm/dd/yy"
            //timeFormat:"hh:mm tt"      
        });
    });
      function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        
        function validateSave()
        {
          debugger
          var valid=true;

              
         if (document.getElementById('txtName').value=="") {
               alert("Please enter name.");
               valid = false;
               document.getElementById("txtName").value = "";
               document.getElementById("txtName").focus();
           }

          else if (document.getElementById('txtIncome').value.trim().length < 1) {
               alert("Please enter InitialAmount.");
               valid = false;
               document.getElementById("txtIncome").value = "";
               document.getElementById("txtIncome").focus();
           }


   return valid;
        
        }
        
    </script>
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
            font-size: 12px;
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
                                            <asp:LinkButton runat="server" ID="lnkUserMangement" Text="Employee Management" PostBackUrl="UserManagement.aspx"></asp:LinkButton></li>
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
        petty cash management
    </h2>
    <div style="display: none;">
        <div>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-danger w997"
                Style="margin-left: 962px;" OnClick="btnSave_Click" OnClientClick="return validateSave();" />
        </div>
        <div style="width: 90%; margin-left: 10px;">
            <table style="width: 90%">
                <tr>
                    <td style="width: 49%">
                        <fieldset class="popupFieldSet">
                            <legend>Account details</legend>
                            <table style="width: 95%; margin-left: 12px;">
                                <tr>
                                    <td style="width: 150px;">
                                        Account name<span class="must">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Type
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="up" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlIncomeType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIncomeType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Cheque" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr runat="server" style="display: none;" id="Cheque">
                                    <td>
                                        Cheque#
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChequeNum" runat="server" onkeypress="return isNumberKey(event)"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Initial amount<span class="must">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIncome" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        From whom
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFrmWhom" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="width: 2%">
                        &nbsp;
                    </td>
                    <td style="width: 49%">
                        <fieldset class="popupFieldSet">
                            <legend>Expense details</legend>
                            <table style="width: 95%; margin-left: 12px;">
                                <tr>
                                    <td style="width: 150px;">
                                        Amount
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpenseAmnt" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Type
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpenseType" runat="server" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sub type
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpenseSubType" runat="server" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpenseDate" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Service man name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtServiceName" runat="server" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 49%">
                        <fieldset class="popupFieldSet">
                            <legend>Bill details</legend>
                            <table style="width: 95%; margin-left: 12px;">
                                <tr>
                                    <td style="width: 150px;">
                                        Bill#
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBillNum" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Voucher#
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVoucherNum" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <fieldset class="popupFieldSet">
                            <legend>Expense notes</legend>
                            <table style="width: 95%; margin-left: 12px;">
                                <tr>
                                    <td style="width: 150px;">
                                        Expense notes
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNotes" runat="server" MaxLength="800" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="width: 98%; margin-left: 10px;">
        <div class="inner">
            <table style="width: 100%; border-collapse: collapse">
                <tr>
                    <td style="width: 44%; vertical-align: top;">
                        <h4 class="ppHed" style="margin-bottom: 0">
                            Petty Cash Details <span class="actions">
                                <asp:Button ID="btnAddPettyCashAmount" runat="server" class="btn btn-inverse btn-small"
                                    Text="Add" />
                            </span>
                        </h4>
                       
                        <div style="border: #ccc 1px solid; height: 150px;">
                                          
                            <asp:GridView ID="grdPettyCashDet" runat="server" AutoGenerateColumns="false" Width="90%" style="margin-left:20px;margin-top:10px;" CssClass="table1" >
                                <Columns>
                                    <asp:TemplateField SortExpression="name" HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpFirstname" runat="server" Text='<%#Eval("AccountHolderName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Income" HeaderText="Initail amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Income") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="AcountDate" HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblACountDate" runat="server" Text='<%#Bind("AcountDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%#Eval("IncomeType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="chequeNo" HeaderText="Cheque no">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCheque" runat="server" Text='<%#Eval("chequeNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="FromWhom" HeaderText="From whom">
                                        <ItemTemplate>
                                            <asp:Label ID="FromWhom" runat="server" Text='<%#Eval("FromWhom")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                </Columns>
                            </asp:GridView>
                        </div>
                          </td>
                          </tr> 
                          <tr>
                           <td>&nbsp;</td>
                          </tr> 
                          <tr>
                          <td style="width: 44%; vertical-align: top;">
                        <h4 class="ppHed" style="margin-bottom: 0">
                            Expense Details <span class="actions">
                                <asp:Button ID="btnExpenseAdd" runat="server" class="btn btn-inverse btn-small"
                                    Text="Add" />
                              </span>
                        </h4>
                                <div style="border: #ccc 1px solid; height: 150px;">
                                 <asp:GridView ID="grdExpenseDetails" runat="server" AutoGenerateColumns="false" Width="90%" style="margin-left:20px;margin-top:10px;" CssClass="table1" >
                                <Columns>
                                   <asp:TemplateField SortExpression="Accountid" HeaderText="PettyCashID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAcountID" runat="server" Text='<%#Eval("Accountid")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="expenditureAmount" HeaderText="Expense amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpenseAmount" runat="server" Text='<%#Eval("expenditureAmount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="expenditureTpe" HeaderText="Expense type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpe" runat="server" Text='<%# Eval("expenditureTpe") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="expenditureSubtype" HeaderText="Expense subtype">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpSubType" runat="server" Text='<%# Eval("expenditureSubtype") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service man">
                                        <ItemTemplate>
                                            <asp:Label ID="lblService" runat="server" Text='<%#Eval("ServicemanName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="BillNo" HeaderText="Bill no">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBill" runat="server" Text='<%#Eval("BillNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="VoucherNo" HeaderText="Voucher no">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucher" runat="server" Text='<%#Eval("VoucherNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField SortExpression="expenditureNotes" HeaderText="Notes">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucher" runat="server" Text='<%#Eval("expenditureNotes")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                </Columns>
                            </asp:GridView>
                             </div>
                          
                          </tr>
                           
                           
    </form>
</body>
</html>
