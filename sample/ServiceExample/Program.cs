using HelperLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ServiceExample
{
    public class Program
    {
        public static void Main()
        {
            CreateHostBuilder().Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder() =>
            new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureServices(services => ConfigureServices(services))
                    .UseStartup<Startup>()
                    .UseUrls("http://localhost:8025");

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IService, Service>();
        }
    }
}
