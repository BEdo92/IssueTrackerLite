using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTrackerLite.Data;

public class Ticket
{
    [Key]
    public int TicketID { get; set; }

    [Required]
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public AppUser User { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Priority { get; set; } // e.g., "Low", "Medium", "High"

    [Required]
    public DateTime DateAdded { get; set; }

    [Required]
    public string State { get; set; } // e.g., "Open", "Closed", "In Progress"

    public DateTime? LastStateChanged { get; set; }
}
