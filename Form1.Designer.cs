using System;
using System.ComponentModel;

namespace EncryptTool
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmbxEncryptType = new System.Windows.Forms.ComboBox();
            this.lblEncryptType = new System.Windows.Forms.Label();
            this.lblEncryptKey = new System.Windows.Forms.Label();
            this.txtEncryptKey = new System.Windows.Forms.TextBox();
            this.tipEncrypt = new System.Windows.Forms.ToolTip(this.components);
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.progress_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // cmbxEncryptType
            // 
            this.cmbxEncryptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEncryptType.FormattingEnabled = true;
            this.cmbxEncryptType.Location = new System.Drawing.Point(158, 24);
            this.cmbxEncryptType.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.cmbxEncryptType.Name = "cmbxEncryptType";
            this.cmbxEncryptType.Size = new System.Drawing.Size(305, 29);
            this.cmbxEncryptType.TabIndex = 1;
            this.cmbxEncryptType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblEncryptType
            // 
            this.lblEncryptType.AutoSize = true;
            this.lblEncryptType.Location = new System.Drawing.Point(28, 30);
            this.lblEncryptType.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblEncryptType.Name = "lblEncryptType";
            this.lblEncryptType.Size = new System.Drawing.Size(115, 21);
            this.lblEncryptType.TabIndex = 2;
            this.lblEncryptType.Text = "加解密方式";
            this.lblEncryptType.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblEncryptKey
            // 
            this.lblEncryptKey.AutoSize = true;
            this.lblEncryptKey.Location = new System.Drawing.Point(94, 93);
            this.lblEncryptKey.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblEncryptKey.Name = "lblEncryptKey";
            this.lblEncryptKey.Size = new System.Drawing.Size(52, 21);
            this.lblEncryptKey.TabIndex = 3;
            this.lblEncryptKey.Text = "秘钥";
            // 
            // txtEncryptKey
            // 
            this.txtEncryptKey.Location = new System.Drawing.Point(160, 88);
            this.txtEncryptKey.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtEncryptKey.Name = "txtEncryptKey";
            this.txtEncryptKey.Size = new System.Drawing.Size(303, 31);
            this.txtEncryptKey.TabIndex = 4;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.BackColor = System.Drawing.Color.Transparent;
            this.btnDecrypt.BackgroundImage = global::EncryptTool.Properties.Resources.unlock_fill_green;
            this.btnDecrypt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDecrypt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDecrypt.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDecrypt.FlatAppearance.BorderSize = 0;
            this.btnDecrypt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDecrypt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDecrypt.Location = new System.Drawing.Point(321, 158);
            this.btnDecrypt.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(145, 135);
            this.btnDecrypt.TabIndex = 5;
            this.tipEncrypt.SetToolTip(this.btnDecrypt, "解密文件");
            this.btnDecrypt.UseVisualStyleBackColor = false;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.BackColor = System.Drawing.Color.Transparent;
            this.btnEncrypt.BackgroundImage = global::EncryptTool.Properties.Resources.lock_fill_red;
            this.btnEncrypt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEncrypt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEncrypt.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEncrypt.FlatAppearance.BorderSize = 0;
            this.btnEncrypt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEncrypt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEncrypt.Location = new System.Drawing.Point(97, 158);
            this.btnEncrypt.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(145, 135);
            this.btnEncrypt.TabIndex = 0;
            this.tipEncrypt.SetToolTip(this.btnEncrypt, "加密文件");
            this.btnEncrypt.UseVisualStyleBackColor = false;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // progress_backgroundWorker
            // 
            this.progress_backgroundWorker.WorkerReportsProgress = true;
            this.progress_backgroundWorker.WorkerSupportsCancellation = true;
            this.progress_backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.progress_backgroundWorker_DoWork);
            this.progress_backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.progress_backgroundWorker_ProgressChanged);
            this.progress_backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.progress_backgroundWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 313);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.txtEncryptKey);
            this.Controls.Add(this.lblEncryptKey);
            this.Controls.Add(this.lblEncryptType);
            this.Controls.Add(this.cmbxEncryptType);
            this.Controls.Add(this.btnEncrypt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "文件加解密工具 Bate tanzibiao";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.ComboBox cmbxEncryptType;
        private System.Windows.Forms.Label lblEncryptType;
        private System.Windows.Forms.Label lblEncryptKey;
        private System.Windows.Forms.TextBox txtEncryptKey;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.ToolTip tipEncrypt;
        private System.ComponentModel.BackgroundWorker progress_backgroundWorker;
    }
}

