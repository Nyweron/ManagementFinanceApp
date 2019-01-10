using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementFinanceApp.Entities
{
  public class User
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "The FirstName must be submitted", AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [MaxLength(40)]
    public string LastName { get; set; }

    [MaxLength(20)]
    public string Nick { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    [Required]
    [MaxLength(50)]
    public string Email { get; set; }

    [Required]
    public bool IsDelete { get; set; } = false;

  }
}