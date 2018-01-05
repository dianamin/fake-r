<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h4>
        <i class="material-icons md-14">loyalty</i>
        <asp:Label ID="CategoryName" runat="server" Text="Label"></asp:Label>
    </h4>
    
    <asp:SqlDataSource ID="PhotosSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>">
    </asp:SqlDataSource>
    <asp:ListView ID="Photos" runat="server" DataSourceID="PhotosSource">
        <ItemTemplate>
            <div class="card" style="width: 20rem; display: inline-block;">
                <asp:Image ID="Image1" runat="server" class="card-img-top mx-auto d-block"
                           ImageUrl='<%# "Images/" + Eval("PhotoName") %>' />
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
                    <div class="card-text">
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
    </asp:ListView>
</asp:Content>

