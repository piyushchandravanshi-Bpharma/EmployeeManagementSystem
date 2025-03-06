using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmployeeManagementSystem.Server.Extensions;
using Microsoft.AspNetCore.Identity;
using EmployeeManagementSystem.Server.Data.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args,
            WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
        });

        // Configure all services
        builder.Services.ConfigureServices(builder.Configuration);

        // Build App
        var app = builder.Build();

        // Configure all middleware
        app.ConfigureMiddleware();

        // Seed Data
        await app.SeedDataAsync();

        app.Run();
    }
}
