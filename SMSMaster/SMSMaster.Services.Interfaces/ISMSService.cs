using System.ServiceModel;

namespace SMSMaster.Services.Interfaces
{
    /// <summary>
    /// Presents The SMS Service functionality
    /// </summary>
    [ServiceContract]
    public interface ISMSService
    {
        /// <summary>
        /// Use for Send sms message via serial port
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="message">SMS message</param>
        /// <returns>Result of the operation</returns>
        [OperationContract]
        bool Send(string phone, string message);
    }
}