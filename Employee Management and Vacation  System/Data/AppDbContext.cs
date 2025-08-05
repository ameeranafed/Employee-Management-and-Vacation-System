using Employee_Management_and_Vacation_System.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;


namespace Employee_Management_and_Vacation_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // كل DbSet = جدول في قاعدة البيانات
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<RequestState> RequestStates { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }

        // هنا نضبط العلاقات (Relationships)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee - Department (Many to One)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            // Employee - Position (Many to One)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);

            // Employee - ReportedTo (Self-Reference)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ReportedTo)
                .WithMany(e => e.Subordinates)
                .HasForeignKey(e => e.ReportedToEmployeeNumber)
                .OnDelete(DeleteBehavior.NoAction); // ما نحذف الموظف إذا حذف المدير

            // VacationRequest - Employee
            modelBuilder.Entity<VacationRequest>()
                .HasOne(vr => vr.Employee)
                .WithMany(e => e.VacationRequests)
                .HasForeignKey(vr => vr.EmployeeNumber);

            // VacationRequest - VacationType
            modelBuilder.Entity<VacationRequest>()
                .HasOne(vr => vr.VacationType)
                .WithMany(vt => vt.VacationRequests)
                .HasForeignKey(vr => vr.VacationTypeCode);

            // VacationRequest - RequestState
            modelBuilder.Entity<VacationRequest>()
                .HasOne(vr => vr.RequestState)
                .WithMany(rs => rs.VacationRequests)
                .HasForeignKey(vr => vr.RequestStateId);

            // ApprovedBy - Employee
            modelBuilder.Entity<VacationRequest>()
                .HasOne(vr => vr.ApprovedByEmployee)
                .WithMany()
                .HasForeignKey(vr => vr.ApprovedByEmployeeNumber)
                .OnDelete(DeleteBehavior.NoAction);

            // DeclinedBy - Employee
            modelBuilder.Entity<VacationRequest>()
                .HasOne(vr => vr.DeclinedByEmployee)
                .WithMany()
                .HasForeignKey(vr => vr.DeclinedByEmployeeNumber)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed initial data for RequestState
            modelBuilder.Entity<RequestState>().HasData(
                new RequestState { StateId = 1, StateName = "Submitted" },
                new RequestState { StateId = 2, StateName = "Approved" },
                new RequestState { StateId = 3, StateName = "Declined" }
            );

            // Seed initial data for VacationType
            modelBuilder.Entity<VacationType>().HasData(
                new VacationType { VacationTypeCode = 'S', VacationTypeName = "Sick" },
                new VacationType { VacationTypeCode = 'U', VacationTypeName = "Unpaid" },
                new VacationType { VacationTypeCode = 'A', VacationTypeName = "Annual" },
                new VacationType { VacationTypeCode = 'O', VacationTypeName = "Day Off" },
                new VacationType { VacationTypeCode = 'B', VacationTypeName = "Business Trip" }
            );
        }
    }
}
