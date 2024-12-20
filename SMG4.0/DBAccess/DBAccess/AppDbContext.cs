using DBAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.DBAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<EmployeeEntity>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<EmployeeEntity>().Property(x => x.LastName).IsRequired();
            modelBuilder.Entity<EmployeeEntity>().Property(x => x.DateOfBirth).IsRequired();
            modelBuilder.Entity<EmployeeEntity>().Property(x => x.EmploymentDate).IsRequired();
            modelBuilder.Entity<EmployeeEntity>().Property(x => x.IsActive).HasDefaultValue(true);

            modelBuilder.Entity<EmployeeActivationEntity>().HasKey(x => x.EmployeeId);

            modelBuilder.Entity<EmployeeEntity>().HasData
                (
                    new EmployeeEntity() { Id = 1,FirstName = "Admin", LastName = "Dev", IsActive = true, DateOfBirth = DateOnly.FromDateTime(DateTime.Now), Email = "test.test@email.com", EmploymentDate = DateOnly.FromDateTime(DateTime.Now) }
                );

            modelBuilder.Entity<EmployeeActivationEntity>().HasData(
                new EmployeeActivationEntity() { EmployeeId = 1, Code = "9999999999", ActivationDate = null });
        }

        public DbSet<EmployeeEntity> Employees { get; set;}
        public DbSet<AuthenticationTokenEntity> AuthenticationTokens { get; set;}
        public DbSet<EmployeeActivationEntity> EmployeeActivations { get; set;}
    }
}
