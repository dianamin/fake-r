<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LoginView ID="EditProfileLoginView" runat="server">
        <LoggedInTemplate>
            <div>
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label>
                    <asp:TextBox ID="TBFirstName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBFirstName" ErrorMessage="Prenumele este necesar!"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
                    <asp:TextBox ID="TBLastName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TBLastName" ErrorMessage="Numele este necesar!"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label>
                    <asp:TextBox ID="TBDescription" runat="server" class="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="SaveProfile" runat="server" Text="Save Profile" 
                    onclick="SaveProfile_Click" class="btn btn-primary" />
            </div>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>

