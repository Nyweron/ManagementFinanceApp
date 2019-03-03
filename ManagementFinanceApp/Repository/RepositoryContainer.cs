using Autofac;
using ManagementFinanceApp.Settings;

namespace ManagementFinanceApp.Repository
{
  public static class RepositoryContainer
  {
    public static void Update(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(RepositoryContainer).Assembly)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
      builder.RegisterInstance(AutoMapperConfig.GetMapper())
        .SingleInstance();
    }
  }
}