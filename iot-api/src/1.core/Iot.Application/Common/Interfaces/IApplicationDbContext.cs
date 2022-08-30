using Iot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iot.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Device> Devices { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
