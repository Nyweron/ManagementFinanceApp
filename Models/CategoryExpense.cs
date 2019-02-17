namespace ManagementFinanceApp.Models
{
  /* KategorieWydatkow - CategoryExpense */
  public class CategoryExpense
  {
    public int Id { get; set; }

    public string Description { get; set; }

    public bool IsDeleted { get; set; }

    public int Weight { get; set; }
    public int CategoryGroupId { get; set; }
  }
}