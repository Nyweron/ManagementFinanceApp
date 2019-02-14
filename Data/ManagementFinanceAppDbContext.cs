using ManagementFinanceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Data
{
  public class ManagementFinanceAppDbContext : DbContext
  {
    public ManagementFinanceAppDbContext(DbContextOptions<ManagementFinanceAppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<CategoryGroup> CategoryGroups { get; set; }
    public DbSet<CategorySaving> CategorySavings { get; set; }
    public DbSet<CategoryIncome> CategoryIncomes { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<TransferHistory> TransferHistories { get; set; }
    public DbSet<CategoryExpense> CategoryExpenses { get; set; }
    // public DbSet<Expense> Expenses { get; set; }
    // public DbSet<Restriction> Restrictions { get; set; }
    // public DbSet<Frequency> Frequencies { get; set; }
    // public DbSet<Saving> Savings { get; set; }
    // public DbSet<StandingOrder> StandingOrders { get; set; }
    // public DbSet<StandingOrderHistory> StandingOrderHistories { get; set; }
    // public DbSet<SavingState> SavingStates { get; set; }
    // public DbSet<Plan> Plans { get; set; }
    // public DbSet<Investment> Investments { get; set; }
    // public DbSet<InvestmentSchedule> InvestmentSchedules { get; set; }
  }
}