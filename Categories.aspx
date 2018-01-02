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
            <div class="card align-items-center" style="width: 200px; display: inline-block;">
                <div class="card-body text-center">
                    <a href='Category.aspx?category=<%# Eval("CategoryId") %>'>
                        <i class="material-icons md-14">loyalty</i>
                        <%# Eval("Name") %>
                    </a>
                    <br />
                    <span class="badge badge-primary badge-pill"> <%# Eval("PhotosCount") %> photos </span>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

