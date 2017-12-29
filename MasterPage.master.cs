﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Attributes.Add("onClick", "test();");
    }

    protected void MyProfileLinkButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/User.aspx?username=" + Profile.UserName);
    }
}
