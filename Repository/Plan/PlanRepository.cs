using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.Plan
{
  public class PlanRepository : Repository<Entities.Plan>, IPlanRepository
  {
    public PlanRepository(ManagementFinanceAppDbContext context) : base(context)
    {

    }
  }
}