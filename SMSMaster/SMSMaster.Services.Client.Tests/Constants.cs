namespace SMSMaster.Services.Client.Tests
{
    /// <summary>
    /// Presents all Constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Url for connect to SMS Service
        /// </summary>
        public const string SMSServiceURL = "http://localhost:9001/SMSService";
        /// <summary>
        /// Phone Number for send test sms
        /// </summary>
        public const string TestSMSPhoneNumber = "+79999999999";
        /// <summary>
        /// SMS Message for send
        /// </summary>
        public const string TestSMSMessage = "\"message was sent\"";
    }
}