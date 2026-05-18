namespace BeautySalonApp.ViewModels;

public class BookingFormModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int MasterId { get; set; }
    public DateTime AppointmentDate { get; set; } = DateTime.Today.AddDays(1);
    public TimeSpan AppointmentTime { get; set; } = new(10, 0, 0);
    public List<int> SelectedServiceIds { get; set; } = new();
}
