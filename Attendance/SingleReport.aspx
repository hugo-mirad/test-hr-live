<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleReport.aspx.cs" Inherits="Attendance.SingleReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <!-- <script src="js/overlibmws.js" type="text/javascript"></script>  -->
    <link rel="stylesheet" href="css/reset.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/UI.css" />
    <link rel="stylesheet" href="css/inputs.css" type="text/css" />
    <link href="css/admin.css" rel="stylesheet" type="text/css" />
    <!-- <link rel="stylesheet" href="css/style.css" type="text/css" />  -->
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
        .table2, .tableLeave td
        {
            border: #ccc 1px dotted;
            padding: 2px 4px;
            white-space: nowrap;
        }
        .table2 th, .table2 tr:first-child td, .tableLeave th, .tableLeave tr:first-child td
        {
            padding: 2px 4px;
            vertical-align: middle;
            background: #ccc;
            text-align: center;
        }
        .table2, .tableLeave
        {
            font-size: 13px;
        }
        .table2 tr:first-child td, .tableLeave tr:first-child td
        {
            /* border:#888 2px solid;*/
            font-weight: bold;
            font-size: 13px;
        }
        .table2, .tableLeave
        {
            border: #999 1px solid;
            border-collapse: collapse;
            width: 64%;
            margin-top: 2%;
            margin-left: 10px;
            margin-right: 25%;
            padding-left: 99px;
        }
        .table2 td span, .tableLeave td span
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
            border-left: #888 1px solid;
        }
        .bT, th.bT, td.bT
        {
            border-top: #888 1px solid;
        }
        .bR, th.bR, td.bR
        {
            border-right: #888 1px solid;
        }
        .bB, th.bB, td.bB
        {
            border-bottom: #888 1px solid;
        }
        /* Popup */.popupHolder
        {
            background: url(../images/popupBack.png) repeat;
        }
        .popupHolder2
        {
            background: url(../images/popupBack.png) repeat;
            z-index: 100002;
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

    <link href="css/tipsy.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery.tipsy.js" type="text/javascript"></script>

    <!--  <script src="js/datetimepicker.js" type="text/javascript"></script> 
    <script src="js/overlibmws.js" type="text/javascript"></script>   
    -->

    <script src="js/jquery-ui.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/jquery-ui-timepicker-addon.js"></script>

    <script type="text/javascript" src="js/jquery-ui-sliderAccess.js"></script>

    <script src="js/jquery.tools.min.js" type="text/javascript"></script>

    <script type="text/javascript">
       
      $(function(){
        var leaveDt=new Date($.trim($('#lblDate2').text()));
        
           $('#txtLeaveFrom').live('focus', function(){
             if($(this).hasClass('hasDatepicker')){
                $(this).datepicker( "destroy" );
                $(this).removeClass("hasDatepicker")
            }  
            
             $('#txtLeaveFrom').datepicker({	        
	            minDate: leaveDt,
	            maxDate: 90
            });            
            
        })       
        
       $('#txtLeaveFrom').live('change', function(){
            $('#txtLeaveTo').val('');           
       });
       $('#txtLeaveTo').live('focus', function(){
                 if($(this).hasClass('hasDatepicker')){
                    $(this).datepicker( "destroy" );
                    $(this).removeClass("hasDatepicker")
                 }  
                
                $('#txtLeaveTo').datepicker({	        
                    minDate: $('#txtLeaveFrom').val(),
                    maxDate: 90
                });
            
        }) 
});
      $(window).load(function(){
                   $('[rel=tooltip]').tooltip();
           $('.table2 tr:last-child').remove();
     })
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
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
                                        <li><a href="SingleReport.aspx">Attendance Report</a></li>
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
                                        <li>
                                            <asp:UpdatePanel ID="uplev" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkNewLeaveReq" Text="New Leave Request" runat="server" OnClick="lnkNewLeaveReq_Click"></asp:LinkButton>
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
                                    <b>--:--:-- AM </b><strong> (<asp:Label ID="lblTimeZoneName" runat="server"></asp:Label>)</strong>
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
                <asp:Label ID="lblWeekReport" CssClass="lbl" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </h2>
    <div style="display: inline-block;">
        <div style="float: left; width: 855px; padding: 5px;">
            <asp:UpdatePanel ID="updd" runat="server">
                <ContentTemplate>
                    <b>Report type</b>&nbsp;
                    <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged"
                        Visible="true">
                        <asp:ListItem Text="Weekly Report" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Monthly Report" Value="1"></asp:ListItem>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div style="float: right; padding-right: 250px;">
            <br />
            <asp:UpdatePanel ID="upLeave" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="lnkLeaveReq" Text="Show Leave Requests" runat="server" OnClick="lnkLeaveReq_Click"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="DvSingleRep" runat="server">
        <asp:UpdatePanel ID="upSingle" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblName" runat="server" Visible="false"></asp:Label>
                <div style="display: block" id="dvSingle" runat="server">
                    <asp:GridView ID="grdAttendanceSingle" runat="server" AutoGenerateColumns="false"
                        CssClass="table2" OnRowCreated="grdAttendanceSingle_RowCreated" OnRowDataBound="grdAttendanceSingle_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeadDay" runat="server" Text="Day"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDay" runat="server" Text='<%#Eval("Day")%>'></asp:Label>
                                    <asp:HiddenField ID="hdnLogUserID" runat="server" Value='<%#Eval("LogUserID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeadSchIn" runat="server" Text="Schedule"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblScIn" runat="server" Text='<%#Eval("SchIn")==""?"":Eval("SchIn")+"-"+Eval("SchOut")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeadIN" runat="server" Text="SignIn-Out"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSignIn" runat="server" Font-Underline="False" Text='<%#Eval("SignIn")%>'></asp:Label>
                                    <asp:Label ID="lblSignOut" runat="server" Font-Underline="False" Visible="false"
                                        Text='<%#Eval("SignOut")%>'></asp:Label>
                                    <asp:HiddenField ID="hdnMultiple" runat="server" Value='<%#Eval("Multiple")%>' />
                                    <asp:HiddenField ID="hdnSigninNotes" runat="server" Value='<%# objFun.ToProperHtml(DataBinder.Eval(Container.DataItem, "LoginNotes"))%>' />
                                    <asp:HiddenField ID="hdnSignInFlag" runat="server" Value='<%#Eval("LoginFlag")%>' />
                                    <asp:HiddenField ID="hdnLvStatus" runat="server" Value='<%#Eval("LvStatus")%>' />
                                    <%--    <asp:HiddenField ID="hdnSignOutNotes" runat="server" Value='<%#Eval("LogoutNotes")%>' />--%>
                                    <asp:HiddenField ID="hdnSignOutFlag" runat="server" Value='<%#Eval("LogoutFlag")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMonHeadHours" runat="server" Text="Hrs"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonHours" runat="server" Font-Underline="False" Text='<%#Eval("Hrs")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="margin: 10px;" id="dvMonthrep" runat="server">
                    <div class="picker calender">
                        <div class="header">
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left; width: 35%; padding-left: 10px;">
                                        <span id="lblMonthEmp" class="current" runat="server"></span>
                                    </td>
                                    <td style="text-align: center">
                                        <span id="lblMonth" class="current" runat="server"></span>
                                    </td>
                                    <td style="text-align: right; width: 35%; padding-right: 10px;">
                                        <span id="lblMonthHrs" class="current" runat="server"></span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:GridView ID="grdMonthlyRep" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdMonthlyRep_RowDataBound"
                            CssClass="table1 tblCal">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDaySun" runat="server" Text="Sun"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="calIn">
                                            <asp:Label ID="lblSun" runat="server" Text='<%#Eval("Sunday")%>' CssClass="dateSpan "></asp:Label>
                                            <asp:Label ID="lblSunSignIn" runat="server"></asp:Label>
                                            <asp:Label ID="lblSunHrs" runat="server" CssClass="hours"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDayMon" runat="server" Text="Mon"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="calIn">
                                            <asp:Label ID="lblMon" runat="server" Text='<%#Eval("Monday")%>' CssClass="dateSpan "></asp:Label>
                                            <asp:Label ID="lblMonSignIn" runat="server"></asp:Label>
                                            <asp:Label ID="lblMonHrs" runat="server" CssClass="hours"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDayTue" runat="server" Text="Tue"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="calIn">
                                            <asp:Label ID="lblTue" runat="server" Text='<%#Eval("Tuesday")%>' CssClass="dateSpan "></asp:Label>
                                            <asp:Label ID="lblTueSignIn" runat="server"></asp:Label>
                                            <asp:Label ID="lblTueHrs" runat="server" CssClass="hours"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDayWed" runat="server" Text="Wed"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="calIn">
                                            <asp:Label ID="lblWed" runat="server" Text='<%#Eval("Wednesday")%>' CssClass="dateSpan "></asp:Label>
                                            <asp:Label ID="lblWedSignIn" runat="server"></asp:Label>
                                            <asp:Label ID="lblWedHrs" runat="server" CssClass="hours"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDayThu" runat="server" Text="Thu"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="calIn">
                                            <asp:Label ID="lblThu" runat="server" Text='<%#Eval("Thursday")%>' CssClass="dateSpan "></asp:Label>
                                            <asp:Label ID="lblThuSignIn" runat="server"></asp:Label>
                                            <asp:Label ID="lblThuHrs" runat="server" CssClass="hours"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDayFri" runat="server" Text="Fri"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="calIn">
                                            <asp:Label ID="lblFri" runat="server" Text='<%#Eval("Friday")%>' CssClass="dateSpan "></asp:Label>
                                            <asp:Label ID="lblFriSignIn" runat="server"></asp:Label>
                                            <asp:Label ID="lblFriHrs" runat="server" CssClass="hours"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblDaySat" runat="server" Text="Sat"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="calIn">
                                            <asp:Label ID="lblSat" runat="server" Text='<%#Eval("Saturday")%>' CssClass="dateSpan "></asp:Label>
                                            <asp:Label ID="lblSatSignIn" runat="server"></asp:Label>
                                            <asp:Label ID="lblSatHrs" runat="server" CssClass="hours"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <asp:GridView ID="grdSingleLeaveReq" runat="server" AutoGenerateColumns="false" CssClass="tableLeave"
                    OnRowDataBound="grdSingleLeaveReq_RowDataBound">
                    <Columns>
                        <asp:TemplateField SortExpression="empid" HeaderText="EmpID">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("empid")%>' CommandName="user"></asp:Label>
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
            </ContentTemplate>
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
            <asp:UpdatePanel ID="uppwddv" runat="server">
                <ContentTemplate>
                    <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                        <tr>
                            <td>
                                Old password
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
    <!--New leave request start-->
    <cc1:ModalPopupExtender ID="mdlNewLeaveRequest" BackgroundCssClass="popupHolder"
        runat="server" PopupControlID="NewLeaveRequest" TargetControlID="hdnNewleaveReq"
        CancelControlID="lnkLeaveClose">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnNewleaveReq" runat="server" />
    <div id="NewLeaveRequest" runat="server" class="popContent" style="width: 400px;
        display: none">
        <h2>
            New Leave Request <span class="close">
                <asp:LinkButton ID="lnkLeaveClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <asp:UpdatePanel ID="upNewl" runat="server">
                <ContentTemplate>
                    <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                        <tr>
                            <td>
                                From <span class="must">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLeaveFrom" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                To <span class="must">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLeaveTo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Reason <span class="must">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <div style="display: inline-block">
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnLeaveReqSubmit" runat="server" Text="Submit" OnClientClick="return validLeaveReq();"
                                                CssClass="btn btn-danger" OnClick="btnLeaveReqSubmit_Click" />
                                            <br />
                                            <asp:Label ID="lblLeaveError" runat="server" ForeColor="Red"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <input type="button" class="btn" value="Cancel" name="btnLvCancel" onclick="return hideNewleave();" />
                                <br />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--New leave request popup end-->
    </form>

    <script type="text/javascript">
      function pageLoad()
    {
      $('[rel=tooltip]').tooltip();
    
            $('.table2 tr:last-child').remove();
//             $('.atnHoliday').each(function(){
//                $(this).prev().removeAttr('class').addClass('atnHoliday')
//                $(this).next().removeAttr('class').addClass('atnHoliday')
//           });
//           
//           $('.atnSun').each(function(){
//                $(this).prev().removeAttr('class').addClass('atnSun')
//                $(this).next().removeAttr('class').addClass('atnSun')
//           });
//           
//           $('.atnUnLeave').each(function(){
//                $(this).prev().removeAttr('class').addClass('atnUnLeave')
//                $(this).next().removeAttr('class').addClass('atnUnLeave')
//           });
//           
//           $('.atnEdit').each(function(){
//                $(this).prev().removeAttr('class').addClass('atnEdit')
//                $(this).next().removeAttr('class').addClass('atnEdit')
//           });
//           
//           $('.atnLeave').each(function(){
//                $(this).prev().removeAttr('class').addClass('atnLeave')
//                $(this).next().removeAttr('class').addClass('atnLeave')
//           });
     }
            
  
    
      function hideNewleave()
      {
         $find('mdlNewLeaveRequest').hide();
      }
      
       function validLeaveReq()
      {
      
       if($.trim($('#txtLeaveFrom').val()).length<=0)
       {
         alert('Please select from date');
             $('#txtLeaveFrom').focus();
             return false;
       }
        if($.trim($('#txtLeaveTo').val()).length<=0)
       {
         alert('Please select to date');
         $('#txtLeaveTo').focus();
          return false;
       }
       if($.trim($('#txtReason').val()).length<=0)
       {
         alert('Please enter reason');
        $('#txtReason').focus();
         return false;
       }
       return true;
      }
      
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
 
    </script>

</body>
</html>
