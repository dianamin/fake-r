<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT c.CategoryId, c.Name, COUNT(p.PhotoId)  as PhotosCount
FROM [Categories] c LEFT JOIN [Photos] p ON (p.CategoryId = c.CategoryId)
GROUP BY c.CategoryId, c.Name
ORDER BY PhotosCount DESC"></asp:SqlDataSource>
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <div class="list-group-item d-flex justify-content-between align-items-center">
                <%# Eval("Name") %>
                <span class="badge badge-primary badge-pill"> <%# Eval("PhotosCount") %> </span>
            </div>
            
            <br />

        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

