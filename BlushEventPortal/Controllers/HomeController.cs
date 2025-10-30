using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlushEventPortal.Models;
using BlushEventPortal.Data;
using Microsoft.EntityFrameworkCore;

namespace BlushEventPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var upcomingEvents = await _context.Events
            .Where(e => e.EventDate >= DateTime.Now)
            .OrderBy(e => e.EventDate)
            .Include(e => e.RSVPs)
            .ToListAsync();
        
        return View(upcomingEvents);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
