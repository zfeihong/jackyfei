using Iot.Application.Products.Queries.ExportProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildDeviceFile(IEnumerable<DeviceRecord> records);
    }
}
