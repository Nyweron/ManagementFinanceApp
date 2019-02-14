using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class Income : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HowMuch = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    StandingOrder = table.Column<bool>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CategoryIncomeId = table.Column<int>(nullable: false),
                    CategorySavingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_CategoryIncomes_CategoryIncomeId",
                        column: x => x.CategoryIncomeId,
                        principalTable: "CategoryIncomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incomes_CategorySavings_CategorySavingId",
                        column: x => x.CategorySavingId,
                        principalTable: "CategorySavings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incomes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incomes");
        }
    }
}
