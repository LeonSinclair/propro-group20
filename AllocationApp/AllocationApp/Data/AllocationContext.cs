using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AllocationApp.Models;
//using MySQL.Data.EntityFrameworkCore.Extensions;
namespace AllocationApp.Data
{
    public class AllocationContext : DbContext
    {
        public AllocationContext(DbContextOptions options) : base(options)
        {

        }
        public AllocationContext()
        {
                
        }
        public DbSet<Subordinates> Subordinates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseUser>()
                .HasKey(s => new { s.CourseID, s.UserID });
            modelBuilder.Entity<CourseUser>()
                .HasOne(m => m.Course)
                .WithMany(ma => ma.Users)
                .HasForeignKey(m => m.CourseID);
            modelBuilder.Entity<CourseUser>()
                .HasOne(m => m.User)
                .WithMany(ma => ma.Courses)
                .HasForeignKey(m => m.UserID);
            modelBuilder.Entity<SubordinateModule>()
                .HasKey(s => new { s.ModuleID, s.SubordinateID });
            modelBuilder.Entity<Proposal>()
                .HasKey(p => new { p.CourseID, p.UserID });

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Proposal> Proposal { get; set; }
        public DbSet<SubordinateModule> SubordinateModules { get; set; }
    }
}
