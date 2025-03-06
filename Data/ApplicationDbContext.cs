using EmployeeManagementSystem.Server.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }
        public DbSet<AdminSchedule> AdminSchedules { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee - ApplicationUser Unique Foreign Key
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ApplicationUser)              // Define FK relationship
                .WithOne()                                   // One-to-one relationship
                .HasForeignKey<Employee>(e => e.UserId)      // UserId as FK
                .IsRequired()                                // FK is required
                .OnDelete(DeleteBehavior.Cascade);           // Optional: Cascade delete

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.UserId)                     // ✅ Unique constraint on UserId
                .IsUnique();

            // Many-to-Many Relationship (Employee-Task)
            modelBuilder.Entity<TaskAssignment>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.TaskAssignments)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(t => t.ApplicationUser)
                .WithMany()
                .HasForeignKey(t => t.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique Constraints
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.DepartmentName)
                .IsUnique();

            // Fix Multiple Cascade Paths
            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.ChatRoom)
                .WithMany(cr => cr.ChatMessages)
                .HasForeignKey(cm => cm.ChatRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Removed navigation property to ApplicationUser
            modelBuilder.Entity<ChatMessage>()
                .Property(cm => cm.SenderId)
                .IsRequired()
                .HasMaxLength(450);
        }
    }
}
