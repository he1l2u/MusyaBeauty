using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models;

public class Appointment
{
    public int Id { get; set; }

    public DateTime AppointmentDateTime { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int MasterId { get; set; }
    public Master Master { get; set; } = null!;

    [Column(TypeName = "numeric(10,2)")]
    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = "Новая";

    public ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();
}
