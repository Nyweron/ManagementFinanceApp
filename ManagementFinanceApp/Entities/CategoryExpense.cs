using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /* KategorieWydatkow - CategoryExpense */
  public class CategoryExpense
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public int Weight { get; set; }

    [Required]
    public CategoryGroup CategoryGroup { get; set; }

    [Required]
    public int CategoryGroupId { get; set; }
  }
}