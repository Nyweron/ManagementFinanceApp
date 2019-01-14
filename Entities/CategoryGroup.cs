using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
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
  }
}