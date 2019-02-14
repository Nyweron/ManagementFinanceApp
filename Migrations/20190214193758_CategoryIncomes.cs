using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementFinanceApp.Migrations
{
    public partial class CategoryIncomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryIncome_CategoryGroups_CategoryGroupId",
                table: "CategoryIncome");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryIncome",
                table: "CategoryIncome");

            migrationBuilder.RenameTable(
                name: "CategoryIncome",
                newName: "CategoryIncomes");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryIncome_CategoryGroupId",
                table: "CategoryIncomes",
                newName: "IX_CategoryIncomes_CategoryGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryIncomes",
                table: "CategoryIncomes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryIncomes_CategoryGroups_CategoryGroupId",
                table: "CategoryIncomes",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryIncomes_CategoryGroups_CategoryGroupId",
                table: "CategoryIncomes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryIncomes",
                table: "CategoryIncomes");

            migrationBuilder.RenameTable(
                name: "CategoryIncomes",
                newName: "CategoryIncome");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryIncomes_CategoryGroupId",
                table: "CategoryIncome",
                newName: "IX_CategoryIncome_CategoryGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryIncome",
                table: "CategoryIncome",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryIncome_CategoryGroups_CategoryGroupId",
                table: "CategoryIncome",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
