using AutoMapper;
using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Models;

namespace ManagementFinanceApp.Settings
{
  public static class AutoMapperConfig
  {
    public static IMapper GetMapper() => new MapperConfiguration(cfg =>
    {
      cfg.CreateMap<UserDto, User>();
    }).CreateMapper();
  }
}