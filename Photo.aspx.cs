using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Photo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
        {
            int id = int.Parse(Request.Params["id"]);
            string cerereSQL = "SELECT Url,UserId,c.Name as Category,Description,UploadDate FROM Photos p JOIN Categories c ON (c.CategoryId = p.CategoryId) WHERE PhotoId=@pid";
            SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-T2GBBU6T\SQLEXPRESS;Initial Catalog=Database;Integrated Security=True");
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(cerereSQL, cn);
                cmd.Parameters.AddWithValue("pId", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Image.ImageUrl = reader["Url"].ToString();
                    Category.Text = reader["Category"].ToString();
                    Description.Text = reader["Description"].ToString();
                }
                cn.Close();
            }
            catch (Exception exCMD)
            {
                Console.WriteLine(exCMD.Message);
            }
            finally
            {
                this.updateComments();
            }
        }
    }

    protected void AddComment_Click(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
        {
            int photoId = int.Parse(Request.Params["id"]);
            string commentMessage = CommentMessage.Text; 

            string insertCommentQuery = "INSERT INTO Comments (PhotoId,UserId,Message) VALUES (@PhotoId,'14bc71cc-2838-4807-b95b-eb6f2d7625a6',@Message)";

            SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-T2GBBU6T\SQLEXPRESS;Initial Catalog=Database;Integrated Security=True");
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(insertCommentQuery, cn);
                cmd.Parameters.AddWithValue("PhotoId", photoId);
                cmd.Parameters.AddWithValue("Message", commentMessage);
                System.Diagnostics.Debug.WriteLine(photoId);
                System.Diagnostics.Debug.WriteLine(commentMessage);
                cmd.ExecuteNonQuery();
                cn.Close();
                CommentMessage.Text = "";
            }
            catch (Exception exCMD)
            {
                Console.WriteLine(exCMD.Message);
            }
            finally
            {
                this.updateComments();
            }
        }
    }

    private void updateComments()
    {
        if (Request.Params["id"] != null)
        {
            int id = int.Parse(Request.Params["id"]);
            string commentsQuery = "SELECT CommentId,u.UserName as UserName,Message,PostDate FROM Comments c,aspnet_Users u " + 
                                    "WHERE PhotoId=@pid AND u.UserId=c.UserId ORDER BY PostDate Desc";
            SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-T2GBBU6T\SQLEXPRESS;Initial Catalog=Database;Integrated Security=True");
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(commentsQuery, cn);
                cmd.Parameters.AddWithValue("pid", id);
                SqlDataReader reader = cmd.ExecuteReader();
                Comments.DataSource = reader;
                Comments.DataBind();
                cn.Close();
            }
            catch (Exception exCMD)
            {
                Console.WriteLine(exCMD.Message);
            }
        }
    }
}