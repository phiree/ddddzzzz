﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Dianzhu.Model;
namespace Dianzhu.CSClient.IVew
{
    public delegate void ActiveCustomerHandler(string customername);
    public delegate void SendMessageHandler();
    public delegate void PushExternalService();
    public delegate void PushInternalService(DZService service);
    public delegate void SearchService();
    public delegate void SendPayLink(ReceptionChat chat);

    public interface IMainFormView
    {
        #region Chat
        IList<ReceptionChat> ChatLog { set; get; }
        void LoadOneChat( ReceptionChat chat);
        
        /// <summary>
        /// 搜索关键字.
        /// </summary>
  
        /// <summary>
        /// 设置按钮的样式.
        /// </summary>
        /// <param name="buttonText">按钮文本(等同于客户登录名)</param>
        /// <param name="buttonStyle">按钮样式</param>
        void SetCustomerButtonStyle(string buttonText, em_ButtonStyle buttonStyle);
        /// <summary>
        /// 增加一个客户按钮,并设置样式
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="buttonStyle"></param>
        void AddCustomerButtonWithStyle(string buttonText, em_ButtonStyle buttonStyle);
        string ButtonNamePrefix { get; set; }
        event SendMessageHandler SendMessageHandler;
        event ActiveCustomerHandler ActiveCustomerHandler;
        #endregion

        string SerachKeyword { get; set; }
        string MessageTextBox { get; set; }
        string CurrentCustomerName { get; set; }


        IList<DZService> SearchedService { get; set; }
        //外部服务
        string ServiceName { get; set; }
        string ServiceBusinessName { get; set; }
        string ServiceDescription { get; set; }
        string ServiceUnitPrice { get; set; }
        string ServiceUrl { get; set; }
        event PushExternalService PushExternalService;
        event PushInternalService PushInternalService;
        event SearchService SearchService;
        event SendPayLink SendPayLink;
    }
}
