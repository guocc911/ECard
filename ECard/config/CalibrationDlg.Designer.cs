namespace ECard
{
    partial class CalibrationDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationDlg));
            this.btCancel = new System.Windows.Forms.Button();
            this.btCalibration = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(268, 93);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 17;
            this.btCancel.Text = "关闭";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btCalibration
            // 
            this.btCalibration.Location = new System.Drawing.Point(104, 93);
            this.btCalibration.Name = "btCalibration";
            this.btCalibration.Size = new System.Drawing.Size(75, 23);
            this.btCalibration.TabIndex = 16;
            this.btCalibration.Text = "开始标定";
            this.btCalibration.UseVisualStyleBackColor = true;
            this.btCalibration.Click += new System.EventHandler(this.btCalibration_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(104, 12);
            this.txtInput.MaxLength = 40;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(321, 21);
            this.txtInput.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "扫描枪设备号:";
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Location = new System.Drawing.Point(104, 51);
            this.txtDeviceID.MaxLength = 40;
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(321, 21);
            this.txtDeviceID.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "输出:";
            // 
            // CalibrationDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 129);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btCalibration);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDeviceID);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalibrationDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "扫码枪标定";
            this.Load += new System.EventHandler(this.CalibrationDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btCalibration;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeviceID;
        private System.Windows.Forms.Label label2;
    }
}