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
            modelBuilder.Entity<Employee>().HasKey(x => x.Id);
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<Employee>().Property(x => x.LastName).IsRequired();
            modelBuilder.Entity<Employee>().Property(x => x.DateOfBirth).IsRequired();
            modelBuilder.Entity<Employee>().Property(x => x.EmploymentDate).IsRequired();
            modelBuilder.Entity<Employee>().Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}
