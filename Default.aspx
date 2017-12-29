<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="
        SELECT TOP 100 PhotoId,Url,UploadDate,a.Name as AlbumName, a.AlbumId as AlbumId,p.Description as Description,c.Name,u.userName as UserName
        FROM [Photos] p, [Categories] c, [Albums] a, [aspnet_Users] u
        WHERE p.CategoryId = c.CategoryId AND a.AlbumId = p.AlbumId AND u.UserId = p.UserId
        ORDER BY p.UploadDate DESC">
    </asp:SqlDataSource>
    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <div class="card" style="width: 20rem; display: inline-block;">
            <asp:Image ID="Image1" runat="server" class="card-img-top" ImageUrl='<%# Eval("Url") %>' />
              <div class="card-body">     
                <ol class="breadcrumb">
                  <li class="breadcrumb-item"> <a href='User.aspx?username=<%# Eval("UserName") %>'> <%# Eval("UserName") %> </a> </li>
                  <li class="breadcrumb-item"> <a href='Album.aspx?album=<%# Eval("AlbumId") %>'> <%# Eval("AlbumName") %> </a> </li>
                </ol>
                <h4 class="card-title"><%# Eval("Name") %> </h4>
                <asp:Label ID="Label1" class="card-text" runat="server"
                    Text='<%# Eval("Description") %>' />
                <asp:Label ID="Label2" class="card-text" runat="server" 
                    Text='<%# Eval("UploadDate") %>' />
                <br />
                <a href='Photo.aspx?photo=<%# Eval("PhotoId") %>' class="card-link">View Details</a>
              </div>
            </div>
        </ItemTemplate>
        <LayoutTemplate>
            <div ID="itemPlaceholderContainer" runat="server" style="">
                <span runat="server" id="itemPlaceholder" />
            </div>
            <div style="">
            </div>
        </LayoutTemplate>
    </asp:ListView>
</asp:Content>

