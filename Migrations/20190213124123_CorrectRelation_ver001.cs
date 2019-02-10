using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementFinanceApp.Migrations
{
    public partial class CorrectRelation_ver001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Frequency",
                table: "StandingOrderHistories",
                newName: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingOrderHistories_FrequencyId",
                table: "StandingOrderHistories",
                column: "FrequencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_StandingOrderHistories_Frequencies_FrequencyId",
                table: "StandingOrderHistories",
                column: "FrequencyId",
                principalTable: "Frequencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StandingOrderHistories_Frequencies_FrequencyId",
                table: "StandingOrderHistories");

            migrationBuilder.DropIndex(
                name: "IX_StandingOrderHistories_FrequencyId",
                table: "StandingOrderHistories");

            migrationBuilder.RenameColumn(
                name: "FrequencyId",
                table: "StandingOrderHistories",
                newName: "Frequency");
        }
    }
}
