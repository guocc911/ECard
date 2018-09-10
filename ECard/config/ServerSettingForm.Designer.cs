namespace ECard
{
    partial class ServerSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerSettingForm));
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScanReader = new System.Windows.Forms.TextBox();
            this.btConfigScan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(104, 12);
            this.txtPort.MaxLength = 4;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(161, 21);
            this.txtPort.TabIndex = 7;
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "监听端口:";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(212, 94);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 10;
            this.btCancel.Text = "取  消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(69, 94);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 9;
            this.btOK.Text = "确  定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "扫描设备:";
            // 
            // txtScanReader
            // 
            this.txtScanReader.Location = new System.Drawing.Point(104, 53);
            this.txtScanReader.MaxLength = 4;
            this.txtScanReader.Name = "txtScanReader";
            this.txtScanReader.ReadOnly = true;
            this.txtScanReader.Size = new System.Drawing.Size(161, 21);
            this.txtScanReader.TabIndex = 12;
            // 
            // btConfigScan
            // 
            this.btConfigScan.Location = new System.Drawing.Point(280, 53);
            this.btConfigScan.Name = "btConfigScan";
            this.btConfigScan.Size = new System.Drawing.Size(46, 23);
            this.btConfigScan.TabIndex = 13;
            this.btConfigScan.Text = "配置";
            this.btConfigScan.UseVisualStyleBackColor = true;
            this.btConfigScan.Click += new System.EventHandler(this.btConfigScan_Click);
            // 
            // ServerSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 128);
            this.Controls.Add(this.btConfigScan);
            this.Controls.Add(this.txtScanReader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "监听端口设置";
            this.Load += new System.EventHandler(this.ServerSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtScanReader;
        private System.Windows.Forms.Button btConfigScan;
    }
}