using System;

namespace ManagementFinanceApp.Models.List
{
  /* Przychód - Income */
  public class IncomeList
  {
    public int Id { get; set; }
    public double HowMuch { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public string Attachment { get; set; }
    /*ZlecenieStale - StandingOrder  */
    public bool StandingOrder { get; set; }
    public int UserId { get; set; }
    public string UserDescription { get; set; }
    public int CategorySavingId { get; set; }
    public string CategorySavingDescription { get; set; }
    public int CategoryIncomeId { get; set; }
    public string CategoryIncomeDescription { get; set; }
  }
}