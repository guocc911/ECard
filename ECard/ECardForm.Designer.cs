namespace ECard
{
    partial class ECardForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ECardForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btClear = new System.Windows.Forms.Button();
            this.txtRunReport = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btConfig = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btStartServer = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lbLinkInfo = new System.Windows.Forms.Label();
            this.serialPortEx1 = new COMM.SerialPortEx();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Tag = "CPU卡通信客户端";
            this.notifyIcon1.Text = "CPU卡通信客户端";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 320);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(459, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(104, 17);
            this.toolStripStatusLabel1.Text = "服务器连接状态：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btClear);
            this.groupBox1.Controls.Add(this.txtRunReport);
            this.groupBox1.Location = new System.Drawing.Point(12, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 138);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运行日志：";
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(363, 109);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(51, 23);
            this.btClear.TabIndex = 1;
            this.btClear.Text = "清除";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // txtRunReport
            // 
            this.txtRunReport.Location = new System.Drawing.Point(15, 20);
            this.txtRunReport.Multiline = true;
            this.txtRunReport.Name = "txtRunReport";
            this.txtRunReport.ReadOnly = true;
            this.txtRunReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRunReport.Size = new System.Drawing.Size(399, 86);
            this.txtRunReport.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btSetting);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btConfig);
            this.groupBox2.Controls.Add(this.btClose);
            this.groupBox2.Controls.Add(this.btStartServer);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(435, 125);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "服务管理:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(366, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "设置";
            // 
            // btSetting
            // 
            this.btSetting.Image = global::ECard.Properties.Resources.settings_64px_1202475_easyicon_net;
            this.btSetting.Location = new System.Drawing.Point(347, 29);
            this.btSetting.Name = "btSetting";
            this.btSetting.Size = new System.Drawing.Size(65, 62);
            this.btSetting.TabIndex = 6;
            this.btSetting.UseVisualStyleBackColor = true;
            this.btSetting.Click += new System.EventHandler(this.btSetting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "设备选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "关闭服务";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "开启服务";
            // 
            // btConfig
            // 
            this.btConfig.Image = global::ECard.Properties.Resources.connection_48px_1187376_easyicon_net;
            this.btConfig.Location = new System.Drawing.Point(239, 29);
            this.btConfig.Name = "btConfig";
            this.btConfig.Size = new System.Drawing.Size(65, 62);
            this.btConfig.TabIndex = 2;
            this.btConfig.UseVisualStyleBackColor = true;
            this.btConfig.Click += new System.EventHandler(this.button1_Click);
            // 
            // btClose
            // 
            this.btClose.Image = global::ECard.Properties.Resources.Stop_red_48px_1186323_easyicon_net;
            this.btClose.Location = new System.Drawing.Point(130, 29);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(65, 62);
            this.btClose.TabIndex = 1;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btStartServer
            // 
            this.btStartServer.Image = global::ECard.Properties.Resources.Start_48px_1186321_easyicon_net__1_;
            this.btStartServer.Location = new System.Drawing.Point(23, 29);
            this.btStartServer.Name = "btStartServer";
            this.btStartServer.Size = new System.Drawing.Size(65, 62);
            this.btStartServer.TabIndex = 0;
            this.btStartServer.UseVisualStyleBackColor = true;
            this.btStartServer.Click += new System.EventHandler(this.btStartServer_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "客户端连接地址:";
            // 
            // lbLinkInfo
            // 
            this.lbLinkInfo.AutoSize = true;
            this.lbLinkInfo.Location = new System.Drawing.Point(113, 293);
            this.lbLinkInfo.Name = "lbLinkInfo";
            this.lbLinkInfo.Size = new System.Drawing.Size(29, 12);
            this.lbLinkInfo.TabIndex = 6;
            this.lbLinkInfo.Text = "null";
            // 
            // ECardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 342);
            this.Controls.Add(this.lbLinkInfo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ECardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "畅的智能卡通信客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ECardForm_FormClosing);
            this.Load += new System.EventHandler(this.ECardForm_Load);
            this.SizeChanged += new System.EventHandler(this.ECardForm_SizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRunReport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btConfig;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btStartServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btSetting;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private COMM.SerialPortEx serialPortEx1;
        private System.Windows.Forms.Label lbLinkInfo;
    }
}

