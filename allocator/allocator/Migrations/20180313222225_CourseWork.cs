using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace allocator.Migrations
{
    public partial class CourseWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseWork",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CourseName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseWork", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CourseWork_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    RoleName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserId",
                table: "Role",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseWork");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Users",
                nullable: true);
        }
    }
}
