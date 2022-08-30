using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iot.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Products.Queries.ExportProduct
{
    public class ExportProductQuery : IRequest<ExportProductVm>
    {
        public int Id { get; set; }
    }

    public class ExportProductQueryHandler : IRequestHandler<ExportProductQuery, ExportProductVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportProductQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportProductVm> Handle(ExportProductQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportProductVm();

            var records = await _context.Devices
                .Where(t => t.Id == request.Id)
                .ProjectTo<DeviceRecord>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            vm.Content = _fileBuilder.BuildDeviceFile(records);
            vm.ContentType = "text/csv";
            vm.FileName = "Devices.csv";

            return await Task.FromResult(vm);
        }
    }
}
