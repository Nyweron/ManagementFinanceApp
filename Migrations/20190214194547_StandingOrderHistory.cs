using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class StandingOrderHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StandingOrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TypeStandingOrder = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    SavingId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    StandingOrderId = table.Column<int>(nullable: false),
                    FrequencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandingOrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandingOrderHistories_Frequencies_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_StandingOrderHistories_FrequencyId",
                table: "StandingOrderHistories",
                column: "FrequencyId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StandingOrderHistories");
        }
    }
}
