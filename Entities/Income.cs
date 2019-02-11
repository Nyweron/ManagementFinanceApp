using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /* Przychód - Income */
  public class Income
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public double HowMuch { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string Comment { get; set; }

    [Required]
    public bool StandingOrder { get; set; }
    public string Attachment { get; set; }

    [Required]
    public User User { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public CategoryIncome CategoryIncome { get; set; }

    [Required]
    public int CategoryIncomeId { get; set; }

    [Required]
    public CategorySaving CategorySaving { get; set; }

    [Required]
    public int CategorySavingId { get; set; }

  }
}