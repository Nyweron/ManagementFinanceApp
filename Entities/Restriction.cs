using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /* Restriction - Ograniczenia */
  public class Restriction
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public double MaxMonth { get; set; }

    [Required]
    public double MaxYear { get; set; }

    [Required]
    public int RestrictionYear { get; set; }

    [Required]
    public System.DateTime Date { get; set; }

    [Required]
    public CategoryExpense CategoryExpense { get; set; }

    [Required]
    public int CategoryExpenseId { get; set; }

  }
}