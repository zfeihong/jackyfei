using Iot.Application.Common.Exceptions;
using Iot.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Devices.Commands.UpdateDevice
{
    public class UpdateDeviceCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }

    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDeviceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Devices.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Devices), request.Id);
            }
            entity.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
