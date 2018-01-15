using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected String accountLinkUserName;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack) return;

        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            accountLinkUserName = Profile.UserName;
            LoginView1.DataBind();
        }
    }
}
