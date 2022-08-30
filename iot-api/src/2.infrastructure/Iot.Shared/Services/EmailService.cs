using Iot.Application.Common.Exceptions;
using Iot.Application.Common.Interfaces;
using Iot.Application.Dto.Email;
using Iot.Domain.Settings;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Iot.Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings MailSettings { get; }
        private ILogger<EmailService> Logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            MailSettings = mailSettings.Value;
            Logger = logger;
        }

        public async Task SendAsync(EmailDto request)
        {
            try
            {
                var email = new MimeMessage { Sender = MailboxAddress.Parse(request.From ?? MailSettings.EmailFrom) };
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder { HtmlBody = request.Body };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}
