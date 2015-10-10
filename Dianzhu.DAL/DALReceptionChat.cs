﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALReceptionChat : DALBase<Model.ReceptionChat>
    {
         public DALReceptionChat()
        {
            Session = new HybridSessionBuilder().GetSession();
        }
        //注入依赖,供测试使用;
         public DALReceptionChat(string fortest)
        {
            
        }

        public new void Save(ReceptionChat chat)
        {
            if (chat.Id != Guid.Empty)
            {
                base.Save(chat, chat.Id);
            }
            else {
                base.Save(chat);
            }
            //if (chat.Id != null)
            //{ Session.Save(chat, chat.Id); }
            //else {
            //    Session.Save(chat);
            //}
            //return chat;
        }
        
        

        
    }
}
