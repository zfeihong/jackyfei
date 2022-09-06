using Iot.Application.Common.Exceptions;
using Iot.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Devices.Commands.DeleteDevice
{
    public class DeleteDeviceCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteDeviceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Devices
              .Where(l => l.Id == request.Id)
              .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Products), request.Id);
            }

            _context.Devices.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
