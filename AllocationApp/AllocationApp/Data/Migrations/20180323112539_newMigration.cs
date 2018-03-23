using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AllocationApp.Data.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "CourseUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CourseUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "CourseUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CourseUsers",
                nullable: true);
        }
    }
}
