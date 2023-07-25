using System.ComponentModel.DataAnnotations;

namespace ManagementFinanceApp.Entities
{
  public class UserRefreshToken
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; } // UserName == email
    [Required]
    public string RefreshToken { get; set; }
    public bool IsActive { get; set; } = true;
  }
}
