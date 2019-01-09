using System.ComponentModel.DataAnnotations;

namespace ManagementFinanceApp.Models
{
  public class UserDto
  {
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nick { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public bool IsDelete { get; set; } = false;

  }
}