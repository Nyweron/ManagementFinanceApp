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
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Restriction> Restrictions { get; set; }
    public DbSet<Frequency> Frequencies { get; set; }
    public DbSet<Saving> Savings { get; set; }
    public DbSet<StandingOrder> StandingOrders { get; set; }
    public DbSet<StandingOrderHistory> StandingOrderHistories { get; set; }
    public DbSet<SavingState> SavingStates { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Investment> Investments { get; set; }
    public DbSet<InvestmentSchedule> InvestmentSchedules { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    //https://codepedia.info/aspnet-core-jwt-refresh-token-authentication
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.UseIdentityColumns();

      modelBuilder.Entity<Expense>()
        .Property(proper => proper.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<User>()
        .Property(u => u.Email)
        .IsRequired();

      modelBuilder.Entity<Role>()
        .Property(u => u.Name)
        .IsRequired();
    }
  }
}