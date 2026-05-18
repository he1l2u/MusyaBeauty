using BeautySalonApp.Models;
using BeautySalonApp.Repositories;
using BeautySalonApp.ViewModels;

namespace BeautySalonApp.Services;

public class BookingService : IBookingService
{
    private readonly IRepository<Client> _clients;
    private readonly IRepository<Master> _masters;
    private readonly IRepository<Service> _services;
    private readonly IAppointmentRepository _appointments;

    public BookingService(
        IRepository<Client> clients,
        IRepository<Master> masters,
        IRepository<Service> services,
        IAppointmentRepository appointments)
    {
        _clients = clients;
        _masters = masters;
        _services = services;
        _appointments = appointments;
    }

    public async Task<List<Service>> GetServicesAsync()
    {
        var services = await _services.GetAllAsync();
        return services
            .OrderBy(GetServiceSortOrder)
            .ThenBy(x => x.Name)
            .ToList();
    }

    public async Task<List<Master>> GetMastersAsync()
    {
        var masters = await _masters.GetAllAsync();
        return masters
            .Where(x => x.IsActive)
            .OrderBy(GetMasterSortOrder)
            .ThenBy(x => x.FullName)
            .ToList();
    }

    public Task<List<Appointment>> GetAppointmentsAsync()
    {
        return _appointments.GetAllWithDetailsAsync();
    }

    private static int GetServiceSortOrder(Service service)
    {
        if (service.Name.Contains("стриж", StringComparison.OrdinalIgnoreCase))
        {
            return 1;
        }

        if (service.Name.Contains("маник", StringComparison.OrdinalIgnoreCase))
        {
            return 2;
        }

        if (service.Name.Contains("окраш", StringComparison.OrdinalIgnoreCase))
        {
            return 3;
        }

        return 99;
    }

    private static int GetMasterSortOrder(Master master)
    {
        if (master.FullName.Contains("Акрамова", StringComparison.OrdinalIgnoreCase))
        {
            return 1;
        }

        if (master.FullName.Contains("Ефимова", StringComparison.OrdinalIgnoreCase))
        {
            return 2;
        }

        if (master.FullName.Contains("Валеева", StringComparison.OrdinalIgnoreCase))
        {
            return 3;
        }

        return 99;
    }

    public async Task CreateAppointmentAsync(BookingFormModel model)
    {
        var appointmentDateTime = model.AppointmentDate.Date.Add(model.AppointmentTime);

        if (await _appointments.IsSlotBusyAsync(model.MasterId, appointmentDateTime))
        {
            throw new InvalidOperationException("Выбранное время уже занято. Выберите другое время.");
        }

        var clients = await _clients.FindAsync(x => x.Email == model.Email);
        var client = clients.FirstOrDefault();

        if (client is null)
        {
            client = new Client
            {
                FullName = model.FullName.Trim(),
                Email = model.Email.Trim(),
                Phone = model.Phone.Trim()
            };

            await _clients.AddAsync(client);
            await _clients.SaveChangesAsync();
        }

        var selectedServices = (await _services.GetAllAsync())
            .Where(x => model.SelectedServiceIds.Contains(x.Id))
            .ToList();

        var appointment = new Appointment
        {
            ClientId = client.Id,
            MasterId = model.MasterId,
            AppointmentDateTime = appointmentDateTime,
            TotalPrice = selectedServices.Sum(x => x.Price),
            Status = "Новая"
        };

        foreach (var service in selectedServices)
        {
            appointment.AppointmentServices.Add(new AppointmentService
            {
                ServiceId = service.Id
            });
        }

        await _appointments.AddAsync(appointment);
        await _appointments.SaveChangesAsync();
    }
}
