using System.ServiceModel;
using System.ServiceModel.Channels;

using SMSMaster.Services.Interfaces;

namespace SMSMaster.Services.Client.Tests
{
    /// <summary>
    /// Implements SMS Client functionality for send sms messages
    /// </summary>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    public class SMSClient : ClientBase<ISMSService>, ISMSService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="remoteAddress"></param>
        public SMSClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Use for Send sms message to phone number
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="message">SMS message</param>
        /// <returns></returns>
        public bool Send(string phone, string message)
        {
            return base.Channel.Send(phone, message);
        }
    }
}