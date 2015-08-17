﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/site.master"
    CodeFile="ChangePassword_suc.aspx.cs" Inherits="Account_ChangePassword" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../css/myshop.css" rel="stylesheet" type="text/css" />
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
    </asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="mainContent clearfix">
            <div class="leftContent" id="leftCont">
                <div>
                    <ul>
                        <li><a href="./Default.aspx"><i class="side-btn side-btn-myshop"></i></a></li>
                        <li><a href="./Security.aspx"><i class="side-btn side-btn-secret"></i></a></li>
                    </ul>
                </div>
            </div>
            <div class="rightContent" id="rightCont">
                
                <div class="secret-title">
                    <span >账号信息相关</span>
                </div>
                <div class="secret-cont">
                    <div class="secret-m standard-info">
                            <div class="secret-change-m">
                                <div id="CPResult" class="m-auto">
                                    <div class="t-c">
                                        <i class="icon secret-icon-done"></i>
                                        <div class="secret-change-done d-inb">
                                            <p>恭喜你</p>
                                            <p>修改密码成功</p>
                                        </div>
                                    </div>
                                    <div class="secret-change-sub"><a class="secret-a-done white-a" href="Security.aspx">确认</a></div>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
    </div>
    </asp:Content>
    <asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js" ></script>
    <script type="text/javascript" src="http://apps.bdimg.com/libs/angular.js/1.2.5/angular.min.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    </asp:Content>
