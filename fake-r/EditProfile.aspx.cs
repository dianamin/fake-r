using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        TextBox firstName = (TextBox)EditProfileLoginView.FindControl("TBFirstName");
        if (firstName == null) return;

        TextBox lastName = (TextBox)EditProfileLoginView.FindControl("TBLastName");
        if (lastName == null) return;

        TextBox description = (TextBox)EditProfileLoginView.FindControl("TBDescription");
        if (description == null) return;

        firstName.Text = Profile.FirstName;
        lastName.Text = Profile.LastName;
        description.Text = Profile.Description;
    }

    protected void SaveProfile_Click(object sender, EventArgs e)
    {
        TextBox firstName = (TextBox)EditProfileLoginView.FindControl("TBFirstName");
        if (firstName == null) return;
        
        TextBox lastName = (TextBox)EditProfileLoginView.FindControl("TBLastName");
        if (lastName == null) return;

        TextBox description = (TextBox)EditProfileLoginView.FindControl("TBDescription");
        System.Diagnostics.Debug.WriteLine(description.Text);
        if (description == null) return;

        Profile.FirstName = firstName.Text;
        Profile.LastName = lastName.Text;
        Profile.Description = description.Text;
        Response.Redirect("~/User.aspx?username=" + Profile.UserName);
    }
}