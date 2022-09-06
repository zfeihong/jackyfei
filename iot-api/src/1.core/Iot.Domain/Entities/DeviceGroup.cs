using System;
using System.Collections.Generic;
using System.Text;

namespace Iot.Domain.Entities
{
    public class DeviceGroup
    {
        public DeviceGroup()
        {
            Devices = new List<Device>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }

        public List<Device> Devices { get; set; }
    }
}
