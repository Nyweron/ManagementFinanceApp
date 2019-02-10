using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementFinanceApp.Migrations
{
    public partial class CategorySaving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySaving_CategoryGroups_CategoryGroupId",
                table: "CategorySaving");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorySaving",
                table: "CategorySaving");

            migrationBuilder.RenameTable(
                name: "CategorySaving",
                newName: "CategorySavings");

            migrationBuilder.RenameIndex(
                name: "IX_CategorySaving_CategoryGroupId",
                table: "CategorySavings",
                newName: "IX_CategorySavings_CategoryGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorySavings",
                table: "CategorySavings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySavings_CategoryGroups_CategoryGroupId",
                table: "CategorySavings",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySavings_CategoryGroups_CategoryGroupId",
                table: "CategorySavings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorySavings",
                table: "CategorySavings");

            migrationBuilder.RenameTable(
                name: "CategorySavings",
                newName: "CategorySaving");

            migrationBuilder.RenameIndex(
                name: "IX_CategorySavings_CategoryGroupId",
                table: "CategorySaving",
                newName: "IX_CategorySaving_CategoryGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorySaving",
                table: "CategorySaving",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySaving_CategoryGroups_CategoryGroupId",
                table: "CategorySaving",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
