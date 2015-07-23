var map = new BMap.Map("addressMap");
var cityListObject = new BMapLib.CityList({ container: "addressCity", map : map});
var geoc = new BMap.Geocoder();
//var myGeo = new BMap.Geocoder();

map.enableScrollWheelZoom();
map.disableDoubleClickZoom();
map.clearOverlays();

var myCity = new BMap.LocalCity();

map.addEventListener("click",setAddressPoint);

function setAddressPoint (e){
    map.clearOverlays();

    var addPrintBox = $("#addPrintBox");
    var addressText = $("#addressText");
    var addressP = new BMap.Point(e.point.lng, e.point.lat);
    var addressMark = new BMap.Marker(addressP);
    var addrNodeBox = $(document.createElement("div"));


    geoc.getLocation(addressP, function(rs){
        var addComp = rs.addressComponents;
        var addJson = {
            "province": addComp.province,
            "city": addComp.city,
            "district": addComp.district,
            "street" : addComp.street,
            "streetNumber" : addComp.streetNumber,
            "lng" : rs.point.lng,
            "lat" : rs.point.lat
        };

        //console.log(JSON.stringify(addJson));
        $('#hiAddrId').attr("value",JSON.stringify(addJson));
        var addressNode = "<span>" + addComp.province + "</span><span>" + addComp.city + "</span><span>" + addComp.district + "</span><span>" + addComp.street + "</span><span>" + addComp.streetNumber + "</span>"
        addressText.html(addressNode);

        if ( addPrintBox.html() != "" ){
            addPrintBox.find('div').html(addressNode);
        } else {
            addrNodeBox.html(addressNode);
            addrNodeBox.addClass('myshop-addPrint');
            addPrintBox.append(addrNodeBox);
        }
    });
    map.addOverlay(addressMark);

}

/**
 * ����ʱ��ȡ��ͼ��Ϣ
 */
(function readAddressLoc() {
    if ( $('#hiAddrId').attr("value") ){
        var readAddrJson = jQuery.parseJSON($('#hiAddrId').attr("value"));
        var addrNodeBox = $(document.createElement("div"));
        var addressNode = "<span>" + readAddrJson.province + "</span><span>" + readAddrJson.city + "</span><span>" + readAddrJson.district + "</span><span>" + readAddrJson.street + "</span><span>" + readAddrJson.streetNumber + "</span>"
        var addPrintBox = $("#addPrintBox");
        var addressText = $("#addressText");

        addressText.html(addressNode);
        addrNodeBox.html(addressNode);
        addrNodeBox.addClass('myshop-addPrint m-b10');
        addPrintBox.append(addrNodeBox);
    }
})();

/**
 * �򿪵�ͼʱ���ر��������
 */
$("#setAddress").click(function (e) {
    $('#addrlightBox').lightbox_me({
        centered: true,
        onLoad : function(){
            if( !$("#hiAddrId").attr("value") ) {
                myCity.get(function(result){
                    map.panTo(result.center);
                });
            } else {
                var readAddrJson = jQuery.parseJSON($('#hiAddrId').attr("value"));
                var nPoint = new BMap.Point(readAddrJson.lng, readAddrJson.lat);
                var addressMark = new BMap.Marker(nPoint);

                map.centerAndZoom(nPoint,13);
                map.addOverlay(addressMark);
            }

        }
    });
    e.preventDefault();
});

//        function G(id) {
//                return document.getElementById(id);
//            }
//
//            var ac = new BMap.Autocomplete(    //����һ���Զ���ɵĶ���
//                    {"input" : "suggestId"
//                        ,"location" : map
//                    });
//
//            ac.addEventListener("onhighlight", function(e) {  //�����������б��ϵ��¼�
//                var str = "";
//                var _value = e.fromitem.value;
//                var value = "";
//                if (e.fromitem.index > -1) {
//                    value = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                }
//                str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;
//
//                value = "";
//                if (e.toitem.index > -1) {
//                    _value = e.toitem.value;
//                    value = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                }
//                str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
//                G("searchResultPanel").innerHTML = str;
//            });
//
//            var myValue;
//            ac.addEventListener("onconfirm", function(e) {    //����������б����¼�
//                var _value = e.item.value;
//                myValue = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                G("searchResultPanel").innerHTML ="onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue;
//
//                setPlace();
//            });
//
//            function setPlace(){
//                map.clearOverlays();    //�����ͼ�����и�����
//                function myFun(){
//                    var pp = local.getResults().getPoi(0).point;    //��ȡ��һ�����������Ľ��
//                    map.centerAndZoom(pp, 18);
//                    map.addOverlay(new BMap.Marker(pp));    //��ӱ�ע
//
//                }
//                var local = new BMap.LocalSearch(map, { //��������
//                    onSearchComplete: myFun
//                });
//                local.search(myValue);
//            }

//        // ����ַ���������ʾ�ڵ�ͼ��,��������ͼ��Ұ
//        myGeo.getPoint("����ʡ������������������·125��", function(point){
//            if (point) {
//                map.centerAndZoom(point, 18);
//                map.addOverlay(new BMap.Marker(point));
//            }else{
//                alert("��ѡ���ַû�н��������!");
//            }
//        }, "������");