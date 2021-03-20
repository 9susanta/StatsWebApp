using Microsoft.EntityFrameworkCore.Migrations;

namespace StatsWebApp.Migrations
{
    public partial class AddedAppDataEntity01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppData_subCategoryId",
                table: "AppData",
                column: "subCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppData_SubCategories_subCategoryId",
                table: "AppData",
                column: "subCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppData_SubCategories_subCategoryId",
                table: "AppData");

            migrationBuilder.DropIndex(
                name: "IX_AppData_subCategoryId",
                table: "AppData");
        }
    }
}
