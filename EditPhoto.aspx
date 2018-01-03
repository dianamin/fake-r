<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditPhoto.aspx.cs" Inherits="EditPhoto" %>

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
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-8" style="text-align: center;">
                <asp:Image ID="imgUpload" class="rounded" runat="server" style="max-height: 80vh; max-width: 100%; margin: 2px auto;" />
                <asp:HiddenField ID="X" runat="server" />  
                <asp:HiddenField ID="Y" runat="server" />  
                <asp:HiddenField ID="W" runat="server" />  
                <asp:HiddenField ID="H" runat="server" />
            </div>
            <div class="col-sm-4 jumbotron" style="height: 80vh; overflow-y: scroll;">
                <h2> Crop Photo </h2>  
                <asp:Button ID="btnCrop" runat="server" Text="Save" OnClick="btnCrop_Click" class="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>

