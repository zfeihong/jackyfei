using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iot.Application.Common.Interfaces;
using Iot.Application.Dto.Iot;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<ProductsVm> { }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper, IDistributedCache distributedCache)
        {
            _context = context;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<ProductsVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = new ProductsVm
            {
                Lists = await _context.Products
                   .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.Name)
                   .ToListAsync(cancellationToken)
            };

            return products;

            //const string cacheKey = "GetProducts";
            //ProductsVm products;
            //string serializedProducts;

            //var redisProducts = await _distributedCache.GetAsync(cacheKey, cancellationToken);

            //if (redisProducts == null)
            //{
            //    products = new ProductsVm
            //    {
            //        Lists = await _context.Products
            //            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            //            .OrderBy(t => t.Name)
            //            .ToListAsync(cancellationToken)
            //    };

            //    serializedProducts = JsonConvert.SerializeObject(products);
            //    redisProducts = Encoding.UTF8.GetBytes(serializedProducts);
            //    var options = new DistributedCacheEntryOptions()
            //        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
            //        .SetSlidingExpiration(TimeSpan.FromMinutes(1));
            //    await _distributedCache.SetAsync(cacheKey, redisProducts, options, cancellationToken);

            //    return products;
            //}

            //serializedProducts = Encoding.UTF8.GetString(redisProducts);
            //products = JsonConvert.DeserializeObject<ProductsVm>(serializedProducts);

            //return products;



            //return new ProductsVm
            //{
            //    Lists = await _context.Products
            //    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            //    .OrderBy(t => t.Name)
            //    .ToListAsync(cancellationToken)
            //};
        }
    }
}
