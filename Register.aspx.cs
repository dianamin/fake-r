using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        string username = CreateUserWizard1.UserName;
        Roles.AddUserToRole(username, "Member");
    }

    protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        TextBox t = CreateUserWizard1.FindControl("TBFirstName") as TextBox;
        if (t != null) Profile.FirstName = t.Text;

        t = CreateUserWizard1.FindControl("TBLastName") as TextBox;
        if (t != null) Profile.LastName = t.Text;

        t = CreateUserWizard1.FindControl("TBDescription") as TextBox;
        if (t != null) Profile.Description = t.Text;

        Profile.JoinDate = DateTime.Now;
    }

    protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}