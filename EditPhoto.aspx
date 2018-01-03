<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditPhoto.aspx.cs" Inherits="EditPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <link href="CSS/jquery.Jcrop.css" rel="stylesheet" />  
    <script src="JS/jquery.Jcrop.js"></script>  
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
    <h3>Image Upload, Crop & Save using ASP.NET & Jquery</h3>  
    <table>  
        <tr>  
            <td> Select Image File : </td>  
            <td>  
                <asp:FileUpload ID="FU1" runat="server" /> </td>  
            <td>  
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /> </td>  
        </tr>  
        <tr>  
            <td colspan="3">  
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" /> </td>  
        </tr>  
    </table>  
    <asp:Panel ID="panCrop" runat="server" Visible="false">  
        <table width="50%">  
            <tr>  
                <td>  
                    <asp:Image ID="imgUpload" runat="server" Width="100%" /> </td>  
            </tr>  
            <tr>  
                <td>  
                    <asp:Button ID="btnCrop" runat="server" Text="Crop & Save" OnClick="btnCrop_Click" /> </td>  
            </tr>  
            <tr>  
                <td>  
                    <asp:HiddenField ID="X" runat="server" />  
                    <asp:HiddenField ID="Y" runat="server" />  
                    <asp:HiddenField ID="W" runat="server" />  
                    <asp:HiddenField ID="H" runat="server" /> </td>  
            </tr>  
        </table>  
    </asp:Panel>
</asp:Content>

