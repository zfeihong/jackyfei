using Iot.Application.Dto.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Devices.Queries.GetDevices
{
    public class DevicesVm
    {
        public IList<DeviceDto>? Lists { get; set; }
    }
}
