﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore.User.Dev
{
    public partial class DPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Developer"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("~/Home.aspx");
            }
        }
    }
}