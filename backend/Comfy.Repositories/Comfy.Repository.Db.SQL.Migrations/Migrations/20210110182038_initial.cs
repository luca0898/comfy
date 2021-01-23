using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comfy.Repository.Db.SQL.Migrations.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ComfyDb");

            migrationBuilder.CreateTable(
                name: "Schedule",
                schema: "ComfyDb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 1, 10, 15, 20, 38, 760, DateTimeKind.Local).AddTicks(9294)),
                    ProcedurePerformed = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedule",
                schema: "ComfyDb");
        }
    }
}
