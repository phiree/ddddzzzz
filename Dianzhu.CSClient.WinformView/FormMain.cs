﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dianzhu.Model;
using Dianzhu.CSClient.IVew;
 
//using Dianzhu.CSClient.Model;
 
namespace Dianzhu.CSClient.WinformView
{
    public partial class FormMain : Form, IMainFormView
    {
       
        /// <summary>
        /// todo:view 不需要知道formcontroller , 应该在该窗体初始化的时候 初始化controller
        /// 
        /// </summary>
        
        public FormMain()
        {
            InitializeComponent();
       
        }
        #region Impletion view event
        public event MessageSent SendMessageHandler;
        public event MediaMessageSent SendMediaHandler;
        public event IdentityItemActived IdentityItemActived;
        public event PushExternalService PushExternalService;
        public event PushInternalService PushInternalService;
        public event SearchService SearchService;
        public event SendPayLink SendPayLink;
        public event CreateOrder CreateOrder;
        public event BeforeCustomerChanged BeforeCustomerChanged;
        public event ViewClosed ViewClosed;
        public event AudioPlay PlayAudio;
        public event OrderStateChanged OrderStateChanged;
        public event CreateNewOrder CreateNewOrder;
        

        #endregion



        /// <summary>
        /// 界面当前绑定的客户名称,用于判断聊天记录的呈现方式
        /// </summary>
        private string currentCustomerName;
        public string CurrentCustomerName {
            get { return currentCustomerName; }
            set { currentCustomerName = value; }
        }
        private string localMediaSaveDir;
        IList<ReceptionChat> chatLog;
        public IList<ReceptionChat> ChatLog
        {

            set
            {
                Action lambda = () =>
                {
                    pnlChat.Controls.Clear();
                    foreach (ReceptionChat chat in value)
                    {
                        LoadOneChat(chat);
                    }
                    chatLog = value;
                };
                if (InvokeRequired)
                {
                    Invoke(lambda);
                }
                else { lambda(); }

            }
            get
            {
                return chatLog;
            }
        }
        public string MessageTextBox
        {
            get { return tbxChatMsg.Text; }
            set { tbxChatMsg.Text = value; }
        }
        string mediaServerRoot;
        public string MediaServerRoot {
            get { return mediaServerRoot; }
            set { mediaServerRoot = value; }
        }
        string buttonNamePrefix;

        public string ButtonNamePrefix
        {
            get { return buttonNamePrefix; }
            set { buttonNamePrefix = value; }
        }

        /// <summary>
        /// 加载一条聊天记录,
        /// </summary>
        
        /// <param name="chat"></param>
        public void LoadOneChat(ReceptionChat chat)
        {
          
            bool isSender = chat.From.UserName == currentCustomerName;
            Label lblTime = new Label();
            _AutoSize(lblTime);
            lblTime.ForeColor = Color.FromArgb(200);
            Label lblFrom = new Label ();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();

            pnlOneChat.Controls.AddRange(new Control[] {lblTime,lblMessage });
            pnlOneChat.FlowDirection = FlowDirection.LeftToRight  ;
            pnlOneChat.Dock=isSender? DockStyle.Left: DockStyle.Right;
                lblTime.BorderStyle 
                = lblMessage.BorderStyle 
                = lblFrom.BorderStyle
                =pnlOneChat.BorderStyle = BorderStyle.FixedSingle;
            
            _AutoSize(pnlOneChat);
            
            lblTime.Text = chat.SavedTime.ToShortTimeString() + " ";
 
            lblFrom.Text = chat.From.UserName;
            lblMessage.Text = chat.MessageBody;
            _AutoSize(lblMessage);
            pnlOneChat.Width = pnlChat.Size.Width - 36;

            //显示多媒体信息.

            if (chat is ReceptionChatMedia)
            {

                string mediaType = ((ReceptionChatMedia)chat).MediaType;
                string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                switch (mediaType)
                {
                    case "image":
                        PictureBox pb = new PictureBox();
                        pb.Click += new EventHandler(pb_Click);
                        string filename = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);
                        string localFile = LocalMediaSaveDir + filename;
                        if(File.Exists(localFile))
                        {
                            pb.ImageLocation = localFile;
                        }
                        else { 
                        pb.Load(mediaUrl);
                        }
                        pb.Size = new System.Drawing.Size(100, 100);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pnlOneChat.Controls.Add(pb);
                        break;
                    case "voice":
                        Button btnAudio = new Button();
                        btnAudio.Text = "播放音频---";
                        btnAudio.Tag = mediaUrl;
                        btnAudio.AutoSize = true;
                        btnAudio.Click += BtnAudio_Click;
                        pnlOneChat.Controls.Add(btnAudio);
                        break;
                    case "video":
                        Button btnVideo = new Button();
                        btnVideo.Text = "播放视频";
                        pnlOneChat.Controls.Add(btnVideo);
                        break;
                    case "url":
                        LinkLabel ll = new LinkLabel { Text=chat.MessageBody };
                        ll.Links.Add(0, ll.Text.Length, mediaUrl);
                        ll.LinkClicked += Ll_LinkClicked;
                        pnlOneChat.Controls.Add(ll);
                        break;
                }
            }
            //bye bye. you are abandoned. 2015-9-2
  

            Action lambda = () =>
            {
                FlashInTaskBar.FlashWindowEx(this);
                pnlChat.Controls.Add(pnlOneChat);
                pnlChat.ScrollControlIntoView(pnlOneChat);
            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else
            {
                lambda();
            }

        }

        private void Ll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void BtnAudio_Click(object sender, EventArgs e)
        {
            ;
            PlayAudio(((Button)sender).Tag,this.Handle);
        }

        private void pb_Click(object sender, EventArgs e)
        {
            new ShowFullImage(((PictureBox)sender).Image).Show();
        }

        

        public void SetCustomerButtonStyle(ServiceOrder order, em_ButtonStyle buttonStyle)
        {
            Action lambda=()=>{
            Button btn = (Button)pnlCustomerList.Controls.Find
                (buttonNamePrefix + order.CustomerName+order.Id, true)[0];
            Color foreColor = Color.White;
            switch (buttonStyle)
            {
                case em_ButtonStyle.Login:
                    foreColor = Color.Green;
                    break;
                case em_ButtonStyle.LogOff:
                    foreColor = Color.Gray;
                    break;
                case em_ButtonStyle.Readed: foreColor = Color.Black; break;
                case em_ButtonStyle.Unread: foreColor = Color.Red; break;
                default: break;
            }
            btn.ForeColor = foreColor;};
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else
            {
                lambda();
            }

        }
        public void AddCustomerButtonWithStyle(ServiceOrder order   , em_ButtonStyle buttonStyle)
        {
            //Action lambda = () =>this.DialogResult=value? System.Windows.Forms.DialogResult.OK: System.Windows.Forms.DialogResult.Abort;

            Action lamda = () =>
            {
                Button btn = new Button();
                btn.Text = order.CustomerName+order.Id;
                btn.Tag = order;
                btn.Name = buttonNamePrefix +order.CustomerName+ order.Id;
                btn.Click += new EventHandler(btnCustomer_Click);
                pnlCustomerList.Controls.Add(btn);
                SetCustomerButtonStyle(order, buttonStyle);
            };
            if (InvokeRequired)
                Invoke(lamda);
            else
                lamda();
        }

        void btnCustomer_Click(object sender, EventArgs e)
        {
            if (IdentityItemActived != null)
            {
                BeforeCustomerChanged();
                IdentityItemActived((ServiceOrder) ((Button)sender).Tag);
            }
            
        }
 

        private void tbxChatMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSend.PerformClick();
            }
        }
        

        #region searchService

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchService();    
        }


        public string SerachKeyword
        {
            get
            {
                return tbxKeywords.Text;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        IList<DZService> searchedService;
        public IList<DZService> SearchedService
        {
            get
            {
                return searchedService;
            }
            set
            {
                searchedService = value;
                pnlResultService.Controls.Clear();
                foreach (DZService service in searchedService)
                {
                    LoadServiceToPanel(service);
                }
            }
        }

        private void LoadServiceToPanel(DZService service)
        {
            FlowLayoutPanel pnl = new FlowLayoutPanel();
            pnl.BorderStyle = BorderStyle.FixedSingle;
            pnl.FlowDirection = FlowDirection.LeftToRight;
            Label lblBusinessName = new Label();
            lblBusinessName.BorderStyle = BorderStyle.FixedSingle;
            lblBusinessName.Text = service.Business.Name;
            lblBusinessName.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
            pnl.Controls.Add(lblBusinessName);
            Label lblServiceName = new Label();
            lblServiceName.BorderStyle = BorderStyle.FixedSingle;
            lblServiceName.Text = service.Name;
            pnl.Controls.Add(lblServiceName);
            Button btnPushService = new Button();
            btnPushService.Text = "推送";
            btnPushService.Tag = service;
            btnPushService.Click += new EventHandler(btnPushService_Click);
            pnl.Controls.Add(btnPushService);
            pnlResultService.Controls.Add(pnl);

        }

        void btnPushService_Click(object sender, EventArgs e)
        {
            PushInternalService((DZService)((Button)sender).Tag);
            //xmpp发送消息 由xmpp实现,在csclient
            //agsXMPP.protocol.client.Message m = new agsXMPP.protocol.client.Message();
            //m.Type = MessageType.chat;
            //DZService service = (DZService)((Button)sender).Tag;
            //string serviceId = service.Id.ToString();
            //string serviceName = service.Name;
            //m.SetAttribute("service_name", service.Name);
            //m.SetAttribute("service_id", serviceId);
            //m.SetAttribute("t", "push");
            // m.SetAttribute("service_name",
           // m.To = StringHelper.EnsureOpenfireUserName(CurrentCustomerName) + "@" + GlobalViables.Domain;
            // GlobalViables.XMPPConnection.Send(m);
            //业务逻辑
           // FormController.PushService(new Guid(serviceId));
        }

        #endregion

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {

        }
        private void _AutoSize(Control c)
        {
            c.Size = new Size(0, 0);
            if (c is Label)
            {
                ((Label)c).AutoSize = true;
            }
            if (c is Panel)
            {
                ((Panel)c).AutoSize = true;
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (SendMessageHandler != null)
            {
                SendMessageHandler();
            }
        }

        private void btnPushExternalService_Click(object sender, EventArgs e)
        {
            PushExternalService();
        }



        public string ServiceName
        {
            get
            {
                return tbxServiceName.Text;
            }
            set
            {
                tbxServiceName.Text = value;
            }
        }

        public string ServiceBusinessName
        {
            get
            {
                return tbxServiceBusinessName.Text;
            }
            set
            {
                tbxServiceBusinessName.Text = value;
            }
        }

        public string ServiceDescription
        {
            get
            {
                return tbxServiceDescription.Text;
            }
            set
            {
                tbxServiceDescription.Text = value;
            }
        }

        public string ServiceUnitPrice
        {
            get
            {
                return tbxServiceUnitPrice.Text ;
            }
            set
            {
                tbxServiceUnitPrice.Text = value ;
            }
        }

        public string ServiceUrl
        {
            get
            {
                return tbxServiceUrl.Text;
            }
            set
            {
                tbxServiceUrl.Text = value;
            }
        }
        public string OrderAmount
        {
            get { return tbxAmount.Text ; }
            set { tbxAmount.Text = value; }
        }
        public string TargetAddress
        {
            get { return tbxTargetAddress.Text; }
            set { tbxTargetAddress.Text = value; }
        }
        public string Memo {
            get { return tbxMemo.Text; }
            set { tbxMemo.Text = value; }
        }
        
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            CreateOrder();
        }

        private void btnSearchOut_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

           // ViewClosed();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

            ViewClosed();
             
            
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            ViewClosed();
        }




        public string ServiceTime
        {
            get
            {
                return tbxServiceTime.Text;
            }
            set
            {
                tbxServiceTime.Text = value;
            }
        }
        public System.IO.Stream SelectedImageStream
        {
            get
            {
                return dlgSelectPic.OpenFile();
            }

        }
        public string OrderStatus {
            get {
                return lblOrderStatus.Text;
            }
            set {
                lblOrderStatus.Text = value;
            }
        }
        bool canEditor = true;
        public bool CanEditOrder {
            get { return canEditor; }
            set {
                pnlOrder.Enabled = value;
            }
        }
        public string SelectedImageName {
            get { return dlgSelectPic.FileName; }
        }
        public string OrderNumber {

            get { return lblOrderNumber.Text; }
            set { lblOrderNumber.Text= value; }
        }

        public string LocalMediaSaveDir
        {
            get
            {
                return localMediaSaveDir;
            }

            set
            {
                localMediaSaveDir = value;
            }
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            if(SendMediaHandler != null)
            { 
                if(dlgSelectPic.ShowDialog()== DialogResult.OK)
                {
                    SendMediaHandler("ChatImage","image");
                }
            }
        }

        private void btnOrderChangedNotice_Click(object sender, EventArgs e)
        {
            OrderStateChanged();
        }

        private void btnSendAudio_Click(object sender, EventArgs e)
        {
            if (SendMediaHandler != null)
            {
                if (dlgSelectPic.ShowDialog() == DialogResult.OK)
                {
                    SendMediaHandler("ChatAudio", "voice");
                }
            }
        }

        private void btnCreateNewDraft_Click(object sender, EventArgs e)
        {
            if (CreateNewOrder != null)
            {
                CreateNewOrder();
            }
        }
    }
}
