/**
 * display tree structure data in jquery-ui tabs
 
 */
$.fn.TabSelection= function(options) {

   
    var params = $.extend({
        "datasource":null
    },options);
    if (!params.datasource) {
        return false;
    }

    var tabs = $(this).tabs();
    //load top-level data
    
    
    function loadcontent(tabIndex, parentid) {
        
    }
    $('.ui-tabs-panel').not('.ui-tabs-hide').html(content);
    /*tab�ڵ�һ����ť���֮��
          �������Ŀ�Ƿ���������
     û��:��������             ��:�Ƴ���tab֮�������tab,Ȼ�󴴽�һ����tab,����������.
                               ��Ҫ�жϵ�ǰtab�� index.����ȷ����λ��.����ʹ��
    */
    $(".item").click(function () {


    })

    function get_children(parentid) {
        var list = [];
        for (var i = 0; i < params.datasource.length; i++) {
            var item = params.datasource[i];
            if (item.parentid == parentid) {

            
                list.push(item);
            }
        }
    }
    this.build_children_panel= function(parentid) {
        
        var tab_panel_content = "";
        var item_list = get_children(parentid);
        for (var i in item_list) {
            tab_panel_content += "<span item_id=" + item_list[i].id + "'>" + item_list[i].name + "</span>";
        }
        tab_panel_content = "<div>" + tab_panel_content + "</div>";
        $(tab_panel_content).appendTo(tabs);
    }

}