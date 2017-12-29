<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="
        SELECT TOP 100 PhotoId,Url,UploadDate,a.Name as AlbumName, a.AlbumId as AlbumId,p.Description as Description,c.Name,a.UserName as UserName
        FROM [Photos] p, [Categories] c, [Albums] a
        WHERE p.CategoryId = c.CategoryId AND a.AlbumId = p.AlbumId
        ORDER BY p.UploadDate DESC">
    </asp:SqlDataSource>
    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <div class="card" style="width: 20rem; display: inline-block;">
                <asp:Image ID="Image1" runat="server" class="card-img-top" ImageUrl='<%# Eval("Url") %>' />
                <div class="card-body">     
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"> 
                            <a href='User.aspx?username=<%# Eval("UserName") %>'>
                                <i class="material-icons md-14">person</i>
                                <%# Eval("UserName") %>
                            </a> 
                        </li>
                        <li class="breadcrumb-item"> 
                            <a href='Album.aspx?album=<%# Eval("AlbumId") %>'>
                                <i class="material-icons md-14">book</i>
                                <%# Eval("AlbumName") %>
                            </a>
                        </li>
                    </ol>
                    <h4 class="card-title">
                        <i class="material-icons">loyalty</i>
                        <%# Eval("Name") %>
                    </h4>
                    <div class="card-text" style="height: 22px; width: 100%; text-overflow: ellipsis; display: block; overflow: hidden; white-space: nowrap;">
                        <%# Eval("Description") %>
                    </div>
                    <div>
                        <i class="material-icons md-14">date_range</i>
                        <asp:Label ID="Label2" class="card-text" runat="server" 
                                   Text='<%# Eval("UploadDate") %>' />
                    </div>
                    <a href='Photo.aspx?photo=<%# Eval("PhotoId") %>' class="card-link">
                        <i class="material-icons md-14">remove_red_eye</i>
                        View Details
                    </a>
                </div>
            </div>
        </ItemTemplate>
        <LayoutTemplate>
            <div ID="itemPlaceholderContainer" runat="server">
                <span runat="server" id="itemPlaceholder" />
            </div>
            <div style="">
            </div>
        </LayoutTemplate>
    </asp:ListView>
</asp:Content>

