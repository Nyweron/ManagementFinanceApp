namespace ManagementFinanceApp
{
  public class AuthenticationSettings
  {
    public string JwtKey { get; set; }
    public int JwtExpireDays { get; set; }
    public int JwtExpireMinutes { get; set; }
    public string JwtIssuer { get; set; }
    public string JWtRefreshTokenValidityInMinutes { get; set; }
  }
}
