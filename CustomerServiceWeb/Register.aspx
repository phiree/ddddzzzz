<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr><td>用户名</td><td><asp:TextBox runat="server" ID="tbxUserName"></asp:TextBox></td></tr>
        <tr><td>密码</td><td><asp:TextBox runat="server" ID="tbxPassword"></asp:TextBox></td></tr>
        <tr><td><asp:Button runat="server" ID="btnRegister" OnClick="btnRegister_Click" Text="注册" /></td><td>

            <asp:Label runat="server" ID="lblMsg"></asp:Label>
                                                                             </td></tr>
    </table>
</asp:Content>

