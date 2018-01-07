<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2>
            <i class="material-icons md-18">person</i>
            User Profile
        </h2>
        <div class="jumbotron jumbotron-fluid">
            <div class="row">
                <div class="col-md-6 d-flex align-items-center justify-content-end" style="padding-right: 20px; border-right: 1px solid #aaaaaa;">
                    <div>
                        <h1 class="display-4" style="text-align: right">
                            <asp:Label ID="UserName" runat="server" Text="Label"></asp:Label>
                        </h1>
                
                        <asp:LoginView ID="AdminView" runat="server">
                            <RoleGroups>
                                <asp:RoleGroup Roles="Admin">
                                    <ContentTemplate>
                                        <br />
                                        <div class="btn-group">
                                            <asp:Button ID="ChangeRole" runat="server" Text="Change role"
                                                        OnClick="ChangeRole_Click" class="btn btn-danger"
                                                        OnClientClick="return confirm('Are you sure you want to change the role of this user?')" />
                                            <asp:Button ID="DeleteUser" runat="server" Text="Delete account"
                                                        OnClick="DeleteUser_Click" class="btn btn-danger"
                                                        OnClientClick="return confirm('Are you sure you want to delete this user?')" />
                                        </div>
                                        <br />
                                        <asp:Label ID="UserError" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:RoleGroup>
                            </RoleGroups>
                        </asp:LoginView>
                    </div>
                </div>

                <div class="col-md-6" style="text-align: left; padding-left: 20px">
                    <p><asp:Label ID="Name" runat="server"></asp:Label></p>
                    <p><asp:Label ID="Description" runat="server"></asp:Label></p>
                    <p><asp:Label ID="JoinDate" runat="server"></asp:Label></p>
                    <asp:Label ID="UserRole" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div id="googleMap" style="width:100%; height:200px;"></div>
        <br />
        <h2>
            <i class="material-icons md-18">book</i>
             Albums
        </h2>
        <asp:Button ID="AddAlbum" runat="server" Text="New album" 
            onclick="AddAlbum_Click" class="card add-album" visible="<%# seeButtons %>" />
        <asp:SqlDataSource ID="AlbumsSource" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>">
        </asp:SqlDataSource>
        <asp:Repeater ID="Albums" runat="server" DataSourceID="AlbumsSource">
            <ItemTemplate>
                <div class="card" style="width: 10rem; height: 160px; vertical-align: top; display: inline-block;">
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
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script>
        function myMap() {
            let mapProp = {
                zoom:2,
                center: new google.maps.LatLng(44, 26)
            };
            let locations = <%# Locations %>

            let map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
            locations.forEach((location) => {
                let marker = new google.maps.Marker({position: new google.maps.LatLng(location.Latitude, location.Longitude)});
                marker.setMap(map);
            });
        }
    </script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyByCtycj8MOJ7pOQ7LtQYQ1eMKtSSJk9GA&callback=myMap"></script>
</asp:Content>