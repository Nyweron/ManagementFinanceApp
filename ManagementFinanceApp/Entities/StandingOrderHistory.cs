using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /* Standing orders history- historia zlecen stałych*/
  public class StandingOrderHistory
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public int TypeStandingOrder { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public Saving Savings { get; set; }

    [Required]
    public int SavingId { get; set; }

    [Required]
    public User User { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public StandingOrder StandingOrder { get; set; }

    [Required]
    public int StandingOrderId { get; set; }

    [Required]
    public Frequency Frequency { get; set; }

    [Required]
    public int FrequencyId { get; set; }

  }
}