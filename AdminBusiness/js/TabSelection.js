﻿/**
 * display tree structure data in jquery-ui tabs
 
 */
$.fn.TabSelection = function (options) {


    var params = $.extend({
        "datasource": null
    }, options);
    if (!params.datasource) {
        return false;
    }

    var tabs = $(this).tabs();
    //load top-level data
    $(this).tabs({
        activate: function (event, ui) {

            var tab_name = $(ui.newTab[0]).text();
            var item_names = $(ui.newPanel[0]).children("div").children("span");
            for (var i in item_names) {
                var item = item_names[i];
                if ($(item).text() == tab_name) {
                   // $(item).attr("style","font-weight:900");
                }
            }
        }
    });



    /*
    tab内的一个按钮点击之后
    被点击项目是否有子数据
    没有:其他处理             有:移除该tab之后的所有tab,然后创建一个新tab,加载子数据.
    需要判断当前tab的 index.用以确定其位置.可以使用
    */

    function get_children(parentid) {
        var list = [];
        for (var i = 0; i < params.datasource.length; i++) {
            var item = params.datasource[i];
            if (item.parentid == parentid) {

                list.push(item);
            }
        }
        return list;
    }


    function item_click(that, id, name) {
        //将当前tab的值设置为name,如果有子项,激活下一个tab,

        $(".ui-tabs-active a").html(name);

        var tab_panel_content = "";
        //移除后面的tab页面
        var tabs_panel = $(that).parents('.ui-tabs-panel')[0];
        var tabs_panels = $($(that).parents('.ui-tabs').children('div'));
        var tabs_headers = $($(that).parents('.ui-tabs').children('ul').children('li'));

        var tabs_panel_index = tabs_panels.index(tabs_panel);
        for (var i = 0; i < tabs_panels.length; i++) {
            if (i > tabs_panel_index) {
                $(tabs_panels[i]).remove();
                $(tabs_headers[i]).remove();
            }
        }
        var item_list = get_children(id);
        if (item_list.length == 0) {
            $("div#tabsServiceType").tabs("option", "active", tabs_panel_index);
        return false; }
        var num_tabs = $("div#tabsServiceType ul li").length + 1;
        for (var i in item_list) {
            var item_content = "<span style='display:inline-block;margin:5px;' class='item'  item_id=" + item_list[i].id + ">" + item_list[i].name + "</span>";

            tab_panel_content += item_content;
        }
        tab_panel_content = "<div>" + tab_panel_content + "</div>";

        $("div#tabsServiceType ul").append(
            "<li><a href='#tab" + num_tabs + "'>" + "请选择" + "</a></li>"
        );
        $("div#tabsServiceType").append(
            "<div id='tab" + num_tabs + "'>" + tab_panel_content + "</div>"
        );


        $("div#tabsServiceType").tabs("refresh");
        $("div#tabsServiceType").tabs("option", "active", num_tabs - 1);
    }
    function build_children_panel(id) {
        /*var tab_panel_content = "";
        var item_list = get_children(parentid);
        if (item_list.length == 0) { return false; }
        for (var i in item_list) {
        var item_content = "<span class='item'  item_id=" + item_list[i].id + ">" + item_list[i].name + "</span>";

        tab_panel_content += item_content;


        }
        tab_panel_content = "<div>" + tab_panel_content + "</div>";
        return tab_panel_content;
        */

        var item_list = get_children(id);
        if (item_list.length == 0) { return false; }
        var num_tabs = $("div#tabsServiceType ul li").length + 1;
        var tab_panel_content = "";
        for (var i in item_list) {
            var item_content = "<span style='display:inline-block;margin:5px;' class='item'  item_id=" + item_list[i].id + ">" + item_list[i].name + "</span>";

            tab_panel_content += item_content;
        }
        tab_panel_content = "<div>" + tab_panel_content + "</div>";

        $("div#tabsServiceType ul").append(
            "<li><a href='#tab" + num_tabs + "'>" + "请选择" + "</a></li>"
        );
        $("div#tabsServiceType").append(
            "<div id='tab" + num_tabs + "'>" + tab_panel_content + "</div>"
        );


        $("div#tabsServiceType").tabs("refresh");
        $("div#tabsServiceType").tabs("option", "active", num_tabs - 1);
    }
    build_children_panel(0);
    //$('.ui-tabs-panel').not('.ui-tabs-hide').html(build_children_panel(0));
    $('div#tabsServiceType').on('click', '.item', function () {
        var that = this;
        var id = $(that).attr('item_id');
        var name = $(that).html();
        item_click(that, id, name);
    });


}