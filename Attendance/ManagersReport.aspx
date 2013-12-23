<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagersReport.aspx.cs" Inherits="Attendance.ManagersReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style=" padding:20px; background:#fff; width:980px; margin:80px auto 0 auto; border:#ccc 1px solid; box-shadow:0 0 8px rgba(0,0,0,0.1) ">
    
    <table class="grid1">
     <asp:DataList ID="dtlstdatesData" CssClass="grid1" runat="server" RepeatDirection="Horizontal" RepeatColumns="7"
                                                OnItemDataBound="dtlstdatesData_ItemDataBound" Width="400px">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("FirstName")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnDate" runat="server" Value='<%# Eval("FirstName")%>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lblday" runat="server" Text='<%# Eval("LastName")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnDay" runat="server" Value='<%# Eval("LastName")%>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lblSchStartTime" runat="server" Text='<%# Eval("loginDate")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnScheduleStart" runat="server" Value='<%# Eval("loginDate")%>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lblSchEndTime" runat="server" Text='<%# Eval("Logoutdate")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnScheduleEnd" runat="server" Value='<%# Eval("Logoutdate")%>' />
                                                            </td>
                                                        </tr>
                                                     
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
        <%--<tr>
            <th>Sno</th>
            <th>Head 1</th>
            <th>Head 2</th>
            <th>Head 1</th>
            <th>Head 2</th>
        </tr>
        <tr>
            <td>1</td>
            <td>Name 1</td>
            <td>Name 2</td>
            <td>Name 1</td>
            <td>Name 2</td>
        </tr>--%>
    </table>
     <asp:GridView ID="grdEmpData" CssClass="grid1" runat="server"></asp:GridView>
    </div>
    
     <asp:ScriptManager ID="SM" runat="server">
    </asp:ScriptManager>
    <!--  Grid Start  -->
    <div class=" container1 " >
        <%-- <div class="main">--%>
        <table class="mainTable">
            
            <tr>
                <td style="padding-left: 60px; width: 250px;">
                    <br />
                    <asp:UpdatePanel ID="updtpnlMonthChange" runat="server">
                        <ContentTemplate>
                           <%-- Select Month:--%>
                           <%-- <asp:LinkButton ID="lnkDec" runat="server" Text="&laquo;" Style="font-size: 17px;
                                font-weight: bold;" ></asp:LinkButton>
                            <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
                            <asp:LinkButton ID="lnkInc" runat="server" Text="&raquo;" Style="font-size: 17px;
                                font-weight: bold;" Visible="false" ></asp:LinkButton>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="padding-top: 10px;">
                    <asp:UpdatePanel ID="updtpnlGo" runat="server">
                        <ContentTemplate>
                           <%-- <asp:Button ID="btnGo" runat="server" Text="" CssClass="g-button g-button-submit"
                                />--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 60px;" colspan="2">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="wordWrap">
                                <tr>
                                    <td style="width: 80px; min-width: 80px;" class="hed111">
                                        <table>
                                        <tr>
                                                <td>
                                                    Name
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Day
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Sch_Start
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Sch_End
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Sign In
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Sign Out
                                                </td>
                                            </tr>
                                           <%-- <tr>
                                                <td>
                                                    Total
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </td>
                                    <td>
                                        <div style="overflow-x: scroll; overflow-y: hidden; width: 530px; min-width: 530px;
                                            padding: 0">
                                            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="7"
                                                Width="400px">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("FirstName")%>'></asp:Label>
                                                               <%-- <asp:HiddenField ID="hdnDate" runat="server" Value='<%# Eval("date")%>' />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lblday" runat="server" ></asp:Label>
                                                            <%--    <asp:HiddenField ID="hdnDay" runat="server" Value='<%# Eval("day")%>' /--%>
                                                            </td>
                                                        </tr>
                                                        
                                                         <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="Label1" runat="server" ></asp:Label>
                                                              <%--  <asp:HiddenField ID="hdnScheduleStart" runat="server" Value='<%# Eval("StartTime")%>' />--%>
                                                            </td>
                                                        </tr>
                                                        
                                                         <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("EndTime")%>'></asp:Label>
                                                              <%--  <asp:HiddenField ID="hdnScheduleStart" runat="server" Value='<%# Eval("StartTime")%>' />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lblSchStartTime" runat="server" Text='<%# Eval("EndTime")%>'></asp:Label>
                                                              <%--  <asp:HiddenField ID="hdnScheduleStart" runat="server" Value='<%# Eval("StartTime")%>' />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lblSchEndTime" runat="server" Text='<%# Eval("LoginDate")%>'></asp:Label>
                                                              <%--  <asp:HiddenField ID="hdnScheduleEnd" runat="server" Value='<%# Eval("EndTime")%>' />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lblSigninTime" runat="server" Text='<%# Eval("LogOutDate")%>'></asp:Label>
                                                               <%-- <asp:HiddenField ID="hdnSignIN" runat="server" Value='<%# Eval("SignIn")%>' />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                               <%-- <asp:Label ID="lblSignoutTime" runat="server" Text='<%# Eval("SignOut")%>'></asp:Label>--%>
                                                               <%-- <asp:HiddenField ID="hdnSignOut" runat="server" Value='<%# Eval("SignOut")%>' />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <asp:Label ID="lblTotalHoursWorkINDay" runat="server"></asp:Label>
                                                               <%-- <asp:HiddenField ID="hdnTotalHours" runat="server" Value='<%# Eval("HoursApart")%>' />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </td>
                                    <td style="width: 120px; min-width: 120px;" class="hed111">
                                        <table>
                                            <tr>
                                                <td>
                                                    Total hours worked
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTotalHoursinWeek" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 10px;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
   <%-- </div> --%>  
    </div>
    <!-- Grid End  -->
    
    
    
    </form>
</body>
</html>
