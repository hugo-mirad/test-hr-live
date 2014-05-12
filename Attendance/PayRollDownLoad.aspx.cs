using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Attendance
{
    public partial class PayRollDownLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string filepath = Session["FilePath"].ToString();
                string documentPath = string.Format(filepath);
                string attachmentHeader = String.Format("attachment; filename={0}.pdf", filepath);

                Response.AppendHeader("content-disposition", attachmentHeader);
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(documentPath);


                Response.End();
                //string filepath = Session["FilePath"].ToString();
                //string documentPath = string.Format(filepath);
                //string attachmentHeader = String.Format("attachment; filename={0}.pdf",  Session["filename"].ToString());
                //Response.AppendHeader("content-disposition", attachmentHeader);
                //Response.ContentType = "application/octet-stream";
                //Response.WriteFile(documentPath);
                //Response.End();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
