using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /* Kategoria Grup - CategoryGroup */
  public class CategoryGroup
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public int CategoryType { get; set; } = -1;

    public List<CategorySaving> CategorySavings { get; set; }
    public List<CategoryExpense> CategoryExpense { get; set; }
    public List<CategoryIncome> CategoryIncomes { get; set; }
  }
}