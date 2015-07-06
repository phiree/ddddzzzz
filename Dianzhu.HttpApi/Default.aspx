﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/json2.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
</head>
<body>

    <form id="form1" runat="server">
    <div> 
        <div id="dvResults">
        </div>
    </div>
    </form>
    <script>
 
    var apiTest={
    requestArray:[
                { 
                    "protocol_CODE": "USM001001", 
                    "ReqData": { 
                                "userEmail": "a@a.a", 
                                "userPWord": "123456", 
                                }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
                { 
                    "protocol_CODE": "USM001002", 
                    "ReqData": { 
                                "userEmail": "issumao@126.com", 
                                "userPWord": '<%=PHSuit.Security.Encrypt("password", false)%>', 
                                
                                }, 
                "stamp_TIMES": "1490192929212", 
                "serial_NUMBER": "00147001015869149751" 
                },
                { 
                    "protocol_CODE": "USM001003", 
                    "ReqData": { 
                    "uid": "9343d2583fd34de0adc4a4c700a47f0e", 
                    "userPWord": "123456", 
                    "alias": "*%+", 
                    "email": "12333@126.com", 
                    "phone": "1999938xxxx", 
                    "password":"123456",
                    "address":"海牙国际大厦20B"
                   
                    }, 
                    "stamp_TIMES": "1490192929222", 
                    "serial_NUMBER": "00147001015869149756" 
                }
            ],
    begin:function(){
            for(var  i=0;i<this.requestArray.length;i++)
            {
                    var data=this.requestArray[i];
                    var data_str=JSON.stringify(data);
                    $.ajax({
                            url:"DianzhuApi.ashx",
                            method:"POST",
                            data:data_str, 
                            async:false,
                            success:function (result) { 
                               apiTest.writelog("请求Code:"+data.protocol_CODE+ ":<br/>------返回值"+JSON.stringify(result));
                                },//success
                            error:function(errmsg)
                            {
                                
                            },//error
                            complete:function(result){}//complete
                        });//ajax
                }//for
       },//begin
    writelog:function(msg){
        $("#dvResults").append(msg+"<br/>");
    }//writelog
};//apitest
 
 apiTest.begin();
    </script>
</body>
</html>
