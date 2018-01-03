﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
        oncontinuebuttonclick="CreateUserWizard1_ContinueButtonClick" 
        oncreateduser="CreateUserWizard1_CreatedUser" 
        onfinishbuttonclick="CreateUserWizard1_FinishButtonClick">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <h2> Register </h2>
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                            ControlToValidate="UserName" ErrorMessage="User Name is required." 
                            ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                            ControlToValidate="Password" ErrorMessage="Password is required." 
                            ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                            AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                            ControlToValidate="ConfirmPassword" 
                            ErrorMessage="Confirm Password is required." 
                            ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                        <asp:TextBox ID="Email" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                            ControlToValidate="Email" ErrorMessage="E-mail is required." 
                            ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security Question:</asp:Label>
                        <asp:TextBox ID="Question" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                            ControlToValidate="Question" ErrorMessage="Security question is required." 
                            ToolTip="Security question is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label>
                        <asp:TextBox ID="Answer" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                            ControlToValidate="Answer" ErrorMessage="Security answer is required." 
                            ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:CompareValidator ID="PasswordCompare" runat="server" 
                            ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                            Display="Dynamic" 
                            ErrorMessage="The Password and Confirmation Password must match." 
                            ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                    </div>
                    <div style="color: red;">
                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                    </div>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Complete Profile">
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
                        <asp:Label ID="Label3" runat="server" Text="Birthday"></asp:Label>
                        <asp:TextBox ID="TBBirthday" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="TBBirthday" ErrorMessage="Data nasterii este necesara!"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ErrorMessage="Data trebuie specificata in formatul LL/ZZ/AAA" ControlToValidate="TBBirthday" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                    </div>
                </div>
            </asp:WizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="center"> Complete </td>
                        </tr>
                        <tr>
                            <td> Your account has been successfully created. </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" 
                                    CommandName="Continue" Text="Continue" ValidationGroup="CreateUserWizard1"
                                    class="btn btn-primary" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>

