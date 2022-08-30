using Iot.Application.Dto.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailDto emailRequest);
    }
}
