<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="Photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-8" style="text-align: center;">
                <asp:Image ID="Image" class="rounded" style="max-height: 80vh; max-width: 100%; margin: 2px auto;" runat="server" />
            </div>
            <div class="col-sm-4" class="text-right" style="height: 80vh; overflow-y: scroll;">
                <asp:Button ID="DeletePhoto" runat="server" Text="Delete" 
                    class="btn btn-danger" style="float: right;" onclick="DeletePhoto_Click" />
                <br />
                <i class="material-icons md-18">loyalty</i>
                <asp:Label ID="Category" runat="server" Text="Label" Font-Size="Large"></asp:Label>
                <br />
                <asp:Label ID="Description" runat="server" Text="Label" Font-Size="Small"></asp:Label>
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
                
                <asp:ListView ID="Comments" runat="server">
                    <ItemTemplate>
                        <div class="card">
                            <div class="card-body">
                                <asp:Button ID="DeleteComment" runat="server" Text="Delete" 
                                    class="btn btn-danger" style="float: right;" onclick="DeleteComment_Click" 
                                    CommandArgument='<%# Eval("CommentId") %>' />
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

