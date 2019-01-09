using ManagementFinanceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Data
{
  public class ManagementFinanceAppDbContext : DbContext
  {
    public ManagementFinanceAppDbContext(DbContextOptions<ManagementFinanceAppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
  }
}