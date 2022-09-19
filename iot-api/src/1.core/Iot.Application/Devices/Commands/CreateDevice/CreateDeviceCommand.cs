using Iot.Application.Common.Interfaces;
using Iot.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Devices.Commands.CreateDevice
{
    public partial class CreateDeviceCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int ProductId { get; set; }
        public string? Secret { get; set; }
    }

    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateDeviceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            var entity = new Device
            {
                Name = request.Name,
                Code = request.Code,
                Secret = request.Secret,
                ProductId = request.ProductId
            };


            _context.Devices.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
