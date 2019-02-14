using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class Investment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Bank = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PeriodDeposit = table.Column<int>(nullable: false),
                    UnitDeposit = table.Column<int>(nullable: false),
                    InterestRateInAllPerdiodInvestment = table.Column<bool>(nullable: false),
                    InterestRateOnScaleOfYear = table.Column<double>(nullable: false),
                    Capitalization = table.Column<int>(nullable: false),
                    MinAmount = table.Column<double>(nullable: false),
                    MaxAmount = table.Column<double>(nullable: false),
                    RequiredPersonalAccountInCurrentBank = table.Column<bool>(nullable: false),
                    PossibilityEarlyTerminationInvestment = table.Column<bool>(nullable: false),
                    ConditionEarlyTerminationInvestment = table.Column<string>(nullable: true),
                    RestInformation = table.Column<string>(nullable: true),
                    AddedScheduleFromUser = table.Column<bool>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    PeriodInvestment = table.Column<int>(nullable: false),
                    UnitInvestment = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false),
                    InvestmentScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investments_InvestmentSchedule_InvestmentScheduleId",
                        column: x => x.InvestmentScheduleId,
                        principalTable: "InvestmentSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investments_InvestmentScheduleId",
                table: "Investments",
                column: "InvestmentScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investments");

            migrationBuilder.DropTable(
                name: "InvestmentSchedule");
        }
    }
}
