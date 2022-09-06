using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iot.Application.Common.Interfaces;
using Iot.Application.Devices.Queries.GetDevices;
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

namespace Iot.Application.Devices.Queries.GetProducts
{
    public class GetDevicesQuery : IRequest<DevicesVm> { }

    public class GetDevicesQueryHandler : IRequestHandler<GetDevicesQuery, DevicesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public GetDevicesQueryHandler(IApplicationDbContext context, IMapper mapper, IDistributedCache distributedCache)
        {
            _context = context;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<DevicesVm> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
        {
            var devices = new DevicesVm
            {
                Lists = await _context.Devices
                   .ProjectTo<DeviceDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.Name)
                   .ToListAsync(cancellationToken)
            };

            return devices;
        }
    }
}
