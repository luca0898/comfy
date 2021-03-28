using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comfy.Repository.Db.SQL.Migrations.Migrations
{
    public partial class MappingUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "ComfyDb",
                table: "Schedule",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 28, 0, 31, 36, 711, DateTimeKind.Local).AddTicks(6565),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 28, 0, 0, 15, 849, DateTimeKind.Local).AddTicks(2649));

            migrationBuilder.CreateTable(
                name: "User",
                schema: "ComfyDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityReference = table.Column<string>(type: "VARCHAR(36)", maxLength: 36, nullable: false),
                    GivenName = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    SurName = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(32)", maxLength: 32, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "ComfyDb");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "ComfyDb",
                table: "Schedule",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 28, 0, 0, 15, 849, DateTimeKind.Local).AddTicks(2649),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 28, 0, 31, 36, 711, DateTimeKind.Local).AddTicks(6565));
        }
    }
}
