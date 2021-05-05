using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TaskScript.Application.Areas.Identity.IdentityHostingStartup))]
namespace TaskScript.Application.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}