using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementFinanceApp.Migrations
{
    public partial class CorrectRelation_ver002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CategoryExpenses_CategoryExpenseId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategoryExpenseId",
                table: "Expenses",
                newName: "CategoryIncomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategoryExpenseId",
                table: "Expenses",
                newName: "IX_Expenses_CategoryIncomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_CategoryIncomes_CategoryIncomeId",
                table: "Expenses",
                column: "CategoryIncomeId",
                principalTable: "CategoryIncomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CategoryIncomes_CategoryIncomeId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategoryIncomeId",
                table: "Expenses",
                newName: "CategoryExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategoryIncomeId",
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
    }
}
