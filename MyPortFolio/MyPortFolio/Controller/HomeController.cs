using Microsoft.AspNetCore.Mvc;
using MyPortFolio.Modal;
using MyPortFolio.Services;


namespace MyPortFolio.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly EmailService _emailService;
        public IActionResult Index()
        {
            return View();
        }
        public HomeController(EmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Contact()
        {
            return View(new InquiryForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(InquiryForm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var subject = model.Subject ?? $"New Inquiry from {model.Name}";
                    var message = $"Name: {model.Name}\nEmail: {model.Email}\nSubject: {model.Subject}\nMessage: {model.Message}";
                    //    await _emailService.SendEmailAsync("patelbhargav2022@example.com", subject, message);

                    var confirmationMessage = $"Dear {model.Name},\n\nThank you for your message. We will get back to you soon!\n\nBest regards,\nYour Website Team";
                    //   await _emailService.SendEmailAsync(model.Email, "Thank You for Your Inquiry", confirmationMessage);

                    ViewBag.Message = "Thank you for your message! We will get back to you soon.";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "An error occurred while sending your message. Please try again later.";
                    return View(model);
                }
            }
            return View(model);
        }

    }
}
