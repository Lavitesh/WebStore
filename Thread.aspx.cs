using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebStore
{
    public partial class Thread : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ThreadName.Text = Request.QueryString["thread"];
                CreateTable();
            }
        }

        DateTime getDateNew(string str) //string must have format of dd-MM-yyyy_HH-mm-ss or dd-MM-yyyy
        {
            string[] args = str.Split('_');
            DateTime dt;
            if (args.Length == 1)
            {
                string[] date = args[0].Split('-');
                dt = new DateTime(
                    int.Parse(date[2]),
                    int.Parse(date[1]),
                    int.Parse(date[0])
                );
            }
            else
            {
                string[] date = args[0].Split('-');
                string[] time = args[1].Split('-');
                dt = new DateTime(
                    int.Parse(date[2]),
                    int.Parse(date[1]),
                    int.Parse(date[0]),
                    int.Parse(time[0]),
                    int.Parse(time[1]),
                    int.Parse(time[2])
                );
            }
            return dt;
        }          

        void CreateTable()
        {
            try
            {
                if (ThreadAvailable())
                {
                    emptylist.Visible = false;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ThreadFileName");
                    dt.Columns.Add("UName");
                    dt.Columns.Add("DateThread");
                    dt.Columns.Add("UType");
                    dt.Columns.Add("DateTimeThread");                

                    string type = Request.QueryString["type"], thread = Request.QueryString["thread"].ToString();
                    string path = Server.MapPath("~/Forums/" + type + "/" + thread + "/");

                    string[] filepaths = Directory.GetFiles(path);

                    foreach (string pth in filepaths)
                    {
                        string filenamewext = Path.GetFileNameWithoutExtension(pth);
                        string filename = Path.GetFileName(pth);
                        string[] args = filenamewext.Split('%');
                        dt.Rows.Add(filename, args[0], getDateNew(args[2]).ToString("dd-MM-yyyy"), args[1], getDateNew(args[2]));
                    }
                    DataView view = new DataView(dt, string.Empty, "DateTimeThread DESC", DataViewRowState.CurrentRows);
                    GDThread.DataSource = view;
                    GDThread.DataBind(); 
                }
                else
                {
                    emptylist.Visible = true;
                    emptylist.Text = "No posts. <a href='Login.aspx?type=GC' style='font-size:100%'>Login</a> to post";
                }
            }
            catch (Exception ex)
            {
                Status.Text = "Some error occured. Reason: " + ex.Message;
            }
        }

        bool ThreadAvailable()
        {
            int val = 0;
            string type = Request.QueryString["type"],thread = Request.QueryString["thread"];
            string path = Server.MapPath("~/Forums/" + type + "/" + thread);
            val = Directory.GetFiles(path).Length;
            return (val == 0) ? false : true;
        }

        public string getUserType(string type)
        {
            if (type == "C")
                return "Customer";
            else
                return "Developer";            
        }

        public string getThreaddata(string threadfile)
        {
            string path = Server.MapPath("~/Forums/" + Request.QueryString["type"] + "/" + Request.QueryString["thread"] + "/" + threadfile);
            string str = "";
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                    str += s;
            }
            return str;
        }

        protected void GDThread_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Removefile")
            {
                string confirmVal = Request.Form["confirm_value"];
                if (confirmVal == "Yes")
                {
                    try
                    {
                        string type = Request.QueryString["type"], thread = Request.QueryString["thread"];
                        File.Delete(Server.MapPath("~/Forums/" + type + "/" + thread + "/" + e.CommandArgument.ToString()));
                        CreateTable();
                    }
                    catch (Exception ex)
                    {
                        Status.Text = "Some error occured. Reason: " + ex.Message;
                    }
                }
            }
        }        

        protected void gD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GDThread.PageIndex = e.NewPageIndex;
            CreateTable();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = GDThread.BottomPagerRow;

            // Retrieve the PageDropDownList DropDownList from the bottom pager row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

            // Set the PageIndex property to display that page selected by the user.
            GDThread.PageIndex = pageList.SelectedIndex;
            CreateTable();
        }

        protected void PreviousPage_Click(object sender, EventArgs e)
        {
            int currentPage = GDThread.PageIndex;
            if (currentPage != 0)
            {
                GDThread.PageIndex = currentPage - 1;
                CreateTable();
            }
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            int currentPage = GDThread.PageIndex;
            if (currentPage != GDThread.PageCount)
            {
                GDThread.PageIndex = currentPage + 1;
                CreateTable();
            }
        }

        protected void gD_DataBound(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = GDThread.BottomPagerRow;

            // Retrieve the DropDownList and Label controls from the row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");


            if (pageList != null)
            {

                // Create the values for the DropDownList control based on 
                // the  total number of pages required to display the data
                // source.
                for (int i = 0; i < GDThread.PageCount; i++)
                {

                    // Create a ListItem object to represent a page.
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    // If the ListItem object matches the currently selected
                    // page, flag the ListItem object as being selected. Because
                    // the DropDownList control is recreated each time the pager
                    // row gets created, this will persist the selected item in
                    // the DropDownList control.   
                    if (i == GDThread.PageIndex)
                    {
                        item.Selected = true;
                    }

                    // Add the ListItem object to the Items collection of the 
                    // DropDownList.
                    pageList.Items.Add(item);

                }

            }

            if (pageLabel != null)
            {

                // Calculate the current page number.
                int currentPage = GDThread.PageIndex + 1;

                // Update the Label control with the current page information.
                pageLabel.Text = "Page " + currentPage.ToString() + " of " + GDThread.PageCount.ToString();
            }

        }
    }
}