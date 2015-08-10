﻿
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Business/Default.aspx.cs" Inherits="Business_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--<!doctype html>-->
<!--店铺列表-->
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>商家后台主页</title>
    <link href="/js/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="/js/metisMenu/metisMenu.css" rel="stylesheet" type="text/css" />
    <link href="/css/onePointFive-custom.css" rel="stylesheet" type="text/css" />
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <link href="/css/business.css" rel="stylesheet" type="text/css" />
    <link href="/css/validation.css" rel="stylesheet" type="text/css" />

    <%--<link href="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jqueryui/themes\jquery-ui-1.10.4.custom\css\custom-theme\jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />--%>
    <script src="/js/html5shiv.min.js"></script>
    <script src="/js/respond.min.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
</head>
<body>
        <div class="wrap">
            <div class="mainContainer">

                <form id="form1" runat="server">
                    <div class="wrapper">
                        <div class="navbar navbar-default navbar-static-top" role="navigation">
                            <div class="container-fluid">
                                <div class="navbar-header">
                                      <!--<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">-->
                                    <!--<span class="sr-only">Toggle navigation</span>-->
                                    <!--<span class="icon-bar"></span>-->
                                    <!--<span class="icon-bar"></span>-->
                                    <!--<span class="icon-bar"></span>-->
                                  <!--</button>-->
                                  <a class="navbar-brand" href="#">
                                       <img alt="一点半" src="/image/master/shop-LOGO-1.png">
                                  </a>
                                  <a class="navbar-brand" >
                                    一点半商户后台管理
                                    </a>
                                </div>
                                <ul class="nav navbar-nav navbar-right">
                                    <li class="dropdown">
                                      <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><asp:LoginName ID="LoginName1" runat="server" /><span class="caret"></span></a>
                                      <ul class="dropdown-menu">
                                        <li><a href="#">帐号安全</a></li>
                                      </ul>
                                    </li>
                                    <li role="presentaion" ><asp:LoginStatus ID="LoginStatus1" formnovalidate  runat="server"  /></li>
                                  </ul>
                            </div>
                        </div>

                        <!--<div class="clearfix">-->
                            <!--<div class="sidebar">-->
                                    <!--<aside class="sidebar">-->
                                      <!--<nav class="sidebar-nav">-->
                                        <!--<ul class="metismenu" id="menu">-->
                                          <!--<li>-->
                                            <!--<a href="#">我的商铺</a>-->
                                          <!--</li>-->
                                          <!--<li>-->
                                            <!--<a href="#">我的服务</a>-->
                                          <!--</li>-->
                                          <!--<li role="presentation" class="disabled">-->
                                            <!--<a href="#" >我的订单</a>-->
                                            <!--&lt;!&ndash;<a href="#">Menu 2 <span class="glyphicon arrow"></span></a>&ndash;&gt;-->
                                          <!--</li>-->
                                        <!--</ul>-->
                                      <!--</nav>-->
                                    <!--</aside>-->
                            <!--</div>-->
                         <!--</div>-->
                <div class="business-container">
                    <div class="business-add">
                           <a id="addNewBusiness" href="/Business/edit.aspx" class="btn btn-add">+&nbsp;新建店铺</a>
                        </div>
                       <div class="business-list">
                           <asp:Repeater runat="server" ID="rptBusinessList">
                              <ItemTemplate>
                                  <div class="business-list-item m-b20">
                                    <div class="cont-container">
                                        <div class="cont-row">
                                            <div class="cont-col-4">

                                                <div class="business-h"><%#Eval("Name")%></div>
</div>
                                            <div class="cont-col-8">
                                                <div class="cont-row">
                                                    <div class="cont-col-12">
        <p class="business-default-intro">
            服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。
</p>
</div>
</div>
                                                <div class="cont-row">
                                                    <div class="cont-col-6">
                                                        <div class="business-note">
                                                        <p><span>联系电话：</span><span></span></p>
                                                        <p><span>从业时间：</span><span></span></p>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col-6">
                                                        <div class="business-note">
                                                            <p><span>店铺地址：</span><span></span></p>
                                                            <p><span>员工人数：</span><span></span></p>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="cont-row">
                                                    <div class="cont-col">
                                                        <p class="t-r"><a class="btn btn-info btn-into" href='Detail.aspx?businessId=<%#Eval("Id") %>'>进入店铺</a></p>
                                                    </div>

</div>
</div>
</div>
</div>
                                  </div>
                              </ItemTemplate>
                          </asp:Repeater>
                        </div>
                    </div>
                   <!--</div>-->
                   </div>
                   <div id="newBusslightBox" class="dis-n">
                       <div class="cont-container BusslightBox">
                       <div class="cont-row">
                        <div class="cont-col-12">
                            <p class="cont-h2 t-c p-20 theme-color-58789a">创建新的商铺</p>
                        </div>
                        </div>
                        <div class="cont-row m-b10">
                            <div class="cont-col-4"><p class="cont-h5 theme-color-58789a">您的商铺名称</p></div>
                                <div class="cont-col-8">
                                <p><input class="input-mid" runat="server" id="tbxName" type="text" /></p>
                                <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写商铺名称</p>
                                </div>
                        </div>
                        <div class="cont-row m-b10">
                            <div class="cont-col-4"><p class="cont-h5 theme-color-58789a">输入店铺介绍</p></div>
                            <div class="cont-col-8">
                                <div>
                                    <textarea class="input-textarea buss-textarea" runat="server" id="tbxDescription" rows="5" cols="20"></textarea>
                                </div>
                                <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写简单商铺介绍</p>
                            </div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-12">
                            <p class="t-c"><input class="btn btn-add" type="submit" runat="server" id="btnCreate"  onserverclick="btnCreate_Click" value="创建"/><input class="lightClose btn btn-cancel m-l20" type="button" value="取消"/></p>
                            </div>
</div>
</div>


                   </div>
                </form>

            </div>
        </div>
</body>
<script src="/js/bootstrap/js/bootstrap.js"></script>
<script src="/js/metisMenu/metisMenu.js"></script>
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
<script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/additional-methods.js" type="text/javascript"></script>
<script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
<script>
    var name_prefix = '';

    $(function(){
        $("#menu").metisMenu();
    })

    $("#addNewBusiness").click(function (e) {
            $('#newBusslightBox').lightbox_me({
                centered: true,
                onLoad : function(){

                }
            });
            $("#newBusslightBox").appendTo($("form:first"));
            e.preventDefault();
        });

    $(function(){
         $($("form")[0]).validate(
            {
                errorElement: "p",
                errorPlacement: function (error, element) {
                    error.appendTo(element.parent());
                },
                rules: business_validate_rules,
                messages: business_validate_messages,
//                invalidHandler: invalidHandler
            }
        );
    })

    var business_validate_rules = [];
    var business_validate_messages = [];

    //tbxname
    business_validate_rules["tbxName"] =
    {
        required: true,
        maxlength: 100
    };
    business_validate_messages["tbxName"] =
    {
        required: "请填写服务名称",
        maxlength: "不能超过100个字符"
    };

     //description
    business_validate_rules["tbxDescription"] =
    {
        required: true,
        rangelength: [1, 1000]
    };
    business_validate_messages["tbxDescription"] =
    {
        required: "请填写服务介绍",
        range: "不能超过1000个字符"
    };
</script>
<!--<script src="/js/validation_shop_edit.js" type="text/javascript"></script>-->
<!--<script src="/js/validation_invalidHandler.js" type="text/javascript"></script>-->
</html>
