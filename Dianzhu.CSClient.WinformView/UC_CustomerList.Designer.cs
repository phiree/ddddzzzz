﻿using Dianzhu.CSClient.IView;
using System.Windows.Forms;

namespace Dianzhu.CSClient.WinformView
{
    public partial class UC_CustomerList
    { 
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlCustomerList = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pnlCustomerList
            // 
            this.pnlCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomerList.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomerList.Name = "pnlCustomerList";
            this.pnlCustomerList.Size = new System.Drawing.Size(150, 150);
            this.pnlCustomerList.TabIndex = 0;
            // 
            // UC_CustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCustomerList);
            this.Name = "UC_CustomerList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlCustomerList;
    }
}
