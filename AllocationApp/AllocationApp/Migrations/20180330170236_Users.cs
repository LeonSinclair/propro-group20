using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AllocationApp.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAddress",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortCode",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAddress",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SortCode",
                table: "Users");
        }
    }
}
