using System;

namespace ManagementFinanceApp.Models
{
  /* Restriction - Ograniczenia */
  public class Restriction
  {
    public int Id { get; set; }
    public double MaxMonth { get; set; }
    public double MaxYear { get; set; }
    public int RestrictionYear { get; set; }
    public DateTime Date { get; set; }
    public int CategoryExpenseId { get; set; }

  }
}