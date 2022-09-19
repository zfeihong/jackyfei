using Iot.Domain.Entities;
using Iot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Data
{
    public static class IotDbContextSeed
    {
        public static async Task SeedSampleDataAsync(IotDbContext context)
        {
            if (!context.Products.Any())
            {
                await context.Products.AddAsync(new Product
                {
                    //Id = 1,
                    Code = "ele",
                    Name = "电梯",
                    DeviceCount = 1,
                    NetType = NetType.MQTT,
                    NodeType = NodeType.DirectDevice,
                    Secret = "123qweasd",
                    Devices = new List<Device>()
                     {
                         new()
                         {
                             Name = "电梯设备1",
                             Code = "ele1",
                              ProductId = 1,
                               //Id = 1
                         }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
