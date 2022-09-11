using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Firstname = table.Column<string>(type: "TEXT", nullable: false),
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Firstname", "Lastname" },
                values: new object[] { 1001, "liyojose@gmail.com", "Liyo", "Jose" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Firstname", "Lastname" },
                values: new object[] { 1002, "roger@gmail.com", "Roger", "Wan" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Firstname", "Lastname" },
                values: new object[] { 1003, "pedro@gmail.com", "Pedro", "hugo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
