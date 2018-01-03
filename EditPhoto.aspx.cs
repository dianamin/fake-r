using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

public partial class EditPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        // Upload Original Image Here  
        string uploadFileName = "";
        string uploadFilePath = "";
        if (FU1.HasFile)
        {
            string ext = Path.GetExtension(FU1.FileName).ToLower();
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
            {
                uploadFileName = Guid.NewGuid().ToString() + ext;
                uploadFilePath = Path.Combine(Server.MapPath("~/UploadImages"), uploadFileName);
                try
                {
                    FU1.SaveAs(uploadFilePath);
                    imgUpload.ImageUrl = "~/UploadImages/" + uploadFileName;
                    panCrop.Visible = true;
                }
                catch (Exception)
                {
                    lblMsg.Text = "Error! Please try again.";
                }
            }
            else
            {
                lblMsg.Text = "Selected file type not allowed!";
            }
        }
        else
        {
            lblMsg.Text = "Please select file first!";
        }
    }

    protected void btnCrop_Click(object sender, EventArgs e) 
    {
        // Crop Image Here & Save  
        string fileName = Path.GetFileName(imgUpload.ImageUrl);
        string filePath = Path.Combine(Server.MapPath("~/UploadImages"), fileName);
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
                cropFilePath = Path.Combine(Server.MapPath("~/UploadImages"), cropFileName);
                bitMap.Save(cropFilePath);
                Response.Redirect("~/UploadImages/" + cropFileName, false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }   
    }
}