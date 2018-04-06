using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AllocationApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AllocationApp.Data.Entities;
//using MySQL.Data.EntityFrameworkCore.Extensions;

namespace AllocationApp.Data
{
    public class AllocationContext : IdentityDbContext<AppUser>
    {
        public AllocationContext(DbContextOptions options) : base(options)
        {

        }
        public AllocationContext()
        {
                
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ModuleUser>()
                .HasKey(s => new { s.ModuleID, s.UserID });
            modelBuilder.Entity<ModuleUser>()
                .HasOne(m => m.Module)
                .WithMany(ma => ma.Users)
                .HasForeignKey(m => m.ModuleID);
            modelBuilder.Entity<ModuleUser>()
                .HasOne(m => m.User)
                .WithMany(ma => ma.Modules)
                .HasForeignKey(m => m.UserID);
            modelBuilder.Entity<SubordinateModule>()
                .HasKey(s => new { s.ModuleID, s.SubordinateID });
            modelBuilder.Entity<Proposal>()
                .HasKey(p => new { p.ModuleID, p.UserID });
            modelBuilder.Entity<UserRole>()
                .HasKey(s => new { s.UserID, s.RoleID });
            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(ua => ua.Roles)
                .HasForeignKey(u => u.UserID);
            modelBuilder.Entity<UserRole>()
                .HasOne(r => r.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(r => r.RoleID);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ModuleUser> ModuleUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Proposal> Proposal { get; set; }
        public DbSet<SubordinateModule> SubordinateModules { get; set; }
    }
}
