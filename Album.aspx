<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <ol class="breadcrumb">
        <li class="breadcrumb-item"> <asp:Label ID="UserName" runat="server" Text="Label"></asp:Label> </li>
        <li class="breadcrumb-item"> <asp:Label ID="AlbumName" runat="server" Text="Label" Font-Size="Large"></asp:Label> </li>
    </ol>

    <br />
    <asp:ListView ID="Photos" runat="server">
        <ItemTemplate>
            <div class="card" style="width: 20rem; display: inline-block;">
                <asp:Image ID="Image1" runat="server" class="card-img-top" ImageUrl='<%# "Images/" + Eval("PhotoName") %>' />
                <div class="card-body">
                    <asp:Label ID="Label1" class="card-text" runat="server"
                        Text='<%# Eval("Description") %>' />
                    <asp:Label ID="Label2" class="card-text" runat="server" 
                        Text='<%# Eval("UploadDate") %>' />
                    <br />
                    <a href='Photo.aspx?photo=<%# Eval("PhotoId") %>' class="card-link">View Details</a>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>

