﻿// <auto-generated />
using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace AllocationApp.Migrations
{
    [DbContext(typeof(AllocationContext))]
    partial class AllocationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("AllocationApp.Models.CheckboxViewModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Checked");

                    b.Property<string>("ModuleName");

                    b.Property<int?>("SubordinatesID");

                    b.HasKey("ID");

                    b.HasIndex("SubordinatesID");

                    b.ToTable("CheckboxViewModel");
                });

            modelBuilder.Entity("AllocationApp.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("CourseID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("AllocationApp.Models.CourseUser", b =>
                {
                    b.Property<int>("CourseID");

                    b.Property<int>("UserID");

                    b.HasKey("CourseID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("CourseUsers");
                });

            modelBuilder.Entity("AllocationApp.Models.Hour", b =>
                {
                    b.Property<int>("HourID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseID");

                    b.Property<DateTime>("Date");

                    b.Property<string>("HourType");

                    b.Property<bool>("IsApproved");

                    b.Property<int>("PayRate");

                    b.Property<int>("UserID");

                    b.HasKey("HourID");

                    b.HasIndex("UserID");

                    b.ToTable("Hours");
                });

            modelBuilder.Entity("AllocationApp.Models.Module", b =>
                {
                    b.Property<int>("ModuleID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ModuleID");

                    b.ToTable("Module");
                });

            modelBuilder.Entity("AllocationApp.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoleType");

                    b.Property<int>("UserID");

                    b.HasKey("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AllocationApp.Models.Skill", b =>
                {
                    b.Property<int>("SkillID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CourseID");

                    b.Property<string>("Name");

                    b.Property<int?>("UserID");

                    b.HasKey("SkillID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UserID");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("AllocationApp.Models.SubordinateModule", b =>
                {
                    b.Property<int>("ModuleID");

                    b.Property<int>("SubordinateID");

                    b.Property<int?>("SubordinatesID");

                    b.HasKey("ModuleID", "SubordinateID");

                    b.HasIndex("SubordinatesID");

                    b.ToTable("SubordinateModules");
                });

            modelBuilder.Entity("AllocationApp.Models.Subordinates", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BankAddress");

                    b.Property<string>("BankName");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("IBAN");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Occupation")
                        .IsRequired();

                    b.Property<int>("SortCode");

                    b.HasKey("ID");

                    b.ToTable("Subordinates");
                });

            modelBuilder.Entity("AllocationApp.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AllocationApp.Models.CheckboxViewModel", b =>
                {
                    b.HasOne("AllocationApp.Models.Subordinates")
                        .WithMany("Modules")
                        .HasForeignKey("SubordinatesID");
                });

            modelBuilder.Entity("AllocationApp.Models.CourseUser", b =>
                {
                    b.HasOne("AllocationApp.Models.Course", "Course")
                        .WithMany("Users")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AllocationApp.Models.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllocationApp.Models.Hour", b =>
                {
                    b.HasOne("AllocationApp.Models.User")
                        .WithMany("WorkHours")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllocationApp.Models.Role", b =>
                {
                    b.HasOne("AllocationApp.Models.User")
                        .WithMany("RoleType")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllocationApp.Models.Skill", b =>
                {
                    b.HasOne("AllocationApp.Models.Course")
                        .WithMany("SkillRequirements")
                        .HasForeignKey("CourseID");

                    b.HasOne("AllocationApp.Models.User")
                        .WithMany("Skills")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("AllocationApp.Models.SubordinateModule", b =>
                {
                    b.HasOne("AllocationApp.Models.Module", "Module")
                        .WithMany("SubordinateModules")
                        .HasForeignKey("ModuleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AllocationApp.Models.Subordinates", "Subordinates")
                        .WithMany("SubordinateModules")
                        .HasForeignKey("SubordinatesID");
                });
#pragma warning restore 612, 618
        }
    }
}
