using System.ComponentModel.DataAnnotations;

namespace BlushEventPortal.Data;

public class RSVP
{
    public int Id { get; set; }
    
    [Required]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    
    public DateTime RSVPDate { get; set; } = DateTime.Now;
    
    public string? Notes { get; set; }
}
