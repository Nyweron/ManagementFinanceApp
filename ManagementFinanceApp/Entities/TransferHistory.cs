using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /* TransferHistory - Historia przelewow*/
  public class TransferHistory
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
    public CategorySaving CategorySaving { get; set; }

    [Required]
    public int CategorySavingId { get; set; }

    [Required]
    public int CategorySavingIdFrom { get; set; }

    [Required]
    public int CategorySavingIdTo { get; set; }

  }
}