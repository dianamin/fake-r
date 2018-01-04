using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddAlbum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (IsPostBack) return;
    }

    protected void SaveAlbum_Click(object sender, EventArgs e)
    {
        String name = Name.Text;
        String description = Description.Text;

        string insertAlbumQuery =
            "INSERT INTO Albums (Name, UserName, Description) " +
            "VALUES (@pName, @pUserName, @pDescription)";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(insertAlbumQuery, cn);
            cmd.Parameters.AddWithValue("pUserName", Profile.UserName);
            cmd.Parameters.AddWithValue("pName", name);
            cmd.Parameters.AddWithValue("pDescription", description);
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/User.aspx?username=" + Profile.UserName);
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}