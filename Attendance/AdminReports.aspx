<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminReports.aspx.cs" Inherits="Attendance.AdminReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- <script src="js/overlibmws.js" type="text/javascript"></script>  -->
    <link rel="stylesheet" href="css/reset.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/UI.css" />
    <link rel="stylesheet" href="css/inputs.css" type="text/css" />
    <link href="css/admin.css" rel="stylesheet" type="text/css" />
    <!-- <link rel="stylesheet" href="css/style.css" type="text/css" />  -->
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    
    
    <style type="text/css">
        body, html
        {
            font-family: Arial;
            font-size: 12px;
            color: #333;
            background: #fff;
        }
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
        .table3 td
        {
            border: #ccc 1px dotted;
            padding: 0px 4px;
            white-space: nowrap;
        }
        .table3 th, .table3 tr:first-child td
        {
            padding: 1px 2px;
            vertical-align: middle;
            background: #ccc;
            text-align: center;
        }
        .table3
        {
            font-size: 10px;
            width: 90%;
        }
        .table3 tr:first-child
        {
            font-weight: bold;
        }
        .table3
        {
            border: #999 1px solid;
            border-collapse: collapse;
        }
        .table3 td span
        {
            white-space: nowrap;
        }
        .table2 td
        {
            border: #ccc 1px dotted;
            padding: 2px 4px;
            white-space: nowrap;
        }
        .table2 th, .table2 tr:first-child td
        {
            padding: 2px 4px;
            vertical-align: middle;
            background: #ccc;
            text-align: center;
        }
        .table2
        {
            font-size: 13px;
        }
        .table2 tr:first-child td
        {
            /* border:#888 2px solid;*/
            font-weight: bold;
            font-size: 13px;
        }
        .table2
        {
            border: #999 1px solid;
            border-collapse: collapse;
            width: 64%;
            margin-top: 2%;
            margin-left: 10px;
            margin-right: 25%;
            padding-left: 99px;
        }
        .table2 td span
        {
            white-space: nowrap;
            font-size: 14px;
        }
        .col1
        {
            background: #F7F7F7;
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
        /* Popup */.popupHolder
        {
            background: url(../images/popupBack.png) repeat;
        }
        .popContent
        {
            border: #ccc 1px solid;
            background: #fff;
            margin: 0 auto;
            width: auto;
            position: relative;
            z-index: 1001;
            box-shadow: 0 0 6px rgba(0,0,0,0.3);
        }
        .popContent h2
        {
            padding: 12px;
            color: #232323;
            background: #fff;
            font-size: 15px;
            border-bottom: #666 1px solid;
            text-align: left;
            padding-top: 0;
            font-family: "Century Gothic";
        }
        .popContent .userThumb
        {
            width: 120px;
            height: auto;
            border: #ccc 1px dashed;
            padding: 2px;
            background: #fff;
        }
        .popContent h2 span
        {
            color: #ff6600;
            font-family: "Century Gothic";
            font-size: 15px;
        }
        .loginForm
        {
            border-collapse: collapse;
            padding: 0;
            margin: 0 0 0 10px;
            width: 100%;
        }
        .loginForm textarea
        {
            width: 93%;
        }
        .loginForm td
        {
            padding-bottom: 10px;
        }
        .popContent i
        {
            font-size: 11px;
            color: #999;
        }
        .must
        {
            font-size: 14px;
            color: Red;
            font-weight: bold;
        }
        .activeDiv, .bor.activeDiv
        {
            z-index: 30;
            position: relative;
            box-shadow: 0 0 10px #000;
        }
        .popContent.error1
        {
            box-shadow: 0 0 15px red;
            borderred: 11pxsolid;
            background: #FFE5D8;
        }
        .dummyPopup
        {
            width: 100%;
            height: 100%;
            min-height: 100%;
            background: url(../images/popupBack.png) repeat;
            position: fixed;
            left: 0;
            top: 0;
            z-index: 30;
            display: none;
        }
        .totalHolder
        {
            width: 100%;
            height: 98%;
            min-height: 98%;
            border-collapse: collapse;
        }
        .totalHolder td
        {
            min-width: 200px;
        }
        .totalHolder .inner
        {
            display: table;
            min-height: 90%;
            height: 90%;
            width: 90%;
            min-width: 90%;
        }
        .totalHolder ul
        {
            height: 100%;
            min-height: 100%;
            width: 100%;
            min-width: 100%;
            display: table-cell;
        }
        .h100p
        {
            height: 100%;
            min-height: 100%;
            margin: 0;
            padding: 0;
        }
        .one span, .two span, .three span
        {
            font-weight: normal;
            color: #666;
        }
        /* css for timepicker */.ui-timepicker-div .ui-widget-header
        {
            margin-bottom: 8px;
        }
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            float: left;
            clear: left;
            padding: 0 0 0 5px;
        }
        .ui-timepicker-div dl dd
        {
            margin: 0 10px 10px 45%;
        }
        .ui-timepicker-div td
        {
            font-size: 90%;
        }
        .ui-tpicker-grid-label
        {
            background: none;
            border: none;
            margin: 0;
            padding: 0;
        }
        .ui-timepicker-rtl
        {
            direction: rtl;
        }
        .ui-timepicker-rtl dl
        {
            text-align: right;
            padding: 0 5px 0 0;
        }
        .ui-timepicker-rtl dl dt
        {
            float: right;
            clear: right;
        }
        .ui-timepicker-rtl dl dd
        {
            margin: 0 45% 10px 10px;
        }
        .ui-tooltip, .arrow:after
        {
            background: #fff;
        }
        .ui-tooltip
        {
            padding: 5px;
            border-radius: 3px;
            font: bold 14px "Helvetica Neue" , Sans-Serif;
            text-transform: uppercase;
            box-shadow: 0 0 7px black;
        }
        .ui-tooltip
        {
            border: 1px solid #888;
            background: #fff;
            color: #222;
            text-transform: capitalize;
            box-shadow: 0 0 8px #000;
        }
        .ui-tooltip b
        {
            text-transform: uppercase;
            font-size: 14px;
        }
        .ui-tooltip span
        {
            display: inline-block;
            min-width: 60px;
            font-weight: bold;
        }
        .arrow
        {
            width: 70px;
            height: 16px;
            overflow: hidden;
            position: absolute;
            left: 50%;
            margin-left: -35px;
            bottom: -16px;
        }
        .arrow.top
        {
            top: -16px;
            bottom: auto;
        }
        .arrow.left
        {
            left: 20%;
        }
        .arrow:after
        {
            content: "";
            position: absolute;
            left: 20px;
            top: -20px;
            width: 25px;
            height: 25px;
            box-shadow: 0 0 9px black;
            -webkit-transform: rotate(45deg);
            -moz-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            -o-transform: rotate(45deg);
            tranform: rotate(45deg);
            border: 2px solid #888;
        }
        .arrow.top:after
        {
            bottom: -20px;
            top: auto;
        }
        .center
        {
            display: block;
            text-align: center;
            width: 100%;
        }
        body .ui-tooltip
        {
            width: 420px;
        }
        body
        {
            padding-top: 55px;
        }
    </style>

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    
    

    <script type="text/javascript">
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
    
    function validateFreeze()
    {
    debugger
     var vr=false;
     
      var freedt= document.getElementById("hdnFreeze").value;
      var valid=confirm("This operation will freeze attendance data until "+freedt+".You will not able to do further modifications.Are you sure to proceed?") 
       if(valid)
       {
        vr=true;
       }
       else
       { 
        vr=false;
       }
      return vr;
    }

    
    
    </script>

    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    

    <script type="text/javascript" language="javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(clearDisposableItems)


function clearDisposableItems( sender , args ) {
  if (Sys.Browser.agent == Sys.Browser.InternetExplorer ) {
		$('#spinner').show();	
  } else {
	$('#spinner').show();
    //$get('<%=grdAttandence.ClientID%>').innerHTML="";
  }
}
    </script>

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
                                        <li><a href="AdminReports.aspx">Attendance Report</a></li>
                                        <li>
                                            <asp:LinkButton ID="lnkPayroll" runat="server" Text="Payroll Report" PostBackUrl="PayRoll.aspx"></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkUserMangement" Text="Employee Management" PostBackUrl="AdminUserManagement.aspx"></asp:LinkButton></li>
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
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblWeekReportheading" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblWeekReport" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </h2>
    <asp:UpdatePanel ID="updd" runat="server">
        <ContentTemplate>
            <b>
                <asp:Label ID="lblReport" runat="server" Text="Report type"></asp:Label></b>&nbsp;
            <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="true" Visible="true"
                OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                <asp:ListItem Text="Weekly Report - Detail" Value="0"></asp:ListItem>
                <asp:ListItem Text="Weekly Reprot - Summary" Value="1"></asp:ListItem>
                <asp:ListItem Text="Monthly Report - Summary" Value="2"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp; <b>
                <asp:Label ID="lblGrdLocaton" runat="server" Text="Location"></asp:Label></b>&nbsp;&nbsp;
            <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" Style="width: 70px;">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <%--<div style="display:inline-block;">--%>
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
                        <asp:Button ID="btnFreeze" runat="server" Text="Freeze" Visible="false" CssClass="btn btn-warning  btn-small"
                            OnClientClick="return validateFreeze();" OnClick="btnFreeze_Click" />
                        <span>
                            <asp:Label ID="lblFreeze" runat="server" Visible="false"></asp:Label></span>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="upbtns"
            DisplayAfter="0">
            <ProgressTemplate>
                <div id="spinner">
                    <h4>
                        <div>
                            Processing
                            <img src="images/loading.gif" />
                        </div>
                        <h4>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updd"
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
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdnFreeze" />
                <asp:GridView ID="grdAttandence" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdAttandence_RowDataBound"
                    OnRowCreated="grdAttandence_RowCreated" CssClass="table1" OnRowCommand="grdAttandence_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHeadID" runat="server" Text="EmpID"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHeadName" runat="server" Text="Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Empname")%>'></asp:Label>
                                <asp:Label ID="lblPName" runat="server"  Text='<%#Eval("PEmpname")%>' Visible="false"></asp:Label>
                                <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("Startdate")%>' Visible="false"></asp:Label>
                                <asp:Label ID="lblTermDate" runat="server" Text='<%#Eval("Termdate")%>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblMonHeadSCIN" runat="server" Text="SchIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMonScIn" runat="server" Text='<%#Eval("MonSchIn")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblMonHeadSCout" runat="server" Text="SchOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMonScOut" runat="server" Text='<%#Eval("MonSchOut")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblMonHeadIN" runat="server" Text="SignIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblMonIn" runat="server" Font-Underline="False" Text='<%#Eval("MonSignIn")%>'
                                    CommandName="LoginMonEdit" CommandArgument='<%#Eval("MonLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnMonSigninNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "MonLoginNotes"))%>' />
                                <asp:HiddenField ID="hdnMonSignInFlag" runat="server" Value='<%#Eval("MonLoginFlag")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblMonHeadOut" runat="server" Text="SignOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblMonOut" runat="server" Font-Underline="False" Text='<%#Eval("MonSignOut")%>'
                                    CommandName="LogOutMonEdit" CommandArgument='<%#Eval("MonLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnMonSignOutNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "MonLogoutNotes"))%>' />
                                <asp:HiddenField ID="hdnMonSignOutFlag" runat="server" Value='<%#Eval("MonLogoutFlag")%>' />
                                <asp:HiddenField ID="hdnMonFreeze" runat="server" Value='<%#Eval("MonFreeze")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblMonHeadHours" runat="server" Text="Hrs"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMonHours" runat="server" Font-Underline="False" Text='<%#Eval("MonHrs")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblTueSCIN" runat="server" Text="SchIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTueScIn" runat="server" Text='<%#Eval("TueSchIn")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblTueSCout" runat="server" Text="SchOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTueScOut" runat="server" Text='<%#Eval("TueSchOut")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblTueIN" runat="server" Text="SignIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblTueIn" runat="server" Font-Underline="False" Text='<%#Eval("TueSignIn")%>'
                                    CommandName="LoginTueEdit" CommandArgument='<%#Eval("TueLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnTueSigninNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "TueLoginNotes"))%>' />
                                <asp:HiddenField ID="hdnTueSignInFlag" runat="server" Value='<%#Eval("TueLoginFlag")%>' />
                                <asp:HiddenField ID="hdnTueFreeze" runat="server" Value='<%#Eval("TueFreeze")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblTueOut" runat="server" Text="SignOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblTueOut" runat="server" Font-Underline="False" Text='<%#Eval("TueSignOut")%>'
                                    CommandName="LogOutTueEdit" CommandArgument='<%#Eval("TueLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnTueSignOutNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "TueLogoutNotes"))%>' />
                                <asp:HiddenField ID="hdnTueSignOutFlag" runat="server" Value='<%#Eval("TueLogoutFlag")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblTueHeadHours" runat="server" Text="Hrs"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTueHours" runat="server" Text='<%#Eval("TueHrs")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblWedSCIN" runat="server" Text="SchIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblWedScIn" runat="server" Text='<%#Eval("WedSchIn")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblWedSCout" runat="server" Text="SchOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblWedScOut" runat="server" Text='<%#Eval("WedSchOut")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblWedIN" runat="server" Text="SignIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblWedIn" runat="server" Font-Underline="False" Text='<%#Eval("WedSignIn")%>'
                                    CommandName="LoginWedEdit" CommandArgument='<%#Eval("WedLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnWedSigninNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "WedLoginNotes"))%>' />
                                <asp:HiddenField ID="hdnWedSignInFlag" runat="server" Value='<%#Eval("WedLoginFlag")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblWedOut" runat="server" Text="SignOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblWedOut" runat="server" Font-Underline="False" Text='<%#Eval("WedSignOut")%>'
                                    CommandName="LogOutWedEdit" CommandArgument='<%#Eval("WedLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnWedSignOutNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "WedLogoutNotes"))%>' />
                                <asp:HiddenField ID="hdnWedSignOutFlag" runat="server" Value='<%#Eval("WedLogoutFlag")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblWedHeadHours" runat="server" Text="Hrs"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblWedHours" runat="server" Text='<%#Eval("WedHrs")%>'></asp:Label>
                                <asp:HiddenField ID="hdnWedFreeze" runat="server" Value='<%#Eval("WedFreeze")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblThuSCIN" runat="server" Text="SchIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblThuScIn" runat="server" Text='<%#Eval("ThuSchIn")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblThuSCout" runat="server" Text="SchOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblThuScOut" runat="server" Text='<%#Eval("ThuSchOut")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblThuIN" runat="server" Text="SignIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblThuIn" runat="server" Font-Underline="False" Text='<%#Eval("ThuSignIn")%>'
                                    CommandName="LoginThuEdit" CommandArgument='<%#Eval("ThuLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnThuSigninNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "ThuLoginNotes"))%>' />
                                <asp:HiddenField ID="hdnThuSignInFlag" runat="server" Value='<%#Eval("ThuLoginFlag")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblThuOut" runat="server" Text="SignOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblThuOut" runat="server" Font-Underline="False" Text='<%#Eval("ThuSignOut")%>'
                                    CommandName="LogOutThuEdit" CommandArgument='<%#Eval("ThuLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnThuSignOutNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "ThuLogoutNotes"))%>' />
                                <asp:HiddenField ID="hdnThuSignOutFlag" runat="server" Value='<%#Eval("ThuLogoutFlag")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblThuHEHours" runat="server" Text="Hrs"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblThuHours" runat="server" Text='<%#Eval("ThuHrs")%>'></asp:Label>
                                <asp:HiddenField ID="hdnThuFreeze" runat="server" Value='<%#Eval("ThuFreeze")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblFriSCIN" runat="server" Text="SchIn"> </asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFriScIn" runat="server" Text='<%#Eval("FriSchIn")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblFriSCout" runat="server" Text="SchOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFriScOut" runat="server" Text='<%#Eval("FriSchOut")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblFriIN" runat="server" Text="SignIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFriIn" runat="server" Font-Underline="False" Text='<%#Eval("FriSignIn")%>'
                                    CommandName="LoginFriEdit" CommandArgument='<%#Eval("FriLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnFriSigninNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "FriLoginNotes"))%>' />
                                <asp:HiddenField ID="hdnFriSignInFlag" runat="server" Value='<%#Eval("FriLoginFlag")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblFriSCOut" runat="server" Text="SignOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFriOut" runat="server" Font-Underline="False" Text='<%#Eval("FriSignOut")%>'
                                    CommandName="LogOutFriEdit" CommandArgument='<%#Eval("FriLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnFriSignOutNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "FriLogoutNotes"))%>' />
                                <asp:HiddenField ID="hdnFriSignOutFlag" runat="server" Value='<%#Eval("FriLogoutFlag")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblFriHeadHours" runat="server" Text="Hrs"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFriHours" runat="server" Text='<%#Eval("FriHrs")%>'></asp:Label>
                                <asp:HiddenField ID="hdnFriFreeze" runat="server" Value='<%#Eval("FriFreeze")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSatSCIN" runat="server" Text="SchIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSatScIn" runat="server" Text='<%#Eval("SatSchIn")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSatSCout" runat="server" Text="SchOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSatScOut" runat="server" Text='<%#Eval("SatSchOut")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSatIN" runat="server" Text="SignIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSatIn" runat="server" Font-Underline="False" Text='<%#Eval("SatSignIn")%>'
                                    CommandName="LoginSatEdit" CommandArgument='<%#Eval("SatLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnSatSigninNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "SatLoginNotes"))%>' />
                                <asp:HiddenField ID="hdnSatSignInFlag" runat="server" Value='<%#Eval("SatLoginFlag")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSatOut" runat="server" Text="SignOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSatOut" runat="server" Font-Underline="False" Text='<%#Eval("SatSignOut")%>'
                                    CommandName="LogOutSatEdit" CommandArgument='<%#Eval("SatLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnSatSignOutNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "SatLogoutNotes"))%>' />
                                <asp:HiddenField ID="hdnSatSignOutFlag" runat="server" Value='<%#Eval("SatLogoutFlag")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSatHeadHours" runat="server" Text="Hrs"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSatHours" runat="server" Text='<%#Eval("SatHrs")%>'></asp:Label>
                                <asp:HiddenField ID="hdnSatFreeze" runat="server" Value='<%#Eval("SatFreeze")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSunSCIN" runat="server" Text="SchIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSunScIn" runat="server" Text='<%#Eval("SunSchIn")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSunSCout" runat="server" Text="SchOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSunScOut" runat="server" Text='<%#Eval("SunSchOut")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSunIN" runat="server" Text="SignIn"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSunIn" runat="server" Font-Underline="False" Text='<%#Eval("SunSignIn")%>'
                                    CommandName="LoginSunEdit" CommandArgument='<%#Eval("SunLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnSunSigninNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "SunLoginNotes"))%>' />
                                <asp:HiddenField ID="hdnSunSignInFlag" runat="server" Value='<%#Eval("SunLoginFlag")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSunOut" runat="server" Text="SignOut"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSunOut" runat="server" Font-Underline="False" Text='<%#Eval("SunSignOut")%>'
                                    CommandName="LogOutSunEdit" CommandArgument='<%#Eval("SunLogUserID")%>'></asp:LinkButton>
                                <asp:HiddenField ID="hdnSunSignOutNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "SunLogoutNotes"))%>' />
                                <asp:HiddenField ID="hdnSunSignOutFlag" runat="server" Value='<%#Eval("SunLogoutFlag")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSunHeadHours" runat="server" Text="Hrs"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSunHours" runat="server" Text='<%#Eval("SunHrs")%>'></asp:Label>
                                <asp:HiddenField ID="hdnSunFreeze" runat="server" Value='<%#Eval("SunFreeze")%>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="col1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHeadTotalPresent" runat="server" Text="Days"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPresent" runat="server" Text='<%#Eval("PresentDays")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="tdBack" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHeadTotalHours" runat="server" Text="Hrs"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTotalHours" runat="server" Text='<%#Eval("TotalHours")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="tdBack" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlReportType" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="display: block">
            <asp:UpdatePanel ID="UpWeek" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnWeeklyStartDt" runat="server" />
                   <asp:GridView CssClass="table3" ID="grdWeeklyAttendance" runat="server" AutoGenerateColumns="false"
                        OnRowDataBound="grdWeeklyAttendance_RowDataBound1" style="max-width:985px" 
                        onrowcreated="grdWeeklyAttendance_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="EmpID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="43px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpname" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle Width="160px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StartDt">
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDt" runat="server" Text='<%#Eval("StatingDate")%>'></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle Width="65px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TermDt">
                                <ItemTemplate>
                                    <asp:Label ID="lblTermDt" runat="server" Text='<%#Eval("TermDate")%>'></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle Width="65px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TermReason">
                                <ItemTemplate>
                                    <asp:Label ID="lblTermReason" runat="server" Text='<%#Eval("TermReason")%>'></asp:Label>
                                </ItemTemplate>
                                    <ItemStyle Width="90px" CssClass="bR" />
                                <HeaderStyle CssClass="bR" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblWeek1Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblWeek1" runat="server" Text='<%#Eval("Week1")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                                <HeaderStyle CssClass="bT" />
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDay1Head" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDay11" runat="server" Text='<%#Eval("Days1")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30" CssClass="bR" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="bT bR" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblWeek2Head" runat="server" Text="Hrs"> </asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblWeek2" runat="server" Text='<%#Eval("Week2")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                                <HeaderStyle CssClass="bT " />
                            </asp:TemplateField>
                            
                                <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDay2Head" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDay2" runat="server" Text='<%#Eval("Days2")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30" CssClass="bR" HorizontalAlign="Center"/>
                                <HeaderStyle CssClass="bT bR" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblWeek3Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblWeek3" runat="server" Text='<%#Eval("Week3")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                                <HeaderStyle CssClass="bT " />
                            </asp:TemplateField>
                            
                                     <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDay3Head" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDay3" runat="server" Text='<%#Eval("Days3")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30" CssClass="bR" HorizontalAlign="Center"/>
                                <HeaderStyle CssClass="bT bR" />
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblWeek4Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblWeek4" runat="server" Text='<%#Eval("Week4")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                                <HeaderStyle CssClass="bT" />
                            </asp:TemplateField>
                            
                              <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDay4Head" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDay4" runat="server" Text='<%#Eval("Days4")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30" CssClass="bR"  HorizontalAlign="Center"/>
                                <HeaderStyle CssClass="bT bR" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblTotalHraHead" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("TotalHrs")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="50" />
                                <HeaderStyle CssClass="bT" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDaysHraHead" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDays" runat="server" Text='<%#Eval("Days")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="30" CssClass="bR"  HorizontalAlign="Center"/>
                                <HeaderStyle CssClass="bT bR" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="display: block">
            <asp:UpdatePanel ID="UpMonth" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnMonthlyStartDt" runat="server" />
                   <asp:GridView CssClass="table3" ID="grdMonthlyAttendance" runat="server" AutoGenerateColumns="false"
                        OnRowDataBound="grdMonthlyAttendance_RowDataBound" style="max-width:985px" 
                        onrowcreated="grdMonthlyAttendance_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="EmpID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpname" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StartDt">
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDt" runat="server" Text='<%#Eval("StatingDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TermDt">
                                <ItemTemplate>
                                    <asp:Label ID="lblTermDt" runat="server" Text='<%#Eval("TermDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TermReason">
                                <ItemTemplate>
                                    <asp:Label ID="lblTermReason" runat="server" Text='<%#Eval("TermReason")%>'></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle Width="90" CssClass="bR" />
                               
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMonth1Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth1" runat="server" Text='<%#Eval("Month1")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60" CssClass="bL"/>
                                <HeaderStyle CssClass="bT bL" />
                                
                            </asp:TemplateField>
                            
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDaysHraHead" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDays1" runat="server" Text='<%#Eval("Days1")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="30" />
                                <HeaderStyle CssClass="bT" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMonth2Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth2" runat="server" Text='<%#Eval("Month2")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60" CssClass="bL"/>
                                <HeaderStyle CssClass="bT bL" />
                                
                            </asp:TemplateField>
                            
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDaysHraHead" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDays2" runat="server" Text='<%#Eval("Days2")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="30" />
                                <HeaderStyle CssClass="bT " />
                                
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMonth3Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth3" runat="server" Text='<%#Eval("Month3")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60" CssClass="bL"/>
                                <HeaderStyle CssClass="bT bL" />
                                
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDaysHraHead" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDays3" runat="server" Text='<%#Eval("Days3")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="30" />
                                <HeaderStyle CssClass="bT" />
                              
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMonth4Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth4" runat="server" Text='<%#Eval("Month4")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60" CssClass="bL"/>
                                <HeaderStyle CssClass="bT bL" />
                            </asp:TemplateField>
                            
                              <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDaysHraHead" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDays4" runat="server" Text='<%#Eval("Days4")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="30" />
                                <HeaderStyle CssClass="bT" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMonth5Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth5" runat="server" Text='<%#Eval("Month5")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60" CssClass="bL "/>
                                <HeaderStyle CssClass="bT bL" />
                            </asp:TemplateField>
                              <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDaysHraHead" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDays5" runat="server" Text='<%#Eval("Days5")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="30" />
                                <HeaderStyle CssClass="bT" />
                            </asp:TemplateField>
                            
                            
                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMonth6Head" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth6" runat="server" Text='<%#Eval("Month6")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60" CssClass="bL"/>
                                <HeaderStyle CssClass="bT bL" />
                            </asp:TemplateField>
                            
                              <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDaysHraHead" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDays6" runat="server" Text='<%#Eval("Days6")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="30" />
                                <HeaderStyle CssClass="bT" />
                            </asp:TemplateField>
                            
                          
                           <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblTotalHraHead" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("TotalHrs")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="60"  CssClass="bL" />
                                <HeaderStyle CssClass="bL bT" />
                            </asp:TemplateField>
                            
                              <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblDaysHraHead" runat="server" Text="Days"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDays" runat="server" Text='<%#Eval("Days")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" Width="30"  />
                                <HeaderStyle CssClass="bR bT" />
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--Loginedit popup start-->
    <cc1:ModalPopupExtender ID="mdlLoginPopUpEdit" BackgroundCssClass="popupHolder" runat="server"
        PopupControlID="LoginEditPopup" TargetControlID="hdnPopLogIn" CancelControlID="lnkEditClose">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPopLogIn" runat="server" />
    <div id="LoginEditPopup" runat="server" class="popContent" style="width: 400px; display: none">
        <h2>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblLoginPopName" runat="server"></asp:Label>
                    <asp:Label ID="lblLoginDay" runat="server" CssClass="right"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkEditClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="Label1" Text="Sign in time" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnEmpID" runat="server" />
                                <asp:HiddenField ID="hdnLogUserID" runat="server" />
                                <asp:HiddenField ID="hdnSignInTime" runat="server" />
                                <asp:HiddenField ID="hdnSchInTime" runat="server" />
                                <asp:HiddenField ID="hdnSignOutTimeHrs" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtSignIn" runat="server"></asp:TextBox>
                                <%--       <cc1:CalendarExtender runat="server" TargetControlID="txtSignIn" 
                                    DaysModeTitleFormat="MMMM, yyyy hh:mm" 
                                    TodaysDateFormat="MMMM d, yyyy  hh:mm tt">
                                </cc1:CalendarExtender>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnUpdateIn" runat="server" Text="Update" OnClick="btnUpdateIn_Click"
                                        OnClientClick="return ValidSignInTime();" CssClass="btn btn-danger" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelIn" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelIn" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="btnCancelIn" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancelIn_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--Loginedit popup end-->
    <!--Logoutedit popup start-->
    <cc1:ModalPopupExtender ID="mdlLogoutEditPopUp" BackgroundCssClass="popupHolder"
        runat="server" PopupControlID="LogoutEditPopup" CancelControlID="lnkEditOutClose"
        TargetControlID="hdnLogoutpopup">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnLogoutpopup" runat="server" />
    <div id="LogoutEditPopup" runat="server" class="popContent" style="width: 400px;
        display: none">
        <h2>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblLogOutPopName" runat="server"></asp:Label>
                    <asp:Label ID="lblLogoutDay" runat="server" CssClass="right"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="close">
                <asp:LinkButton ID="lnkEditOutClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="Label2" Text="Sign out time" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnLogoutLogUserID" runat="server" />
                                <asp:HiddenField ID="hdnSignoutTime" runat="server" />
                                <asp:HiddenField ID="hdnSchOutTime" runat="server" />
                                <asp:HiddenField ID="hdnSignInHrs" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtSignOut" runat="server"></asp:TextBox>
                                <%--  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSignOut">
                                </cc1:CalendarExtender>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="UpdatePaneA2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnUpdateOut" runat="server" Text="Update" OnClick="btnUpdateOut_Click"
                                        OnClientClick="return ValidSignOutTime();" CssClass="btn btn-danger" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCancleOut" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Button ID="btnCancleOut" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancleOut_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--Logoutedit popup end-->
    <!--Freeze confirmation alert popup start-->
    <cc1:ModalPopupExtender ID="mdlFeeze" runat="server" BackgroundCssClass="popupHolder"
        TargetControlID="hdnFeezedDat" PopupControlID="Alertpopup" CancelControlID="lnkFeezeClose">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnFeezedDat" runat="server" />
    <div id="Alertpopup" runat="server" class="popContent" style="width: 500px; display: none">
        <h2>
            Freeze alert <span class="close">
                <asp:LinkButton ID="lnkFeezeClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <strong>This operation will freeze attendance data until
                                    <asp:Label ID="lblFreezedate" runat="server" ForeColor="Blue"></asp:Label>.&nbsp;You
                                    will not able to do further modifications. Are you sure you want to proceed?
                                </strong>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 296px;">
                        <asp:Button ID="btnFreezeOk" runat="server" Text="OK" CssClass="btn btn-small btn-danger"
                            OnClick="btnFreezeOk_Click" />
                        &nbsp;
                        <asp:Button ID="btnFreezeCancle" runat="server" Text="Cancel" CssClass="btn btn-small btn-success"
                            OnClick="btnFreezeCancle_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--Successfull alert popup End-->
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
                        <asp:TextBox ID="txtOldpwd" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
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

   

    <!--  <script src="js/datetimepicker.js" type="text/javascript"></script> 
    <script src="js/overlibmws.js" type="text/javascript"></script>   
    -->

    <script src="js/jquery-ui.min.js" type="text/javascript"></script>
    
    <link href="css/tipsy.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.tipsy.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/jquery-ui-timepicker-addon.js"></script>

    <script type="text/javascript" src="js/jquery-ui-sliderAccess.js"></script>

    <script type="text/javascript" language="javascript">
        $(window).load(function(){
        $('#spinner').hide();
              $('.table1 tr:last-child').remove();
            $('.table2 tr:last-child').remove();
            // $('.table3 tr:last-child').remove();
            
        
            var start = 2;
            var totalRO = $(".table1 tr").length;
            var totalTD = $('.table1 tr:eq(1) th').length;
            //console.log(start, totalRO, totalTD)
            
            
           
            
           style1('table1')
           //style1('table2')
            
           
            
            // Counter
           
           /*
            var count2 = 0;
            var count3 = 0;
            var count7 = 0;
            var count8 = 0; 
            var count12 = 0;
            var count13 = 0;
            var count17 = 0;
            var count18 = 0;
            var count22 = 0;
            var count23 = 0;
            var count27 = 0;
            var count28 = 0;
            var count32 = 0;
            var count33 = 0;
           */
           
           var arr = [2,3,7,8,12,13,17,18,22,23,27,28,32,33];
           
           for(kk=0;kk<arr.length; kk++){
                //var eval('count'+arr[kk]) = 0;
                eval('var count'+arr[kk]+' = ' + 0 + ';');
           }
           
            
            $('.table1 tr').each(function(){
            
                for(kk=0; kk<arr.length; kk++){
                    var val1 = $.trim($(this).children('td:eq('+arr[kk]+')').children('span').text());
                    if(val1 != null && val1 != '' && val1  != ' '){                   
                        eval('count'+arr[kk]+'++');
                    } 
                }
            /*
                var val1 = $.trim($(this).children('td:eq(2)').children('span').text());
                if(val1 != null && val1 != '' && val1  != ' '){ 
                    count2++; 
                } 
                
                var val1 = $.trim($(this).children('td:eq(3)').children('span').text());
                if(val1 != null && val1 != '' && val1  != ' '){ 
                    count3++; 
                } 
                
                var val1 = $.trim($(this).children('td:eq(7)').children('span').text());
                if(val1 != null && val1 != '' && val1  != ' '){ 
                    count7++; 
                }  
                
                var val1 = $.trim($(this).children('td:eq(8)').children('span').text());
                if(val1 != null && val1 != '' && val1  != ' '){ 
                    count8++; 
                }   
                
                var val1 = $.trim($(this).children('td:eq(12)').children('span').text());
                if(val1 != null && val1 != '' && val1  != ' '){ 
                    count12++; 
                }  
                
                var val1 = $.trim($(this).children('td:eq(13)').children('span').text());
                if(val1 != null && val1 != '' && val1  != ' '){ 
                    count13++; 
                }  
                */     
            })
            
            var $lastRow = $('.table1 tr:last-child');
            //console.log('Hi');
            
            
            $lastRow.addClass('bold1')
            
            for(kk=0; kk<arr.length; kk++){
                $lastRow.children('td:eq('+arr[kk]+')').children('span').text( eval('count'+arr[kk]));
            }
            
            /*
            $lastRow.children('td:eq(2)').children('span').text(count2)
            $lastRow.children('td:eq(3)').children('span').text(count3);
            $lastRow.children('td:eq(7)').children('span').text(count7);
            $lastRow.children('td:eq(8)').children('span').text(count8);
            $lastRow.children('td:eq(12)').children('span').text(count12);
            $lastRow.children('td:eq(13)').children('span').text(count13);  
            $lastRow.children('td:eq(17)').children('span').text(count12);
            $lastRow.children('td:eq(18)').children('span').text(count13);  
            $lastRow.children('td:eq(22)').children('span').text(count12);
            $lastRow.children('td:eq(23)').children('span').text(count13);    
            $lastRow.children('td:eq(27)').children('span').text(count12);
            $lastRow.children('td:eq(28)').children('span').text(count13);  
            $lastRow.children('td:eq(32)').children('span').text(count12);
            $lastRow.children('td:eq(33)').children('span').text(count13);          
            */
            $lastRow.children('td').children('a').removeAttr('href');
            
            
            
            
        });
        
        $(function(){
        
            $('.tooltip2').tipsy({html: true, gravity:'sw' });
            
           $.widget("ui.tooltip", $.ui.tooltip, {
                options: {
                    content: function() {
                        return $(this).prop('title');
                    }
                }
            });

            $('[rel=tooltip]').tooltip({
                position: {
                    my: "center bottom-20",
                    at: "center top",
                    using: function(position, feedback) {
                        $(this).css(position);
                        $("<div>")
                             .addClass("arrow")
                             .addClass(feedback.vertical)
                             .addClass(feedback.horizontal)
                             .appendTo(this);
                    }
                }
            });
        })
          
            //$('#grdAttandence tr').last().children().css({ 'background':'#CCC'})
            // $('#grdAttendanceSingle tr').last().children().css({ 'background':'#CCC'})
        
        function style1(class1){
           // console.log(class1)
            
            
            
            
             for(i=2; i<9; i++){
                $('.table1 tr:eq(0) td:eq('+i+')').css({'border':'#888 2px solid'})
            } 
            
//            for(i=2; i<9; i++){
//                $('.table2 tr:eq(0) td:eq('+i+')').css({'border':'#888 2px solid'})
//            }    
                
            var totalRO = $('.'+class1+' tr').length;
            var totalTD = $('.'+class1+' tr:eq(1) th').length;
           //------------------------------------------------------------ 
           $('.table1 tr:eq(1) th:eq(0), .table1 tr:eq(1) th:eq(1)').css('border','2px solid #888')
           //$('.table1 tr td:eq(0), .table1 tr tdh:eq(1)')
           $('.table1 tr').each(function(){
                if($(this).index() != 0){
                    $(this).children('td:eq(0)').css({'border-left':'2px solid #888', 'border-right':'2px solid #888'})
                    $(this).children('td:eq(1)').css({'border-left':'2px solid #888', 'border-right':'2px solid #888'})
                }
                
                $(this).children('td:last-child').addClass('bR')
                $(this).children('th:last-child').addClass('bR')
                
           })
           $('.table1 tr:eq(0) td:eq(0)').addClass('bL bT')
           $('.table1 tr:eq(0) td:eq(1)').addClass(' bT bR')
           $('.table1 tr:eq(0) td:last-child').css('border','2px solid #888');
           //---------------------------------------------
           
           
           
            for(r=1; r<totalRO; r++){              
                
                            
                if(r==1){
                    start = 2;
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bL bT');
                    $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bL'); 
                    start++;
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bT');
                    start++;
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bT');
                    start++;
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bT');
                    start++;
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bT bR');
                    $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bR');
                    
                    start += 5             
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bR'); 
                    start += 5             
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bR');  
                    start += 5             
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bR'); 
                    start += 5             
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bR');  
                    start += 5             
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bR'); 
                    start += 5             
                    $('.'+class1+' tr:eq('+r+') th:eq('+start+')').addClass('bR'); 
                          
                }
                
                start = 2;  
                $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bL'); 
                start += 4             
                $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bR');  
                
                 
                start += 5             
                $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bR'); 
                start += 5             
                $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bR');  
                 start += 5             
                $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bR'); 
                start += 5             
                $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bR');  
                 start += 5             
                $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bR'); 
                start += 5             
                $('.'+class1+' tr:eq('+r+') td:eq('+start+')').addClass('bR');    
                
                
                
                         
            }
            var len = $('.table1 tr').length-1;
            
            $('.table1 tr:eq('+len+') td').css({'background':'#ddd', 'border-top':'#888 1px solid'});
            
             var len = $('.table3 tr').length-1;
            
            $('.table3 tr:eq('+len+') td').css({'background':'#ddd', 'border-top':'#888 2px solid', 'border-bottom':'#888 2px solid', 'font-weight':'bold'});
            $('.table3').css('border','#888 2px solid');
            
        }
        
        
       
        
        function showspinner()
        {
         $('#spinner').show();
         return true;
        }
        
        function pageLoad(){
        
            $('.tooltip2').tipsy({html: true, gravity:'sw' });
            //console.log('Time')
              $('#spinner').hide();
             $('#txtSignIn').timepicker({                    
	                timeFormat: "hh:mm TT"	                
                });
                
                 $('#txtSignOut').timepicker({                    
	                timeFormat: "hh:mm TT"	                
                });
                
            $('.table1 tr:last-child').remove();
            $('.table2 tr:last-child').remove();
            // $('.table3 tr:last-child').remove();
            //$('#grdAttandence tr').last().children().css({'background':'#CCC'})
            //$('#grdAttendanceSingle tr').last().children().css({ 'background':'#CCC'})
           style1('table1')
          // style1('table2')
           
           
           // Counter    start     
           
            var arr = [2,3,7,8,12,13,17,18,22,23,27,28,32,33];
           
           for(kk=0;kk<arr.length; kk++){                
                eval('var count'+arr[kk]+' = ' + 0 + ';');
           }
           
            
            $('.table1 tr').each(function(){
            
                for(kk=0; kk<arr.length; kk++){
                    var val1 = $.trim($(this).children('td:eq('+arr[kk]+')').children('span').text());
                    if(val1 != null && val1 != '' && val1  != ' '){                   
                        eval('count'+arr[kk]+'++');
                    } 
                }                
            })
            
            var $lastRow = $('.table1 tr:last-child');            
            
            $lastRow.addClass('bold1')
            
            for(kk=0; kk<arr.length; kk++){
                $lastRow.children('td:eq('+arr[kk]+')').children('span').text( eval('count'+arr[kk]));
            }           
           
            $lastRow.children('td').children('a').removeAttr('href');
            // Counter    End     
            
           
           
           $.widget("ui.tooltip", $.ui.tooltip, {
                options: {
                    content: function() {
                        return $(this).prop('title');
                    }
                }
            });

            $('[rel=tooltip]').tooltip({
                position: {
                    my: "center bottom-20",
                    at: "center top",
                    using: function(position, feedback) {
                        $(this).css(position);
                        $("<div>")
                             .addClass("arrow")
                             .addClass(feedback.vertical)
                             .addClass(feedback.horizontal)
                             .appendTo(this);
                    }
                }
            });
            
            
            
            
           
           
        }
        
        
   function ValidSignInTime()
    {
    debugger
     var valid=true;
        var tim1 = $.trim($('#txtSignIn').val()).split(':')
   
        var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
        tim1[1] = tim1[1].substring(0, 2);
         
        var current_tim = $.trim($('#hdnSchInTime').val()).split(':')
        var current_timAMPM = current_tim[1].substring(current_tim[1].length - 2, current_tim[1].length);
        current_tim[1]=current_tim[1].substring(0,2);
       
        var LogoutTim1= $.trim($('#hdnSignOutTimeHrs').val()).split(':')
      
        var LogoutTim1AMPM=LogoutTim1[1].substring(LogoutTim1[1].length-2,LogoutTim1[1].length);
        LogoutTim1[1]=LogoutTim1[1].substring(0,2);
       
        var Stl = tim1[0] * 3600 + tim1[1] * 60;
        var St2 = current_tim[0] * 3600 + current_tim[1] * 60;
        var St3=LogoutTim1[0]*3600+LogoutTim1[1]*60;
        
        if(tim1[0]<10&&(tim1AMPM.trim()=="PM"||tim1AMPM.trim()=="pm"||tim1AMPM.trim()=="Pm"))
        {
           Stl=Stl+12*3600;
        }
        
         if(LogoutTim1[0]&&(LogoutTim1AMPM.trim()=="PM"||LogoutTim1AMPM.trim()=="pm"||LogoutTim1AMPM.trim()=="Pm"))
        {
           St3=St3+12*3600;
        }
           if(current_tim[0]<10&&(current_timAMPM.trim()=="PM"||current_timAMPM.trim()=="pm"||current_timAMPM.trim()=="Pm"))
        {
           St2=St2+12*3600;
        }
        
        if((Stl < (St2-1200))&&(tim1AMPM==current_timAMPM)){
           var v=confirm('Sign in time is much earlier than schedule time...Are you sure to update..')
           if(!v)
           {
              $('#txtSignIn').focus();
              valid=false;
           }
           
        }
        
        if(Stl>St3)
        {
          alert('Signin time should be lessthan signout time');
          valid=false;
         // $('#txtSignIn').val('');
          $('#txtSignIn').focus();
        }
  
        return valid;
    }
    
        
        
         function ValidSignOutTime()
    {
    debugger
     var valid=true;
        var tim1 = $.trim($('#txtSignOut').val()).split(':')
   
        var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
        tim1[1] = tim1[1].substring(0, 2);
         
        var current_tim = $.trim($('#hdnSchOutTime').val()).split(':')
        var current_timAMPM = current_tim[1].substring(current_tim[1].length - 2, current_tim[1].length);
        current_tim[1]=current_tim[1].substring(0,2);
       
        var LogoutTim1= $.trim($('#hdnSignInHrs').val()).split(':')
      
        var LogoutTim1AMPM=LogoutTim1[1].substring(LogoutTim1[1].length-2,LogoutTim1[1].length);
        LogoutTim1[1]=LogoutTim1[1].substring(0,2);
       
        var Stl = tim1[0] * 3600 + tim1[1] * 60;
        var St2 = current_tim[0] * 3600 + current_tim[1] * 60;
        var St3=LogoutTim1[0]*3600+LogoutTim1[1]*60;
        
        if(tim1[0]&&(tim1AMPM.trim()=="PM"||tim1AMPM.trim()=="pm"||tim1AMPM.trim()=="Pm"))
        {
           Stl=Stl+12*3600;
        }
        
         if(LogoutTim1[0]<10&&(LogoutTim1AMPM.trim()=="PM"||LogoutTim1AMPM.trim()=="pm"||LogoutTim1AMPM.trim()=="Pm"))
        {
           St3=St3+12*3600;
        }
        
        if(current_tim[0]<10&&(current_timAMPM.trim()=="PM"||current_timAMPM.trim()=="pm"||current_timAMPM.trim()=="Pm"))
        {
           St2=St2+12*3600;
        }
        
        
        if((Stl < (St2-1200))&&(tim1AMPM==current_timAMPM)){
           var v=confirm('Sign out time is much earlier than schedule time...Are you sure to update..')
           if(!v)
           {
              $('#txtSignIn').focus();
              valid=false;
           }

        }
        
        if(Stl<St3)
        {
          alert('Signout time should be greaterthan signin time');
          valid=false;
         
          $('#txtSignOut').focus();
        }
  
        return valid;
    }
        
        
        
        
        
        
        
    </script>

</body>
</html>
