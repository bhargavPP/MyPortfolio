﻿//using SendGrid;
//using SendGrid.Helpers.Mail;
using MimeKit;
using MailKit.Net.Smtp;

namespace MyPortFolio.Services
{
    public class EmailService
    {
        private readonly string _apiKey;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"];
            _fromEmail = configuration["SendGrid:FromEmail"];
            _fromName = configuration["SendGrid:FromName"];
        }

        //public async Task SendEmailAsync(string toEmail, string subject, string message)
        //{
        //    var client = new SendGridClient(_apiKey);
        //    var from = new EmailAddress(_fromEmail, _fromName);
        //    var to = new EmailAddress(toEmail);
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
        //    await client.SendEmailAsync(msg);
        //}

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {


                var messageBody = new MimeMessage();
                messageBody.From.Add(new MailboxAddress("Portfolio", "patelbhargav2020@gmail.com"));
                messageBody.To.Add(new MailboxAddress("Bhargav Patel", toEmail));
                messageBody.Subject = subject;

                messageBody.Body = new TextPart("plain")
                {
                    Text = message
                };

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("patelbhargav2022@gmail.com", "vxti mimw bxhu xccq");
                await client.SendAsync(messageBody);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
