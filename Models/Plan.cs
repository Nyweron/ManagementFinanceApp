using System;

namespace ManagementFinanceApp.Models
{
  public class Plan
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public double Amount { get; set; }

    public int CategoryId { get; set; }

    public bool IsDone { get; set; }

    public bool IsAddedToQueue { get; set; }

    public int PlanType { get; set; }

    public DateTime Date { get; set; }

    public string Comment { get; set; }
  }
}