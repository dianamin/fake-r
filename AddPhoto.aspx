<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddPhoto.aspx.cs" Inherits="AddPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h5>Upload a Photo</h5>
    <asp:LoginView ID="AddPhotoForm" runat="server">
        <LoggedInTemplate>
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Image"></asp:Label>
                <asp:FileUpload ID="Image" runat="server" class="form-control"/>
            </div>
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label>
                <asp:TextBox ID="Description" runat="server" class="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="Label4" runat="server" Text="Category"></asp:Label>
                <asp:DropDownList ID="Category" runat="server" class="form-control"
                    DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="CategoryId">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Album"></asp:Label>
                <asp:DropDownList ID="Album" runat="server" class="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                SelectCommand="SELECT [CategoryId], [Name] FROM [Categories]">
            </asp:SqlDataSource>
            <asp:Button ID="Upload" runat="server" onclick="Upload_Click" Text="Upload" class="btn btn-primary" />
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>

