using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryIncomeId",
                table: "Incomes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategorySavingId",
                table: "Incomes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Incomes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    CategoryGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryExpenses_CategoryGroups_CategoryGroupId",
                        column: x => x.CategoryGroupId,
                        principalTable: "CategoryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HowMuch = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true),
                    StandingOrder = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CategoryExpenseId = table.Column<int>(nullable: false),
                    CategorySavingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_CategoryExpenses_CategoryExpenseId",
                        column: x => x.CategoryExpenseId,
                        principalTable: "CategoryExpenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_CategorySavings_CategorySavingId",
                        column: x => x.CategorySavingId,
                        principalTable: "CategorySavings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restrictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaxMonth = table.Column<double>(nullable: false),
                    MaxYear = table.Column<double>(nullable: false),
                    RestrictionYear = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    CategoryExpenseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restrictions_CategoryExpenses_CategoryExpenseId",
                        column: x => x.CategoryExpenseId,
                        principalTable: "CategoryExpenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_CategoryIncomeId",
                table: "Incomes",
                column: "CategoryIncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_CategorySavingId",
                table: "Incomes",
                column: "CategorySavingId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryExpenses_CategoryGroupId",
                table: "CategoryExpenses",
                column: "CategoryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryExpenseId",
                table: "Expenses",
                column: "CategoryExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategorySavingId",
                table: "Expenses",
                column: "CategorySavingId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Restrictions_CategoryExpenseId",
                table: "Restrictions",
                column: "CategoryExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_CategoryIncomes_CategoryIncomeId",
                table: "Incomes",
                column: "CategoryIncomeId",
                principalTable: "CategoryIncomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_CategorySavings_CategorySavingId",
                table: "Incomes",
                column: "CategorySavingId",
                principalTable: "CategorySavings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_UserId",
                table: "Incomes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_CategoryIncomes_CategoryIncomeId",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_CategorySavings_CategorySavingId",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_UserId",
                table: "Incomes");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropTable(
                name: "CategoryExpenses");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_CategoryIncomeId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_CategorySavingId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "CategoryIncomeId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "CategorySavingId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Incomes");
        }
    }
}
