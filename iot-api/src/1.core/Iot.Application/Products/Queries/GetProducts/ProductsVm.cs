using Iot.Application.Dto.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Products.Queries.GetProducts
{
    public class ProductsVm
    {
        public IList<ProductDto>? Lists { get; set; }
    }
}