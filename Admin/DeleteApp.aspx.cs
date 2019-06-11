using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace WebStore.Admin
{
    public partial class DeleteApp : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore; Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] != null)
            {
                fillTable();
            }
            else
            {
                Session.RemoveAll();
                Response.Redirect("~/Home.aspx");
            }
        }

        void fillTable()
        {
            try
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
                        dt.Rows.Add(di.Name, fi.Name, fi.Length , fi.Extension);                            
                    }
                }

                GDDel.DataSource = dt;
                GDDel.DataBind();

            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured. Reason: " + ex.Message;
            }
        }

        protected void GDDel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "DownloadNow")
            {
                string[] commandargs = e.CommandArgument.ToString().Split('/');
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + commandargs[1]);
                Response.TransmitFile(Server.MapPath("~/Apps/" + e.CommandArgument));
                Response.End();
            }
            else if(e.CommandName == "DeleteNow")
            {
                string confirmVal = Request.Form["confirm_value"];
                if(confirmVal == "Yes")
                {                    
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_deleteApp", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("AppName" , e.CommandArgument.ToString());
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Directory.Delete(Server.MapPath("~/Apps/" + e.CommandArgument.ToString() + "/"), true);

                        Response.Redirect("~/Admin/DeleteApp.aspx");
                    }
                    catch (Exception ex)
                    {
                        Status.Text = "Some error occured. Reason: " + ex.Message;
                    }
                }
            }
        }
    }
}