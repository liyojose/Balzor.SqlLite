using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApp.Server.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Firstname", "LastUpdated", "Lastname" },
                values: new object[] { 1001, "liyojose@gmail.com", "Liyo", new DateTime(2022, 9, 7, 16, 27, 57, 465, DateTimeKind.Local).AddTicks(7521), "Jose" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Firstname", "LastUpdated", "Lastname" },
                values: new object[] { 1002, "roger@gmail.com", "Roger", new DateTime(2022, 9, 7, 16, 27, 57, 465, DateTimeKind.Local).AddTicks(7533), "Wan" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Firstname", "LastUpdated", "Lastname" },
                values: new object[] { 1003, "pedro@gmail.com", "Pedro", new DateTime(2022, 9, 7, 16, 27, 57, 465, DateTimeKind.Local).AddTicks(7534), "hugo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
