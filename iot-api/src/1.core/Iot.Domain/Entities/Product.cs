using Iot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iot.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Devices = new List<Device>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Secret { get; set; }
        public string? Category { get; set; }
        public NodeType NodeType { get; set; }
        public string? DataFormat { get; set; }
        public ProtocolType ProtocolType { get; set; }
        public NetType NetType { get; set; }
        public int DeviceCount { get; set; }

        public IList<Device> Devices { get; set; }

    }
}
