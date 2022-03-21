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
      cfg.CreateMap<User, UserDto>();
      cfg.CreateMap<Entities.CategoryGroup, Models.CategoryGroup>();
      cfg.CreateMap<Models.CategoryGroup, Entities.CategoryGroup>();
      cfg.CreateMap<Entities.CategoryIncome, Models.Income>();
      cfg.CreateMap<Models.CategorySaving, Entities.CategorySaving>();
      cfg.CreateMap<Models.TransferHistory, Entities.TransferHistory>();
      cfg.CreateMap<Entities.TransferHistory, Models.TransferHistory>();
      cfg.CreateMap<Entities.CategoryIncome, Models.CategoryIncome>();
      cfg.CreateMap<Models.CategoryIncome, Entities.CategoryIncome>();
      cfg.CreateMap<Models.Expense, Entities.Expense>();
      cfg.CreateMap<Models.Income, Entities.Income>().ReverseMap();
      cfg.CreateMap<Models.Saving, Entities.Saving>().ReverseMap();
      cfg.CreateMap<Models.CategoryExpense, Entities.CategoryExpense>().ReverseMap();

    }).CreateMapper();
  }
}