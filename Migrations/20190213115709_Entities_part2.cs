using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class Entities_part2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Frequencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Savings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HowMuch = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    SavingType = table.Column<int>(nullable: false),
                    CategorySavingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Savings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Savings_CategorySavings_CategorySavingId",
                        column: x => x.CategorySavingId,
                        principalTable: "CategorySavings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Frequency = table.Column<int>(nullable: false),
                    TypeStandingOrder = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    SavingId = table.Column<int>(nullable: false),
                    FrequencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandingOrders_Frequencies_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandingOrders_Savings_SavingId",
                        column: x => x.SavingId,
                        principalTable: "Savings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandingOrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Frequency = table.Column<int>(nullable: false),
                    TypeStandingOrder = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    SavingId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    StandingOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandingOrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandingOrderHistories_Savings_SavingId",
                        column: x => x.SavingId,
                        principalTable: "Savings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandingOrderHistories_StandingOrders_StandingOrderId",
                        column: x => x.StandingOrderId,
                        principalTable: "StandingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandingOrderHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Savings_CategorySavingId",
                table: "Savings",
                column: "CategorySavingId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingOrderHistories_SavingId",
                table: "StandingOrderHistories",
                column: "SavingId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingOrderHistories_StandingOrderId",
                table: "StandingOrderHistories",
                column: "StandingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingOrderHistories_UserId",
                table: "StandingOrderHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingOrders_FrequencyId",
                table: "StandingOrders",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingOrders_SavingId",
                table: "StandingOrders",
                column: "SavingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StandingOrderHistories");

            migrationBuilder.DropTable(
                name: "StandingOrders");

            migrationBuilder.DropTable(
                name: "Frequencies");

            migrationBuilder.DropTable(
                name: "Savings");
        }
    }
}
