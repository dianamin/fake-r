using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected String accountLinkUserName;
    protected void Page_Load(object sender, EventArgs e)
    {
        Attributes.Add("onClick", "test();");

        if (Page.IsPostBack) return;

        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            accountLinkUserName = Profile.UserName;
            LoginView1.DataBind();
        }
    }
}
