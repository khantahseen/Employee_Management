using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasData(
               new Department { DepartmentID = 1, Name = ".NET" });
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1,  Name = "Tahseen", Surname = "Khan", Email = "tahseenk211@gmail.com", Address = "Vadodara", ContactNumber = 343434343, Qualification = "BE", DepartmentID = 1 });
            modelBuilder.Entity<Employee>().HasData(
                 new Employee { EmployeeId = 2, Name = "Tahira", Surname = "Khan", Email = "tahira@gmail.com", Address = "Vadodara", ContactNumber = 56565656, Qualification = "BE", DepartmentID  = 1 });

        }
    }
}
