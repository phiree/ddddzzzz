﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办支付" />
    <meta name="keywords" content="一点办" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>一点办支付</title>
    <link href="css/mobilepay.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="wrapper">
    <div class="container">
        <form enctype="multipart/form-data">
            <div class="main">
                <div class="m order-list">
                    <div class="order-list-item">
                        <div class="order-list-pra">
                            <div class="order-list-title">订单号</div>
                            <div class="order-list-content"><%=orderId %></div>
                        </div>
                        <div class="order-list-pra bold">
                            <div class="order-list-title">下单时间</div>
                            <div class="order-list-content">2015-08-28 15：24</div>
                        </div>
                    </div>
                    <div class="order-list-item">
                        <div class="order-list-pra">
                            <div class="order-list-title">林小姐</div>
                            <div class="order-list-content">13500000000</div>
                        </div>
                        <div class="order-list-pra">
                            <div class="order-list-title">服务地址</div>
                            <div class="order-list-content">XXXXXXXXXX</div>
                        </div>
                        <div class="order-list-pra">
                            <div class="order-list-title">服务时间</div>
                            <div class="order-list-content">2015-8-31 18:00 - 20:00</div>
                        </div>
                    </div>
                    <div class="order-list-item">
                        <div class="order-list-pra">
                            <div class="order-list-title">服务描述</div>
                            <div class="order-list-content"><%=desc %></div>
                        </div>
                        <div class="order-list-pra">
                            <div class="order-list-title">订单备注</div>
                            <div class="order-list-content"><%=desc %></div>
                        </div>
                    </div>
                    <div class="order-list-item">
                        <div class="order-list-pra">
                            <div class="order-list-title">王大妈家政</div>
                            <div class="order-list-content">15500000000</div>
                        </div>
                        <div class="order-list-pra">
                            <div class="order-list-title">商家地址</div>
                            <div class="order-list-content">XXXXXXXXXXX</div>
                        </div>
                    </div>
                </div>
                <div class="m pay-methods">
                    <label for="roundedTwo" class="pay-method method-hr">
                        <div class="pay-method-logo">
                            <div class="alipayLogo icon"></div>
                        </div>
                        <div class="pay-method-text">
                            <div class="text-cont">
                                <div class="title">支付宝</div>
                                <div class="pra">推荐支付宝用户使用</div>
                            </div>
                        </div>
                        <div class="pay-method-radio">
                            <div class="roundedTwo">
                                <input type="radio" value="None" id="roundedTwo" name="check" />
                                <i for="roundedTwo"></i>
                            </div>
                            <!--<input type="radio" name="alipay" id="alipay"/>-->
                        </div>
                    </label>
                    <label for="roundedThree" class="pay-method">
 
 
                        <div class="pay-method-logo">
 
                            <div class="wechatLogo icon"></div>
                        </div>
                        <div class="pay-method-text">
                            <div class="text-cont">
                                <div class="title">微信支付</div>
                                <div class="pra">亿万用户的选择，更快更安全</div>
                            </div>
                        </div>
                        <div class="pay-method-radio">
                            <div class="roundedTwo">
                                <input type="radio" value="None" id="roundedThree" name="check" />
                                <i for="roundedThree"></i>
                            </div>
                            <!--<input type="radio" name="alipay" id="alipay"/>-->
                        </div>
                    </label>
                </div>
                <div class="pay-confirm">
                    <div class="pay-charge">
                        <div class="pay-charge-cont">
                            <span>需支付</span><span class="price">￥<%=amount %></span>
                        </div>
                    </div>
                    <a class="pay-button" href="DemoPayApi.aspx">去付款</a>
                </div>
            </div>
            <div class="bottom">
                <a href="#" class="pay-back"/>
                    取&nbsp;消
                </a>
            </div>
        </form>

    </div>
</div>
</body>
</html>
