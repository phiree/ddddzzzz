﻿<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="Static" CodeFile="ServiceEdit.ascx.cs"
    Inherits="DZService_ServiceEdit" %>
    <%@ Register Src="~/TagControl.ascx" TagName="Tag" TagPrefix="DZ" %>
<div class="cont-wrap theme-color-58789a">
    <div class="cont-container">
    <div class="cont-row step-row">
        <div class="cont-col-2">
            <h3 class="step-head step-1">第<em>1</em>步</h3>
        </div>
        <div class="cont-col-10"><p class="step-text step-1">基本服务信息</p></div>
    </div>

        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">服务名称</p>
            </div>
            <div class="cont-col-10">

                <div class="clearfix">
                    <div>
                        <div>
                            <asp:TextBox  runat="server"   CssClass="input-mid" ID="tbxName"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>填写服务名称</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">服务类型</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <input id="setSerType" class="ser-btn-SerType"  type="button" value="请选择服务信息" />
                    <input type="hidden" runat="server" focusid="setSerType" id="hiTypeId" />
                    <asp:Label CssClass="business-radioCf dis-n m-l10" runat="server" ID="lblSelectedType"></asp:Label>
                    <div id="serLightContainer" class="serviceTabs dis-n">
                        <div class="serChoiceInfo clearfix">
                            <div id="serChoiceContainer" class="serChoiceContainer fl"></div>
                            <div class="serChoiceBtnContainer fr">
                                <div class="d-inb">
                                    <div id="serChoiceConf" class="serChoiceBtn lightClose dis-n" >确认</div>
                                </div>
                                <div id="serChoiceCancel" class="serChoiceBtn lightClose d-inb" >取消</div>
                            </div>
                        </div>
                        <div id="serList" class="serList clearfix"></div>
                    </div>
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>请选择该服务的类型</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">服务介绍</p>
            </div>
            <div class="cont-col-10">
                <div class="cont-row">
                    <div class="cont-col-12">

                        <div>
                             <asp:TextBox   CssClass="input-textarea"   runat="server" TextMode="MultiLine"
                                                    ID="tbxDescription"></asp:TextBox>
                        </div>
                        <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务的特色介绍</p>
                    </div>
                </div>
                <!--<div class="cont-row">-->
                    <!--<div class="cont-col-12">-->
                        <!--<p>-->
                            <!--<asp:CheckBox runat="server" ID="cbxEnable" Text="启用" />-->
                        <!--</p>-->
                    <!--</div>-->
                <!--</div>-->
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">您的服务区域</p>
            </div>
            <div class="cont-col-10">

                <div class="cont-row">
                    <div class="cont-col-4">
                        <div class="fl clearfix m-b20">
                            <div id="setBusiness" class="setLocationMap">
                                <div id="businessMapSub" class="mapSub">
                                </div>
                            </div>
                            <input id="hiBusinessAreaCode" runat="server" snsi type="hidden">
                        </div>
                    </div>
                    <div class="cont-col-8">
                        <div>
                            <p class="m-b20">您选择的区域：</p>
                            <p id="businessText" class="business-text"></p>
                        </div>

                    </div>
                    <div class="cont-col-12">
                        <p><span class="text-anno-r">（点击地图为您的服务区域进行定位）</span></p>
</div>
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>请点击地图，设置该服务的服务区域。</p>
            </div>
        </div>
        <div class="cont-row step-row">
            <div class="cont-col-2">
                <h3 class="step-head step-2">第<em>2</em>步</h3>
            </div>
            <div class="cont-col-10"><p class="step-text step-2">完善服务信息</p></div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">服务起步价</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <asp:TextBox  CssClass="input-sm" required runat="server" snsi ID="tbxMinPrice"></asp:TextBox>&nbsp;&nbsp;元
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务的起步价</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">服务单价</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <asp:TextBox CssClass="input-sm" snsi runat="server" ID="tbxUnitPrice"></asp:TextBox>&nbsp;&nbsp;元&nbsp;/&nbsp;每&nbsp;&nbsp;（
                    <asp:RadioButtonList CssClass="serviceDataRadio d-inb" runat="server" ID="rblChargeUnit">
                        <asp:ListItem Selected="True" Value="0" Text="小时"></asp:ListItem>
                        <asp:ListItem Value="1" Text="天"></asp:ListItem>
                        <asp:ListItem Value="2" Text="次"></asp:ListItem>
                    </asp:RadioButtonList>
                    ）
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务的服务价</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">提前预约时间</p>
            </div>
            <div class="cont-col-10">
                <div >
                        至少&nbsp;&nbsp;<asp:TextBox runat="server" snsi CssClass="input-sm" ID="tbxOrderDelay">60</asp:TextBox>&nbsp;&nbsp;分钟
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务是否需要提前预约的时间</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">服务时间</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <asp:TextBox CssClass="input-sm" snsi runat="server" ID="tbxServiceTimeBegin">8:30</asp:TextBox>&nbsp;&nbsp;至&nbsp;&nbsp;
                    <asp:TextBox CssClass="input-sm" snsi runat="server" ID="tbxServiceTimeEnd">21:00</asp:TextBox>
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务的服务时段</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-6">
                <div class="cont-row">
                    <div class="cont-col-4">
                        <p class="cont-sub-title">每日最大接单量:</p>
                    </div>
                    <div class="cont-col-8">
                        <div>
                            <asp:TextBox CssClass="input-sm" snsi runat="server" ID="tbxMaxOrdersPerDay">50</asp:TextBox>&nbsp;&nbsp;单
                        </div>
                        <p class="cont-input-tip"><i class="icon icon-tip"></i>该服务的每日或每小时最大接单量</p>
                    </div>
                </div>
            </div>
            <div class="cont-col-6">
                <div class="cont-row">
                    <div class="cont-col-6">
                        <p class="cont-sub-title">每小时最大接单量</p>
                    </div>
                    <div class="cont-col-6">
                        <div>
                            <asp:TextBox CssClass="input-sm" snsi runat="server" ID="tbxMaxOrdersPerHour">20</asp:TextBox>&nbsp;&nbsp;单
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">是否上门</p>
</div>
            <div class="cont-col-10">
                <div>
                    <asp:RadioButtonList CssClass="service-input-radio" runat="server" ID="rblServiceMode">
                        <asp:ListItem Selected="True" Value="0" Text="是"></asp:ListItem>
                        <asp:ListItem Value="1" Text="否"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>选择是否提供上门服务</p>
</div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">服务对象</p>
            </div>
            <div class="cont-col-10">
                <div class="service-checkBox">
                    <asp:CheckBox CssClass="service-input-radio" runat="server" ID="cblIsForBusiness"
                        Text="是否可以给公司提供该服务" />
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>选择是否可以给公司提供该服务</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
                    <div class="cont-col-2">
                        <p class="cont-sub-title">服务保障</p>
                    </div>
                    <div class="cont-col-10">
                        <div class="service-checkBox">
                            <asp:CheckBox CssClass="service-input-radio" runat="server" ID="cbxIsCompensationAdvance" Text="加入先行赔付" />
                        </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>选择是否加入先行赔付</p>

                    </div>
        </div>
        <!--<div class="cont-row service-cont-row">-->
            <!--<div class="cont-col-2">-->
                <!--<p class="cont-sub-title">平台认证</p>-->
            <!--</div>-->
            <!--<div class="cont-col-10">-->
                <!--<div class="service-checkBox">-->
                    <!--<asp:CheckBox CssClass="service-input-radio" runat="server" ID="cbxIsCertificated" Text="已通过" />-->
                <!--</div>-->
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i></p>-->
            <!--</div>-->
        <!--</div>-->
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">付款方式</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <asp:CheckBoxList CssClass="service-input-radio" ID="rblPayType" runat="server">
                        <asp:ListItem Selected="True" Value="1" Text="线上支付"></asp:ListItem>
                        <asp:ListItem Value="2" Text="当面支付"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>付款方式选择</p>
            </div>

        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">服务标签</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <DZ:Tag runat="server" ID="dzTag"   />
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>添加该服务的特色标签</p>
            </div>
        </div>
    </div>
    <div id="mapLightBox" class="dis-n">
        <div class="mapWrap">
            <div id="businessMap" class="mapMain">
            </div>
            <div id="businessCity" class="mapCity">
            </div>
            <div class="mapButton">
                <input id="confBusiness" class="lightClose ser-sm-input" type="button" value="确定"></div>
            <input id="businessValue" type="hidden" value="" />
        </div>
    </div>
    <div class="service-saveSubmit">
       <asp:Button Text="保存" CssClass="btn btn-info btn-big" runat="server" ID="btnSave" OnClick="btnSave_Click" />
       <a class="btn btn-cancel btn-big m-l10" href="/DZService/default.aspx?businessId=<%=Request["businessid"] %>">取消</a>
    </div>
</div>


<script>
    $(document).ready(function () {
        $.validator.setDefaults({
            ignore: []
        });

        $.validator.addMethod("time24", function (value, element) {
            return /([01]?[0-9]|2[0-3])(:[0-5][0-9])/.test(value);
        }, "Invalid time format.");
        function setTime(date,timeString)
        {
            var arr=timeString.split(":");
            var hour=parseInt(arr[0]);
            var minites=arr[1]?parseInt(arr[1]):0;
            var seconds=arr[2]?parseInt(arr[2]):0;
            return date.setHours(hour,minites,seconds);
        }
        $.validator.addMethod("endtime_should_greater_starttime", function (value, element) {

            var x_date =new Date();
            var start = $("#tbxServiceTimeBegin").val();
            var end = $("#tbxServiceTimeEnd").val();

            var date_start = setTime(x_date,start);
            var date_end = setTime(x_date,end);
            return date_end > date_start;

        }, "结束时间应该大于开始时间");


        
            $($("form")[0]).validate(
                {
                    errorElement: "p",
                    errorPlacement: function(error, element) {
                        error.appendTo( element.parent() );
                    },
                    rules: service_validate_rules,
                    messages: service_validate_messages,
                    invalidHandler: invalidHandler,
                }

        );
 
    });       //document.ready

</script>
