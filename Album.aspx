<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="UserName" runat="server" Text="Label"></asp:Label> /
    <asp:Label ID="AlbumName" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:ListView ID="Photos" runat="server">
        <ItemTemplate>
            <div class="card" style="width: 20rem; display: inline-block;">
                <asp:Image ID="Image1" runat="server" class="card-img-top" ImageUrl='<%# Eval("Url") %>' />
                <div class="card-body">
                    <asp:Label ID="Label1" class="card-text" runat="server"
                        Text='<%# Eval("Description") %>' />
                    <asp:Label ID="Label2" class="card-text" runat="server" 
                        Text='<%# Eval("UploadDate") %>' />
                    <br />
                    <a href='Photo.aspx?id=<%# Eval("PhotoId") %>' class="card-link">View Details</a>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>

