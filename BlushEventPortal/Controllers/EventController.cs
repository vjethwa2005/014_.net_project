using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlushEventPortal.Data;

namespace BlushEventPortal.Controllers;

public class EventController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var events = await _context.Events
            .Where(e => e.EventDate >= DateTime.Now)
            .OrderBy(e => e.EventDate)
            .Include(e => e.RSVPs)
            .ToListAsync();
        
        return View(events);
    }

    public async Task<IActionResult> Details(int id)
    {
        var eventItem = await _context.Events
            .Include(e => e.RSVPs)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (eventItem == null)
        {
            return NotFound();
        }

        return View(eventItem);
    }
}
