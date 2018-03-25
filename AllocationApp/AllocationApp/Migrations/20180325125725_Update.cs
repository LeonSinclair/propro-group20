using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AllocationApp.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckboxViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Checked = table.Column<bool>(nullable: false),
                    ModuleName = table.Column<string>(nullable: true),
                    SubordinatesID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckboxViewModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CheckboxViewModel_Subordinates_SubordinatesID",
                        column: x => x.SubordinatesID,
                        principalTable: "Subordinates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckboxViewModel_SubordinatesID",
                table: "CheckboxViewModel",
                column: "SubordinatesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckboxViewModel");
        }
    }
}
