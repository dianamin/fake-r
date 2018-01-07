<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2>
            <i class="material-icons md-18">book</i>
            Album
        </h2>
        <div class="jumbotron jumbotron-fluid">
            <div class="row">
                <div class="col-md-6 d-flex align-items-center justify-content-end" style="padding-right: 20px; border-right: 1px solid #aaaaaa;">
                    <div>
                        <h1 class="display-4" style="text-align: right">
                            <asp:Label ID="AlbumName" runat="server"></asp:Label>
                        </h1>

                        <asp:Panel ID="Panel1" class="btn-group" visible="<%# seeEditButtons %>" style="float: right;" runat="server" >
                            <asp:Button ID="EditAlbum" runat="server" Text="Edit album" 
                                class="btn btn-default" onclick="EditAlbum_Click" />
                            <asp:Button ID="DeleteAlbum" runat="server" Text="Delete album" 
                                onclick="DeleteAlbum_Click" class="btn btn-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this album?')" />
                        </asp:Panel>
                    </div>
                </div>

                <div class="col-md-6 d-flex align-items-center justify-content-start" style="padding-left: 20px">
                    <blockquote class="blockquote">
                        <asp:Label ID="Description" runat="server" Text=""></asp:Label>
                        <footer class="blockquote-footer"> <asp:Label ID="UserName" runat="server"></asp:Label> </footer>
                    </blockquote>
                </div>
            </div>
        </div>
    </div>
    <div id="googleMap" style="width:100%; height:200px;"></div>
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

