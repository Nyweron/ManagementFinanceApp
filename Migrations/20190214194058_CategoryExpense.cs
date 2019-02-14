using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementFinanceApp.Migrations
{
    public partial class CategoryExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryExpense_CategoryGroups_CategoryGroupId",
                table: "CategoryExpense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryExpense",
                table: "CategoryExpense");

            migrationBuilder.RenameTable(
                name: "CategoryExpense",
                newName: "CategoryExpenses");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryExpense_CategoryGroupId",
                table: "CategoryExpenses",
                newName: "IX_CategoryExpenses_CategoryGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryExpenses",
                table: "CategoryExpenses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryExpenses_CategoryGroups_CategoryGroupId",
                table: "CategoryExpenses",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryExpenses_CategoryGroups_CategoryGroupId",
                table: "CategoryExpenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryExpenses",
                table: "CategoryExpenses");

            migrationBuilder.RenameTable(
                name: "CategoryExpenses",
                newName: "CategoryExpense");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryExpenses_CategoryGroupId",
                table: "CategoryExpense",
                newName: "IX_CategoryExpense_CategoryGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryExpense",
                table: "CategoryExpense",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryExpense_CategoryGroups_CategoryGroupId",
                table: "CategoryExpense",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
