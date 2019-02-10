using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class TransferHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HowMuch = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CategorySavingId = table.Column<int>(nullable: false),
                    CategorySavingIdFrom = table.Column<int>(nullable: false),
                    CategorySavingIdTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferHistories_CategorySavings_CategorySavingId",
                        column: x => x.CategorySavingId,
                        principalTable: "CategorySavings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferHistories_CategorySavingId",
                table: "TransferHistories",
                column: "CategorySavingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferHistories");
        }
    }
}
