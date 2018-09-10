using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECard
{
    public partial class ServerSettingForm : Form
    {


        private int port;


        private string scanDevice=String.Empty;

        private delegate void refreshInfoDelegate();


        public int Prot
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ScanDevice
        {
            get { return scanDevice; }
            set { scanDevice = value; }
        }



        public ServerSettingForm()
        {
            InitializeComponent();
        }

        private void ServerSettingForm_Load(object sender, EventArgs e)
        {


            LoadInfo();

        }



        private void LoadInfo()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new refreshInfoDelegate(LoadInfo));
            }
            else
            {
                this.txtPort.Text = Convert.ToString(port);
                this.txtScanReader.Text = this.scanDevice;
            }

        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.port = Convert.ToInt32(this.txtPort.Text.Trim());
            this.DialogResult = DialogResult.Yes;
            this.Close();
          
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键

            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }

        private void btConfigScan_Click(object sender, EventArgs e)
        {

            try
            {

                CalibrationDlg cbDlg = new CalibrationDlg();

                cbDlg.ShowDialog();
                if( this.scanDevice!= cbDlg.DeviceID)
                {  
                    this.scanDevice = cbDlg.DeviceID;
                    LoadInfo();
                   //ECardForm._workSpace.ScanGanID= cbDlg.DeviceID;
                   //ECardForm._workSpace.Save();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
