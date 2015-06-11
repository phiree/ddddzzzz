﻿<%@ Page Title="" Language="C#" MasterPageFile="~/a.master" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link rel='stylesheet' href='static/css/chat.css'>
    <script src='static/js/chat.js'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Gab</h1>

    <div id='toolbar'>
      <span class='button' id='new-contact'>add contact...</span> ||
      <span class='button' id='new-chat'>chat with...</span> ||
      <span class='button' id='disconnect'>disconnect</span>
    </div>

    <div id='chat-area'>
      <ul></ul>
    </div>
    
    <div id='roster-area'>
      <ul></ul>
    </div>

    <!-- login dialog -->
    <div id='login_dialog' class='hidden'>
      <label>JID:</label><input type='text' id='jid'>
      <label>Password:</label><input type='password' id='password'>
    </div>

    <!-- contact dialog -->
    <div id='contact_dialog' class='hidden'>
      <label>JID:</label><input type='text' id='contact-jid'>
      <label>Name:</label><input type='text' id='contact-name'>
    </div>

    <!-- chat dialog -->
    <div id='chat_dialog' class='hidden'>
      <label>JID:</label><input type='text' id='chat-jid'>
    </div>

    <!-- approval dialog -->
    <div id='approve_dialog' class='hidden'>
      <p><span id='approve-jid'></span> has requested a subscription
        to your presence.  Approve or deny?</p>
    </div>

</asp:Content>
