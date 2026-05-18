using BeautySalonApp.Models;
using BeautySalonApp.ViewModels;

namespace BeautySalonApp.Services;

public interface IBookingService
{
    Task<List<Service>> GetServicesAsync();
    Task<List<Master>> GetMastersAsync();
    Task<List<Appointment>> GetAppointmentsAsync();
    Task CreateAppointmentAsync(BookingFormModel model);
}
