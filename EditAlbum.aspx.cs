using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;

public partial class EditAlbum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (Request.Params["album"] == null)
            Response.Redirect("~/");

        int albumId = int.Parse(Request.Params["album"]);
        this.fetchAlbum(albumId);
    }

    private void fetchAlbum(int AlbumId)
    {
        string albumQuery =
            "SELECT UserName, Name, Description, CreatedDate " +
            "FROM Albums " +
            "WHERE AlbumId=@pAlbumId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(albumQuery, cn);
            cmd.Parameters.AddWithValue("pAlbumId", AlbumId);
            SqlDataReader reader = cmd.ExecuteReader();
            bool canEdit = false;
            while (reader.Read())
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (Profile.UserName == reader["UserName"].ToString())
                        canEdit = true;
                    if (Roles.GetRolesForUser().Contains("admin"))
                        canEdit = true;
                }

                Name.Text = reader["Name"].ToString();
                Description.Text = reader["Description"].ToString();
                DataBind();
            }
            cn.Close();
            if (!canEdit)
                Response.Redirect("~/Album.aspx?album=" + AlbumId);
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void SaveAlbum_Click(object sender, EventArgs e)
    {
        String name = Name.Text;
        String description = Description.Text;

        string insertAlbumQuery =
            "UPDATE Albums " +
            "SET Name=@pName, Description=@pDescription " +
            "WHERE AlbumId=@pAlbumId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(insertAlbumQuery, cn);
            cmd.Parameters.AddWithValue("pAlbumId", Request.Params["album"]);
            cmd.Parameters.AddWithValue("pName", name);
            cmd.Parameters.AddWithValue("pDescription", description);
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/Album.aspx?album=" + Request.Params["album"]);
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}