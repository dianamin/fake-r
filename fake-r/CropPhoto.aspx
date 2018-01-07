<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CropPhoto.aspx.cs" Inherits="CropPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <link href="CSS/jquery.Jcrop.css" rel="stylesheet" />  
    <script src="JS/jquery.Jcrop.js" type="text/javascript"></script>  
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=imgUpload.ClientID%>').Jcrop({
                onSelect: SelectCropArea
            });
        });

        function SelectCropArea(c) {
            $('#<%=X.ClientID%>').val(parseInt(c.x));
            $('#<%=Y.ClientID%>').val(parseInt(c.y));
            $('#<%=W.ClientID%>').val(parseInt(c.w));
            $('#<%=H.ClientID%>').val(parseInt(c.h));
        }  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2>
            <i class="material-icons md-18">content_cut</i>
            Crop Photo
        </h2>
        <asp:Image ID="imgUpload" class="rounded" runat="server" style="max-height:65vh; max-width: 100%; margin: 2px auto;" />
        <asp:HiddenField ID="X" runat="server" />  
        <asp:HiddenField ID="Y" runat="server" />  
        <asp:HiddenField ID="W" runat="server" />  
        <asp:HiddenField ID="H" runat="server" />
        <br />
        <asp:Button ID="btnCrop" runat="server" Text="Save" OnClick="btnCrop_Click" class="btn btn-primary" />
    </div>
</asp:Content>

