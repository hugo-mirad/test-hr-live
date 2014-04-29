<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Attendance._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/reset.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/UI.css" />
    <link rel="stylesheet" href="css/inputs.css" type="text/css" />
    <link rel="stylesheet" href="css/style.css" type="text/css" />

    <!-- 
<link href='http://fonts.googleapis.com/css?family=Aldrich' rel='stylesheet' type='text/css'>
<link href='http://fonts.googleapis.com/css?family=Wallpoet' rel='stylesheet' type='text/css'>
-->
    <style>
        h2 .close
        {
            float: right;
            text-decoration: none;
            font-size: 20px;
            color: Red;
            width: 24px;
            height: 24px;
        }
        h2 .close a
        {
            font-weight: normal;
            text-decoration: none;
            font-size: 25px;
            color: Red;
            width: 24px;
            height: 24px;
            display: block;
            background: url(../images/close.png) 0 0 no-repeat;
        }

        /* css for timepicker */.ui-datepicker .ui-datepicker
        {
            margin-bottom: 8px;
        }
        .ui-datepicker dl
        {
            text-align: left;
        }
        .ui-datepicker dl dt
        {
            float: left;
            clear: left;
            padding: 0 0 0 5px;
        }
        .ui-datepicker dl dd
        {
            margin: 0 10px 10px 45%;
        }
        .ui-datepicker td
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
        .ui-datepicker-rtl
        {
            direction: rtl;
        }
        .ui-datepicker-rtl dl
        {
            text-align: right;
            padding: 0 5px 0 0;
        }
        .ui-datepicker-rtl dl dt
        {
            float: right;
            clear: right;
        }
        .ui-datepicker-rtl dl dd
        {
            margin: 0 45% 10px 10px;
        }
       
       .ui-datepicker span{ width:auto; min-width:16px;;  }
       
       
       .ui-widget-content.ui-datepicker {
	        border: 1px solid #aaaaaa;
	        background: #ffffff url(images/ui-bg_flat_75_ffffff_40x100.png) 50% 50% repeat-x;
	        color: #222222;
        }
       
        
    </style>

    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>

    <script type="text/javascript">
        var BrowserDetect = {
            init: function() {
                this.browser = this.searchString(this.dataBrowser) || "An unknown browser";

                this.version = this.searchVersion(navigator.userAgent)
			|| this.searchVersion(navigator.appVersion)
			|| "an unknown version";
                this.OS = this.searchString(this.dataOS) || "an unknown OS";
            },
            searchString: function(data) {
                for (var i = 0; i < data.length; i++) {
                    var dataString = data[i].string;
                    var dataProp = data[i].prop;
                    this.versionSearchString = data[i].versionSearch || data[i].identity;
                    if (dataString) {
                        if (dataString.indexOf(data[i].subString) != -1)
                            return data[i].identity;
                    }
                    else if (dataProp)
                        return data[i].identity;
                }
            },
            searchVersion: function(dataString) {
                var index = dataString.indexOf(this.versionSearchString);
                if (index == -1) return;
                return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
            },
            dataBrowser: [
		{
		    string: navigator.userAgent,
		    subString: "Chrome",
		    identity: "Chrome"
		},
		{ string: navigator.userAgent,
		    subString: "OmniWeb",
		    versionSearch: "OmniWeb/",
		    identity: "OmniWeb"
		},
		{
		    string: navigator.vendor,
		    subString: "Apple",
		    identity: "Safari",
		    versionSearch: "Version"
		},
		{
		    prop: window.opera,
		    identity: "Opera",
		    versionSearch: "Version"
		},
		{
		    string: navigator.vendor,
		    subString: "iCab",
		    identity: "iCab"
		},
		{
		    string: navigator.vendor,
		    subString: "KDE",
		    identity: "Konqueror"
		},
		{
		    string: navigator.userAgent,
		    subString: "Firefox",
		    identity: "Firefox"
		},
		{
		    string: navigator.vendor,
		    subString: "Camino",
		    identity: "Camino"
		},
		{		// for newer Netscapes (6+)
		    string: navigator.userAgent,
		    subString: "Netscape",
		    identity: "Netscape"
		},
		{
		    string: navigator.userAgent,
		    subString: "MSIE",
		    identity: "Explorer",
		    versionSearch: "MSIE"
		},
		{
		    string: navigator.userAgent,
		    subString: "Gecko",
		    identity: "Mozilla",
		    versionSearch: "rv"
		},
		{ 		// for older Netscapes (4-)
		    string: navigator.userAgent,
		    subString: "Mozilla",
		    identity: "Netscape",
		    versionSearch: "Mozilla"
		}
	        ],
            dataOS: [
		{
		    string: navigator.platform,
		    subString: "Win",
		    identity: "Windows"
		},
		{
		    string: navigator.platform,
		    subString: "Mac",
		    identity: "Mac"
		},
		{
		    string: navigator.userAgent,
		    subString: "iPhone",
		    identity: "iPhone/iPod"
		},
		{
		    string: navigator.platform,
		    subString: "Linux",
		    identity: "Linux"
		}
	]

        };
        BrowserDetect.init();


        var Browser = BrowserDetect.browser;
        //alert(BrowserDetect.version)


        // alert($('#hdnBversionC').val())

        if (Browser != 'Firefox' && Browser != 'Chrome') {

            if ($.browser.msie) {
                document.execCommand("Stop");
            } else {
                window.stop(); //works in all browsers but IE
            }

            alert('Use Mozilla Firefox or Google Chrome web browser only..!')
        }


        if (Browser == 'Firefox' && 24 > BrowserDetect.version) {
            // alert('Firefox');
            if ($.browser.msie) {
                document.execCommand("Stop");
            } else {
                window.stop(); //works in all browsers but IE
            }

            alert('You are using old version of Mozilla Firefox..! ')
        }
        if (Browser == 'Chrome' && 29 > BrowserDetect.version) {
            //  alert('Chrome');
            if ($.browser.msie) {
                document.execCommand("Stop");
            } else {
                window.stop(); //works in all browsers but IE
            }

            alert('You are using old version of Google Chrome..! ')
        }


        // Chrome

        function stopLoading() {
            //  alert('Stop')
            $('body').empty();
            if ($.browser.msie) {
                document.execCommand("Stop");
            } else {
                window.stop(); //works in all browsers but IE   
            }
        }
        
        
       //Managerpopup- Start
        function validateSubmit() {
        //debugger
          var valid=true;



          if (document.getElementById('txtUserIdM').value.trim().length < 1)
           {
               alert("Please enter the valid User Id.");               
               valid=false;
               document.getElementById("txtUserIdM").value = "";
               document.getElementById("txtUserIdM").focus();
           }

           else if (document.getElementById('txtLocationM').value.trim().length < 1) {
               alert("Please enter the valid location Code.");
               valid = false;
               document.getElementById("txtLocationM").value = "";
               document.getElementById("txtLocationM").focus();
           }
           else if (document.getElementById('txtPasswordM').value.length < 1)
           {
               alert("Please enter the password .");               
               valid=false;
               document.getElementById("txtPasswordM").focus();
           }
               
           return valid;
       }

       //Managerpopup- End
        
        
     function showLeaveSuccess()
     {
       alert('Leave request updated successfully');
       $find('mdlApplyingLeave').hide();
       location.reload();
     }

   
    </script>

    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    
    

    <script type="text/javascript" src="js/main.js"></script>
    
    

</head>
<body>
    <form id="form1" runat="server" class="h100p">
    <asp:HiddenField runat="server" ID="hdnBversionC" />
    <asp:HiddenField runat="server" ID="hdnBversionM" />
    <asp:HiddenField runat="server" ID="hdnRtime" />
    <div class="dummyPopup">
        &nbsp;</div>
    <cc1:ToolkitScriptManager ID="scp1" runat="server">
    </cc1:ToolkitScriptManager>
    <!-- Headder Start  -->
    <div class="wid100p">
        <div class="headder">
            <a href="#" class="logo">
                <asp:Label ID="comanyname" runat="server" ForeColor="White"></asp:Label>
                <asp:Label ID="lblLocation" runat="server"></asp:Label>
            </a>
            
            <div class="shifts">
              <asp:DropDownList ID="ddlShifts" runat="server" 
                    onselectedindexchanged="ddlShifts_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="tDate">
                <%--  <asp:Label ID="lblPrevTime" runat="server" Text=""></asp:Label>--%>
                <span class="cDate" style="margin-bottom: 1px; margin-top: 2px;"></span>
                <div class="cTime">
                    <b>--:--:-- AM </b><strong> (<asp:Label ID="lblTimeZoneName" runat="server"></asp:Label>)</strong>
                    <a href="javascript:location.reload();" class="refresh btn btn-small btn-mini btn-success">
                        Refresh</a></div>
            </div>
           
            <div style="display: inline-block; float: right; color: #AAA; font-size: 10px; background: #444;
                padding: 2px 5px; margin: 22px 14px 0 0;">
                Last Update: <span style="font-size: 10px; color: #55B355; display: inline-bolck;"
                    id="rTime"></span>
            </div>
             
            <div style="display: none">
                <asp:Label ID="lblDate" runat="server"></asp:Label>
                <asp:Label ID="lblDate2" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <!-- Headder End  -->
    <!-- Content Start  -->
    <div class="wid100p" style="height: 92%; min-height: 92%;">
        <div style="margin: 1%; width: 98%; height: 98%; min-width: 98%;">
            <table class="totalHolder">
                <tr>
                    <td style="vertical-align: top;">
                        <!-- Left Total Users List Start  -->
                        <div class="bor boxC1" style="margin: 0 5px; height:auto; max-height:auto; min-height:auto   ">
                            <h2 class="one" style="background: #fff; color: #2286c1; border-bottom: #2286c1 1px solid;">
                                SCHEDULED <span>(<b></b>)</span></h2>
                            <div class="inner ">
                                <asp:Repeater ID="rpEmp" runat="server" OnItemDataBound="rpEmp_ItemDataBound">
                                    <HeaderTemplate>
                                        <ul class="users" id="origin">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="user" rel="tooltip">
                                            <asp:Image ID="imgPicture" runat="server" oncontextmenu="return false" ImageUrl='<%#Eval("PhotoLink") %>' />
                                            <%-- <div style="z-index:1000" >
                                          <asp:Label  ID="lblFirstName" runat="server" style="text-align:center" Text='<%#Eval("FirstName")%>'></asp:Label>                                     
                                         </div>--%>
                                            <asp:HiddenField ID="hdnFirstName" runat="server" Value='<%#String.Concat(Eval("FirstName"),"  ",Eval("LastName").ToString()) %>' />
                                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%#Eval("UserID") %>' />
                                            <asp:HiddenField ID="hdnStartTime1" runat="server" Value='<%#Eval("StartTime") %>' />
                                            <asp:HiddenField ID="hdnEndTime1" runat="server" Value='<%#Eval("EndTime") %>' />
                                            <h3 style="display: none">
                                                <asp:Label ID="hdnDeptName1" runat="server" Text='<%#Eval("DeptName") %>'></asp:Label>
                                                <asp:Label ID="hdnDesignation" CssClass="degi" runat="server" Value='<%#Eval("designation") %>'></asp:Label>
                                            </h3>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <div class="clear">
                                    &nbsp;</div>
                            </div>
                            <div class="clear">
                                &nbsp;</div>
                        </div>
                        <!-- Left Total Users List End  -->
                        <!-- Left Leave Users Start  -->
                      <%--  <div style="display:none">--%>
                        <div class="bor boxC4" style="margin: 10px 5px 0 5px; ">
                            <h2 class="four" style="background: #fff; color: #888; border-bottom: #888 1px solid;">
                                On Leave <span>(<b></b>)</span></h2>
                            <div class="inner ">
                               <asp:Repeater ID="rpLeave" runat="server" OnItemDataBound="rpEmp_ItemDataBound" >
                                    <HeaderTemplate>
                                       <ul class="users ui-droppable" id="drop3">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="user" rel="tooltip">
                                            <asp:Image ID="imgPicture" runat="server" oncontextmenu="return false" ImageUrl='<%#Eval("PhotoLink") %>' />
                                            <%-- <div style="z-index:1000" >
                                          <asp:Label  ID="lblFirstName" runat="server" style="text-align:center" Text='<%#Eval("FirstName")%>'></asp:Label>                                     
                                         </div>--%>
                                            <asp:HiddenField ID="hdnFirstName" runat="server" Value='<%#String.Concat(Eval("FirstName"),"  ",Eval("LastName").ToString()) %>' />
                                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%#Eval("UserID") %>' />
                                            <asp:HiddenField ID="hdnStartTime1" runat="server" Value='<%#Eval("StartTime") %>' />
                                            <asp:HiddenField ID="hdnEndTime1" runat="server" Value='<%#Eval("EndTime") %>' />
                                            <asp:HiddenField ID="hdnFromDt" runat="server" Value='<%# Bind("Fromdate", "{0:MM/dd/yyyy}") %>' />
                                            <asp:HiddenField ID="hdnTodt" runat="server" Value='<%# Bind("Todate", "{0:MM/dd/yyyy}") %>'/>
                                            <asp:HiddenField ID="hdnApprovedStatus" runat="server" Value='<%#Eval("Status") %>' />
                                            <h3 style="display: none">
                                                <asp:Label ID="hdnDeptName1" runat="server" Text='<%#Eval("DeptName") %>'></asp:Label>
                                                <asp:Label ID="hdnDesignation" CssClass="degi" runat="server" Value='<%#Eval("designation") %>'></asp:Label>
                                            </h3>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <div class="clear">
                                    &nbsp;</div>
                            </div>
                            <div class="clear">
                                &nbsp;</div>
                        </div>
                      <%--  </div>--%>
                        <!-- Left Leave Users End  -->
                    </td>
                    <td style="vertical-align: top;">
                        <!-- Mid Users List Start  -->
                        <div class="bor boxC2" style="margin: 0 5px;">
                            <h2 class="two" style="background: #fff; color: #82c621; border-bottom: #82c621 1px solid;">
                                Signed in <span>(<b></b>)</span></h2>
                            <div class="inner ">
                                <asp:Repeater ID="rpLogin" runat="server" OnItemDataBound="rpLogin_ItemDataBound">
                                    <HeaderTemplate>
                                        <ul class="users" id="drop1">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="user" rel="tooltip">
                                            <%--login time--%>
                                            <asp:Image ID="imgPicture1" runat="server" ImageUrl='<%#Eval("PhotoLink") %>' />
                                            <asp:HiddenField ID="hdnFirstName1" runat="server" Value='<%#String.Concat(Eval("FirstName"),"  ",Eval("LastName").ToString()) %>' />
                                            <asp:HiddenField ID="hdnUserID1" runat="server" Value='<%#Eval("UserID") %>' />
                                            <asp:HiddenField ID="hdnStartTime2" runat="server" Value='<%#Eval("StartTime") %>' />
                                            <asp:HiddenField ID="hdnEndTime2" runat="server" Value='<%#Eval("EndTime") %>' />
                                            <asp:HiddenField ID="hdnLogin" runat="server" Value='<%#Eval("LoginDate") %>' />
                                            <asp:HiddenField ID="hdnUserLogID" runat="server" Value='<%#Eval("LogUserID") %>' />
                                            <h3 style="display: none">
                                                <asp:Label ID="hdnDeptName2" runat="server" Text='<%#Eval("DeptName") %>'></asp:Label>
                                                <asp:Label ID="hdnDesignationLogin" CssClass="degi" runat="server" Value='<%#Eval("designation")%>'></asp:Label>
                                            </h3>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clear">
                                &nbsp;</div>
                        </div>
                        <!-- Mid Users List End  -->
                    </td>
                    <td style="vertical-align: top;">
                        <!-- Right Users List Start  -->
                        <div class="bor boxC3" style="margin: 0 5px;">
                            <h2 class="three" style="background: #fff; color: #ee8d1a; border-bottom: #ee8d1a 1px solid;">
                                Signed out <span>(<b></b>)</span></h2>
                            <div class="inner  ">
                                <asp:Repeater ID="rplogout" runat="server" OnItemDataBound="rplogout_ItemDataBound">
                                    <HeaderTemplate>
                                        <ul class="users drop3" id="drop2">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="user" rel="tooltip">
                                            <asp:Image ID="imgPicture2" runat="server" ImageUrl='<%#Eval("PhotoLink") %>' />
                                            <asp:HiddenField ID="hdnFirstName2" runat="server" Value='<%#String.Concat(Eval("FirstName"),"  ",Eval("LastName").ToString()) %>' />
                                            <asp:HiddenField ID="hdnUserID2" runat="server" Value='<%#Eval("UserID") %>' />
                                            <asp:HiddenField ID="hdnStartTime3" runat="server" Value='<%#Eval("StartTime") %>' />
                                            <asp:HiddenField ID="hdnEndTime3" runat="server" Value='<%#Eval("EndTime") %>' />
                                            <asp:HiddenField ID="hdnLoginout" runat="server" Value='<%#Eval("LoginDate") %>' />
                                            <asp:HiddenField ID="hdnLogout" runat="server" Value='<%#Eval("LogoutDate") %>' />
                                            <h3 style="display: none">
                                                <asp:Label ID="hdnDeptName3" runat="server" Text='<%#Eval("DeptName") %>'></asp:Label>
                                                <asp:Label ID="hdnDesignationLogout" CssClass="degi" runat="server" Value='<%#Eval("designation")%>'></asp:Label>
                                            </h3>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clear">
                                &nbsp;</div>
                        </div>
                        <!-- Right Users List End  -->
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Contrnt End -->
    <!-- Login Popup Start  -->
    <cc1:ModalPopupExtender runat="server" BackgroundCssClass="popupHolder" ID="mdlLoginpopup"
        TargetControlID="hdnLogin" PopupControlID="loginPopup" CancelControlID="cancel">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnLogin" runat="server" />
    <div class="popContent" runat="server" id="loginPopup" style="width: 450px; display: none">
        <h2>
            <asp:Label Text="Signing in for" runat="server"></asp:Label>
            <asp:Label ID="lblLIName" runat="server"></asp:Label>
            <span class="close">
                <asp:LinkButton ID="lnkClose" runat="server" OnClick="lnkClose_Click"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td style="width: 110px; vertical-align: top;">
                        <img src="images/defaultUSer.jpg" class="userThumb" style="margin-bottom: 10px" />
                        Passcode <span class="must">*</span><br />
                        <input type="password" id="userPass" runat="server" />
                        <div style="display: none">
                            <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <table style="border-collapse: collapse;" class="loginForm">
                            <tr>
                                <td>
                                    Sign In Notes <i>(Optional)</i><br />
                                    <asp:TextBox ID="txtNpte" runat="server" TextMode="MultiLine" Style="height: 107px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="display: inline-block">
                                        <asp:UpdatePanel ID="Updt1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="subm" runat="server" Text="Sign in" class="btn btn-danger" OnClick="subm_Click" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="subm" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="lnkClose" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    &nbsp;
                                    <input id="cancel" type="button" value="Cancel" class="btn cancel" />
                                </td>
                            </tr>
                            <div style="display: none">
                                <asp:TextBox ID="ScLIStartTime" runat="server"> </asp:TextBox>
                                <asp:TextBox ID="ScLIEndTime" runat="server"></asp:TextBox>
                                <asp:TextBox ID="ScResignIn" runat="server"></asp:TextBox>
                            </div>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblLIError" runat="server" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="mdlLogoutPopup" BackgroundCssClass="popupHolder" runat="server"
        PopupControlID="logoutPopup" CancelControlID="cancel2" TargetControlID="hdnPopLogout">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPopLogout" runat="server" />
    <div id="logoutPopup" runat="server" class="popContent" style="width: 450px; display: none">
        <h2>
            <asp:Label Text="Signing out for" runat="server"></asp:Label>
            <asp:Label ID="lblLOName" runat="server"></asp:Label>
            <span class="close">
                <asp:LinkButton ID="lnkLOClose" runat="server" OnClick="lnkLOClose_Click"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td style="width: 110px; vertical-align: top;">
                        <img src="images/defaultUSer.jpg" class="userThumb" style="margin-bottom: 10px" />
                        Passcode <span class="must">*</span><br />
                        <input type="password" id="userPass2" runat="server" />
                        <div style="display: none">
                            <asp:TextBox ID="txtUserID2" runat="server"></asp:TextBox></div>
                    </td>
                    <td style="vertical-align: top">
                        <table style="border-collapse: collapse;" class="loginForm">
                            <tr>
                                <td style="padding: 0">
                                    Sign Out Notes <i>(Optional)</i><br />
                                    <asp:TextBox ID="txtNpte2" runat="server" TextMode="MultiLine" Style="height: 120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="display: inline-block">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="logout" runat="server" Text="Sign out" Enabled="true" class="btn btn-danger"
                                                    OnClick="logout_Click" />
                                                <div style="display: none">
                                                    <asp:TextBox ID="loginID" runat="server"> </asp:TextBox>
                                                    <asp:TextBox ID="logintin1" runat="server"> </asp:TextBox>
                                                    <asp:TextBox ID="ScLOStartTime" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="ScLOEndTime" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="Duplicate" runat="server"> </asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    &nbsp;
                                    <input id="cancel2" type="button" value="Cancel" class="btn" runat="server" />
                                </td>
                            </tr>
                            <asp:HiddenField ID="hdnLogoutUserID" runat="server" />
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="up2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblLOError" runat="server" ForeColor="red"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Login Again -->
    <!--Managerpopup start -->
    <cc1:ModalPopupExtender ID="mdlManagerPopup1" BackgroundCssClass="popupHolder" runat="server"
        PopupControlID="managerPopup1" CancelControlID="cancel3" TargetControlID="hdnPopManager">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPopManager" runat="server" />
    <div id="managerPopup1" runat="server" class="popContent" style="width: 400px; display: none">
        <h2>
            <asp:Label ID="Label4" Text="Signing out for" runat="server"></asp:Label>
            <asp:Label ID="Label5" runat="server"></asp:Label>
            <span class="close">
                <asp:LinkButton ID="lnkMNClose" runat="server" OnClick="lnkMNClose_Click"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td style="width: 70px; vertical-align: top;">
                        <img src="images/defaultUSer.jpg" class="userThumb" />
                    </td>
                    <td style="vertical-align: top">
                        <table style="border-collapse: collapse;" class="loginForm">
                            <tr>
                                <td>
                                    Emp ID <span class="must">*</span><br />
                                    <asp:TextBox ID="txtUserIdM" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Password <span class="must">*</span><br />
                                    <asp:TextBox ID="txtPasswordM" TextMode="Password" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Location Code<span class="must">*</span><br />
                                    <asp:TextBox ID="txtLocationM" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblErrorM" runat="server" ForeColor="red"></asp:Label>
                                            <asp:HiddenField ID="hdnManageUserID" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="display: inline-block">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnManager" runat="server" Text="Submit" Enabled="true" class="btn btn-danger"
                                                    OnClick="btnManager_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    &nbsp;
                                    <input id="cancel3" type="button" value="Cancel" class="btn" runat="server" />
                                </td>
                            </tr>
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
      <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel4"
        DisplayAfter="0">
        <ProgressTemplate>
            <div id="spinner">
                <h4>
                    <div>
                        Processing
                        <img src="images/loading.gif" />
                    </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    
    <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="UpdatePanel6"
        DisplayAfter="0">
        <ProgressTemplate>
            <div id="spinner">
                <h4>
                    <div>
                        Processing
                        <img src="images/loading.gif" />
                    </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Updt1"
        DisplayAfter="0">
        <ProgressTemplate>
            <div id="spinner">
                <h4>
                    <div>
                        Processing
                        <img src="images/loading.gif" />
                    </div>
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
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!---Applying for leave start-->
    <cc1:ModalPopupExtender ID="mdlApplyingLeave" BackgroundCssClass="popupHolder" runat="server"
        PopupControlID="dvApplyingLeave" CancelControlID="lnkleaveClose" TargetControlID="hdnLeavePop">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnLeavePop" runat="server" />
    <div id="dvApplyingLeave" runat="server" class="popContent" style="width: 450px;
        display: none">
        <h2>
            <asp:Label ID="Label1" Text="Leave Application for" runat="server"></asp:Label>
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <span class="close">
                <asp:LinkButton ID="lnkleaveClose" runat="server"></asp:LinkButton></span>
        </h2>
        <div class="inner">
            <table style="width: 97%; margin: 20px 5px; border-collapse: collapse;">
                <tr>
                    <td style="width: 45%; vertical-align: top;">
                        <table style="border-collapse: collapse;" class="loginForm" width="100%">
                            <tr>
                                <td>
                                    <img src="images/defaultUSer.jpg" class="userThumb" style="width: 110px;" />
                                </td>
                            </tr>
                             <tr ><%--id="leaveEmp" runat="server" style="display: none;">--%>
                                <td style="vertical-align: top">
                                <span style="color:#A0132C;font-size: 14px;">Submitted by</span>
                                <br />
                                    <asp:Label ID="lblLeaveEmp" runat="server" Text="My EmpID  <span class='must'>*</span> "></asp:Label>
                                       
                                    <br />
                                    <asp:TextBox ID="txtLeaveEmpID" runat="server"  Style="width: 150px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    My Pass code<span class="must">*</span> <br />
                                    <asp:TextBox ID="txtLeavePassCode" runat="server" TextMode="Password" Style="width: 150px;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2%;">
                        &nbsp;
                    </td>
                    <td style="vertical-align: top">
                        <table style="border-collapse: collapse;" class="loginForm">
                            <tr>
                                <td style="vertical-align: top">
                                    <%--<div style="float: right; padding-right: 12px;">
                                        <label class="rdLabel"><input type="radio" id="rdSelf" name="LeaveIdentity" runat="server" value="Self" checked="true"  />Self</label>
                                        <label class="rdLabel"><input type="radio" id="rdOther" name="LeaveIdentity" runat="server" value="Other" />Other</label>
                                    </div>--%>
                                    <%--<br />--%>
                                    From date <span class="must">*</span><br />
                                    <asp:TextBox ID="txtFromDt" runat="server" Style="width: 223px;"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    To date <span class="must">*</span><br />
                                    <asp:TextBox ID="txtToDt" runat="server" Style="width: 223px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Reason <span class="must">*</span><br />
                                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="7"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblLeaveError" runat="server" ForeColor="red"></asp:Label>
                                            <asp:HiddenField ID="hdnLeaveUserID" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                  
                    <td>
                        <div style="display: inline-block">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="LeaveSubmit" runat="server" Text="Submit" Enabled="true" class="btn btn-danger" OnClientClick="return validLeave();"
                                        OnClick="LeaveSubmit_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        &nbsp;
                        <input id="LeaveCancel" type="button" value="Cancel" class="btn" runat="server" />
                    </td>
                </tr>
                        </table>
                    </td>
                </tr>
                
            </table>
        </div>
    </div>
    <!---Applying for leave end-->

    <script type="text/javascript" language="javascript">
    
    var se = new Date($.trim($('#lblDate2').text()));
   var leaveSe=new Date($.trim($('#lblDate2').text()));
   
    var month=new Array();
    month[0]="Jan";
    month[1]="Feb";
    month[2]="Mar";
    month[3]="Apr";
    month[4]="May";
    month[5]="Jun";
    month[6]="Jul";
    month[7]="Aug";
    month[8]="Sep";
    month[9]="Oct";
    month[10]="Nov";
    month[11]="Dec";
    var mon = month[se.getMonth()]; 
    
    var dayname=new Array();
    dayname[0]="Sun";
    dayname[1]="Mon";
    dayname[2]="Tue";
    dayname[3]="Wed";
    dayname[4]="Thu";
    dayname[5]="Fri";
    dayname[6]="Sat";
    
       
    var day = se.getDate();
    var dayee=dayname[se.getDay()];
    var year = se.getFullYear();
    bindDate();
    
    function bindDate(){
        $('.cDate').text('Date: '+day+' '+mon+', '+year+' '+dayee);
    }
    
    
    //var serverTime2 = <%= DateTime.Now.TimeOfDay.TotalSeconds %>;
    
    //console.log(serverTime2);
   // var serverTime = <% %>$.trim($('#lblDate').text());
    
    var serverTime  = parseInt($.trim($('#lblDate').text()));
   //console.log(serverTime);
   //console.log(getClientTime());
    var serverOffset = serverTime - getClientTime();

    function getClientTime()
    {
        var time = new Date($.trim($('#lblDate2').text()));
       // console.log(time.getMinutes());
        return (time.getHours() * 60 * 60) + (time.getMinutes() * 60) + (time.getSeconds());
    }
    
    
    var first = true;
    
    //console.log(serverTime)
    //console.log(serverOffset)
   
   var increment = 0; 
function updateClock ( )
    {
    // lblTime lblDate
    
    var serverTime = getClientTime() + increment; 
    increment++;
    
    var currentHours = Math.floor(serverTime / 60 / 60);
    //var currentMinutes = Math.floor(serverTime / 60 % (currentHours * 60));
    if(currentHours > 0){
        var currentMinutes = Math.floor((serverTime / 60) % (currentHours * 60));
    }else{
        var currentMinutes = Math.floor(serverTime / 60);
    }
    var currentSeconds = Math.floor(serverTime % 60);
    
    //console.log('serverTime: '+serverTime+ ' -currentHours:'+currentHours+' - currentMinutes: '+ currentMinutes)
    // Pad the minutes and seconds with leading zeros, if required
    currentMinutes = ( currentMinutes < 10 ? "0" : "" ) + currentMinutes;
    currentSeconds = ( currentSeconds < 10 ? "0" : "" ) + currentSeconds;

    // Choose either "AM" or "PM" as appropriate
    var timeOfDay = ( currentHours < 12 ) ? "AM" : "PM";
    
   

    // Convert the hours component to 12-hour format if needed
    currentHours = ( currentHours > 12 ) ? currentHours - 12 : currentHours;

    // Convert an hours component of "0" to "12"
    currentHours = ( currentHours == 0 ) ? 12 : currentHours;
    
    
     if(timeOfDay == 'AM' && currentHours == 12 ){
        currentHours = "00";
    }
    

    // Compose the string for display
    var currentTimeString = currentHours + ":" + currentMinutes + ":" + currentSeconds + " " + timeOfDay;
    
    
    $(".cTime b").html(currentTimeString);
    
    
    // Late time Error Border Binding
    if(first == true){
        $('#hdnRtime').val(currentTimeString);
        $('#rTime').text($('#hdnRtime').val());
        first = false;
        $('#origin .user').each(function() {
            var validTime2 = false;
            var title = "<span class='center'>" + $(this).children('input:eq(0)').val() + "</span><br><span class='centerHed'> "+$(this).children('h3').children('span').text()+" </span><br><span>schedule: </span>" + $(this).children('input:eq(2)').val() + " - " + $(this).children('input:eq(3)').val(); //+$(this).children('input:eq(0)').val();
            $(this).attr('title', title);

            var tim1 = $.trim($(this).children('input:eq(2)').val()).split(':')
            var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
            tim1[1] = tim1[1].substring(0, 2);


            var current_tim = $.trim($('.cTime b').text()).split(':')
            var current_timAMPM = current_tim[2].substring(current_tim[2].length - 2, current_tim[2].length);
            //current_tim[2] = current_tim[2].substring(current_tim[2].length - 2, current_tim[2].length);        
            
            //if (parseInt(tim1[0]) >= parseInt(current_tim[0]) && parseInt(tim1[1]) >= parseInt(current_tim[1]) && tim1AMPM == current_timAMPM ) { validTime2 = true } else { validTime2 = false }
            
            //var Stl =  tim1[0]*3600 + tim1[1]*60;
            //var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            
            // Assigned time
            if(tim1[0] == 12){
                var Stl =  tim1[1]*60;
            }else{
                var Stl =  tim1[0]*3600 + tim1[1]*60;
            }
            
            // Actual Clock
            if(current_tim[0] == 12){
                var St2 =  current_tim[1]*60;
            }else{
                var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            }          
            
            
            if(tim1AMPM=='PM'){
                Stl += 43200;
            }
            if(current_timAMPM=='PM' ){
                St2 += 43200;
            }
            
            
            //if(Stl >= St2 && tim1AMPM == current_timAMPM ){ validTime2 = true } else { validTime2 = false }
            if(Stl >= St2  ){ validTime2 = true } else { validTime2 = false }
            
            if (validTime2 == false) {
                $(this).addClass('lateTime');
            } else {
                $(this).removeClass('lateTime');
            }
            
          
            

            /*
            console.log(parseInt(tim1[0]), parseInt(tim1[1]), tim1AMPM)
            console.log(parseInt(current_tim[0]), parseInt(current_tim[1]), current_timAMPM)
            console.log(Stl, St2)
            */
            
            

        });
        
        
        
        $('#drop1 .user').each(function() {
            var validTime2 = false;
            // var title= "<b>Signed in </b><br><span>Name: </span>"+$(this).children('input:eq(0)').val()+"<br><span>Sch_in: </span>"+$(this).children('input:eq(0)').val()+"<br><span>Sig_in: </span>"+$(this).children('input:eq(2)').val()+"";
            var title = "<span class='center'>" + $(this).children('input:eq(0)').val() + "</span><br><span class='centerHed'> "+$(this).children('h3').children('span').text()+" </span><br><span>schedule: </span>" + $(this).children('input:eq(2)').val() + " - " + $(this).children('input:eq(3)').val() + "<br><span>Sign In: </span>" + $(this).children('input:eq(4)').val() + "";
            $(this).attr('title', title);

            var tim1 = $.trim($(this).children('input:eq(2)').val()).split(':')
            var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
            tim1[1] = tim1[1].substring(0, 2);

            var sp = $.trim($(this).children('input:eq(4)').val()).split(' ')
            var current_tim = sp[1].split(':')
            var current_timAMPM = sp[2];
            //current_tim[1] = current_tim[1].substring(0, 2);

           // if (parseInt(tim1[0]) >= parseInt(current_tim[0]) && parseInt(tim1[1]) >= parseInt(current_tim[1]) && tim1AMPM == current_timAMPM ) { validTime2 = true } else { validTime2 = false }
            
            //var Stl =  tim1[0]*3600 + tim1[1]*60;
            //var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            
            // Assigned time
            if(tim1[0] == 12){
                var Stl =  tim1[1]*60;
            }else{
                var Stl =  tim1[0]*3600 + tim1[1]*60;
            }
            
            // Actual Clock
            if(current_tim[0] == 12){
                var St2 =  current_tim[1]*60;
            }else{
                var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            }          
            
            
            if(tim1AMPM=='PM'){
                Stl += 43200;
            }
            if(current_timAMPM=='PM' ){
                St2 += 43200;
            }
            
            
            
            
            
            
             if(tim1AMPM=='PM'){
                Stl += 43200;
            }
            if(current_timAMPM=='PM'){
                St2 += 43200;
            }
            
            if(Stl >= St2  ){ validTime2 = true } else { validTime2 = false }
            
            if (validTime2 == false) {
                $(this).addClass('lateTime');
            }
            
        });
        $('#drop2 .user').each(function() {
            var validTime2 = false
            //var title= "<b>Signed out</b><br><span>Name: </span>"+$(this).children('input:eq(0)').val()+"<br><span>Sch_in: </span>"+$(this).children('input:eq(0)').val()+"<br><span>Sig_in: </span>"+$(this).children('input:eq(2)').val()+"<br><span>Sig_out: </span>"+$(this).children('input:eq(3)').val()+"";
            var title = "<span class='center'>" + $(this).children('input:eq(0)').val() + "</span><br><span class='centerHed'> "+$(this).children('h3').children('span').text()+"</span> <br><span>schedule: </span>" + $(this).children('input:eq(2)').val() + " - " + $(this).children('input:eq(3)').val() + "<br><span>Sign In: </span>" + $(this).children('input:eq(4)').val() + "<br><span>Sign Out: </span>" + $(this).children('input:eq(5)').val() + "";
            $(this).attr('title', title);

            var tim1 = $.trim($(this).children('input:eq(3)').val()).split(':')
            var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
            tim1[1] = tim1[1].substring(0, 2);

            var sp = $.trim($(this).children('input:eq(5)').val()).split(' ')
            var current_tim = sp[1].split(':')
            var current_timAMPM = sp[2];
            //current_tim[1] = current_tim[1].substring(0, 2);

           // if (parseInt(tim1[0]) >= parseInt(current_tim[0]) && parseInt(tim1[1]) >= parseInt(current_tim[1]) && tim1AMPM == current_timAMPM ) { validTime2 = true } else { validTime2 = false }
            
            //var Stl =  tim1[0]*3600 + tim1[1]*60;
            //var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            
            // Assigned time
            if(tim1[0] == 12){
                var Stl =  tim1[1]*60;
            }else{
                var Stl =  tim1[0]*3600 + tim1[1]*60;
            }
            
            // Actual Clock
            if(current_tim[0] == 12){
                var St2 =  current_tim[1]*60;
            }else{
                var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            }          
            
            
            if(tim1AMPM=='PM'){
                Stl += 43200;
            }
            if(current_timAMPM=='PM' ){
                St2 += 43200;
            }
            
            
            
            
             if(tim1AMPM=='PM'){
                Stl += 43200;
            }
            if(current_timAMPM=='PM'){
                St2 += 43200;
            }
            if(Stl <= St2  ){ validTime2 = true } else { validTime2 = false }
            
            if (validTime2 == false) {
                $(this).addClass('lateTime');
            }
            
            
            
            var tim1 = $.trim($(this).children('input:eq(2)').val()).split(':')
            var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
            tim1[1] = tim1[1].substring(0, 2);

            var sp = $.trim($(this).children('input:eq(4)').val()).split(' ')
            var current_tim = sp[1].split(':')
            var current_timAMPM = sp[2];
            //current_tim[1] = current_tim[1].substring(0, 2);

           // if (parseInt(tim1[0]) >= parseInt(current_tim[0]) && parseInt(tim1[1]) >= parseInt(current_tim[1]) && tim1AMPM == current_timAMPM ) { validTime2 = true } else { validTime2 = false }
            
            //var Stl =  tim1[0]*3600 + tim1[1]*60;
            //var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            
            
            // Assigned time
            if(tim1[0] == 12){
                var Stl =  tim1[1]*60;
            }else{
                var Stl =  tim1[0]*3600 + tim1[1]*60;
            }
            
            // Actual Clock
            if(current_tim[0] == 12){
                var St2 =  current_tim[1]*60;
            }else{
                var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            }          
            
            
            if(tim1AMPM=='PM'){
                Stl += 43200;
            }
            if(current_timAMPM=='PM' ){
                St2 += 43200;
            }
            
             if(tim1AMPM=='PM'){
                Stl += 43200;
            }
            if(current_timAMPM=='PM'){
                St2 += 43200;
            }
            if(Stl >= St2  ){ validTime2 = true } else { validTime2 = false }
            
            if (validTime2 == false) {
                $(this).addClass('lateTime');
            } /*else {
                $(this).removeClass('lateTime');
            }
            */
            
            
            
        });
        
        
         $('#drop3 .user').each(function() {
             var title = "<span class='center'>" + $(this).children('input:eq(0)').val() + "</span><br><span class='centerHed'> "+$(this).children('h3').children('span').text()+"</span> <br><span>On Leave: </span>" + $(this).children('input:eq(4)').val()  + " - " + $(this).children('input:eq(5)').val() + "<br><span>Status: </span>" + $(this).children('input:eq(6)').val() + "";
             $(this).attr('title', title);
             
             if($.trim($(this).children('input:eq(6)').val()) == 'Approved')
             {
                $(this).addClass('Approved')
             }else{
                $(this).addClass('Open')
             }
             
         });
        
        
    }  
    
    
    
  
 }

$(function(){

    setInterval('updateClock()', 1000);
    
    
    
    // Comment Start   --------------------
    
    if($('.boxC4 #drop3 li').length > 10){
        $('.boxC1').height(($('.boxC2').height()*(55/100))-10)
        $('.boxC4').height(($('.boxC2').height()*(45/100)))
    }else{
        $('.boxC1').height(($('.boxC2').height()*(65/100))-10)
        $('.boxC4').height(($('.boxC2').height()*(35/100)))
    }   
   
    $( window ).resize(function() {
        if($('.boxC4 #drop3 li').length > 10){
            $('.boxC1').height(($('.boxC2').height()*(55/100))-10)
            $('.boxC4').height(($('.boxC2').height()*(45/100)))
        }else{
            $('.boxC1').height(($('.boxC2').height()*(65/100))-10)
            $('.boxC4').height(($('.boxC2').height()*(35/100)))
        }
   });
   
   
   // Comment End   --------------------
   
   
});




function showSpinner() {
    $('#spinner').show();
}
function hideSpinner() {
    $('#spinner').hide();
}

function validLeave()
{
debugger
  var valid=true;
  if($.trim($('#txtFromDt').val())=='')
  {
   alert('Please enter from date');
   $('#txtFromDt').focus();
   return false;
  }
   if($.trim($('#txtToDt').val())=='')
  {
   alert('Please enter to date');
     $('#txtToDt').focus();
    return false;
  }
  if($.trim($('#txtReason').val())=='')
  {
   alert('Please enter reason for leave');
    $('#txtReason').focus();
   return false;
  }
 
   if($.trim($('#txtLeaveEmpID').val())=='')
   {
   alert('Please enter employee id');
   $('#txtLeaveEmpID').focus();
   return false;
   }

  
   if($.trim($('#txtLeavePassCode').val())=='')
   {
    alert('Please enter passcode');
     $('#txtLeavePassCode').focus();
     return false;
   }
  
  return true;
  
  
}




    </script>

    </form>
</body>
</html>
