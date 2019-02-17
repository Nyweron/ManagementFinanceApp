namespace ManagementFinanceApp.Models
{
  /* Kategoria Przychodu - CategoryIncome */
  public class CategoryIncome
  {
    public int Id { get; set; }
    public string Comment { get; set; }
    public bool IsDeleted { get; set; }
    public int Weight { get; set; }
    public int CategoryGroupId { get; set; }
  }
}