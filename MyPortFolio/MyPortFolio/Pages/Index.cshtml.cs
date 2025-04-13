using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortFolio.Modal;
using MyPortFolio.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace MyPortFolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EmailService _emailService;

        public IndexModel(EmailService emailService)
        {
            _emailService = emailService;
            InquiryForm = new InquiryForm();
        }

        [BindProperty]
        public InquiryForm InquiryForm { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var subject = InquiryForm.Subject ?? $"New Inquiry from {InquiryForm.Name}";
                    var message = $"Name: {InquiryForm.Name}\nEmail: {InquiryForm.Email}\nSubject: {InquiryForm.Subject}\nMessage: {InquiryForm.Message}";
                    //await _emailService.SendEmailAsync("bhargavpp2017@gmail.com", subject, message);
                    await _emailService.SendEmailAsync("bhargavpp2017@gmail.com", subject, message);

                    var confirmationMessage = $"Dear {InquiryForm.Name},\n\nThank you for your message. We will get back to you soon!\n\nBest regards,\nBhargav Patel";
                    //await _emailService.SendEmailAsync(InquiryForm.Email, "Thank You for Your Inquiry", confirmationMessage);
                    await _emailService.SendEmailAsync(InquiryForm.Email, "Thank You for Your Inquiry", confirmationMessage);

                    Message = "Thank you for your message! We will get back to you soon.";
                    InquiryForm = new InquiryForm(); // Reset the form
                }
                catch (Exception ex)
                {
                    Error = "An error occurred while sending your message. Please try again later.";
                }
            }
            return Page();
        }
    }
}
