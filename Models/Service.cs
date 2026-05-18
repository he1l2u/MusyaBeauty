using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models;

public class Service
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Category { get; set; } = string.Empty;

    [Column(TypeName = "numeric(10,2)")]
    public decimal Price { get; set; }

    public int DurationMinutes { get; set; }

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();
}
