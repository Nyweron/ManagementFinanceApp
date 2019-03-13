using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository;
using ManagementFinanceApp.Repository.User;
using ManagementFinanceApp.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ManagementFinanceApp
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public IContainer Container { get; private set; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "Contacts API", Version = "v1" });
      });

      services.AddDbContext<ManagementFinanceAppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

      services.AddCors();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddMemoryCache();
      services.AddResponseCaching();

      // In production, the React files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/build";
      });

      // Configute Autofac
      var builder = new ContainerBuilder();
      // Loads the already configured items from services object
      builder.Populate(services);
      builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
      builder.RegisterInstance(AutoMapperConfig.GetMapper())
        .SingleInstance();
      Container = builder.Build();

      return new AutofacServiceProvider(Container);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      //https://localhost:5001/swagger/
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseSpaStaticFiles();

      app.UseCors(builder => builder
        .WithOrigins("http://localhost:3000")
        .WithOrigins("https://localhost:5001"));

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseReactDevelopmentServer(npmScript: "start");
        }

      });

      applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
    }
  }
}