using System.IO.Ports;
using System.Threading;
using System.Text;

using SMSMaster.Services.Interfaces;

namespace SMSMaster.Services
{
    /// <summary>
    /// Implements The SMS Service functionality
    /// </summary>
    public class SMSService : ISMSService
    {
        /// <summary>Serial port number</summary>
        private SerialPort _port;

        /// <summary>
        /// Constructor
        /// </summary>
        public SMSService()
        {
            _port = new SerialPort();
        }

        /// <summary>
        /// Use for Send sms message via serial port
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="message">SMS message</param>
        /// <returns>Result of the operation</returns>
        public bool Send(string phone, string message)
        {
            OpenPort();

            if (!_port.IsOpen)
                return false;

            if (!PrepareToSend())
                return false;

            try
            {
                _port.Write("AT+CMGS=\"" + phone + "\"" + "\r\n");
                Thread.Sleep(500);

                _port.Write(message + char.ConvertFromUtf32(26) + "\r\n");
                Thread.Sleep(500);
            }
            catch
            {
                return false;
            }

            try
            {
                string recievedData = _port.ReadExisting();

                if (recievedData.Contains("ERROR"))
                    return false;

            }
            catch { }

            _port.Close();
            return true;
        }

        private bool PrepareToSend()
        {
            try
            {
                Thread.Sleep(500);
                _port.WriteLine("AT\r\n");
                Thread.Sleep(500);

                _port.Write("AT+CMGF=1\r\n");
                Thread.Sleep(500);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void OpenPort()
        {
            _port.BaudRate = 2400; // 4800, 9600, 28800 or 56000
            _port.DataBits = 7; // 8 or 9

            _port.StopBits = StopBits.One; // StopBits.Two StopBits.None or StopBits.OnePointFive         
            _port.Parity = Parity.Odd; // Parity.Even Parity.Mark Parity.None or Parity.Space

            _port.ReadTimeout = 500; // 1000, 2500 or 5000
            _port.WriteTimeout = 500; // 1000, 2500 or 5000

            //port.Handshake = Handshake.RequestToSend;
            //port.DtrEnable = true;
            //port.RtsEnable = true;
            //port.NewLine = Environment.NewLine;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _port.Encoding = Encoding.GetEncoding("Unicode");

            _port.PortName = "COM5";

            if (_port.IsOpen)
                _port.Close();

            try
            {
                _port.Open();
            }
            catch
            { }
        }
    }
}