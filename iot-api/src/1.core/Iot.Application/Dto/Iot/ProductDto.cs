using Iot.Application.Common.Mappings;
using Iot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Dto.Iot
{
    public class ProductDto : IMapFrom<Product>
    {
        public ProductDto()
        {
            Devices = new List<DeviceDto>();
        }

        public IList<DeviceDto> Devices { get; set; }
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Secret { get; set; }
        public string? Category { get; set; }
        public string? DataFormat { get; set; }
        public int DeviceCount { get; set; }
    }
}
