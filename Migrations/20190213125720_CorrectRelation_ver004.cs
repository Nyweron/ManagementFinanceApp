using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementFinanceApp.Migrations
{
    public partial class CorrectRelation_ver004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CategoryIncomes_CategoryIncomeId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategoryIncomeId",
                table: "Expenses",
                newName: "CategorySavingId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategoryIncomeId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CategorySavings_CategorySavingId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategorySavingId",
                table: "Expenses",
                newName: "CategoryIncomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategorySavingId",
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
    }
}
