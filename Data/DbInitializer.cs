using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonApp.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(SalonDbContext db)
    {
        if (!await db.Masters.AnyAsync())
        {
            db.Masters.AddRange(
                new Master
                {
                    FullName = "Акрамова Муслима",
                    Specialization = "Парикмахер-стилист",
                    Description = "Стрижки, укладки и базовый уход за волосами"
                },
                new Master
                {
                    FullName = "Ефимова Полина",
                    Specialization = "Мастер маникюра",
                    Description = "Классический маникюр, покрытие и уход за руками"
                },
                new Master
                {
                    FullName = "Валеева Динара",
                    Specialization = "Колорист",
                    Description = "Окрашивание волос и подбор оттенка"
                });
        }

        if (!await db.Services.AnyAsync())
        {
            db.Services.AddRange(
                new Service
                {
                    Name = "Женская стрижка",
                    Category = "Волосы",
                    Price = 1800,
                    DurationMinutes = 60,
                    Description = "Стрижка с учетом формы лица и пожеланий клиента"
                },
                new Service
                {
                    Name = "Маникюр с покрытием",
                    Category = "Ногти",
                    Price = 1600,
                    DurationMinutes = 90,
                    Description = "Классический маникюр и покрытие гель-лаком"
                },
                new Service
                {
                    Name = "Окрашивание волос",
                    Category = "Волосы",
                    Price = 4500,
                    DurationMinutes = 150,
                    Description = "Окрашивание волос с подбором оттенка"
                });
        }

        await db.SaveChangesAsync();
    }
}
