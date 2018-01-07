<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddPhoto.aspx.cs" Inherits="AddPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2>
            <i class="material-icons md-18">add</i>
            Upload Photo
        </h2>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Image"></asp:Label>
            <asp:FileUpload ID="Image" runat="server" class="form-control"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Image" ErrorMessage="Please upload your photo!"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="Description" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Description" ErrorMessage="Please add a description!"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Location"></asp:Label>
            <asp:TextBox ID="LocationBox" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
            
            <input id="Latitude" type="hidden" runat="server" ClientIDMode="Static" />
            <input id="Longitude" type="hidden" runat="server" ClientIDMode="Static" />
        </div>
        <div class="form-group">
            <asp:Label ID="Label4" runat="server" Text="Category"></asp:Label>
            <asp:DropDownList ID="Category" runat="server" class="form-control"
                DataSourceID="CategoriesSource" DataTextField="Name" DataValueField="CategoryId">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Category" ErrorMessage="Please choose a category!"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Album"></asp:Label>
            <asp:DropDownList ID="Album" runat="server" class="form-control" AutoPostBack="true"
                DataSourceID="AlbumsSource" DataTextField="Name" DataValueField="AlbumId">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Album" ErrorMessage="Please choose an album!"></asp:RequiredFieldValidator>
        </div>
        
        <asp:SqlDataSource ID="AlbumsSource" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="CategoriesSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT [CategoryId], [Name] FROM [Categories]">
        </asp:SqlDataSource>
        <asp:Button ID="Upload" runat="server" Text="Upload" class="btn btn-primary" onclick="Upload_Click" />
    </div>
    <script>
        let input = document.getElementById('LocationBox');
        let latitude = document.getElementById('Latitude');
        let longitude = document.getElementById('Longitude');
        let searchBox = null;
        function initAutocomplete() {
            searchBox = new google.maps.places.SearchBox(input);

            searchBox.addListener('places_changed', function() {
                var places = searchBox.getPlaces();
                if (!places || places.length == 0) {
                    latitude.value = null;
                    longitude.value = null;
                    return;
                }
                latitude.value = places[0].geometry.location.lat();
                longitude.value = places[0].geometry.location.lng();
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyByCtycj8MOJ7pOQ7LtQYQ1eMKtSSJk9GA&libraries=places&callback=initAutocomplete"></script>
</asp:Content>
