using Iot.Application.Common.Interfaces;
using Iot.Identity.Helper;
using Iot.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AuthSettings>(config.GetSection(nameof(AuthSettings)));
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
