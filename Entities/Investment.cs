using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /*investment - lokaty */
  public class Investment
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public int PeriodInvestment { get; set; }

    [Required]
    public int UnitInvestment { get; set; }

    [Required]
    public int IsActive { get; set; }

    [Required]
    public InvestmentSchedule InvestmentSchedule { get; set; }

    [Required]
    public int InvestmentScheduleId { get; set; }
  }
}