using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEntries_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskEntries_TaskTypeId",
                table: "TaskEntries",
                column: "TaskTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskEntries");

            migrationBuilder.DropTable(
                name: "TaskTypes");
        }
    }
}
