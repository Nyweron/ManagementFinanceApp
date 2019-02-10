using ManagementFinanceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Data
{
  public class ManagementFinanceAppDbContext : DbContext
  {
    public ManagementFinanceAppDbContext(DbContextOptions<ManagementFinanceAppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<CategoryGroup> CategoryGroups { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<CategorySaving> CategorySavings { get; set; }
    public DbSet<TransferHistory> TransferHistories { get; set; }

  }
}