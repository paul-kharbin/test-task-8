using Microsoft.Extensions.DependencyInjection;
using TestTask.Infrastructure.Contact.Services;
using TestTask.Infrastructure.Mapping;
using TestTask.Infrastructure.Services;

namespace TestTask.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(options =>
        {
            options.AddProfile<InfrastructureMappingProfile>();
        });

        services.AddScoped<ICanditatesService, CanditatesService>();
        services.AddScoped<IUsersService, UsersService>();

        return services;
    }
}
