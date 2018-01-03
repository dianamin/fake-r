<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="jumbotron jumbotron-fluid">
      <div class="container">
        <h1 class="display-4"><asp:Label ID="UserName" runat="server" Text="Label"></asp:Label></h1>
        <p class="lead">This is a modified jumbotron that occupies the entire horizontal space of its parent.</p>
      </div>
      <hr class="my-4" />
      <p><asp:Label ID="Name" runat="server"></asp:Label></p>
      <p><asp:Label ID="JoinDate" runat="server"></asp:Label></p>
    </div>
    <asp:Repeater ID="Albums" runat="server">
        <ItemTemplate>
            <div class="card" style="width: 10rem; display: inline-block;">
                <div class="card-body text-center">
                    <a href='Album.aspx?album=<%# Eval("AlbumId") %>'>
                        <i class="material-icons md-14"> book </i>
                        <%# Eval("Name") %>
                    </a>
                    <br />
                    <p> <%# Eval("Description") %> </p>
                    <span class="badge badge-primary badge-pill"> <%# Eval("PhotosCount") %> </span>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

