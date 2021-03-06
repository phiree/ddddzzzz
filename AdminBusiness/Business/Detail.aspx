﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Business_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--<link rel="Stylesheet" href="/js/lightbox/css/lightbox.css"/>-->
    <!--<link rel="Stylesheet" href="/css/business.css"/>-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <div class="content-main">
            <div class="container-fluid animated fadeInUpSmall">
                <div class="row">
                    <div class="col-md-12">
                        <div class="biz-total-card">
                            <div class="biz-card-t">
                                <strong class="biz-card-tl">6.5</strong>
                                <div class="biz-card-tr">
                                    <p>%</p>
                                    <p>增长率</p>
                                </div>
                            </div>
                            <div class="biz-card-b"></div>
                        </div>
                    </div>
                </div>
                <div class="d-hr"></div>
                <div class="row">
                    <div class="col-md-12">
                        <h3 class="biz-detail-head">店铺基本信息</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9">
                        <div class="model">
                            <div class="model-h">
                                <h4>基本信息</h4>
                            </div>
                            <div class="model-m">
                                <div class="row m-b20">
                                    <div class="col-md-1 model-img">
                                        <div class="t-c m-b20">
                                            <img class="business-detail-face"
                                                 src='<%=CurrentBusiness.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(CurrentBusiness.BusinessAvatar.ImageName)+"&width=70&height=70&tt=3":"../images/common/touxiang/touxiang_70_70.png"%>'/>
                                            <!--<p class="t-c business-detail-name"><%=CurrentBusiness.Name %></p>-->
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <p class="model-pra"><span class="model-pra-t">网址邮箱</span><%=string.IsNullOrEmpty(CurrentBusiness.WebSite)?"无" :CurrentBusiness.WebSite %></p>
                                        <p class="model-pra"><span class="model-pra-t">联系电话</span><%=CurrentBusiness.Phone %></p>
                                        <p class="model-pra"><span class="model-pra-t">店铺介绍</span><span class="text-breakWord"><%=CurrentBusiness.Description %></span></p>
                                    </div>
                                    <div class="col-md-4">
                                        <p class="model-pra"><span class="model-pra-t">从业时间</span><%=CurrentBusiness.WorkingYears %>&nbsp;年</p>
                                        <p class="model-pra"><span class="model-pra-t">员工人数</span><%=CurrentBusiness.StaffAmount %>&nbsp;人</p>
                                        <p class="model-pra"><span class="model-pra-t">店铺地址</span><%=string.IsNullOrEmpty( CurrentBusiness.Address )? "无" :
                                            CurrentBusiness.Address %></p>
                                    </div>

                                </div>
                                <div class="d-hr in"></div>
                                <div class="row model-row">
                                    <div class="col-md-1 model-img"></div>
                                    <div class="col-md-4">
                                        <p class="model-pra"><span class="model-pra-t">店主姓名</span>
                                            <%=CurrentBusiness.Contact %></p>
                                        <p class="model-pra"><span class="model-pra-t">证件类型</span>
                                            <%=CurrentBusiness.ChargePersonIdCardType.ToString() %></p>
                                    </div>
                                    <div class="col-md-4">
                                        <p class="model-pra"><span class="model-pra-t">证件号码</span><%=CurrentBusiness.ChargePersonIdCardNo %></p>
                                    </div>
                                    <div class="col-md-3">
                                        <a class="btn btn-info" href="/Business/edit.aspx?businessId=<%=CurrentBusiness.Id %>">修改店铺信息</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="detail-pra">
                            <div class="row">
                                <div class="col-md-4">

                                </div>
                                <div class="col-md-8">
                                    <div class="detail-img m-b20 t-r">
                                        <asp:Repeater runat="server" ID="rptShow">
                                            <ItemTemplate>
                                                <a class="" data-lightbox="lb_show"
                                                   href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img
                                                        src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3'/>
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <p class="t-r"></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="model">
                            <div class="model-h">
                                <h4>统计图表</h4>
                            </div>
                            <div class="model-m"></div>
                        </div>
                    </div>
                </div>
                <div class="row dis-n">
                    <div class="col-md-5">
                        <div class="business-detail-iden">
                            <p class="cont-h5">负责人信息</p>
                            <p class="cont-h5">营业执照</p>

                            <div class="p-20 detail-img">
                                <asp:Repeater runat="server" ID="rptImageLicense">
                                    <ItemTemplate>
                                        <a class="m-r20" data-lightbox="lb_license"
                                           href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img
                                                src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3'/>
                                        </a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <p class="cont-h5">负责人证件照</p>

                            <div class="p-20 detail-img">
                                <asp:Repeater runat="server" ID="rptCharge">
                                    <ItemTemplate>
                                        <a class="m-r20" data-lightbox="lb_charge"
                                           href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img
                                                src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3'/>
                                        </a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>

