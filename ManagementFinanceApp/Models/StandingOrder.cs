using System;
using System.Collections.Generic;

namespace ManagementFinanceApp.Models
{
  /* Standing orders - Zlecenia stałe*/
  public class StandingOrder
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
    public int Frequency { get; set; }
    public int TypeStandingOrder { get; set; }
    public DateTime Date { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public int SavingId { get; set; }
    public int FrequencyId { get; set; }
  }
}