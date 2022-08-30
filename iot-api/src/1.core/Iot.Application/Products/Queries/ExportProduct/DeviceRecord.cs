using Iot.Application.Common.Mappings;
using Iot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Products.Queries.ExportProduct
{
    public class DeviceRecord : IMapFrom<Device>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
