using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using allocator.Models;
namespace allocator.Data
{
    public class AllocatorDBContext : DbContext
    {
        /*public AllocatorDBContext(DbContextOptions options) : base (options)
        {

        }*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=AllocatorData;user=root;password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseWork>()
                .HasKey(s => new { s.CourseId, s.UserId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

    }
}
