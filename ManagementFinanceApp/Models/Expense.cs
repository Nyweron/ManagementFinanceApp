using System;

namespace ManagementFinanceApp.Models
{
  /* Wydatki - Expense */
  public class Expense
  {
    public int Id { get; set; }
    public double HowMuch { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public string Attachment { get; set; }
    /*ZlecenieStale - StandingOrder  */
    public bool StandingOrder { get; set; }
    public int UserId { get; set; }
    public int CategorySavingId { get; set; }
    public string CategorySavingDescription { get; set; }
    public int CategoryExpenseId { get; set; }
    public string CategoryExpenseDescription { get; set; }
  }
}