<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="jumbotron jumbotron-fluid">
      <div class="container">
        <h1 class="display-4"><asp:Label ID="UserName" runat="server" Text="Label"></asp:Label></h1>
        <hr class="my-4" />
        <p><asp:Label ID="Name" runat="server"></asp:Label></p>
        <p><asp:Label ID="Description" runat="server"></asp:Label></p>
        <p><asp:Label ID="JoinDate" runat="server"></asp:Label></p>
      </div>
    </div>
    <asp:LoginView runat="server">
        <RoleGroups>
            <asp:RoleGroup Roles="Admin">
                <ContentTemplate>
                    <asp:Button ID="DeleteUser" runat="server" Text="Delete account"
                                OnClick="DeleteUser_Click" class="btn btn-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this user?')" />
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
    <br />
    <br />
    <h2> Albums </h2>
    <asp:Button ID="AddAlbum" runat="server" Text="Add Album" 
        onclick="AddAlbum_Click" class="btn btn-primary" visible="<%# seeButtons %>"/>
    <br />
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

