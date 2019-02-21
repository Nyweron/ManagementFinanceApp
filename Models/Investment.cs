using System;

namespace ManagementFinanceApp.Models
{
  /*investment - lokaty */
  public class Investment
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
    public DateTime StartDate { get; set; }
    public int PeriodInvestment { get; set; }
    public int UnitInvestment { get; set; }
    public int IsActive { get; set; }

    public int InvestmentScheduleId { get; set; }
  }
}