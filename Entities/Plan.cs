using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  /*Plan-plany */
  public class Plan
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public bool IsDone { get; set; }

    [Required]
    public bool IsAddedToQueue { get; set; }

    [Required]
    public int PlanType { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string Comment { get; set; }

  }
}