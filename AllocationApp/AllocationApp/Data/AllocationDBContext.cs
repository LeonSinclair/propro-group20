using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AllocationApp.Models;
//using MySQL.Data.EntityFrameworkCore.Extensions;
namespace AllocationApp.Data
{
    public class AllocationDBContext : DbContext
    {
        public AllocationDBContext(DbContextOptions options) : base (options)
        {

        }

        public AllocationDBContext()
        {
        }

        public DbSet<Subordinates> Subordinates { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=library;user=root;password=1234");
        }*/


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CourseWork>()
        //        .HasKey(s => new { s.CourseId, s.UserId });
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

    }
}
