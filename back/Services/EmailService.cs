using System.Net;
using System.Net.Mail;

namespace EitechPfe.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendSubscriptionReminderEmail(string emailAddress)
        {
            var smtpHost = _configuration["Email:SmtpHost"] ?? throw new InvalidOperationException("SMTP host is missing.");
            var smtpPortString = _configuration["Email:SmtpPort"] ?? throw new InvalidOperationException("SMTP port is missing.");
            var smtpPort = int.Parse(smtpPortString);
            var username = _configuration["Email:Username"] ?? throw new InvalidOperationException("Email username is missing.");
            var password = _configuration["Email:Password"] ?? throw new InvalidOperationException("Email password is missing.");
            var fromAddress = _configuration["Email:FromAddress"] ?? throw new InvalidOperationException("From address is missing.");

            var smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = "Subscription Expiration Reminder",
                Body = "Dear user,\n\nYour subscription is about to expire soon. Please renew it to continue enjoying our services.\n\nBest regards,\nEitechPFE Team",
                IsBodyHtml = false
            };

            mailMessage.To.Add(emailAddress);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
