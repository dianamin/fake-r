<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Login ID="Login1" runat="server" style="width: 100%">
        <LayoutTemplate>
            <h2>
                <i class="material-icons md-18">exit_to_app</i>
                Login
            </h2>
            <div class="form-group">
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                <asp:TextBox ID="UserName" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                    ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                <asp:TextBox ID="Password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                    ControlToValidate="Password" ErrorMessage="Password is required." 
                    ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
            </div>
            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                ValidationGroup="Login1" class="btn btn-primary"/>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>