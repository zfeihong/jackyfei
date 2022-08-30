using CsvHelper;
using Iot.Application.Common.Interfaces;
using Iot.Application.Products.Queries.ExportProduct;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Shared.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildDeviceFile(IEnumerable<DeviceRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        } 
    }
}
