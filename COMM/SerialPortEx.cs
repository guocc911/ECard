using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace COMM
{
    public class SerialPortEx : SerialPort
    {


        public virtual bool OpenPort(string port, int nBaudRate, Parity parity, int nDataBits,
                                    StopBits nStopBits, Handshake handshake)
        {
            if (this.IsOpen)
                return true;

            this.PortName = port;

            //this.BaudRate = 9600;
            this.BaudRate = nBaudRate;

            //  this.Parity = Parity.None;
            this.Parity = parity;

            // this.DataBits = 8;
            this.DataBits = nDataBits;

            //this.StopBits = StopBits.One;
            this.StopBits = nStopBits;

            //this.Handshake = Handshake.RequestToSend;
            this.Handshake = handshake;

            this.DataReceived += new SerialDataReceivedEventHandler(ProcessDataReceived);
            this.PinChanged += new SerialPinChangedEventHandler(ConnectedSerialPort_PinChanged);
            this.ErrorReceived += new SerialErrorReceivedEventHandler(ConnectedSerialPort_ErrorReceived);

            this.ReadTimeout = 500;
            this.WriteTimeout = 500;

            try
            {
                this.Open();
                // Get rid of crap build up particularly if devices
                // are switched on first
                this.DiscardInBuffer();
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }
        protected virtual void ProcessDataReceived(object sender, EventArgs e)
        {


        }

        protected virtual void ConnectedSerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {


        }

        void ConnectedSerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //            MessageBox.Show("Received Data Error");
        }
    }



}
