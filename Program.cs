using BeautySalonApp.Data;
using BeautySalonApp.Repositories;
using BeautySalonApp.Services;
using BeautySalonApp.Validators;
using BeautySalonApp.ViewModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<SalonDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string DefaultConnection was not found.");

    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IValidator<BookingFormModel>, BookingFormModelValidator>();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseAntiforgery();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SalonDbContext>();
    db.Database.Migrate();
    await DbInitializer.SeedAsync(db);
}

app.MapHealthChecks("/health");
app.MapRazorComponents<BeautySalonApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
