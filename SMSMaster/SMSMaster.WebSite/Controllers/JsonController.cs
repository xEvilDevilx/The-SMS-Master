using System;
using System.ServiceModel;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using SMSMaster.WebSite.Channel;
using SMSMaster.WebSite.Model;

namespace SMSMaster.WebSite.Controllers
{
    /// <summary>
    /// Implements Json Controller functionality
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class JsonController : ControllerBase
    {
        /// <summary>
        /// Application settings
        /// </summary>
        private AppSettings AppSettings { get; set; }

        /// <summary>SMS Client</summary>
        private SMSClient _smsClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">Application settings</param>
        public JsonController(IOptions<AppSettings> settings)
        {
            AppSettings = settings.Value;
            CreateSMSClient();
        }

        /// <summary>
        /// Use for Create SMS Client
        /// </summary>
        private void CreateSMSClient()
        {
            if (AppSettings == null || AppSettings.Hosts == null || AppSettings.Hosts.Length < 1)
                return;

            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();

                var rnd = new Random();
                var rndNum = rnd.Next(0, AppSettings.Hosts.Length);

                string uri = AppSettings.Hosts[rndNum];//"http://localhost:9001/SMSService";

                EndpointAddress endpoint = new EndpointAddress(new Uri(uri));

                _smsClient = new SMSClient(binding, endpoint);
            }
            catch
            {
                _smsClient = null;
            }
        }

        /// <summary>
        /// Use for Close Client
        /// </summary>
        private void CloseClient()
        {
            if (_smsClient != null)
            {
                _smsClient.Close();
                _smsClient = null;
            }
        }

        /// <summary>
        /// GET api/json
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="message">SMS message</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> Get(string phone, string message)
        {
            if (string.IsNullOrEmpty(phone))
                return "Empty phone number!";

            if (string.IsNullOrEmpty(phone))
                return "Empty message!";

            return await Task.Run(() =>
            {
                if (_smsClient == null)
                    CreateSMSClient();

                var result = _smsClient.Send(phone, message);

                CloseClient();

                if (result)
                    return "Ok";
                else return "Failed";
            });
        }

        /// <summary>
        /// POST api/json
        /// </summary>
        /// <param name="data">JSON Data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody]JSONData data)
        {
            if (data == null)
                return "JSON data is null!";

            if (string.IsNullOrEmpty(data.Phone))
                return "Empty phone number!";

            return await Task.Run(() => 
            {
                if (_smsClient == null)
                    CreateSMSClient();

                var result = _smsClient.Send(data.Phone, data.Message);

                CloseClient();

                if (result)
                    return "Ok";
                else return "Failed";
            });
        }
    }
}