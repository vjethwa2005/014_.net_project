using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlushEventPortal.Data;
using BlushEventPortal.Services;

namespace BlushEventPortal.Controllers;

[Authorize]
public class RSVPController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public RSVPController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _context = context;
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task<IActionResult> MyRSVPs()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var rsvps = await _context.RSVPs
            .Include(r => r.Event)
            .Where(r => r.UserId == user.Id)
            .OrderBy(r => r.Event.EventDate)
            .ToListAsync();

        return View(rsvps);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int eventId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var eventItem = await _context.Events.FindAsync(eventId);
        if (eventItem == null)
        {
            return NotFound();
        }

        var existingRsvp = await _context.RSVPs
            .FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == user.Id);

        if (existingRsvp != null)
        {
            TempData["Error"] = "You have already RSVP'd for this event.";
            return RedirectToAction("Details", "Event", new { id = eventId });
        }

        var rsvpCount = await _context.RSVPs.CountAsync(r => r.EventId == eventId);
        if (rsvpCount >= eventItem.Capacity)
        {
            TempData["Error"] = "This event is at full capacity.";
            return RedirectToAction("Details", "Event", new { id = eventId });
        }

        var rsvp = new RSVP
        {
            EventId = eventId,
            UserId = user.Id,
            RSVPDate = DateTime.Now
        };

        _context.RSVPs.Add(rsvp);
        await _context.SaveChangesAsync();

        var emailBody = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background: linear-gradient(135deg, #fce4ec 0%, #f8bbd0 100%);'>
                <h2 style='color: #d81b60;'>RSVP Confirmation</h2>
                <p>Hi {user.FirstName ?? user.Email},</p>
                <p>Thank you for RSVPing to <strong>{eventItem.Name}</strong>!</p>
                <div style='background: white; padding: 15px; border-radius: 10px; margin: 20px 0;'>
                    <p><strong>Event:</strong> {eventItem.Name}</p>
                    <p><strong>Date:</strong> {eventItem.EventDate:MMMM dd, yyyy 'at' hh:mm tt}</p>
                    <p><strong>Venue:</strong> {eventItem.Venue}</p>
                </div>
                <p>We look forward to seeing you there!</p>
                <p style='color: #d81b60;'><em>- Blush Event Portal Team</em></p>
            </div>
        ";

        await _emailSender.SendEmailAsync(user.Email!, "RSVP Confirmation", emailBody);

        TempData["Success"] = "Successfully RSVP'd! Check your email for confirmation.";
        return RedirectToAction("Details", "Event", new { id = eventId });
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var rsvp = await _context.RSVPs
            .Include(r => r.Event)
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == user.Id);

        if (rsvp == null)
        {
            return NotFound();
        }

        _context.RSVPs.Remove(rsvp);
        await _context.SaveChangesAsync();

        TempData["Success"] = "RSVP cancelled successfully.";
        return RedirectToAction("MyRSVPs");
    }
}
