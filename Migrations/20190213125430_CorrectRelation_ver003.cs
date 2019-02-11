using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementFinanceApp.Migrations
{
    public partial class CorrectRelation_ver003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CategorySavings_CategorySavingId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategorySavingId",
                table: "Expenses",
                newName: "CategoryExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategorySavingId",
                table: "Expenses",
                newName: "IX_Expenses_CategoryExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_CategoryExpenses_CategoryExpenseId",
                table: "Expenses",
                column: "CategoryExpenseId",
                principalTable: "CategoryExpenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CategoryExpenses_CategoryExpenseId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategoryExpenseId",
                table: "Expenses",
                newName: "CategorySavingId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategoryExpenseId",
                table: "Expenses",
                newName: "IX_Expenses_CategorySavingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_CategorySavings_CategorySavingId",
                table: "Expenses",
                column: "CategorySavingId",
                principalTable: "CategorySavings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
