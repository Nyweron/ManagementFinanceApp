namespace ManagementFinanceApp.Models
{
  /* CategorySaving - kategoria oszczednosci*/
  public class CategorySaving
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool CanPay { get; set; }
    public bool Debt { get; set; }
    public int Weight { get; set; }
    public int CategoryGroupId { get; set; }
  }
}