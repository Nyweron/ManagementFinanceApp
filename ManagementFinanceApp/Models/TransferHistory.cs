using System;

namespace ManagementFinanceApp.Models
{
  /* TransferHistory - Historia przelewow*/
  public class TransferHistory
  {
    public int Id { get; set; }
    public double HowMuch { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public int CategorySavingId { get; set; }
    public int CategorySavingIdFrom { get; set; }
    public int CategorySavingIdTo { get; set; }
  }
}