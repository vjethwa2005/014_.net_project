using Microsoft.AspNetCore.Identity;

namespace BlushEventPortal.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        try
        {
            if (context.Events.Any())
            {
                return;
            }
        }
        catch
        {
        }

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        var adminUser = await userManager.FindByEmailAsync("admin@blush.com");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin@blush.com",
                Email = "admin@blush.com",
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var events = new Event[]
        {
            new Event
            {
                Name = "Summer Music Festival",
                Description = "Join us for an unforgettable evening of live music under the stars! Featuring top local bands and artists.",
                EventDate = DateTime.Now.AddDays(30),
                Venue = "Central Park Amphitheater",
                ImageUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=800",
                Capacity = 500,
                CreatedAt = DateTime.Now
            },
            new Event
            {
                Name = "Tech Meetup & Networking",
                Description = "Connect with fellow tech enthusiasts, share ideas, and build your professional network. Light refreshments provided.",
                EventDate = DateTime.Now.AddDays(15),
                Venue = "Innovation Hub Downtown",
                ImageUrl = "https://images.unsplash.com/photo-1540575467063-178a50c2df87?w=800",
                Capacity = 100,
                CreatedAt = DateTime.Now
            },
            new Event
            {
                Name = "Rooftop Sunset Party",
                Description = "Dance the night away at our exclusive rooftop party with DJ, cocktails, and stunning city views!",
                EventDate = DateTime.Now.AddDays(45),
                Venue = "SkyBar Rooftop Lounge",
                ImageUrl = "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=800",
                Capacity = 150,
                CreatedAt = DateTime.Now
            }
        };

        await context.Events.AddRangeAsync(events);
        await context.SaveChangesAsync();
    }
}
