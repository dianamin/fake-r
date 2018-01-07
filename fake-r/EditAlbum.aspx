<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditAlbum.aspx.cs" Inherits="EditAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2>
            <i class="material-icons md-18">edit</i>
            Edit Album
        </h2>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="Name" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name" ErrorMessage="Please add a name!"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="Description" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Description" ErrorMessage="Please add a description!"></asp:RequiredFieldValidator>
        </div>
        <asp:Button ID="SaveAlbum" runat="server" Text="Save" class="btn btn-primary" 
            onclick="SaveAlbum_Click" />
    </div>
</asp:Content>

