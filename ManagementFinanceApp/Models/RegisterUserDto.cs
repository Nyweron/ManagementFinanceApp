using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ManagementFinanceApp.Models
{
 

  public class RegisterUserDto
  {
    private const int UserRole = 1;

    [Required]
    public string Email { get; set; }
    [Required]
    [MinLength(3)]
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    public string Nick { get; set; }
    public int RoleId { get; set; } = UserRole;
  }
}
