using System;

namespace ManagementFinanceApp.Models
{
  /*Saving - Oszczędności */
  public class Saving
  {
    public int Id { get; set; }
    public double HowMuch { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public int SavingType { get; set; }
    public int CategorySavingId { get; set; }
  }
}