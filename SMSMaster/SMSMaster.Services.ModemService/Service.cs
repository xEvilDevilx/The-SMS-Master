using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;

using SMSMaster.Services.Interfaces;

namespace SMSMaster.Services.ModemService
{
    /// <summary>
    /// Implements Service functionality
    /// </summary>
    public partial class Service : ServiceBase
    {
        /// <summary>Service Host</summary>
        private ServiceHost service_host = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 'On Start' event actions
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            if (service_host != null) service_host.Close();

            string address_HTTP = ConfigurationManager.AppSettings["host"];

            Uri[] address_base = { new Uri(address_HTTP) };
            service_host = new ServiceHost(typeof(SMSService), address_base);

            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            service_host.Description.Behaviors.Add(behavior);

            BasicHttpBinding binding_http = new BasicHttpBinding();
            service_host.AddServiceEndpoint(typeof(ISMSService), binding_http, address_HTTP);
            service_host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            ServiceDebugBehavior debug = service_host.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debug == null)
            {
                service_host.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                if (!debug.IncludeExceptionDetailInFaults)
                    debug.IncludeExceptionDetailInFaults = true;
            }

            service_host.Open();
        }

        /// <summary>
        /// 'On Stop' event actions
        /// </summary>
        protected override void OnStop()
        {
            if (service_host != null)
            {
                service_host.Close();
                service_host = null;
            }
        }
    }
}