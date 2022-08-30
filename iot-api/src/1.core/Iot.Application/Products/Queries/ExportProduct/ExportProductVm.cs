using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Products.Queries.ExportProduct
{
    public class ExportProductVm
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public byte[]? Content { get; set; }
    }
}
