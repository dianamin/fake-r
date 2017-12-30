<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddPhoto.aspx.cs" Inherits="AddPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:FileUpload ID="Image" runat="server" />
    <br />
    <asp:TextBox ID="Description" runat="server" Height="73px" Width="280px"></asp:TextBox>
    <br />
    <br />
    <asp:DropDownList ID="Category" runat="server" 
        DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="CategoryId" 
        Height="33px" Width="121px">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT [CategoryId], [Name] FROM [Categories]">
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="Upload" runat="server" onclick="Upload_Click" Text="Upload" />
</asp:Content>

