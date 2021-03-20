using Microsoft.EntityFrameworkCore.Migrations;

namespace StatsWebApp.Migrations
{
    public partial class AddedAppDataEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    metaData = table.Column<string>(type: "TEXT", nullable: true),
                    excelPath = table.Column<string>(type: "TEXT", nullable: true),
                    jsonData = table.Column<string>(type: "TEXT", nullable: true),
                    subCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppData");
        }
    }
}
