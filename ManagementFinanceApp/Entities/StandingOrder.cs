using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /* Standing orders - Zlecenia stałe*/
  public class StandingOrder
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public int Frequency { get; set; }

    [Required]
    public int TypeStandingOrder { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public bool IsActive { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public Saving Savings { get; set; }

    [Required]
    public int SavingId { get; set; }

    [Required]
    public Frequency Frequencies { get; set; }

    [Required]
    public int FrequencyId { get; set; }

  }
}