﻿
 

var service_validate_rules ={};
var service_validate_messages={};

 
//tbxname
service_validate_rules[name_prefix+"tbxName"]=
{
    required: true,
    maxlength: 100
};
service_validate_messages[name_prefix+"tbxName"]=
{
    required: "请填写服务名称",
    maxlength: "不能超过100个字符"
};
//hiTypeId
service_validate_rules[name_prefix+"hiTypeId"]=
{
    required: true
};
service_validate_messages[name_prefix+"hiTypeId"]=
{
    required: "请选择服务类型"
};
//minprice
service_validate_rules[name_prefix+"tbxMinPrice"]=
{
    required: true,
    range: [1, 2000]
};
service_validate_messages[name_prefix+"tbxMinPrice"]=
{
     required:"请填写最低服务价格",
     range: "请输入1-2000之间的数字"
 };
 //description
 service_validate_rules[name_prefix + "tbxDescription"] =
{
    required: true,
    rangelength: [1, 1000]
};
service_validate_messages[name_prefix + "tbxDescription"] =
{
    required: "请填写服务介绍",
    range: "不能超过1000个字符"
};
//serviceScope
service_validate_rules[name_prefix + "hiBusinessAreaCode"] =
{
    required: true
     
};
service_validate_messages[name_prefix + "hiBusinessAreaCode"] =
{
    required: "请选择服务范围"
};
//服务单价
service_validate_rules[name_prefix + "tbxUnitPrice"] =
{
    required: true,
    range: [1, 1000]

};
service_validate_messages[name_prefix + "tbxUnitPrice"] =
{
    required: "请填写单价",
    range: "请输入1-1000之间的数字"
};
//预约时间
service_validate_rules[name_prefix + "tbxOrderDelay"] =
{
    required: true,
    range: [1, 1440]

};
service_validate_messages[name_prefix + "tbxOrderDelay"] =
{
    required: "请填写预约时长",
    range: "请输入1-1440之间的数字"
};
 
//服务时间
service_validate_rules[name_prefix + "tbxServiceTimeBegin"] =
{
     
        required: true,
        time24: true


    };


service_validate_messages[name_prefix + "tbxServiceTimeBegin"] =
{
   
        required: "请填写服务开始时间",
        time24: "请输入时间"

    };

    service_validate_rules[name_prefix + "tbxServiceTimeEnd"] =
{

    required: true,
    endtime_should_greater_starttime:true,
    time24: true


};
service_validate_messages[name_prefix + "tbxServiceTimeEnd"] =
{

    required: "请填写服务结束时间",
    time24: "请输入时间"


};
//每日最大接单量
service_validate_rules[name_prefix + "tbxMaxOrdersPerDay"] =
{

    required: true,
    range:[1,2000]


};
service_validate_messages[name_prefix + "tbxMaxOrdersPerDay"] =
{

    required: "请填写日最大接单量",
    range: "请输入1-2000之间的数值"


};
//每小时最大接单量
service_validate_rules[name_prefix + "tbxMaxOrdersPerHour"] =
{

    required: true,
    range: [1, 1000]


};
service_validate_messages[name_prefix + "tbxMaxOrdersPerHour"] =
{

    required: "请填写小时最大接单量",
    range: "请输入1-1000之间的数值"


};



