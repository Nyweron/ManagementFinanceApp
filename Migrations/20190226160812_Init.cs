using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CategoryType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGroups", x => x.Id);
                });

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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 40, nullable: true),
                    Nick = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    CategoryGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryExpenses_CategoryGroups_CategoryGroupId",
                        column: x => x.CategoryGroupId,
                        principalTable: "CategoryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    CategoryGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryIncomes_CategoryGroups_CategoryGroupId",
                        column: x => x.CategoryGroupId,
                        principalTable: "CategoryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorySavings",
                columns: table => new
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
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySavings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySavings_CategoryGroups_CategoryGroupId",
                        column: x => x.CategoryGroupId,
                        principalTable: "CategoryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HowMuch = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true),
                    StandingOrder = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CategorySavingId = table.Column<int>(nullable: false),
                    CategoryExpenseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_CategoryExpenses_CategoryExpenseId",
                        column: x => x.CategoryExpenseId,
                        principalTable: "CategoryExpenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_CategorySavings_CategorySavingId",
                        column: x => x.CategorySavingId,
                        principalTable: "CategorySavings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CategoryExpenses_CategoryGroupId",
                table: "CategoryExpenses",
                column: "CategoryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryIncomes_CategoryGroupId",
                table: "CategoryIncomes",
                column: "CategoryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySavings_CategoryGroupId",
                table: "CategorySavings",
                column: "CategoryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryExpenseId",
                table: "Expenses",
                column: "CategoryExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategorySavingId",
                table: "Expenses",
                column: "CategorySavingId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Investments_InvestmentScheduleId",
                table: "Investments",
                column: "InvestmentScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Restrictions_CategoryExpenseId",
                table: "Restrictions",
                column: "CategoryExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Savings_CategorySavingId",
                table: "Savings",
                column: "CategorySavingId");

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

            migrationBuilder.CreateIndex(
                name: "IX_StandingOrders_FrequencyId",
                table: "StandingOrders",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingOrders_SavingId",
                table: "StandingOrders",
                column: "SavingId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferHistories_CategorySavingId",
                table: "TransferHistories",
                column: "CategorySavingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Investments");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropTable(
                name: "SavingStates");

            migrationBuilder.DropTable(
                name: "StandingOrderHistories");

            migrationBuilder.DropTable(
                name: "TransferHistories");

            migrationBuilder.DropTable(
                name: "CategoryIncomes");

            migrationBuilder.DropTable(
                name: "InvestmentSchedules");

            migrationBuilder.DropTable(
                name: "CategoryExpenses");

            migrationBuilder.DropTable(
                name: "StandingOrders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Frequencies");

            migrationBuilder.DropTable(
                name: "Savings");

            migrationBuilder.DropTable(
                name: "CategorySavings");

            migrationBuilder.DropTable(
                name: "CategoryGroups");
        }
    }
}
