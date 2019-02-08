using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
  public partial class CategorySaving : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "CategorySavings",
        columns : table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            Description = table.Column<string>(nullable: false),
            IsDeleted = table.Column<bool>(nullable: false),
            CanPay = table.Column<bool>(nullable: false),
            Debt = table.Column<bool>(nullable: false),
            Weight = table.Column<int>(nullable: false),
            CategoryGroupId = table.Column<int>(nullable: false)
        },
        constraints : table =>
        {
          table.PrimaryKey("PK_CategorySavings", x => x.Id);
          table.ForeignKey(
            name: "FK_CategorySavings_CategoryGroups_CategoryGroupId",
            column : x => x.CategoryGroupId,
            principalTable: "CategoryGroups",
            principalColumn: "Id",
            onDelete : ReferentialAction.Cascade);
        });

      migrationBuilder.CreateIndex(
        name: "IX_CategorySavings_CategoryGroupId",
        table: "CategorySavings",
        column: "CategoryGroupId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
        name: "CategorySavings");
    }
  }
}