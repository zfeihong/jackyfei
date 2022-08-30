using System;
using System.Collections.Generic;
using System.Text;

namespace Iot.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Secret { get; set; }
        public int ProductId { get; set; }
        public int GroupId { get; set; }
    }
}
