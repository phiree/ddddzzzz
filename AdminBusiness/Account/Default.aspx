﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Account_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/myshop.css" rel="stylesheet" type="text/css" />
    <%--<link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/base/minified/jquery-ui.min.css' rel="stylesheet" type="text/css" />--%>
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent clearfix">
        <div class="leftContent" id="leftCont">
            <div class="leftContent" id="leftCont">
                <div>
                    <ul>
                        <li><a href="./Default.aspx"><i class="nav-btn side-btn-myshop"></i></a></li>
                        <li><a href="./Security.aspx"><i class="nav-btn side-btn-secret"></i></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div id="myshop-wrap">
                <div class="myshopInfoArea clearfix">
                    <div class="myshopInfoTilte">
                        <h1>商家基本信息</h1>
                        <!--<img src="../image/touxiangkuang11.png" alt="头像"/>-->
                    </div>
                    <div class="headInfoArea clearfix">
                        <!--<div class="headDecoration1">-->
                        <!--</div>-->
                        <div class="headImage">
                            <img src="../image/myshop/touxiangkuang_11.png" alt="头像"/>
                        </div>
                        <div class="headInfo">
                            <p><input runat="server" type="text" id="tbxName" name="inputShopName" value="请输入您的店铺名称" class="inputShopName" /></p>
                            <p class="InfoCompletetxt">资料完成程度</p>
                            <div class="InfoPercentage">
                                <div class="InfoComplete">
                                    <span class="progress"></span>
                                </div>
                                <span class="completePercentage">90%</span>
                            </div>
                        </div>
                        <div class="headEditImg">
                            <a href="javascript:void(0);" class="headEditBtn"></a>
                        </div>
                    </div>
                </div>
                <div class="ShopDetailsTitle">
                    <!--<img src="/image/shopicon.jpg" />-->
                    <!--<i class="icon shop-icon-Title"></i>-->
                    <span >店铺详细资料</span>
                </div>
                <div class="ShopDetailsMain">
                <div class="clearfix">
                    <div class="ShopDetailsAreaLeft">
                        <div class="myshopLeftCont">
                            <p class="myshop-item-title"><i class="icon myshop-icon-shopIntro"></i>店铺介绍</p>
                            <div>
                                <textarea class="myshop-input-textarea" id="tbxIntroduced" runat="server" name="shopIntroduced">(可输入60个字)</textarea>
                            </div>
                        </div>
                        <div class="myshopLeftCont pinput">
                            <p class="p_ContactPhone myshop-item-title"><i class="icon myshop-icon-phone"></i>联系电话</p>
                            <p><input type="text" id="tbxContactPhone" runat="server" name="ContactPhone" /></p>
                        </div>
                        <div class="myshopLeftCont pinput">
                            <p class="p_addressDetail myshop-item-title"><i class="icon myshop-icon-address"></i>详细店址</p>
                            <p><input type="text" id="tbxAddress" runat="server" name="addressDetail" /></p>
                        </div>
                        <div class="myshopLeftCont pinput">
                            <p class="p_email myshop-item-title"><i class="icon myshop-icon-email"></i>邮箱地址</p>
                            <p><input type="text" runat="server" id="tbxEmail" name="email" /></p>
                        </div>
                        <div class="myshopLeftCont">
                            <p class="myshop-item-title"><i class="icon myshop-icon-workTime"></i>从业时间</p>
                            <div class="select select-sm">
                            <ul>
                                <li><a>8:00</a></li>
                                <li><a>9:00</a></li>
                                <li><a>10:00</a></li>
                            </ul>
                            <input type="hidden" runat="server" id="tbxBusinessYears" name="email" />
                            </div>

                        </div>
                        <div class="BusinessLicense">
                            <p class="p_BusinessLicense myshop-item-title"><i class="icon myshop-icon-businessLic"></i>营业执照</p>
                            <a href='<%=Config.BusinessImagePath+"/original/"+b.BusinessLicence.ImageName %>'>
                                <img runat="server" id="imgLicence" src="/image/dianjishangchuan_1.png" />
                            </a>
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuBusinessLicence" />
                                    <i class="input-file-bg"></i>
                                    <i class="input-file-mark"></i>
                                    <img class="input-file-pre" />
                                </div>
                        </div>
                    </div>
                    <div class="ShopDetailsAreaRight">
                        <div class="myshopRightCont ShopFigure">
                            <p class="myshop-item-title"><i class="icon myshop-icon-shopFigure"></i>店铺图片展示</p>
                            <div>
                                <div class="clearfix">
                                    <asp:Repeater runat="server" ID="rpt_show"  OnItemCommand="rpt_show_ItemCommand">
                                        <ItemTemplate>
                                        <div class="picture">
                                            <img class="picEditBtn" src="/image/bianjji_1.png" />
                                            <img class="picDelBtn" src="/image/delete_1.png" runat="server" CommandArgument="Id"  />
                                            <asp:ImageButton runat="server" CommandName="delete" ImageUrl="/image/delete_1.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>'/>
                                             <a href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'  id="imgLicence"  />
                                             </a>
                                         </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="shopPicAdd">
                                    <div class="input-file-box d-inb">
                                        <asp:FileUpload cssclass="input-file-btn" runat="server" ID="fuShow1" />
                                        <i class="input-file-bg"></i>
                                        <i class="input-file-mark"></i>
                                        <img class="input-file-pre" />
                                    </div>
                                    <div class="input-file-box d-inb">
                                        <asp:FileUpload cssclass="input-file-btn" runat="server" ID="fuShow2" />
                                        <i class="input-file-bg"></i>
                                        <i class="input-file-mark"></i>
                                        <img class="input-file-pre"  />
                                    </div>
                                    <div class="input-file-box d-inb">
                                        <asp:FileUpload cssclass="input-file-btn" runat="server" ID="fuShow3" />
                                        <i class="input-file-bg"></i>
                                        <i class="input-file-mark"></i>
                                        <img class="input-file-pre"  />
                                    </div>
                                </div>


                            </div>
                        </div>
                        <div class="myshopRightCont">
                            <p class="myshop-item-title"><i class="icon myshop-icon-empNum"></i>员工人数</p>
                            <div class="d-inb select select-sm">
                                <ul>
                                    <li><a>10人</a></li>
                                    <li><a>20人</a></li>
                                    <li><a>50人</a></li>
                                </ul>
                                <input type="hidden" />
                            </div>
                            <span>员工信息编辑</span>
                        </div>
                        <div class="myshopRightCont pinput2">
                            <p class="myshop-item-title"><i class="icon myshop-icon-owner"></i>负责人姓名</p>
                            <p>
                                <input type="text" name="DirectorName" />
                            </p>
                        </div>
                        <div class="myshopRightCont">
                            <p class="myshop-item-title"><i class="icon myshop-icon-licenseType"></i>证件类型</p>
                            <div class="select select-sm">
                                <ul>
                                    <li><a>身份证</a></li>
                                    <li><a>学生证</a></li>
                                    <li><a>其它</a></li>
                                </ul>
                                <input type="hidden" />
                            </div>
                        </div>
                        <div class="myshopRightCont pinput3">
                            <p class="myshop-item-title"><i class="icon myshop-icon-licenseNum"></i>证件号码</p>
                            <p>
                                <input type="text" name="DocumentNumber" />
                            </p>
                        </div>
                        <div class="myshopRightCont HeadProfilePicture">
                            <p class="myshop-item-title"><i class="icon myshop-icon-ownerPic"></i>负责人证件照上传</p>
                            <div>
                                <a href='<%=Config.BusinessImagePath+"/original/"+b.ChargePersonIdCard.ImageName %>'>
                                    <img runat="server" id="imgChargePerson" src="/image/dianjishangchuan_1.png" />
                                </a>
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload cssclass="input-file-btn" runat="server" ID="fuChargePerson" />
                                    <i class="input-file-bg"></i>
                                    <i class="input-file-mark"></i>
                                    <img class="input-file-pre"  />
                                </div>
                            </div>
                        </div>
                        <!--<div id="tabsServiceType" class="tabsServiceType" >-->
                            <!--<ul> </ul>-->
                             <!---->
                        <!--</div>-->
                    </div>
                </div>
                <div>
                    <div>
                        <input id="setSerType" class="btn-SerType" type="button" value="选择服务信息" />
                        <div id="serCheckedShow">

                        </div>
                    </div>
                    <div id="SerlightBox" class="serviceTabs dis-n">
                        <div id="tabsServiceType" class="" >
                            <ul></ul>
                        </div>
                        <div>
                            <input class="close btn-SerTypeConfirm" type="button" value="确定" />
                            <input class="close btn-SerTypeCancel" type="button" value="取消" />
                        </div>
                    </div>
                </div>
                </div>
                <div class="bottomArea">
                    <input name="imageField" runat="server" onserverclick="btnSave_Click" type="image"
                        id="imageField1" src="../image/myshop/shop_tx_107.png" />
                    <input name="imageField" type="image" id="imageField2" src="../image/myshop/shop_tx_108.png" />
                </div>
            </div>
            <div id="account" class="account">
                账号安全
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.9.1.min.js"></script>-->
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.10.2.js"></script>
    <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>-->
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js" ></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js" ></script>
    <script type="text/javascript" src="../js/global.js"></script>
    <script type="text/javascript">
        var tabCheckedShow = function(that,checked){
//        console.log($('.item').html());
            if (checked == true) {
                var checkedShowBox = $('#serCheckedShow');
                var checkedItem = $($(that).parents('.serviceTabsItem')).find('.item');
                var checkText = checkedItem.html();
                var checkTextNode = "<span>" + checkText + "</span>";
                checkedShowBox.append(checkTextNode);
            } else {
                return;
            }
        }

        $(function () {
            //            $("#tabsServiceType").TabSelection({
            //                "datasource":
            //                [
            //                    { "name": "维修", "id": 1, "parentid": 0 },
            //                    { "name": "家电维修", "id": 2, "parentid": 1 },
            //                     { "name": "冰箱维修", "id":3, "parentid": 2 },
            //                    { "name": "冰箱维修", "id": 6, "parentid": 2 },
            //                    { "name": "冰箱维修", "id": 7, "parentid": 2 },
            //                    { "name": "冰箱维修", "id": 8, "parentid": 2 },

            //                    { "name": "更换氟利昂", "id": 4, "parentid": 3 },
            //                    { "name": "交通服务", "id": 5, "parentid": 0 }
            //                ]
            //            });
            //        });
            $("#tabsServiceType").TabSelection({
                "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
                "enable_multiselect":true,
                'check_changed': function (that,id, checked) {
//                    alert(id + '' + checked);
                      tabCheckedShow(that,checked);
                },

                'leaf_ clicked': function (id, checked) {
//                alert(id);
                }
            });
        });


        $("#setSerType").click(function(e){
            $('#SerlightBox').lightbox_me({
                centered: true
            });
            e.preventDefault();
        })


    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var m1 = true;
            var m2 = false;
            function meinit(str) {
                m1 = false;
                m2 = false;
                $("#me1").attr("src", "/image/jibenxinxi_4.png");
                $("#me2").attr("src", "/image/zhanghaoanquan_2.png");
            }

            $("#me1").click(function () {
                meinit(m1);

                $("#me1").attr("src", "/image/jibenxinxi_3.png");
                $("#userInfoAreaid").css("display", "block");
                $("#account").css("display", "none");


            });
            $("#me2").click(function () {
                meinit(m2);
                $("#me2").attr("src", "/image/zhanghaoanquan_1.png");
                $("#userInfoAreaid").css("display", "none");
                $("#account").css("display", "block");

            });
            $(".picture").hover(function () {
                    $(".picDelBtn").css("display", "block");
                    $(".picEditBtn").css("display", "block");

                }, function () {
                    $(".picDelBtn").css("display", "none");
                    $(".picEditBtn").css("display", "none");

                }
            );

//            $(".picDelBtn").css("display", "none");
//            $(".picEditBtn").css("display", "none");

            $(".progress").css("width", "90%");
            $(".completePercentage").html("90%");

        });
    </script>
</asp:Content>
