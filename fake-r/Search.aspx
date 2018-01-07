<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>
        <i class="material-icons md-18">search</i>
        Search
    </h2>
    <div class="input-group">
        <asp:TextBox ID="SearchBox" runat="server" class="form-control"></asp:TextBox>
        <div class="input-group-btn">
            <asp:Button ID="SearchButton" runat="server" class="btn btn-default" Text="Search" onclick="Search_Click" />
            
            <input id="LatitudeField" type="hidden" runat="server" ClientIDMode="Static" />
            <input id="LongitudeField" type="hidden" runat="server" ClientIDMode="Static" />
        </div>
    </div>
    <br />
    <div class="input-group">
        <asp:TextBox ID="LocationBox" runat="server" class="form-control" ClientIDMode="Static" style="border-bottom-left-radius: 0"></asp:TextBox>
        <div class="input-group-btn">
            <asp:Button ID="Button1" runat="server" class="btn btn-default" Text="Search by location"
                        style="border-bottom-right-radius: 0" onclick="SearchLocation_Click" />
        </div>
        <input id="Hidden1" type="hidden" runat="server" ClientIDMode="Static" />
        <input id="Hidden2" type="hidden" runat="server" ClientIDMode="Static" />
    </div>
    <div id="googleMap" style="width:100%; height:150px;"></div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>">
    </asp:SqlDataSource>
    <asp:ListView ID="Results" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <div class="card" style="width: 20rem; display: inline-block;">
                <asp:Image ID="Image1" runat="server" class="card-img-top mx-auto d-block" 
                           ImageUrl='<%# "Images/" + Eval("PhotoName") %>' ImageAlign="Middle" />
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
        let input = document.getElementById('LocationBox');
        let latitude = document.getElementById('LatitudeField');
        let longitude = document.getElementById('LongitudeField');

        let searchBox = null;

        function initAutocomplete() {
            searchBox = new google.maps.places.SearchBox(input);
           
            let mapProp = {
                center: new google.maps.LatLng(<%# Latitude %>, <%# Longitude %>),
                zoom:5,
            };

            let map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
            let marker = new google.maps.Marker({position: mapProp.center});
            marker.setMap(map);

            searchBox.addListener('places_changed', function() {
                var places = searchBox.getPlaces();
                if (!places || places.length == 0) {
                    latitude.value = null;
                    longitude.value = null;
                    return;
                }
                latitude.value = places[0].geometry.location.lat();
                longitude.value = places[0].geometry.location.lng();
                marker.setMap(null);
                let position = new google.maps.LatLng(latitude.value, longitude.value);
                marker = new google.maps.Marker({position: position});
                marker.setMap(map);
                map.setCenter(position);
            });
        }
    </script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyByCtycj8MOJ7pOQ7LtQYQ1eMKtSSJk9GA&libraries=places&callback=initAutocomplete"></script>
</asp:Content>

