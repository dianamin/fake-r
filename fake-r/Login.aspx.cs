using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/User.aspx?username=" + Profile.UserName);
        }
    }
}