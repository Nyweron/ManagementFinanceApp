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
    public int Weight { get; set; }

    [Phone]
    public string Phone { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Bad email")]
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string PasswordHash { get; set; }
  }
}