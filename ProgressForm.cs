﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptTool
{
    public partial class ProgressForm : Form
    {
        private BackgroundWorker backgroundWorker1; //ProgressForm窗体事件(进度条窗体)
        public ProgressForm(BackgroundWorker bgWork)
        {
            InitializeComponent();
            // add my code
            this.backgroundWorker1 = bgWork;
            //绑定进度条改变事件
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            //绑定后台操作完成，取消，异常时的事件
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Console.WriteLine($"进度条变化={e.ProgressPercentage}");
            int progressPercentage = e.ProgressPercentage;
            lab_process.Text = progressPercentage.ToString() + "%";
            this.progressBar1.Value = progressPercentage;  //获取异步任务的进度百分比
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            this.Close();  //执行完之后，直接关闭页面
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync(); //请求取消挂起的后台操作
            this.btnCancel.Enabled = false;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
