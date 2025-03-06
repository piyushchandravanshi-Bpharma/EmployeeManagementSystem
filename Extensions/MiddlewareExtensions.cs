using Microsoft.AspNetCore.Identity;
using EmployeeManagementSystem.Server.Data.Models;
using System.Security.Claims;

namespace EmployeeManagementSystem.Server.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            // Configure error handling first
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Serve React static files
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Enable CORS
            app.UseCors("AllowAll");

            // Enable Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map identity endpoints
            app.MapIdentityApi<ApplicationUser>();

            // Map custom endpoints
            app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            }).RequireAuthorization();

            app.MapGet("/pingauth", (ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var email = user.FindFirstValue(ClaimTypes.Email);
                var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
                return Results.Json(new { UserId = userId, Email = email, Roles = roles });
            }).RequireAuthorization();

            // Enable Swagger in Development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Redirect HTTP to HTTPS
            app.UseHttpsRedirection();

            // Map Controllers
            app.MapControllers();

            // Serve React frontend (fallback to index.html)
            app.MapFallbackToFile("/index.html");
        }
    }
}
