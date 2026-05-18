using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonApp.Data;

public class SalonDbContext : DbContext
{
    public SalonDbContext(DbContextOptions<SalonDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Master> Masters => Set<Master>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentService> AppointmentServices => Set<AppointmentService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.FullName).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Phone).HasMaxLength(30).IsRequired();
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.Property(x => x.FullName).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Specialization).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Category).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Price).HasColumnType("numeric(10,2)");
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(x => x.AppointmentDateTime).HasColumnType("timestamp without time zone");
            entity.Property(x => x.TotalPrice).HasColumnType("numeric(10,2)");
            entity.Property(x => x.Status).HasMaxLength(40).HasDefaultValue("Новая");
            entity.HasIndex(x => new { x.MasterId, x.AppointmentDateTime }).IsUnique();

            entity.HasOne(x => x.Client)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Master)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.MasterId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AppointmentService>(entity =>
        {
            entity.HasKey(x => new { x.AppointmentId, x.ServiceId });

            entity.HasOne(x => x.Appointment)
                .WithMany(x => x.AppointmentServices)
                .HasForeignKey(x => x.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Service)
                .WithMany(x => x.AppointmentServices)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
