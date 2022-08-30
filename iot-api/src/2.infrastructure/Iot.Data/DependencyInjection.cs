using Iot.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services)
        {

            services.AddDbContext<IotDbContext>(options => options
              .UseSqlite("Data Source=IotDatabase.sqlite3"));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<IotDbContext>());

            return services;
        }
    }
}
