using EmployeeManagementSystem.Server.Data;
using EmployeeManagementSystem.Server.Service.Interfaces;
using EmployeeManagementSystem.Server.Service.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Server.Data.Models;
using EmployeeManagementSystem.Server.Service;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Get connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection String 'DefaultConnection' not found.");

            // Configure DB Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Configure Identity
            services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            // Configure IIS options
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 52428800; // 50 MB
            });

            // Register services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITaskAssignmentService, TaskAssignmentService>();
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IEmployeeScheduleService, EmployeeScheduleService>();

            // Configure Swagger and other services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        }
    }
}
