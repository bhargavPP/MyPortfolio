//using SendGrid;
//using SendGrid.Helpers.Mail;
using MimeKit;
using MailKit.Net.Smtp;

namespace MyPortFolio.Services
{
    public class EmailService
    {
        private readonly string _apiKey;
       private readonly string _fromEmail;
        private readonly string _fromPass;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["Authenticator:ApiKey"];
             _fromEmail = configuration["Authenticator:FromEmail"];
            _fromPass = configuration["Authenticator:pass"];
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
                messageBody.From.Add(new MailboxAddress("Portfolio", _fromEmail));
                messageBody.To.Add(new MailboxAddress("Bhargav Patel", toEmail));
                messageBody.Subject = subject;

                messageBody.Body = new TextPart("plain")
                {
                    Text = message
                };

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_apiKey,_fromPass);
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
