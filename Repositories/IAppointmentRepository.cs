using BeautySalonApp.Models;

namespace BeautySalonApp.Repositories;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAllWithDetailsAsync();
    Task<bool> IsSlotBusyAsync(int masterId, DateTime appointmentDateTime);
    Task AddAsync(Appointment appointment);
    Task SaveChangesAsync();
}
