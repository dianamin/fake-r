using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.Script.Serialization;


public partial class User : System.Web.UI.Page
{
    protected bool seeButtons;
    protected String Locations;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        if (Request.Params["username"] == null)
            Response.Redirect("~/Users.aspx");

        String userName = Request.Params["username"];

        if (Membership.GetUser(userName) == null)
            Response.Redirect("~/Users.aspx");

        this.seeButtons = false;
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            if (Profile.UserName == userName)
                this.seeButtons = true;
        }

        this.UserName.Text = userName;
        this.fetchProfie(userName);
        this.fetchAlbums(userName);
        this.fetchLocations(userName);

        string role = (Roles.GetRolesForUser(userName).Contains("member")) ? "member" : "admin";
        if (UserRole != null) UserRole.Text = role;
        Page.DataBind();
    }

    private void fetchProfie(string userName)
    {
        ProfileCommon userProfile = Profile.GetProfile(userName);
        Name.Text = userProfile.FirstName + " " + userProfile.LastName;
        Description.Text = userProfile.Description;
        JoinDate.Text = userProfile.JoinDate.ToString();
    }

    private void fetchAlbums(string userName)
    {
        AlbumsSource.SelectCommand =
            "SELECT a.AlbumId, a.Name, a.Description, COUNT(PhotoId) as PhotosCount " +
            "FROM Albums a LEFT JOIN Photos p ON (a.AlbumId = p.AlbumId) " +
            "WHERE a.UserName=@pUserName " +
            "GROUP BY a.AlbumId,a.Name,a.Description"; 
        AlbumsSource.SelectParameters.Clear();
        AlbumsSource.SelectParameters.Add("pUserName", userName);
    }

    private void fetchLocations(String userName)
    {
        this.Locations = "[]";
        string locationsQuery =
            "SELECT Latitude, Longitude " +
            "FROM Photos p JOIN Albums a ON (p.AlbumId = a.AlbumId) " +
            "WHERE a.UserName = @pUserName AND Latitude IS NOT NULL AND Longitude IS NOT NULL";
        string locations = "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(locationsQuery, cn);
            cmd.Parameters.AddWithValue("pUserName", userName);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                locations += "{Longitude: " + reader["Longitude"].ToString() + ", Latitude: " + reader["Latitude"].ToString() + "}, ";
            }
            cn.Close();
            this.Locations = "[" + locations + "]";
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void ChangeRole_Click(object sender, EventArgs e)
    {
        string username = Request.Params["username"];

        if (Roles.GetRolesForUser(username).Contains("member"))
        {
            Roles.RemoveUserFromRole(username, "member");
            Roles.AddUserToRole(username, "admin");
        }
        else
        {
            if (Roles.GetUsersInRole("admin").Length <= 2)
            {
                ((Label)AdminView.FindControl("UserError")).Text = ("This website cannot have less than 2 admins.");
                return;
            }
            Roles.RemoveUserFromRole(username, "admin");
            Roles.AddUserToRole(username, "member");
        }
        Response.Redirect("~/User.aspx?username=" + username);
    }

    protected void DeleteUser_Click(object sender, EventArgs e)
    {
        string username = Request.Params["username"];
        if (Roles.GetRolesForUser(username).Contains("admin") && Roles.GetUsersInRole("admin").Length <= 2)
        {
            ((Label)AdminView.FindControl("UserError")).Text = ("This website cannot have less than 2 admins.");
            return;
        }
        Membership.DeleteUser(username);
        Response.Redirect("~/Users.aspx");
    }

    protected void AddAlbum_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddAlbum.aspx");
    }
}