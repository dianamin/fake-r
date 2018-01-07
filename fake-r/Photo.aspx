<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="Photo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-8" style="text-align: center;">
                <asp:Image ID="Image" class="rounded" style="max-height: 80vh; max-width: 100%; margin: 2px auto;" runat="server" />
            </div>
            <div class="col-sm-4 jumbotron" style="height: 80vh; overflow-y: scroll;">
                <h2 style="margin-bottom: 0px; padding-bottom: 0px;"> 
                    <i class="material-icons md-18">loyalty</i>
                    <asp:Label ID="Category" runat="server" Text="Label"></asp:Label>
                </h2>
                <ol class="breadcrumb" style="margin-bottom: 0px;">
                    <li class="breadcrumb-item"> 
                        <a href='<%# "User.aspx?username=" + UserName %>'>
                            <i class="material-icons md-14">person</i>
                            <%# UserName %>
                        </a> 
                    </li>
                    <li class="breadcrumb-item"> 
                        <a href='<%# "Album.aspx?album=" + AlbumId %>'>
                            <i class="material-icons md-14">book</i>
                            <%# AlbumName %>
                        </a>
                    </li>
                </ol>
                <div style="margin-left: 15px">
                    <i class="material-icons md-14">date_range</i>
                    <asp:Label ID="UploadDate" class="card-text" runat="server" />
                </div>
                <p style="margin-left: 35px">
                    <asp:Label ID="Description" runat="server" Text="Label"></asp:Label>
                </p>
                <div id="googleMap" style="width:100%; height:150px;"></div>
                <asp:Panel ID="Panel1" visible="<%# SeeEditButtons %>" runat="server">
                    <br />
                    <div class="btn-group">
                        <asp:Button ID="EditPhotoDetails" runat="server" Text="Edit Details" 
                            class="btn btn-default" onclick="EditPhotoDetails_Click" />
                        <asp:Button ID="CropPhoto" runat="server" Text="Crop" 
                            class="btn btn-default" onclick="CropPhoto_Click" />
                        <asp:Button ID="DeletePhoto" runat="server" Text="Delete" 
                            class="btn btn-danger" onclick="DeletePhoto_Click" 
                            OnClientClick="return confirm('Are you sure you want to delete this photo?')" />
                    </div>
                </asp:Panel>
                <hr />
                <asp:LoginView ID="LoginView2" runat="server">
                    <LoggedInTemplate>
                        <asp:Panel id="CommentPanel" runat="server" DefaultButton="AddComment">
                            <div class="input-group">
                                <asp:TextBox ID="CommentMessage" class="form-control" runat="server"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:Button ID="AddComment" runat="server" Text="Post" ValidationGroup="CommentGroup"
                                        class="btn btn-primary" onclick="AddComment_Click"/>
                                </div>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="CommentGroup"
                                    ControlToValidate="CommentMessage" ErrorMessage="The comment cannot be empty!">
                            </asp:RequiredFieldValidator>
                        </asp:Panel>
                    </LoggedInTemplate>
                </asp:LoginView>
                
                <asp:SqlDataSource ID="CommentsSource" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>">
                </asp:SqlDataSource>
                <asp:ListView ID="Comments" runat="server" DataSourceID="CommentsSource">
                    <ItemTemplate>
                        <div class="card">
                            <div class="card-body">
                                <asp:Button ID="DeleteComment" runat="server" Text="Delete" 
                                    class="btn btn-danger" style="float: right;" onclick="DeleteComment_Click" 
                                    CommandArgument='<%# Eval("CommentId") %>' visible="<%# SeeEditButtons %>"
                                    OnClientClick="return confirm('Are you sure you want to delete this comment?')" />
                                <h6 class="card-title"> 
                                    <i class="material-icons md-14">person</i>
                                    <%# Eval("UserName") %>
                                </h6>
                                <h6 class="card-subtitle mb-2 text-muted">
                                    <i class="material-icons md-14">date_range</i>
                                    <%# Eval("PostDate") %>
                                </h6>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                            </div>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script>
        function myMap() {
            let mapProp = {
                center:new google.maps.LatLng(<%# Latitude %>, <%# Longitude %>),
                zoom:5,
            };

            let map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
            let marker = new google.maps.Marker({position: mapProp.center});
            marker.setMap(map);
        }
    </script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyByCtycj8MOJ7pOQ7LtQYQ1eMKtSSJk9GA&callback=myMap"></script>
</asp:Content>