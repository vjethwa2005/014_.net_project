using Microsoft.AspNetCore.Mvc;
using BlushEventPortal.Models;
using BlushEventPortal.Services;

namespace BlushEventPortal.Controllers;

public class ContactController : Controller
{
    private readonly IEmailSender _emailSender;

    public ContactController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var emailBody = $@"
            <div style='font-family: Arial, sans-serif;'>
                <h3>New Contact Form Submission</h3>
                <p><strong>From:</strong> {model.Name} ({model.Email})</p>
                <p><strong>Message:</strong></p>
                <p>{model.Message}</p>
            </div>
        ";

        await _emailSender.SendEmailAsync("admin@blush.com", "Contact Form Submission", emailBody);

        TempData["Success"] = "Thank you for contacting us! We'll get back to you soon.";
        return RedirectToAction("Index");
    }
}
