<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="Photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-8" style="text-align: center;">
                <asp:Image ID="Image" class="rounded" style="max-height: 80vh; max-width: 100%; margin: 2px auto;" runat="server" />
            </div>
            <div class="col-sm-4 jumbotron" style="height: 80vh; overflow-y: scroll;">
                <asp:Panel class="btn-group" visible="<%# seeEditButtons %>" style="float: right;" runat="server" >
                    <asp:Button ID="EditPhotoDetails" runat="server" Text="Edit Details" 
                        class="btn btn-warning" onclick="EditPhotoDetails_Click" />
                    <asp:Button ID="CropPhoto" runat="server" Text="Crop" 
                        class="btn btn-warning" onclick="CropPhoto_Click" />
                    <asp:Button ID="DeletePhoto" runat="server" Text="Delete" 
                        class="btn btn-danger" onclick="DeletePhoto_Click" 
                        OnClientClick="return confirm('Are you sure you want to delete this photo?')" />
                </asp:Panel>
                <br />
                <i class="material-icons md-18">loyalty</i>
                <asp:Label ID="Category" runat="server" Text="Label" Font-Size="Large"></asp:Label>
                <br />
                <asp:Label ID="Description" runat="server" Text="Label" Font-Size="Small"></asp:Label>
                <div>
                    <i class="material-icons md-14">date_range</i>
                    <asp:Label ID="UploadDate" class="card-text" runat="server" />
                </div>
                <hr />
                <asp:LoginView ID="LoginView2" runat="server">
                    <LoggedInTemplate>
                        <div class="form-group form-inline">
                            <asp:TextBox ID="CommentMessage" class="form-control" runat="server"></asp:TextBox>
                            <asp:Button ID="AddComment" runat="server" Text="Post" 
                                class="btn btn-primary" onclick="AddComment_Click" style="margin-left: 3px;" />
                        </div>
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
                                    CommandArgument='<%# Eval("CommentId") %>' visible="<%# seeEditButtons %>"
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

