using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class Restriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_Restrictions_CategoryExpenseId",
                table: "Restrictions",
                column: "CategoryExpenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restrictions");
        }
    }
}
