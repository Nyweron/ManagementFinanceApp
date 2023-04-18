namespace ManagementFinanceApp.Models
{
  public class RegisterUserDto
  {
    private const int UserRole = 1;

    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string Nick { get; set; }
    public int RoleId { get; set; } = UserRole;
  }
}
