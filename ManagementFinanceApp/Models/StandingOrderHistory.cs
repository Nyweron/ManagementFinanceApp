using System;

namespace ManagementFinanceApp.Models
{
  /* Standing orders history- historia zlecen stałych*/
  public class StandingOrderHistory
  {
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public double Amount { get; set; }
    public int TypeStandingOrder { get; set; }
    public DateTime Date { get; set; }
    public int SavingId { get; set; }
    public int UserId { get; set; }
    public int StandingOrderId { get; set; }
    public int FrequencyId { get; set; }
  }
}