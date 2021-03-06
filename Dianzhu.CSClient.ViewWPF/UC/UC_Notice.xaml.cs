﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_Notice.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Notice : UserControl,IView.IViewNotice
    {
        public UC_Notice()
        {
            InitializeComponent();
        }

        public string NoticeBody
        {
            set
            {
                Action lamda = () =>
                {
                    contNotice.Text += value + ";";
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lamda);
                }
                else
                {
                    lamda();
                }
            }
        }
    }
}
