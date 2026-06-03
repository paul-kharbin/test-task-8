using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Data.Contract.Model;

namespace TestTask.Data.EF;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<DataContext>();

        // Только для тестового задания, миграции не стал делать.
        await db.Database.EnsureCreatedAsync();

        if (!await db.Users.AnyAsync(u => u.Login == "ADMIN"))
        {
            var admin = new DataUser { Login = "ADMIN" };
            admin.PasswordHash = new PasswordHasher<DataUser>().HashPassword(admin, "Admin123!");

            db.Users.Add(admin);
        }

        if (!await db.Candidates.AnyAsync())
        {
            db.Candidates.AddRange(
                new DataCandidate
                {
                    FullName = "Анна Смирнова",
                    BirthDate = new DateTime(1993, 4, 12),
                    DesiredSalary = 145000,
                    Email = "anna@example.com",
                    Position = "Backend-разработчик",
                    ExperienceYears = 6
                },
                new DataCandidate
                {
                    FullName = "Иван Петров",
                    BirthDate = new DateTime(1988, 11, 3),
                    DesiredSalary = 175000,
                    Email = "ivan@example.com",
                    Position = "Системный аналитик",
                    ExperienceYears = 9
                },
                new DataCandidate
                {
                    FullName = "Мария Волкова",
                    BirthDate = new DateTime(1998, 7, 20),
                    DesiredSalary = 110000,
                    Email = "maria@example.com",
                    Position = "Frontend-разработчик",
                    ExperienceYears = 3
                });
        }

        await db.SaveChangesAsync();
    }
}
