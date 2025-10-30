using System.ComponentModel.DataAnnotations;

namespace BlushEventPortal.Data;

public class Event
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public DateTime EventDate { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Venue { get; set; } = string.Empty;
    
    public string? ImageUrl { get; set; }
    
    public int Capacity { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();
}
