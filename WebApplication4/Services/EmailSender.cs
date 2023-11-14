using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;

namespace OrdersSystem.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string emailto, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("nadawca maila", "praktyki_2023@wp.pl"));
            email.To.Add(MailboxAddress.Parse(emailto));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.wp.pl", 587, SecureSocketOptions.Auto);
            smtp.Authenticate("praktyki_2023@wp.pl", "Praktykiszkolne2023!");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Task.CompletedTask;
        }
    }
}