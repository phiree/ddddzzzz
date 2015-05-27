﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" href="css/login_reg.css" type="text/css" />
</head>
<body>
    <div class="wrap">
        <div class="head">
        </div>
        <div class="main">
            <div class="layout">
                <div class="wrap-reg">
                    <form id="form1" runat="server">
                    <div class="conReg">
                        <div class="conLogo">
                            <div class="logo-l">
                                <p>
                                    欢迎来到点助商户会员注册页面<br />
                                    开启移动智能O2O新历程</p>
                            </div>
                            <div class="logo-r">
                                <div class="logo regLogo">
                                    <a href="#">
                                        <img src="image/login_reg/zhuce_2.png" alt="logo" /></a>
                                </div>
                                <div class="logoTitle">
                                </div>
                            </div>
                        </div>
                        <div class="conMain main-reg clearfix">
                            <div class="box-l">
                                <p>
                                    <em>2</em>种注册方式</p>
                                <p>
                                    <em>
                                        <img src="image/login_reg/icon_2.png"></em><span class="methText">输入号码</span></p>
                                <p>
                                    <em>
                                        <img src="image/login_reg/phone_1.png"></em>验证码</p>
                            </div>
                            <div class="box-r">
                                <!--<p class="regMeth">-->
                                <div class="selectdiv">
                                    <div id="regMethList" class="selectMeth">
                                        <cite>手机号码 </cite>
                                        <ul>
                                            <li><a href="javascript:;" value="1">手机号码 </a></li>
                                            <li><a href="javascript:;" value="2">邮箱 </a></li>
                                        </ul>
                                        <input type="hidden" value="1" id="regMeth" />
                                    </div>
                                </div>
                                <!--<select id="regMeth" name="regMeth">-->
                                <!--<option value="1">手机号码</option>-->
                                <!--<option value="2">邮箱</option>-->
                                <!--</select>-->
                                <!--</p>-->
                                <div class="phoneBox">
                                    <p class="phone">
                                        <em>+86</em><input id="phone" name="phone" type="text" /></p>
                                    <i id="phoneCheck" class="checkIcon"></i>
                                </div>
                                <div class="emailBox">
                                    <input id="email" name="email" type="text" />
                                    <asp:TextBox runat="server" ID="tbxUserName"></asp:TextBox>
                                    <i id="emailCheck" class="checkIcon"></i>
                                </div>
                                
                                <p class="checkCode">
                                    <input id="checkCode" name="checkCode" class="checkInput" type="text" value="" /><img
                                        src="image/login_reg/yanzheng_2.png" /><input class="checkFlash" type="button"></p>
                                <p class="agree">
                                    <input id="agree" name="agree" type="checkbox" /><label class="v-m" for="agree">我已经仔细阅读过<a
                                        href="#">点助服务协议</a>,并同意所有条款</label></p>
                                <div class="buttonBox">
                                    <input id="phoneSubmit" type="button" class="regBtn" value="" />
                                    <!--<p class="savePass" ><input id="savePass" type="checkbox"/><label for="savePass">记住密码</label></p>-->
                                </div>
                                <!--<iframe name="uploadForm1" ></iframe>-->
                                <p class="doLogin">
                                    <a href="login.aspx">
                                        <img src="image/login_reg/back_2.png">返回登录</a>
                                </p>
                            </div>
                        </div>
                        <div class="conMain main-psw clearfix">
                            <div class="box-l">
                                <p>
                                    登录名</p>
                                <p>
                                    设置登录密码</p>
                                <p>
                                    <em>
                                        <img src="image/login_reg/lock_2.png"></em>登录密码</p>
                                <p>
                                    <em>
                                        <img src="image/login_reg/lock_2.png"></em>确认密码</p>
                            </div>
                            <div class="box-r">
                                <p class="rPage">
                                    XXXXXXXX</p>
                                <p class="rPage">
                                    此密码用于用户登录</p>
                                <div class="regPs">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="regPs" TextMode="Password"></asp:TextBox>
                                   <!-- <input id="regPs" name="regPs" type="password" placeholder="6-20个字符，包含数字字母" />-->
                                    <i id="psChk" class="checkIcon"></i>
                                </div>
                                <div class="regPsConf">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="regPsConf" TextMode="Password"></asp:TextBox>
                                    <!--<input id="regPsConf" name="regPsConf" type="password" placeholder="6-20个字符，包含数字字母" />-->
                                    <i id="psConfChk" class="checkIcon"></i>
                                </div>
                                <div class="buttonBox">
                                    <input id="regPsSubmit" type="button" class="regBtn" value="" />
                                    <asp:Button runat="server" ID="regPsSubmit" ClientIDMode="Static" OnClick="regPsSubmit_OnClick"
                                        CssClass="regBtn" />
                                </div>
                                <iframe name="uploadForm1" style="display: none"></iframe>
                            </div>
                        </div>
                        
                    </div>
                    </form>
                </div>
            </div>
            <div class="conReg">
            </div>
        </div>
        <div class="footer">
        </div>
    </div>
</body>
<script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/vendor/jquery.js"></script>
<script src="js/login_reg.js" type="text/javascript"></script>
</html>