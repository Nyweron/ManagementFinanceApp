namespace ManagementFinanceApp.Models
{
  /* Kategoria Grup - CategoryGroup */
  public class CategoryGroup
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public int CategoryType { get; set; }
  }
}