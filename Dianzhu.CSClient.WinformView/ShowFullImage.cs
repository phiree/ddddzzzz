﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dianzhu.CSClient.WinformView
{
    /// <summary>
    /// 打开消息记录中的图片
    /// </summary>
    public partial class ShowFullImage : Form
    {
        public ShowFullImage(Image image)
        {
            InitializeComponent();

            pb.Image = image;
            this.Size =  image.Size;
            
        }

         
    }
}
