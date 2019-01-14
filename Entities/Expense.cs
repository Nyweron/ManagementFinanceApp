using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  public class Expense
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    //[Required]
    //public int IdCategoryExpense {get; set;}

    [Required]
    public double HowMuch { get; set; }

    //[Required]
    //public int IdCategorySavings {get; set;}

    [Required]
    public DateTime Date { get; set; }

    //     [Required]
    //     [ForeignKey("IdUser")]
    //     public int IdUser {get;set;}
    //     public User Users {get;set;}
    public string Comment { get; set; }

    public bool StandingOrder { get; set; }
    public string Attachment { get; set; }

  }
}