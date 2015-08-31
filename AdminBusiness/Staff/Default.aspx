﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Staff_Default" %>
   <%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/employee.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <div class="cont-wrap">
      <div class="cont-container mh-in">
         <div class="emp-list-div dis-n">
         <div class="cont-row">
              <div class="add-user-div">
               <a class="btn btn-info" href="Edit.aspx?businessid=<%=Request["businessid"] %>"><span class="add-inco">+</span>添加新员工</a>
              </div>
         </div>
         <div class="emp-content-div ">
            <table class="emp-table">
                <thead>
                    <tr class="emp-head-tr">
                       <th class="emp-th-style epm-th-2"></th>
                       <th class="emp-th-style epm-th-2">编号</th>
                       <th class="emp-th-style epm-th-2">姓名</th>
                       <th class="emp-th-style epm-th-2">昵称</th>
                       <th class="emp-th-style epm-th-2">性别</th>
                       <th class="emp-th-style epm-th-2">联系电话</th>
                       <th class="emp-th-style epm-th-2"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptStaff" OnItemCommand="rptStaff_ItemCommand">
                    <ItemTemplate>

                     <tr onclick="listhref('edit.aspx?id=<%#Eval("id") %>&businessid=<%=Request["businessid"] %>')">

                     <td class="emp-td-style epm-th-2 epm-headimg"><img src=' <%# ((Dianzhu.Model.BusinessImage)Eval("AvatarCurrent")) == null ? "/image/emp-headinco.png" : "/ImageHandler.ashx?imagename=" + HttpUtility.UrlEncode(((Dianzhu.Model.BusinessImage)Eval("AvatarCurrent")).ImageName) + "&width=90&height=90&tt=3)"%>  ' width="49" height="49" /></td>
                      <td class="emp-td-style epm-th-2"><%#Eval("Code") %></td>
                      <td class="emp-td-style epm-th-2"><%#Eval("Name") %></td>
                      <td class="emp-td-style epm-th-2"><%#Eval("NickName") %></td>
                      <td class="emp-td-style epm-th-2"><%#Eval("Gender")%></td>
                      <td class="emp-td-style epm-th-2"><%#Eval("Phone")%></td>
                      <td class="emp-td-style epm-btn epm-th-1"><input type="button" staffid='<%#Eval("id") %>' class=' btnAssign btn <%# (bool)Eval("IsAssigned")?"btn-down-info":"btn-info" %>' value='<%# (bool)Eval("IsAssigned")?"取消指派":"指派" %>'/>

                      <asp:Button runat="server" CssClass="btn btn-delete" CommandArgument='<%#Eval("Id") %>' OnClientClick="javascript:return confirm('确认删除该员工?');" CommandName="delete" Text="删除"/></td>

                    </tr>

                    </ItemTemplate>
                    </asp:Repeater>

                </tbody>

            </table>
         </div>
        
      
          <div class="pageNum">
         
            <UC:AspNetPager runat="server" FirstPageText="首页" NextPageText="下一页" 
       PrevPageText="上一页"  
        id="pager" PageSize="10" UrlPaging="true" LastPageText="尾页"></UC:AspNetPager>
          
          </div>
          </div>
           <div class="add-view-emp dis-n">
                   
                        <div class="new-box">
                            <div class="t-c">
                                <img src="/image/service-new.png"/>
                            </div>
                                <div class="service-new-add">
                                      <a class="new-add-btn" href="Edit.aspx?businessid=<%=Request["businessid"] %>">添加新员工</a>
                                </div>
                        </div>
                    
                </div>

      </div>
      



   </div>


    <asp:GridView runat="server" ID="gvStaff">
        <Columns>
            <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" />
        </Columns>
    </asp:GridView>
        <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
        <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
        <script type="text/javascript" src="/js/TabSelection.js" ></script>
        <script type="text/javascript" src="/js/jquery.lightbox_me.js" ></script>
        <script type="text/javascript">
            function listhref(url) {
                var eve = window.event || arguments.callee.caller.arguments[0];
                var $target = $(eve.target);
                if ($target.hasClass("btn")) {
                    return false
                } else if (eve.target == eve.target) {
                    window.location.href = url
                };
            }




            $(function () {

                if ($(".emp-table").find("tr").length == 1) {
                    $(".add-view-emp").removeClass("dis-n");
                } else {
                    $(".emp-list-div").removeClass("dis-n");
                }


                /* 当鼠标移到表格上是，当前一行背景变色 */
                $(".emp-table tr td").mouseover(function () {
                    $(this).parent().find("td").css("background-color", "#b0d3f5");
                });
                /* 当鼠标在表格上移动时，离开的那一行背景恢复 */
                $(".emp-table tr td").mouseout(function () {
                    var bgc = $(this).parent().attr("bg");
                    $(this).parent().find("td").css("background-color", bgc);
                });
                var color = "#f1f4f7"
                $(".emp-table tr:odd td").css("background-color", color);  //改变奇数行背景色
                /* 把背景色保存到属性中 */
                $(".emp-table tr:odd").attr("bg", color);
                $(".emp-table tr:even").attr("bg", "#fff");

            });

            $(".btnAssign").click(function () {
                var that = this;
                $.post("/ajaxservice/changestaffInfo.ashx",
                        {
                            "changed_field": "assign",
                            "changed_value": false,
                            "id": $(that).attr("staffid")
                        }, function (data) {
                            var enabled = data.data;
                            if (enabled == "True") {
                                $(that).siblings("span").html("已指派");
                                $(that).removeClass("btn-info");
                                $(that).addClass("btn-down-info");
                                $(that).val("取消指派");
                            }
                            else {
                                $(that).siblings("span").html("未指派");
                                $(that).val("指派");
                                $(that).removeClass("btn-down-info");
                                $(that).addClass("btn-info");
                            }

                        });
            });


        </script>

</asp:Content>
