using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class EditPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        if (Request.Params["photo"] != null)
        {
            int photoId = int.Parse(Request.Params["photo"]);
            this.fetchPhoto(photoId);
        }
    }

    private void fetchPhoto(int photoId)
    {
        string photoQuery =
            "SELECT p.Name as PhotoName, p.UserName as UserName, c.Name as Category, UploadDate, a.Name as AlbumName, a.AlbumId as AlbumId, p.Description as Description " +
            "FROM Photos p JOIN Categories c ON (c.CategoryId = p.CategoryId) JOIN Albums a ON (a.AlbumId = p.AlbumId) " +
            "WHERE PhotoId=@pPhotoId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand(photoQuery, cn);
            cmd.Parameters.AddWithValue("pPhotoId", photoId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                imgUpload.ImageUrl = "Images/" + reader["PhotoName"].ToString();
            }
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    private void updatePhotoName(string newPath)
    {
        if (Request.Params["photo"] == null) return;
        int photoId = int.Parse(Request.Params["photo"]);
        string updatePhotoQuery =
            "UPDATE Photos " +
            "SET Name = @pName " +
            "WHERE PhotoId = @pPhotoId";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(updatePhotoQuery, cn);
            cmd.Parameters.AddWithValue("pName", newPath);
            cmd.Parameters.AddWithValue("pPhotoId", photoId);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }

    protected void btnCrop_Click(object sender, EventArgs e) 
    {
        // Crop Image Here & Save  
        string fileName = Path.GetFileName(imgUpload.ImageUrl);
        string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
        string cropFileName = "";
        string cropFilePath = "";
        if (File.Exists(filePath))
        {
            System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
            Rectangle CropArea = new Rectangle(Convert.ToInt32(X.Value), Convert.ToInt32(Y.Value), Convert.ToInt32(W.Value), Convert.ToInt32(H.Value));
            try
            {
                Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                using (Graphics g = Graphics.FromImage(bitMap))
                {
                    g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                }
                cropFileName = "crop_" + fileName;
                cropFilePath = Path.Combine(Server.MapPath("~/Images"), cropFileName);
                bitMap.Save(cropFilePath);
                this.updatePhotoName(cropFileName);
                Response.Redirect("~/Photo.aspx?photo=" + Request.Params["photo"]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }   
    }
}