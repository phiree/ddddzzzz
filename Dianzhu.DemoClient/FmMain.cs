﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsc=agsXMPP.protocol.client;
using agsXMPP;
using System.Text.RegularExpressions;
using agsXMPP.protocol.client;
namespace Dianzhu.DemoClient
{
    public partial class FmMain : Form
    {
        string csId = string.Empty;
        public FmMain()
        {
            InitializeComponent();
           
            GlobalViables.XMPPConnection.OnLogin += new agsXMPP.ObjectHandler(XMPPConnection_OnLogin);
            GlobalViables.XMPPConnection.OnMessage += new agsc.MessageHandler(XMPPConnection_OnMessage);
            GlobalViables.XMPPConnection.OnError += new ErrorHandler(XMPPConnection_OnError);
            GlobalViables.XMPPConnection.OnAuthError += new XmppElementHandler(XMPPConnection_OnAuthError);
        }

        void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            MessageBox.Show("用户名/密码有误");
        }

        void XMPPConnection_OnError(object sender, Exception ex)
        {
           // MessageBox.Show("聊天服务器错误");
        }

        void XMPPConnection_OnMessage(object sender, agsc.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender,msg });
                return;
            }
            AddLog(StringHelper.EnsureNormalUserName( msg.From.User), msg.Body);
        }

        void XMPPConnection_OnLogin(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender});
                return;
            }
            lblLoginStatus.Text = "登录成功";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //为该用户分配客服
            string userName = tbxUserName.Text;
            string userNameForOpenfire = userName;
            if (Regex.IsMatch(userName, @"^[^\.@]+@[^\.@]+\.[^\.@]+$"))
            {
                userNameForOpenfire = userName.Replace("@", "||");
            }
            GlobalViables.XMPPConnection.Open(userNameForOpenfire, tbxPwd.Text);

        }
        void AddLog(string user, string body)
        {
            tbxLog.AppendText( Environment.NewLine+ user + ":" + body);
             
       
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(csId))
            {
                csId = "e||e.e";
            }
             
            GlobalViables.XMPPConnection.Send(new agsc.Message(csId+"@"+GlobalViables.ServerName,agsc.MessageType.chat, tbxMessage.Text));
            AddLog(tbxUserName.Text, tbxMessage.Text);
       
        }

        private void tbxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSend.PerformClick();
            }
        }
    }
}
