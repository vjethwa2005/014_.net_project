using Microsoft.AspNetCore.Identity;

namespace BlushEventPortal.Data;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();
}
