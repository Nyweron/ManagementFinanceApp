using ManagementFinanceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Data
{
  public class ManagementFinanaceAppDbContext : DbContext
  {
    public ManagementFinanaceAppDbContext(DbContextOptions<ManagementFinanaceAppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
  }
}