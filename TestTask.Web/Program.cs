using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTask.Data.EF;
using TestTask.Data.EF.DependencyInjection;
using TestTask.Infrastructure.DependencyInjection;
using TestTask.Infrastructure.Contact.Model;
using TestTask.Web.Identity;
using TestTask.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.Events.OnRedirectToLogin = static context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = static context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<WebMappingProfile>();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentityCore<User>()
    .AddUserStore<UserStore>()
    .AddSignInManager<SignInManager<User>>();

var connectionString = builder.Configuration.GetConnectionString("Default");
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Connection string is required.");

builder.Services.AddEFDataLayer(connectionString);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/{*path:nonfile}", "index.html");

// Только для тестирования
using (var scope = app.Services.CreateScope())
{
    await SeedData.InitializeAsync(scope.ServiceProvider);
}

await app.RunAsync();
