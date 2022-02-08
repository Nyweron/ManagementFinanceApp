using System;

namespace ManagementFinanceApp.Models.List
{
  /*Saving - Oszczędności */
  public class SavingList
  {
    public int Id { get; set; }
    public double HowMuch { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public int SavingType { get; set; }
    public int CategorySavingId { get; set; }
    public string CategorySavingDescription { get; set; }
  }
}
