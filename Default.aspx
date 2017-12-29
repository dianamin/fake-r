<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="
        SELECT TOP 100 * FROM [Photos] p, [Categories] c
        WHERE p.CategoryId = c.CategoryId
        ORDER BY p.UploadDate DESC">
    </asp:SqlDataSource>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="PhotoId,CategoryId1" 
        DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <div class="card" style="width: 20rem; display: inline-block;">
            <asp:Image ID="Image1" runat="server" class="card-img-top" ImageUrl='<%# Eval("Url") %>' />
              <div class="card-body">
                <h4 class="card-title"><%# Eval("Name") %> </h4>
                <asp:Label ID="Label1" class="card-text" runat="server"
                    Text='<%# Eval("Description") %>' />
                <asp:Label ID="Label2" class="card-text" runat="server" 
                    Text='<%# Eval("UploadDate") %>' />
                <br />
                <a href='Photo.aspx?id=<%# Eval("PhotoId") %>' class="card-link">View Details</a>
              </div>
            </div>
        </ItemTemplate>
        <LayoutTemplate>
            <div ID="itemPlaceholderContainer" runat="server" style="">
                <span runat="server" id="itemPlaceholder" />
            </div>
            <div style="">
            </div>
        </LayoutTemplate>
    </asp:ListView>
</asp:Content>

