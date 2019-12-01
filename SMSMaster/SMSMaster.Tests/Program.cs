using System;
using System.Net;

namespace SMSMaster.Tests
{
    /// <summary>
    /// Presents The Main Entry point in test client program for send requests to SMS Web Service
    /// </summary>
    class Program
    {
        /// <summary>
        /// The Main Entry in program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TestPostSendMessage(Constants.TestSMSPhoneNumber, Constants.TestSMSMessage);

            Console.ReadLine();
        }

        /// <summary>
        /// Use for Send Post message
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="message">SMS message</param>
        private static void TestPostSendMessage(string phone, string message)
        {
            string url = Constants.SMSWebServiceURL;
            string parameters = "{phone: " + phone + ", message: " + message + "}";

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                string htmlResult = wc.UploadString(url, "POST", parameters);
            }
        }
    }
}