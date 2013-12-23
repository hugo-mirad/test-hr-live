using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Attendance.BAL;
using Attendance.Entities;
using System.Data.SqlClient;
using System.Data;

namespace Attendance
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection("Data Source=66.23.236.151;Initial Catalog=EMPDB_Test;User ID=mahesh;Password=Mahesh@123;Connect Timeout=500000;pooling=true;Max Pool Size=500;packet size=8000");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cn.State != ConnectionState.Open) cn.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Select UserID,FirstName,LastName,JoiningDate,Desigantion,TermDate from Tbl_HrData", cn);
            cmd.Connection = cn;
            SqlDataAdapter daLocation = new SqlDataAdapter(cmd);
            daLocation.Fill(ds);
            
            
            // GridView1.DataSource = readerFindUserInfo;
            //GridView1.DataBind(); 
            grdEmployeeList.DataSource = ds.Tables[0];
            grdEmployeeList.DataBind();
            
        }
    }
}
