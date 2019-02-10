using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class Entities_part3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentSchedules",
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
                    table.PrimaryKey("PK_InvestmentSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    IsAddedToQueue = table.Column<bool>(nullable: false),
                    PlanType = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavingStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    State = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingStates", x => x.Id);
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
                        name: "FK_Investments_InvestmentSchedules_InvestmentScheduleId",
                        column: x => x.InvestmentScheduleId,
                        principalTable: "InvestmentSchedules",
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
                name: "Plans");

            migrationBuilder.DropTable(
                name: "SavingStates");

            migrationBuilder.DropTable(
                name: "InvestmentSchedules");
        }
    }
}
