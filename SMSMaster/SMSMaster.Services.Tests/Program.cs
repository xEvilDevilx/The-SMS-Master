using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;

using SMSMaster.Services.Interfaces;

namespace SMSMaster.Services.Tests
{
    /// <summary>
    /// Presents The Main Entry point in test Service program
    /// </summary>
    class Program
    {
        /// <summary>Service Host functionality</summary>
        private static ServiceHost service_host = null;

        /// <summary>
        /// The Main Entry in program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
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

            Console.WriteLine("Press any key for stop...");
            Console.ReadLine();

            if (service_host != null)
            {
                service_host.Close();
                service_host = null;
            }
        }
    }
}