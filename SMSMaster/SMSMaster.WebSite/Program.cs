using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SMSMaster.WebSite
{
    /// <summary>
    /// Presents The Main Entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main Entry in program
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create Web Host Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}