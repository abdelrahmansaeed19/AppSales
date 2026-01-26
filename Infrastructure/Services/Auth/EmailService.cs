using Application.Interfaces.IServices.Auth;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net.Mail;

namespace Infrastructure.Services.Auth
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendVerificationEmailAsync(string email, string code)
        {
            var subject = "Verify your email";
            var body = $"Your verification code is: {code}";
            await SendEmailAsync(email, subject, body);
        }

        public async Task SendResetPasswordEmailAsync(string email, string code)
        {
            var subject = "Reset your password";
            var body = $"Your password reset code is: {code}";
            await SendEmailAsync(email, subject, body);
        }

        private async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(_config["Email:From"]));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();


            var smtpServer = _config["Email:SmtpServer"]
                ?? throw new InvalidOperationException("Email:SmtpServer is not configured.");

            var portString = _config["Email:Port"]
                ?? throw new InvalidOperationException("Email:Port is not configured.");

            if (!int.TryParse(portString, out var port))
                throw new InvalidOperationException("Email:Port must be a valid number.");

            await smtp.ConnectAsync(smtpServer, port, false);

            await smtp.AuthenticateAsync(
                _config["Email:Username"],
                _config["Email:Password"]
            );

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
