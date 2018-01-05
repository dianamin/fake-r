using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class User : System.Web.UI.Page
{
    protected bool seeButtons;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        if (Request.Params["username"] == null)
            Response.Redirect("~/Users.aspx");

        String userName = Request.Params["username"];

        this.seeButtons = false;
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            if (Profile.UserName == userName)
                this.seeButtons = true;
        }
        DataBind();

        this.UserName.Text = userName;
        this.fetchProfie(userName);
        this.fetchAlbums(userName);
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
        string albumsQuery =
            "SELECT a.AlbumId,a.Name,a.Description,COUNT(PhotoId) as PhotosCount " +
            "FROM Albums a LEFT JOIN Photos p ON (a.AlbumId = p.AlbumId) " +
            "WHERE a.UserName=@pUserName " +
            "GROUP BY a.AlbumId,a.Name,a.Description"; 
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(albumsQuery, cn);
            cmd.Parameters.AddWithValue("pUserName", userName);
            SqlDataReader reader = cmd.ExecuteReader();
            Albums.DataSource = reader;
            Albums.DataBind();
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void DeleteUser_Click(object sender, EventArgs e)
    {
        string username = Request.Params["username"];
        Membership.DeleteUser(username);
        Response.Redirect("~/Users.aspx");
    }

    protected void AddAlbum_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddAlbum.aspx");
    }
}