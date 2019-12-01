using System;
using System.ServiceModel;

namespace SMSMaster.Services.Client.Tests
{
    /// <summary>
    /// Presents The Main Entry point in test client program
    /// </summary>
    class Program
    {
        /// <summary>SMS Client</summary>
        private static SMSClient _smsClient;

        /// <summary>
        /// The Main Entry in program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CreateSMSClient();
            SendSMS();
            CloseClient();
        }

        /// <summary>
        /// Use for Create SMS Client
        /// </summary>
        private static void CreateSMSClient()
        {
            try
            {
                //NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);
                //binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                //binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                //binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

                BasicHttpBinding binding = new BasicHttpBinding();                
                EndpointAddress endpoint = new EndpointAddress(new Uri(Constants.SMSServiceURL));
                _smsClient = new SMSClient(binding, endpoint);

                //_smsClient.ClientCredentials.Windows.ClientCredential.Domain = "";
                //_smsClient.ClientCredentials.Windows.ClientCredential.UserName = "";
                //_smsClient.ClientCredentials.Windows.ClientCredential.Password = "";
            }
            catch
            {
                _smsClient = null;
            }
        }

        /// <summary>
        /// Use for send sms to test number
        /// </summary>
        private static void SendSMS()
        {
            if (_smsClient == null)
                CreateSMSClient();

            _smsClient.Send(Constants.TestSMSPhoneNumber, Constants.TestSMSMessage); 
        }

        /// <summary>
        /// Use for Close SMS Client
        /// </summary>
        private static void CloseClient()
        {
            if(_smsClient != null)
            {
                _smsClient.Close();
                _smsClient = null;
            }
        }
    }
}