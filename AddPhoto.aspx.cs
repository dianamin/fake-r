using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Upload_Click(object sender, EventArgs e)
    {
        String imageName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-") + Guid.NewGuid().ToString() + ".jpg";
        Image.PostedFile.SaveAs(Server.MapPath("~/Images/") + imageName);
        String description = Description.Text;
        int categoryId = int.Parse(Category.SelectedValue);
        
        string insertPhotoQuery =
            "INSERT INTO Photos (Url, UserName, CategoryId, Description) " +
            "VALUES (@pImageName, @pUserName, @pCategoryId, @pDescription)";

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(insertPhotoQuery, cn);
            cmd.Parameters.AddWithValue("pImageName", imageName);
            cmd.Parameters.AddWithValue("pUserName", "diana");
            cmd.Parameters.AddWithValue("pCategoryId", categoryId);
            cmd.Parameters.AddWithValue("pDescription", description);
            System.Diagnostics.Debug.WriteLine(categoryId);
            System.Diagnostics.Debug.WriteLine(description);
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception exCMD)
        {
            Console.WriteLine(exCMD.Message);
        }
    }
}