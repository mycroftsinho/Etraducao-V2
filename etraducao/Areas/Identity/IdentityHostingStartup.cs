using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(etraducao.Areas.Identity.IdentityHostingStartup))]
namespace etraducao.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}