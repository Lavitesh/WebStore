using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace WebStore.Admin
{
    public partial class AdminDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("App");
            dt.Columns.Add("File");
            dt.Columns.Add("Size");
            dt.Columns.Add("Type");

            foreach (string strfolder in Directory.GetDirectories(Server.MapPath("~/Apps/")))
            {//No Database Interaction....
                DirectoryInfo di = new DirectoryInfo(strfolder);
                foreach (string strfile in Directory.GetFiles(Server.MapPath("~/Apps/" + di.Name + "/")))
                {
                    FileInfo fi = new FileInfo(strfile);
                    dt.Rows.Add(di.Name, fi.Name, (((float)fi.Length) / (1024.0)).ToString("0.000"), fi.Extension);
                }
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string[] commandargs = e.CommandArgument.ToString().Split('/');
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + commandargs[1]);
                Response.TransmitFile(Server.MapPath("~/Apps/" + e.CommandArgument));
                Response.End();
            }
        }
    }
}