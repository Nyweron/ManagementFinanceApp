using System;

namespace ManagementFinanceApp.Models
{
  /* Przychód - Income */
  public class Income
  {
    public int Id { get; set; }
    public double HowMuch { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    /*ZlecenieStale - StandingOrder  */
    public bool StandingOrder { get; set; }
    public string Attachment { get; set; }
  }
}