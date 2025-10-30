using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlushEventPortal.Data;

namespace BlushEventPortal.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var events = await _context.Events
            .Include(e => e.RSVPs)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
        
        return View(events);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Event eventItem)
    {
        if (!ModelState.IsValid)
        {
            return View(eventItem);
        }

        eventItem.CreatedAt = DateTime.Now;
        _context.Events.Add(eventItem);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Event created successfully!";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var eventItem = await _context.Events.FindAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }

        return View(eventItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Event eventItem)
    {
        if (id != eventItem.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(eventItem);
        }

        try
        {
            _context.Update(eventItem);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Event updated successfully!";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await EventExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var eventItem = await _context.Events
            .Include(e => e.RSVPs)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (eventItem == null)
        {
            return NotFound();
        }

        return View(eventItem);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var eventItem = await _context.Events.FindAsync(id);
        if (eventItem != null)
        {
            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Event deleted successfully!";
        }

        return RedirectToAction("Index");
    }

    private async Task<bool> EventExists(int id)
    {
        return await _context.Events.AnyAsync(e => e.Id == id);
    }
}
