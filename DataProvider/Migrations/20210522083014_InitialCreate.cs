using Microsoft.EntityFrameworkCore.Migrations;

namespace DataProvider.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Speaches",
                columns: table => new
                {
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Party = table.Column<string>(type: "varchar(50)", nullable: false)
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Speaches");
        }
    }
}
