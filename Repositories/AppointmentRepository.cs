using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonApp.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly SalonDbContext _db;

    public AppointmentRepository(SalonDbContext db)
    {
        _db = db;
    }

    public Task<List<Appointment>> GetAllWithDetailsAsync()
    {
        return _db.Appointments
            .Include(x => x.Client)
            .Include(x => x.Master)
            .Include(x => x.AppointmentServices)
            .ThenInclude(x => x.Service)
            .OrderByDescending(x => x.AppointmentDateTime)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<bool> IsSlotBusyAsync(int masterId, DateTime appointmentDateTime)
    {
        return _db.Appointments.AnyAsync(x =>
            x.MasterId == masterId &&
            x.AppointmentDateTime == appointmentDateTime);
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _db.Appointments.AddAsync(appointment);
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
