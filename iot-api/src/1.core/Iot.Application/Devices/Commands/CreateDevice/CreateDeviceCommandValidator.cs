using FluentValidation;
using Iot.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Devices.Commands.CreateDevice
{
     public class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateDeviceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
              .NotEmpty().WithMessage("Name is required.")
              .MaximumLength(200).WithMessage("Name must not exceed 90 characters.");

            RuleFor(v => v.Code)
              .NotEmpty().WithMessage("Code is required")
              .MaximumLength(200).WithMessage("Code must not exceed 60 characters.");
        }
    }
}
