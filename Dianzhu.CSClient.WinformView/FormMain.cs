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
using Dianzhu.CSClient.IView;
using System.Collections;

//using Dianzhu.CSClient.Model;

namespace Dianzhu.CSClient.WinformView
{
    public partial class FormMain : Form, IMainFormView
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.CSClient.WinformView");
        /// <summary>
        /// IViewMainForm的 windows form实现.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            //关闭自动加载列
            dgvRpCustomer.AutoGenerateColumns = false;
            dgvOrders.AutoGenerateColumns = false;
            dGVCustom.AutoGenerateColumns = false;
            dGVCustomService.AutoGenerateColumns = false;
        }
        #region Impletion view event
        public event MessageSent SendMessageHandler;
        public event MediaMessageSent SendMediaHandler;
        public event IdentityItemActived IdentityItemActived;
        public event PushExternalService PushExternalService;
        public event PushInternalService PushInternalService;
        public event SearchService SearchService;
        public event SelectService SelectService;
       
        public event SendPayLink SendPayLink;
        public event CreateOrder CreateOrder;
        public event BeforeCustomerChanged BeforeCustomerChanged;
        public event ViewClosed ViewClosed;
        public event AudioPlay PlayAudio;
        public event OrderStateChanged OrderStateChanged;
        public event CreateNewOrder CreateNewOrder;
        public event NoticeCustomerService NoticeCustomerService;
        public event NoticeSystem NoticeSystem;
        public event NoticeOrder NoticeOrder;
        public event NoticePromote NoticePromote;
        
      
        public event ReAssign ReAssign;
        public event SaveReAssign SaveReAssign;

        public event MessageSentAndNew MessageSentAndNew;
        

        #endregion

        /// <summary>
        /// 当前选择的服务
        /// </summary>
        
        private ServiceOrder currentOrder;
        public ServiceOrder CurrentOrder
        {
            get
            {
                return currentOrder;
            }

            set
            {
                currentOrder = value;
            }
        }

        /// <summary>
        /// 界面当前绑定的客户名称,用于判断聊天记录的呈现方式
        /// </summary>
        private string currentCustomerService;
        public string CurrentCustomerService {
            set { currentCustomerService = value; }
        }
        public void ShowNotice(string notice)
        {
            Action lambda = () =>
            {
                Label lbl = new Label { Text = DateTime.Now + "  " + notice };
                lbl.AutoSize = true;
                pnlNotice.Controls.Add(lbl);
            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else { lambda(); }
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
            get{ return tbxChatMsg.Text; }
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

            bool isSender = chat.From.UserName == currentCustomerService;
            Label lblTime = new Label();
            _AutoSize(lblTime);
            lblTime.ForeColor = Color.FromArgb(200);
            Label lblFrom = new Label();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();

            pnlOneChat.Controls.AddRange(new Control[] { lblFrom, lblTime, lblMessage });
            if (isSender) { pnlOneChat.FlowDirection = FlowDirection.RightToLeft; }
            else { pnlOneChat.FlowDirection = FlowDirection.LeftToRight; }
            pnlOneChat.Dock = isSender ? DockStyle.Left : DockStyle.Right;
            lblTime.BorderStyle
            = lblMessage.BorderStyle
            = lblFrom.BorderStyle
            = pnlOneChat.BorderStyle = BorderStyle.FixedSingle;

            _AutoSize(pnlOneChat);

            lblTime.Text = chat.SavedTime.ToShortTimeString() + " ";

            lblFrom.Text = chat.From.UserName;

            if (chat.MessageBody == null)
            {
                return;
            }
            LoadBody(chat.MessageBody, pnlOneChat);

            //lblMessage.Text = chat.MessageBody;

            //如果包含了url信息
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
                        if (File.Exists(localFile))
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
                        LinkLabel ll = new LinkLabel { Text = chat.MessageBody };
                        ll.Links.Add(0, ll.Text.Length, mediaUrl);
                        ll.LinkClicked += Ll_LinkClicked;
                        pnlOneChat.Controls.Add(ll);
                        break;
                }
            }
            //bye bye. you are abandoned. 2015-9-2

            //对当前窗体已存在控件的操作
            Action lambda = () =>
            {
               // WindowNotification();
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
        private void LoadBody(string messageBody, Panel pnlContainer)
        {
            bool containsUrls;
            IList<string> urls = PHSuit.StringHelper.ParseUrl(messageBody, out containsUrls);
            if (!containsUrls)
            {
                Label lblPlainText = new Label();
                lblPlainText.Text = messageBody;
                _AutoSize(lblPlainText);
                pnlContainer.Controls.Add(lblPlainText);
            }
            else
            {
                LinkLabel lb = new LinkLabel();
                lb.Text = messageBody;
                foreach (string s in urls)
                {
                    int startIndex = messageBody.IndexOf(s);
                    int endIndex = startIndex + s.Length;
                    lb.Links.Add(startIndex, s.Length, s);
                    lb.LinkClicked += Ll_LinkClicked;
                }
                _AutoSize(lb);
                pnlContainer.Controls.Add(lb);
            }
        }



        private void Ll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void BtnAudio_Click(object sender, EventArgs e)
        {
            PlayAudio(((Button)sender).Tag, this.Handle);
        }

        private void pb_Click(object sender, EventArgs e)
        {
            new ShowFullImage(((PictureBox)sender).Image).Show();
        }



        //public void SetCustomerButtonStyle(ServiceOrder order, em_ButtonStyle buttonStyle)
        //{
        //    Action lambda = () => {
        //        Button btn = (Button)pnlCustomerList.Controls.Find(buttonNamePrefix + order.Id, true)[0];
        //        Color foreColor = Color.White;
        //        switch (buttonStyle)
        //        {
        //            case em_ButtonStyle.Login:
        //                foreColor = Color.Green;
        //                break;
        //            case em_ButtonStyle.LogOff:
        //                foreColor = Color.Gray;
        //                break;
        //            case em_ButtonStyle.Readed: foreColor = Color.Black; break;
        //            case em_ButtonStyle.Unread: foreColor = Color.Red; break;
        //            case em_ButtonStyle.Actived: foreColor = Color.Yellow; break;
        //            default: break;
        //        }
        //        btn.ForeColor = foreColor; };
        //    if (InvokeRequired)
        //    {
        //        Invoke(lambda);
        //    }
        //    else
        //    {
        //        lambda();
        //    }

        //}
        //public void AddCustomerButtonWithStyle(ServiceOrder order, em_ButtonStyle buttonStyle)
        //{
        //    //Action lambda = () =>this.DialogResult=value? System.Windows.Forms.DialogResult.OK: System.Windows.Forms.DialogResult.Abort;

        //    Action lamda = () =>
        //    {
        //        Button btn = new Button();
        //        btn.Text = order.CustomerName + order.Id;
        //        btn.Tag = order;
        //        btn.Name = buttonNamePrefix + order.Id;
        //        btn.Click += new EventHandler(btnCustomer_Click);
        //        pnlCustomerList.Controls.Add(btn);
        //        SetCustomerButtonStyle(order, buttonStyle);
        //    };
        //    if (InvokeRequired)
        //        Invoke(lamda);
        //    else
        //        lamda();
        //}

        public void SetCustomerButtonStyle(DZMembership dm, em_ButtonStyle buttonStyle)
        {
            Action lambda = () => {
                if (pnlCustomerList.Controls.Find(buttonNamePrefix + dm.Id, true).Count() > 0)
                {
                    Button btn = (Button)pnlCustomerList.Controls.Find(buttonNamePrefix + dm.Id, true)[0];
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
                        case em_ButtonStyle.Actived: foreColor = Color.Yellow; break;
                        default: break;
                    }
                    btn.ForeColor = foreColor;
                }                    
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

        public void AddCustomerButtonWithStyle(DZMembership dm, em_ButtonStyle buttonStyle)
        {
            //Action lambda = () =>this.DialogResult=value? System.Windows.Forms.DialogResult.OK: System.Windows.Forms.DialogResult.Abort;

            Action lamda = () =>
            {
                if (!pnlCustomerList.Controls.ContainsKey(buttonNamePrefix + dm.Id))
                {
                    Button btn = new Button();
                    btn.Text = dm.DisplayName + dm.Id;
                    btn.Tag = dm;
                    btn.Name = buttonNamePrefix + dm.Id;
                    btn.Click += new EventHandler(btnCustomer_Click);

                    pnlCustomerList.Controls.Add(btn);
                    SetCustomerButtonStyle(dm, buttonStyle);
                }
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
                DZMembership custom = (DZMembership)((Button)sender).Tag;
                Button btn = (Button)pnlCustomerList.Controls.Find(buttonNamePrefix + custom.Id, true)[0];
                if(btn.ForeColor== Color.Gray)
                {
                    MessageBox.Show("该用户已下线！");
                    RemoveCustomBtn(custom.Id.ToString());
                    return;
                }
                BeforeCustomerChanged();
                //IdentityItemActived((ServiceOrder)((Button)sender).Tag);
                IdentityItemActived((DZMembership)((Button)sender).Tag);
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
           // SearchService();
        }

        public string currentServiceId;
        public string CurrentServiceId
        {
            get { return currentServiceId; }
            set { currentServiceId = value; }
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
                //foreach (DZService service in searchedService)
                //{
                //    LoadServiceToPanel(service);
                //}Hashtable ht = (Hashtable)list[i];
                foreach(DZService service in searchedService)
                {
                    LoadServiceToPanel(service);
                }
            }
        }

        private void LoadServiceToPanel(DZService service)
        {
            FlowLayoutPanel pnl = new FlowLayoutPanel();
            pnl.Name = "servicePnl" + service.Id.ToString();
            pnl.BorderStyle = BorderStyle.FixedSingle;
            pnl.FlowDirection = FlowDirection.LeftToRight;
            Label lblBusinessName = new Label();
            lblBusinessName.BorderStyle = BorderStyle.FixedSingle;
            lblBusinessName.Text = service.Business.Name;
            lblBusinessName.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
            pnl.Controls.Add(lblBusinessName);
            Label lblServiceName = new Label();
            lblServiceName.BorderStyle = BorderStyle.FixedSingle;
            lblServiceName.Text = service.Description.ToString();
            pnl.Controls.Add(lblServiceName);
            Button btnPushService = new Button();
            btnPushService.Text = "推送";
            btnPushService.Tag = service.Id.ToString();
            btnPushService.Click += new EventHandler(btnPushService_Click);
            pnl.Controls.Add(btnPushService);
            Button btnSelectService = new Button();
            btnSelectService.Text = "选择";
            btnSelectService.Tag = service;// service.Id.ToString();
            btnSelectService.Click += new EventHandler(btnSelectService_Click);
            pnl.Controls.Add(btnSelectService);
            pnlResultService.Controls.Add(pnl);

        }

        void btnSelectService_Click(object sender, EventArgs e)
        {
            //将已经选择的 panel 背景颜色还原
            foreach (Control con in pnlResultService.Controls)
            {
                if (con.BackColor == Color.Green)
                {
                    con.BackColor = DefaultBackColor;
                    break;
                }
            }
            DZService selectedService = (DZService)((Button)sender).Tag;

            //P 将所选择服务加入订单详情
            SelectService(selectedService);

            
            Panel pnl = (Panel)pnlResultService.Controls.Find("servicePnl" + selectedService.Id, true)[0];
            pnl.BackColor = Color.Green;

            LoadOrderDetail(CurrentOrder);
            

            //currentService
           // SetOrderByService(currentService);
        }

        /// <summary>
        /// 加载订单详情
        /// </summary>
        public void LoadOrderDetail(ServiceOrder order)
        {
            OrderNumber = order.Id.ToString();
            OrderStatus = order.OrderStatus.ToString();
            //判断是否已经加载

            foreach (ServiceOrderDetail detail in order.Details)
            {
                string pnlOrderDetailKey = "pnlOrderDetail" + detail.Id;
                bool hasDetailAdded = pnlOrder.Controls.ContainsKey(pnlOrderDetailKey);
                if (hasDetailAdded)
                {
                    pnlOrder.Controls.RemoveByKey(pnlOrderDetailKey);
                }
                UC_OrderDetail pnlDetail = new UC_OrderDetail();
                pnlDetail.Name = pnlOrderDetailKey;
                pnlDetail.LoadData(detail);
                pnlOrder.Controls.Add(pnlDetail);

            }
            CanEditOrder = true;
        }
        private void LoadOneDetail(ServiceOrderDetail detail,Panel pnlOrderDetail)
        {
            
        }

        void btnPushService_Click(object sender, EventArgs e)
        {
            PushInternalService((DZService)((Button)sender).Tag);
            //xmpp发送消息 由xmpp实现,在csclient
            // agsXMPP.protocol.client.Message m = new agsXMPP.protocol.client.Message();
            // m.Type = MessageType.chat;
            // DZService service = (DZService)((Button)sender).Tag;
            // string serviceId = service.Id.ToString();
            // string serviceName = service.Name;
            // m.SetAttribute("service_name", service.Name);
            // m.SetAttribute("service_id", serviceId);
            // m.SetAttribute("t", "push");
            // m.SetAttribute("service_name");
            //m.To = StringHelper.EnsureOpenfireUserName(CurrentCustomerName) + "@" + GlobalViables.Domain;
            // GlobalViables.XMPPConnection.Send(m);
            // //业务逻辑
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
            set { lblOrderNumber.Text = value; }
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
            if (SendMediaHandler != null)
            {
                if (dlgSelectPic.ShowDialog() == DialogResult.OK)
                {
                    byte[] fileData = File.ReadAllBytes(dlgSelectPic.FileName);
                    SendMediaHandler(fileData, "ChatImage", "image");
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
                    byte[] fileData = File.ReadAllBytes(dlgSelectPic.FileName);
                    SendMediaHandler(fileData, "ChatAudio", "voice");
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

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teboscreen.Form1 form1 = new Teboscreen.Form1();
            form1.InstanceRef = this;
            form1.Captured += SendMediaHandler;
            form1.Show();
        }

        private void btnNoticeSystem_Click(object sender, EventArgs e)
        {
            NoticeSystem();
        }

        private void btnNoticeOrder_Click(object sender, EventArgs e)
        {
            NoticeOrder();
        }

        public void ShowStreamError(string streamErrorMes)
        {
            MessageBox.Show("该账号已在其他客户端登录，您将被迫下线！");
        }

        private void btnReAssign_Click(object sender, EventArgs e)
        {
            ReAssign();
        }

        /// <summary>
        /// 当前客服正在接待的客户列表
        /// </summary>
        public IList<DZMembership> RecptingCustomList { set { dGVCustom.DataSource = value; } }
        /// <summary>
        /// 客户重新分配列表
        /// </summary>
        public IDictionary<DZMembership, string> RecptingCustomServiceList
        {
            get
            {
                IDictionary<DZMembership, string> recptingCustomServiceList = new Dictionary<DZMembership, string>();
                for (int i = 0; i < dGVCustomService.RowCount; i++)
                {
                    DataGridViewRow row = dGVCustomService.Rows[i];
                    DZMembership cs = row.DataBoundItem as DZMembership;
                    if (row.Cells[1].Value != null)
                    {
                        recptingCustomServiceList.Add(cs, row.Cells[1].Value.ToString());
                    }
                }

                return recptingCustomServiceList;
            }
            set { dGVCustomService.DataSource = value.Keys.ToList(); }
        }

        /// <summary>
        /// 客服最终分配用户列表
        /// </summary>
        private IList<ServiceOrder> recptingOrderFinaList;
        public IList<ServiceOrder> RecptingOrderFinaList
        {
            get { return recptingOrderFinaList; }
            set { recptingOrderFinaList = value; }
        }

        private void btnSaveCS_Click(object sender, EventArgs e)
        {
            SaveReAssign();
        }

        public void RemoveCustomBtn(string cid)
        {
            Action lamda = () =>
            {
                if (pnlCustomerList.Controls.ContainsKey(buttonNamePrefix + cid))
                {
                    Button btn = (Button)pnlCustomerList.Controls.Find(buttonNamePrefix + cid, true)[0];
                    pnlCustomerList.Controls.Remove(btn);
                }
                else
                {
                    ilog.Error("传入的名称无效！");
                    throw new NotImplementedException("传入的名称无效！");
                }
            };
            if (InvokeRequired)
                Invoke(lamda);
            else
                lamda();            
        }

        public void RemoveCustomBtnAndClear(string cid)
        {
            Action lamda = () =>
            {
                if (pnlCustomerList.Controls.ContainsKey(buttonNamePrefix + cid))
                {
                    Button btn = (Button)pnlCustomerList.Controls.Find(buttonNamePrefix + cid, true)[0];
                    pnlCustomerList.Controls.Remove(btn);
                    pnlChat.Controls.Clear();
                    dgvOrders.DataSource = null;
                }
                else
                {
                    ilog.Error("传入的名称无效！");
                    throw new NotImplementedException("传入的名称无效！");
                }
            };
            if (InvokeRequired)
                Invoke(lamda);
            else
                lamda();
        }

        public void ShowMsg (string msg)
        {
            MessageBox.Show(msg);
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && MessageTextBox.Trim() != string.Empty)
            {
                MessageTextBox = MessageTextBox.Substring(0, MessageTextBox.Length - 1);
                btnSend.PerformClick();
            }
        }

        private void tbxChatMsg_TextChanged(object sender, EventArgs e)
        {
            lblCountText.Text = MessageTextBox.Length + "/255";
        }

        private void btnSendAndNew_Click(object sender, EventArgs e)
        {
            MessageSentAndNew();
        }

        public void WindowNotification()
        {
            Action lamda = () =>
            {
                FlashInTaskBar.FlashWindowEx(this);
            };
            if (InvokeRequired)
                Invoke(lamda);
            else
                lamda();
        }

        /// <summary>
        /// 当前客服正在接待的用户的订单列表
        /// </summary>
        public IList<ServiceOrder> OrdersList { set { dgvOrders.DataSource = value; } }

        /// <summary>
        /// 当前客服接待用户的历史记录
        /// </summary>
        public IList<DZMembership> ReceptionCustomerList { set { dgvRpCustomer.DataSource = value; } }

        
        public decimal OrderDepositAmount
        {
            get
            {
                return string.IsNullOrEmpty(tbxOrderDepositAmount.Text) ? 0 : Convert.ToDecimal(tbxOrderDepositAmount.Text);
            }

            set
            {
                tbxOrderDepositAmount.Text = value.ToString();
            }
        }

        public decimal OrderAmount
        {
            get
            {
             return Convert.ToDecimal(   tbxOrderAmount.Text);
            }

            
        }
    }
}
