﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <ol class="breadcrumb">
        <li class="breadcrumb-item"> <asp:Label ID="UserName" runat="server" Text="Label"></asp:Label> </li>
        <li class="breadcrumb-item"> <asp:Label ID="AlbumName" runat="server" Text="Label" Font-Size="Large"></asp:Label> </li>
    </ol>
    <asp:Button ID="DeleteAlbum" runat="server" Text="Delete Album" 
        onclick="DeleteAlbum_Click" class="btn btn-danger" visible="<%# canDelete %>"
        OnClientClick="return confirm('Are you sure you want to delete this album?')" />
    <br />
    <br />
    <asp:ListView ID="Photos" runat="server">
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
                    </ol>
                    <h4 class="card-title">
                        <i class="material-icons">loyalty</i>
                        <%# Eval("CategoryName") %>
                    </h4>
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

