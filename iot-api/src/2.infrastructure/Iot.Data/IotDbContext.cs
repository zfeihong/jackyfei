using Iot.Application.Common.Interfaces;
using Iot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Data
{
    public class IotDbContext : DbContext, IApplicationDbContext
    {
        public IotDbContext(DbContextOptions<IotDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
