using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementFinanceApp.Migrations
{
    public partial class InvestmentSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_InvestmentSchedule_InvestmentScheduleId",
                table: "Investments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestmentSchedule",
                table: "InvestmentSchedule");

            migrationBuilder.RenameTable(
                name: "InvestmentSchedule",
                newName: "InvestmentSchedules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestmentSchedules",
                table: "InvestmentSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_InvestmentSchedules_InvestmentScheduleId",
                table: "Investments",
                column: "InvestmentScheduleId",
                principalTable: "InvestmentSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_InvestmentSchedules_InvestmentScheduleId",
                table: "Investments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestmentSchedules",
                table: "InvestmentSchedules");

            migrationBuilder.RenameTable(
                name: "InvestmentSchedules",
                newName: "InvestmentSchedule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestmentSchedule",
                table: "InvestmentSchedule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_InvestmentSchedule_InvestmentScheduleId",
                table: "Investments",
                column: "InvestmentScheduleId",
                principalTable: "InvestmentSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
