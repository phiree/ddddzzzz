<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
     用户名:<asp:TextBox runat="server" ID="tbxUserName"></asp:TextBox>
 </div>
    <div>
        密码:<asp:TextBox runat="server" ID="tbxPwd"></asp:TextBox>
    </div>
    <div>
        <asp:Button runat="server" ID="btnLogin" Text="登录" />
    </div>
</asp:Content>

