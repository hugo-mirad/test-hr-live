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
using Attendance.BAL;
using Attendance.Entities;
using System.Data.SqlClient;
using System.Configuration;

namespace Attendance
{
    public partial class ManagersReport : System.Web.UI.Page
    {
        Business business = new Business();
        int value = 0;

        Attendance.Entities.Entities entities = new Attendance.Entities.Entities();
        //SqlConnection cn = new SqlConnection("Data Source=66.23.236.151;Initial Catalog=EMPDB_Test;User ID=mahesh;Password=Mahesh@123;Connect Timeout=500000;pooling=true;Max Pool Size=500;packet size=8000");
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            string Loc = Session["Location"].ToString();
            DataSet dsEmpData = new DataSet();
            dsEmpData = business.BindEmpData();

            // GridView1.DataSource = readerFindUserInfo;
            //GridView1.DataBind(); 
            //grdEmpData.DataSource = dsEmpData.Tables[0];
            //grdEmpData.DataBind();
            DataList1.DataSource = dsEmpData.Tables[0];
            DataList1.DataBind();
            
        }

        protected void dtlstdatesData_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    Label lbldate = (Label)e.Item.FindControl("lbldate");
            //    HiddenField hdnDate = (HiddenField)e.Item.FindControl("hdnDate");

            //    Label lblday = (Label)e.Item.FindControl("lblday");
            //    HiddenField hdnDay = (HiddenField)e.Item.FindControl("hdnDay");

            //    Label lblSchStartTime = (Label)e.Item.FindControl("lblSchStartTime");
            //    HiddenField hdnScheduleStart = (HiddenField)e.Item.FindControl("hdnScheduleStart");

            //    Label lblSchEndTime = (Label)e.Item.FindControl("lblSchEndTime");
            //    HiddenField hdnScheduleEnd = (HiddenField)e.Item.FindControl("hdnScheduleEnd");

              

            //}
        }
    }
}
