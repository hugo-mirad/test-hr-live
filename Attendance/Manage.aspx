<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Attendance.Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link rel="stylesheet" href="css/reset.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/UI.css" />
    <link rel="stylesheet" href="css/inputs.css" type="text/css" />
    <link rel="stylesheet" href="css/style.css" type="text/css" />
    <script type="text/javascript" language="javascript">
        function validateSubmit() {
        debugger
          var valid=true;  
   
            
          
           if(document.getElementById('txtEmpId').value.trim().length<1)
           {
               alert("Please enter the valid User Id.");               
               valid=false;
               document.getElementById("txtEmpId").value="";
               document.getElementById("txtEmpId").focus();
           }
        
           else if (document.getElementById('txtLocationNme').value.trim().length < 1) {
               alert("Please enter the valid location Code.");
               valid = false;
               document.getElementById("txtLocationNme").value = "";
               document.getElementById("txtLocationNme").focus();
           }



           else if (document.getElementById('txtPass').value.length < 1)
           {
               alert("Please enter the password .");               
               valid=false;
               document.getElementById("txtPass").focus();
           }
          
           
       
           return valid;
       }
 
        
        
        
     
    
    
    
    </script>
    
    
    
    
    
    
</head>
<body style="height:auto; min-height:auto">
    <form id="form1" runat="server">
    
    
    
    <div style="height:90px" >&nbsp;</div>
    <div class="bor boxC3" style=" margin:0 auto; width:400px; box-shadow:0 0 10px rgba(0,0,0,0.2) ">
                            <h2 class="three" style="background: #fff; color: #ee8d1a; border-bottom: #ee8d1a 1px solid;">
                               Login</h2>
                            <div class="inner  ">
                                
                                   <table style="width:98%; margin:0 auto;">
                                         <!--
                                        <tr>
                                            <td style="width:100px;">User Name:</td>
                                            <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
                                        </tr>
                                       
                                        <tr>
                                            <td>Last Name:</td>
                                            <td><asp:TextBox ID="txtLastName" runat="server" ></asp:TextBox></td>
                                        </tr>
                                        -->
                                        <tr>
                                            <td>Emp ID:</td>
                                            <td><asp:TextBox ID="txtEmpId" runat="server" ></asp:TextBox></td>
                                        </tr>                                        
                                        <tr>
                                            <td>Password:</td>
                                            <td><asp:TextBox TextMode="Password" ID="txtPass" runat="server" ></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Location Code:</td>
                                            <td><asp:TextBox ID="txtLocationNme" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><!-- --></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><asp:Button ID="btnSubmit" Text="Login" runat="server" CssClass="btn btn-danger" OnClick="btnSubmit_Click"  OnClientClick="return validateSubmit();"/></td>
                                        </tr>
                                         <tr>
                                           
                                            <td colspan="2" align="center"><asp:Label ID="lblmsg" Text="" runat="server" ForeColor="red" ></asp:Label></td>
                                        </tr>
                                   </table>   
                                    <br />
                            </div>
                            <div class="clear">
                                &nbsp;</div>
                        </div>
    
    
    
                        
                       
                        
    
    
    </form>
</body>
</html>
