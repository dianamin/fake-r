<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand= "SELECT photosCount.UserName as UserName, PhotosCount, AlbumsCount
                        FROM (
                        SELECT u.UserName as UserName, COUNT(p.PhotoId) as PhotosCount
                        FROM aspnet_Users u
                        LEFT JOIN Albums a ON (a.UserName = u.UserName) LEFT JOIN Photos p ON (a.AlbumId = p.AlbumId)
                        GROUP BY u.UserName
                        ) photosCount JOIN (
                        SELECT u.UserName as UserName, COUNT(a.AlbumId) as AlbumsCount
                        FROM aspnet_Users u
                        LEFT JOIN Albums a ON (u.UserName = a.UserName)
                        GROUP BY u.UserName
                        ) albumsCount ON (photosCount.UserName = albumsCount.UserName);">
    </asp:SqlDataSource>
    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <div class="list-group-item d-flex justify-content-between align-items-center">
                <a href='User.aspx?username=<%# Eval("UserName") %>'>
                    <%# Eval("UserName") %>
                </a>
                <span class="badge badge-primary badge-pill"> <%# Eval("PhotosCount") %> Photos </span>
                <span class="badge badge-primary badge-pill"> <%# Eval("AlbumsCount") %> Albums </span>
            </div>
            <br />
        </ItemTemplate>
    </asp:ListView>
</asp:Content>

