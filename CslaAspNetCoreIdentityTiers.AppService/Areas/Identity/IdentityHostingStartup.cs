using System;
using CslaAspNetCoreIdentityTiers.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CslaAspNetCoreIdentityTiers.AppService.Areas.Identity.IdentityHostingStartup))]
namespace CslaAspNetCoreIdentityTiers.AppService.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddIdentity<AppUser, Role>().AddDefaultTokenProviders();

                services.AddTransient<IUserStore<AppUser>, AppUserStore>();
                services.AddTransient<IRoleStore<Role>,    RoleStore>();
            });
        }
    }
}