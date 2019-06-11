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

namespace WebStore.User.Cus
{
    public partial class ForumCus : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore; Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Customer"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("~/Home.aspx");
            }
            else if (!IsPostBack)
            {
                CreateTables();
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/Cus/ThreadList.aspx?type=" + (sender as LinkButton).Attributes["CustomParameter"].ToString());
        }

        public int getmessagescount(string topicname, string type)
        {
            string path = Server.MapPath("~/Forums/" + type + "/" + topicname);
            return Directory.GetFiles(path).Length;
        }

        void CreateTables()
        {
            tablenewcreate("PD");
            tablenewcreate("CR");
            tablenewcreate("GC");
        }

        protected void GDForumPD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GoToThread")
            {
                string[] cmdargs = e.CommandArgument.ToString().Split('/');
                Response.Redirect("~/User/Cus/Thread.aspx?type=" + cmdargs[1] + "&thread=" + cmdargs[0]);
            }
        }

        void tablenewcreate(string type)
        {
            try
            {
                DataTable dt = new DataTable();

                con.Open();
                SqlCommand cmd = new SqlCommand("select TOP(5) * from Forum where Type = '" + type + "';", con);
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                rd.Close();
                con.Close();


                if (dt.Rows.Count == 0)
                {
                    if (type == "PD")
                    {
                        PDempty.Visible = true;
                        PDempty.Text = "No Discussions. Start a new <a href='CreateThread.aspx?type=PD' style='font-size:100%'>Discussion</a>";
                    }
                    else if (type == "CR")
                    {
                        CRempty.Visible = true;
                        CRempty.Text = "No Discussions. Start a new <a href='CreateThread.aspx?type=CR' style='font-size:100%'>Discussion</a>";
                    }
                    else
                    {
                        GCempty.Visible = true;
                        GCempty.Text = "No Discussions. Start a new <a href='CreateThread.aspx?type=GC' style='font-size:100%'>Discussion</a>";
                    }
                }
                else if (type == "PD")
                {
                    GDForumPD.DataSource = dt;
                    GDForumPD.DataBind();

                    Label lbl = new Label()
                    {
                        Text = "Start a new <a href='CreateThread.aspx?type=PD' style='font-size:100%'>Discussion</a>",
                        CssClass = "col-12 form-control text-center",
                    };
                    PDLast.Controls.Add(lbl);
                }
                else if (type == "CR")
                {
                    GDForumCR.DataSource = dt;
                    GDForumCR.DataBind();

                    Label lbl = new Label()
                    {
                        Text = "Start a new <a href='CreateThread.aspx?type=CR' style='font-size:100%'>Discussion</a>",
                        CssClass = "col-12 form-control text-center",
                    };
                    CRLast.Controls.Add(lbl);
                }
                else
                {
                    GDForumGC.DataSource = dt;
                    GDForumGC.DataBind();

                    Label lbl = new Label()
                    {
                        Text = "Start a new <a href='CreateThread.aspx?type=GC' style='font-size:100%'>Discussion</a>",
                        CssClass = "col-12 form-control text-center",
                    };
                    GCLast.Controls.Add(lbl);
                }

            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured: " + ex.Message;
            }

        }
    }
}