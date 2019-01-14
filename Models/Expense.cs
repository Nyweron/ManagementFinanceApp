using System;

namespace ManagementFinanceApp.Models
{
  public class Expense
  {
    public int Id { get; set; }
    public double HowMuch { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public bool StandingOrder { get; set; }
    public string Attachment { get; set; }
  }
}