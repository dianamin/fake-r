using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Album : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] == null) return;
        int id = int.Parse(Request.Params["id"]);
        this.fetchAlbumDetails(id);
        this.fetchPhotos(id);
    }

    private void fetchAlbumDetails(int AlbumId)
    {
        string cerereSQL = "SELECT u.UserName,Name,Description,CreatedDate FROM Albums a,aspnet_Users u WHERE a.AlbumId=@pAlbumId AND a.UserId=u.UserId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(cerereSQL, cn);
            cmd.Parameters.AddWithValue("pAlbumId", AlbumId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AlbumName.Text = reader["Name"].ToString();
                UserName.Text = reader["UserName"].ToString();
            }
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    private void fetchPhotos(int AlbumId)
    {
        string photosQuery =
            "SELECT PhotoId,Url,Description,UploadDate FROM [Photos] " +
            "WHERE AlbumId=@pAlbumId " +
            "ORDER BY UploadDate DESC";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(photosQuery, cn);
            cmd.Parameters.AddWithValue("pAlbumId", AlbumId);
            SqlDataReader reader = cmd.ExecuteReader();
            Photos.DataSource = reader;
            Photos.DataBind();
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}