﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class ForgetPassword : System.Web.UI.Page
{

    DZMembershipProvider bllMember = new DZMembershipProvider();
    DZMembership member;
    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Request["p"];
        string[] arr = param.Split(new string[]{ Config.pwssword_recovery_spliter},  StringSplitOptions.None);
        if (arr.Length != 2)
        {
            Response.Write("请求参数有误");
            Response.End();
        }
        string u = PHSuit.Security.Decrypt(Server.UrlDecode(arr[0]).Replace("kewudejiahao", "+"), false);
        string verifycode =arr[1];
      
        member = bllMember.GetUserByName(u);
        if (member.RecoveryCode == Guid.Empty)
        {
            Response.Write("该客户没有申请密码找回");
            Response.End();
        }
        if (member.RecoveryCode.ToString() != verifycode)
        {
            Response.Write("请求参数有误");
            Response.End();
        }
        
        

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        string password = tbxPassword.Text;

        if (password.Length < 6)
        {
            lblMsg.Text = "密码长度应该大于6";
            return;
        }
        if (password != tbxPasswordConfirm.Text)
        {
            lblMsg.Text = "两次输入的不相等";
        }
        member.Password = member.PlainPassword = password;
        member.RecoveryCode = Guid.Empty;
        bllMember.UpdateDZMembership(member);

        lblMsg.Text = "修改完成";
        hlLogin.Visible = true;
    }
}