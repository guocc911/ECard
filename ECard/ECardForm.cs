using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using COMM;
using COMM.TCP;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Windows.Interop;
using System.Windows.Input;
using System.Windows.Interop;
using RawInput_dll;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Fleck;



namespace ECard
{
    public partial class ECardForm : Form
    {

        private string txtContent = String.Empty;

        private delegate void delegateShowInfo(string info);

        private delegate void delegateStatusChange(bool status);

        private delegate void delegateClearText();

        public static WorkSpace _workSpace = null;

        private static RawKeyboard _keyboardDriver;

        private static readonly Guid DeviceInterfaceHid = new Guid("4D1E55B2-F86F-11CF-88CB-001111234530");

        private Queue<RawInput_dll.Win32.KeyAndState> _eventQ = new Queue<RawInput_dll.Win32.KeyAndState>();

        private bool _isMonitoring = true;


        private delegate void ChangePNDelegate(string pnCode);

        private List<IWebSocketConnection> allSockets;

        public event EventHandler SCanErrorEvent;

        private const string serverStartTag = "服务器连接状态：服务开启";

        private const string serverEndTag = "服务器连接状态：服务关闭";


     


        public delegate void invokeDelegate();


        private bool runingStatus = false;

        //Web Server  
        private WebSocketServer webSocketServer;


        private TcpServer tcpServer;

        public ECardForm()
        {
            InitializeComponent();
        }


        private void InitalUSBDriver()
        {
            _keyboardDriver = new RawKeyboard(COMM.User32API.GetCurrentWindowHandle());
            _keyboardDriver.CaptureOnlyIfTopMostWindow = false;
            _keyboardDriver.EnumerateDevices();
            this.lbLinkInfo.Focus();
           
        }


        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Visible = true;

            this.WindowState = FormWindowState.Normal;

            this.notifyIcon1.Visible = false;
        }

        private void ECardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否关闭CPU卡通信客户端", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        private void ECardForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }


        private void btStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                InitalUSBDriver();
                openWebSokcet();
                runingStatus = true;
                ChangeServerStatus(runingStatus);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                SelectDeviceFrom selectDeviceDlg = new SelectDeviceFrom();
                selectDeviceDlg.SelectDeviceType=_workSpace.SelectType;

                if (selectDeviceDlg.ShowDialog() == DialogResult.Yes)
                {
                    _workSpace.SelectType = selectDeviceDlg.SelectDeviceType;

                    if(_workSpace!=null)
                    _workSpace.Save();

                    _workSpace.Load();
                
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        protected override void WndProc(ref System.Windows.Forms.Message m)
        {



            switch (m.Msg)
            {
                case Win32.WM_USB_DEVICECHANGE:

                    if (_keyboardDriver!=null)
                      _keyboardDriver.EnumerateDevices();

                    break;
                case Win32.WM_INPUT:
                    {
                        KeyPressEvent keyPressEvent;
                        if (_keyboardDriver == null)
                        {
                            base.WndProc(ref m); 
                            return;
                        }
                       

                         _keyboardDriver.ProcessRawInput(m.LParam, out keyPressEvent);

                        if (_workSpace.ScanGanID == null || _workSpace.ScanGanID==String.Empty)
                        {
                            if (SCanErrorEvent != null)
                                SCanErrorEvent("未添加扫描枪", EventArgs.Empty);
                            //MessageBox.Show("未添加扫描枪！");
                            return;

                        }

                        // textBox_ScanGunInfoNow.Text = keyPressEvent.DeviceName;

                        //只处理一次事件，不然有按下和弹起事件
                        if (keyPressEvent.KeyPressState == "MAKE" && keyPressEvent.DeviceName == _workSpace.ScanGanID && _workSpace.ScanGanID != string.Empty)
                        {
                            // textBox_Output.Focus();
                           // this.lbStatus.Focus();

                            //找到结尾标志的时候，就不加入队列了，然后就发送到界面上赋值
                            if (KeyInterop.KeyFromVirtualKey(keyPressEvent.VKey) == Key.Enter)
                            {
                                _isMonitoring = false;

                                string str_Out = string.Empty;

                                ThreadPool.QueueUserWorkItem((o) =>
                                {
                                    while (_eventQ.Count > 0)
                                    {
                                        RawInput_dll.Win32.KeyAndState keyAndState = _eventQ.Dequeue();

                                        str_Out += COMM.Utils.Chr(keyAndState.Key).Trim();

                                        System.Threading.Thread.Sleep(5); // might need adjustment
                                    }

                                    //Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                                    //{
                                    //    textBox_Output.Text = str_Out;
                                    //}));
                                    //  this.lbStatus.Text = str_Out;

                                    ProcessUSBScanerCommand(str_Out.Trim());

                                   // DoChangePnCode(str_Out.Trim());
                                    _eventQ.Clear();

                                    _isMonitoring = true;
                                });
                            }

                            // 回车 作为结束标志
                            if (_isMonitoring)
                            {
                                //存储 Win32 按键的int值
                                int key = keyPressEvent.VKey;
                                byte[] state = new byte[256];
                                Win32.GetKeyboardState(state);
                                _eventQ.Enqueue(new RawInput_dll.Win32.KeyAndState(key, state));
                            }
                        }
                    }
                    break;
                default:
                    base.WndProc(ref m);   // 调用基类函数处理其他消息。   
                    break;
            }

        }



        private void ShowClientLinkInfo(string ipaddres)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new delegateShowInfo(ShowClientLinkInfo), new object[] { ipaddres });

                }
                else
                {

                    this.lbLinkInfo.Text = "IP:" + ipaddres;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="info"></param>
        private void ShowCustInfoPro(string info)
        {
            try
            {
                string appendString = Environment.NewLine+ info +DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+ Environment.NewLine;

               
                if (this.InvokeRequired)
                {
                    this.Invoke(new delegateShowInfo(showInfo), new object[] { appendString });
 
                }
                else
                {
                    showInfo(appendString);

                 
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void ClearText()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new delegateClearText(ClearText));
                }
                else
                {
                    this.txtRunReport.Text = string.Empty;
                    this.txtRunReport.ScrollToCaret();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="appendString"></param>
        private void showInfo(string appendString)
        {
            try
            {
                    this.txtRunReport.AppendText(appendString);
                    this.txtRunReport.SelectionStart = txtRunReport.Text.Length;
                    this.txtRunReport.ScrollToCaret();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            try
            {
                CloseWebSocket();

                runingStatus = false;
                ChangeServerStatus(runingStatus);
                ShowCustInfoPro("服务关闭");

                _keyboardDriver = null;

                ShowClientLinkInfo(string.Empty);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void btConfig_Click(object sender, EventArgs e)
        {

        }




        private bool openWebSokcet()
        {
            try
            {

                allSockets = new List<IWebSocketConnection>();

                webSocketServer = new WebSocketServer("ws://0.0.0.0:" + _workSpace.ListenPort.ToString());
               
                webSocketServer.Start(socket =>
                {
                    //接收线程创建
                    socket.OnOpen = () =>
                    {

                        ShowCustInfoPro(socket.ConnectionInfo.ClientIpAddress.ToString() + "已连接");
 
                        if (allSockets.Count >= 1)
                            return;
                        ShowClientLinkInfo(socket.ConnectionInfo.ClientIpAddress.ToString());
                        allSockets.Add(socket);
                    };
                    //线程关闭
                    socket.OnClose = () =>
                    {
                        ShowCustInfoPro(socket.ConnectionInfo.ClientIpAddress.ToString()+"已断开"); 

                        allSockets.Remove(socket);

                        ShowClientLinkInfo(String.Empty);
                    };
                    socket.OnMessage = message =>
                    {

                        CmdUnit unit = ECardDevice.CreateDeviceUnit(message);

                        switch (unit.Type)
                        {
                            case (int)CDDeviceType.DCD10:

                                ProcessDeviceDCD10Command(allSockets[0], unit);

                                break;
                            case (int)CDDeviceType.CD_M1:
                                ProcessDeviceCDM1Command(allSockets[0], unit);

                                break;
                            default:
                                break;
                        }
                        
                    
                    };
                });


                ShowCustInfoPro("服务已开启");

                return true;
            }
            catch
            {
                throw;
            }
           
        }



        private void ProcessData(string cmd)
        {
            try
            {
                CDM1Unit unit = (CDM1Unit)ECardDevice.CreateDeviceUnit(cmd);


                CDM1Device device = new CDM1Device();
                CDM1Unit ret=device.SendCommand(unit);


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        
        
        }
        private void ChangeServerStatus(bool start)
        {

            if(this.InvokeRequired)
            {

                this.Invoke(new delegateStatusChange(ChangeServerStatus), new object[] { start });
            }
            else
            {
      
                if (start)
                {

                    this.Text = "智能卡通信客户端--" + Enum.GetName(typeof(CDDeviceType), _workSpace.SelectType);
                    this.btClose.Enabled = true;
                    this.btStartServer.Enabled = false;
                    this.btConfig.Enabled = false;
                    this.btSetting.Enabled = false;
                    this.toolStripStatusLabel1.Text = serverStartTag + " 地址:" +getIP()+" 端口:"+_workSpace.ListenPort.ToString();

                }
                else 
                {
                    this.Text = "智能卡通信客户端";
                    this.btClose.Enabled = false;
                    this.btStartServer.Enabled = true;
                    this.btConfig.Enabled = true;
                    this.btSetting.Enabled = true;
                    this.toolStripStatusLabel1.Text = serverEndTag;
                }
            }

        }


         private void CloseWebSocket()
        {

            try
            {
                if (webSocketServer == null)
                    return ;

                webSocketServer.Dispose();


                

            }
            catch
            {
                throw;
            }
        }


  


        private void ProcessDeviceDCD10Command(IWebSocketConnection connection, CmdUnit unit)
        {
            try
            {
                D10CMDUnit d10 = (D10CMDUnit)unit;

                D10CMDUnit ret = null;

                DCT10RFDevice device = new DCT10RFDevice(100, 115200);

                device.delegateSendErrorInfo = new ECardDevice.SendErrorInfoDelegate(ShowCustInfoPro);
                device.delegateSendReportInfo = new ECardDevice.SendReportInfoDelegate(ShowCustInfoPro);


                ret = device.SendCommand(unit);

                string retBuffer = System.Text.Encoding.UTF8.GetString(ret.LoadData());
               // connection.sendData(retBuffer);
                connection.Send(retBuffer);
        
            
            }
            catch
            {
                throw;
            }

        }


        private void ProcessDeviceCDM1Command(IWebSocketConnection connection, CmdUnit unit)
        {

            try
            {
                CDM1Unit d10 = (CDM1Unit)unit;

                CDM1Unit ret = null;

                CDM1Device device = new CDM1Device();

                device.delegateSendErrorInfo = new ECardDevice.SendErrorInfoDelegate(ShowCustInfoPro);
                device.delegateSendReportInfo = new ECardDevice.SendReportInfoDelegate(ShowCustInfoPro);


                ret = device.SendCommand(unit);

                string retBuffer = System.Text.Encoding.UTF8.GetString(ret.LoadData());
                // connection.sendData(retBuffer);
                connection.Send(retBuffer);


            }
            catch
            {
                throw;
            }

        }



        /// <summary>
        /// 处理USB扫描枪数据
        /// </summary>
        private void ProcessUSBScanerCommand(string InputInfo)
        {
            try
            {

                if (this.allSockets == null || this.allSockets.Count < 1)
                    return;
                

                ScanerQRTbox qrbox = new ScanerQRTbox();
                qrbox.DeviceName = "QRCodeGan";
                qrbox.ScanDevicesType = ScanDevicesType.QR230CD;
                qrbox.load(InputInfo);

                string report = qrbox.getJson();

                ShowCustInfoPro("USB Scan:" + report);

                this.allSockets[0].Send(report);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
 
        }
        /// <summary>
        /// 关闭TCP端口
        /// </summary>
        private void closeTcpPort()
        {
            tcpServer.Close();
        }


        /// <summary>
        /// 读数据流
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected byte[] readStream(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            if (stream.DataAvailable)
            {
                byte[] data = new byte[client.Available];

                int bytesRead = 0;

                try
                {
                    bytesRead = stream.Read(data, 0, data.Length);
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }

                if (bytesRead < data.Length)
                {
                    byte[] lastData = data;
                    data = new byte[bytesRead];
                    Array.ConstrainedCopy(lastData, 0, data, 0, bytesRead);
                }
                return data;
            }
            return null;
        }

        private void ECardForm_Load(object sender, EventArgs e)
        {
            try
            {

                _workSpace = new WorkSpace(COMM.SystemUtils.ApplicationPath);

                _workSpace.Load();
               
                ChangeServerStatus(false);

              //  InitalUSBDriver();

                this.lbLinkInfo.Text = string.Empty;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }




        private string getIP()
        {
            System.Net.IPAddress addr;
            addr = new System.Net.IPAddress(System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].Address);
            return addr.ToString();

        }

        private void btClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btSetting_Click(object sender, EventArgs e)
        {

            ServerSettingForm settingForm = new ServerSettingForm();

            settingForm.Prot = _workSpace.ListenPort;
            settingForm.ScanDevice = _workSpace.ScanGanID;

            if (settingForm.ShowDialog() == DialogResult.Yes)
            {
                _workSpace.ListenPort = settingForm.Prot;
                _workSpace.ScanGanID = settingForm.ScanDevice;

                _workSpace.Save();

                //if (_workSpace.ListenPort != settingForm.Prot)
                //{

      

                //    //if (MessageBox.Show("是否重启服务", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                //    //                        == System.Windows.Forms.DialogResult.Yes)
                //    //{
                //    //    closeTcpPort();
                //    //    openWebSokcet();
                //    //    runingStatus = true;
                //    //    ChangeServerStatus(runingStatus);
                //    //}
                //}


                
            }
        }
  
       
      
    }
}
