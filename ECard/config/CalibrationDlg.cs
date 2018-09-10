using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows.Threading;
using System.Windows.Interop;
using System.Windows.Input;
//using System
using RawInput_dll;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace ECard
{
    public partial class CalibrationDlg : Form
    {

        private Dispatcher dispatcher;
        private static RawKeyboard _keyboardDriver;
        private static readonly Guid DeviceInterfaceHid = new Guid("4D1E55B2-F86F-11CF-88CB-001111234530");

        private Queue<RawInput_dll.Win32.KeyAndState> _eventQ = new Queue<RawInput_dll.Win32.KeyAndState>();



        //本窗体的句柄
        private IntPtr _wpfHwnd = IntPtr.Zero;
        //输入队列
        private bool _isMonitoring = true;


        private string device_id;



        private delegate void DelegateShowInfo(string info);


        public event RawKeyboard.DeviceEventHandler KeyPressed
        {
            add { _keyboardDriver.KeyPressed += value; }
            remove { _keyboardDriver.KeyPressed -= value; }
        }

        public int NumberOfKeyboards
        {
            get { return _keyboardDriver.NumberOfKeyboards; }
        }

        public bool CaptureOnlyIfTopMostWindow
        {
            get { return _keyboardDriver.CaptureOnlyIfTopMostWindow; }
            set { _keyboardDriver.CaptureOnlyIfTopMostWindow = value; }
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceID
        {
            get { return device_id; }
            set { device_id = value; }

        }

        public CalibrationDlg()
        {
            InitializeComponent();
        }

        private void btCalibration_Click(object sender, EventArgs e)
        {
            if (this.btCalibration.Text == "开始标定")
            {

                _eventQ.Clear();
                _isMonitoring = true;

                this.txtDeviceID.ReadOnly = true;
                this.txtInput.Focus();
                this.btCalibration.Text = "结束标定";

            }
            else
            {
                string str_Out = string.Empty;

                ThreadPool.QueueUserWorkItem((o) =>
                {
                    while (_eventQ.Count > 0)
                    {
                        RawInput_dll.Win32.KeyAndState keyAndState = _eventQ.Dequeue();

                        str_Out += COMM.Utils.Chr(keyAndState.Key).ToString();

                        System.Threading.Thread.Sleep(5); // might need adjustment
                    }


                    DoShowDeviceInfo(str_Out);
                 //   this.DialogResult = DialogResult.Yes;

                });

                if (device_id == null || device_id == string.Empty)
                {
                    MessageBox.Show("未获取到设备信息,请重试！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    this.DialogResult = DialogResult.No;
                }
                else
                {
                    this.txtDeviceID.Text = device_id;
               
                    this.btCalibration.Text = "开始标定";

                    this.DialogResult = DialogResult.Yes;

                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CalibrationDlg_Load(object sender, EventArgs e)
        {
  
               
            _keyboardDriver = new RawKeyboard(COMM.User32API.GetCurrentWindowHandle());
            _keyboardDriver.CaptureOnlyIfTopMostWindow = false;
            _keyboardDriver.EnumerateDevices();
        }


        private void ECardForm_SCanDataSendEvent(object sender, EventArgs e)
        {


        }



        protected override void WndProc(ref System.Windows.Forms.Message m)
        {



            switch (m.Msg)
            {
                case Win32.WM_USB_DEVICECHANGE:

                    _keyboardDriver.EnumerateDevices();

                    break;
                case Win32.WM_INPUT:
                    {
                        // Should never get here if you are using PreMessageFiltering
                        KeyPressEvent keyPressEvent;


                        _keyboardDriver.ProcessRawInput(m.LParam, out keyPressEvent);



                        if (KeyInterop.KeyFromVirtualKey(keyPressEvent.VKey) == Key.Enter)
                        {
                            _isMonitoring = false;
                            //  Btn_OK.IsEnabled = true;
                        }

                        // 回车 作为结束标志
                        if (_isMonitoring && keyPressEvent.KeyPressState == "BREAK")
                        {
                            //存储 Win32 按键的int值
                            int key = keyPressEvent.VKey;
                            byte[] state = new byte[256];
                            Win32.GetKeyboardState(state);
                            _eventQ.Enqueue(new RawInput_dll.Win32.KeyAndState(key, state));
                        }

                        if (_isMonitoring == false)
                        {
                            device_id = keyPressEvent.DeviceName;
                            DoShowDeviceID(device_id);
                        }
                    }
                    break;
                default:
                    base.WndProc(ref m);   // 调用基类函数处理其他消息。   
                    break;
            }

        }




        private void DoShowDeviceInfo(string strValue)
        {
            if (this.InvokeRequired)
            {

                this.Invoke(new DelegateShowInfo(DoShowDeviceInfo), new object[] { strValue });
            }
            else
            {
                this.txtInput.Text = strValue;
            }
        }


        private void DoShowDeviceID(string strValue)
        {
            if (this.InvokeRequired)
            {

                this.Invoke(new DelegateShowInfo(DoShowDeviceID), new object[] { strValue });
            }
            else
            {
                this.txtDeviceID.Text = strValue;
            }
        }

    }
}
