using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Data.Contract.Rrepository;
using TestTask.Data.EF.Repositories;

namespace TestTask.Data.EF.DependencyInjection;

public static class ServiceCollectionExtrensions
{
    public static IServiceCollection AddEFDataLayer(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<ICanditatesRrepositry, CanditatesRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}
