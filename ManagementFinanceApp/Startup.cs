using System;
using System.Net.Mime;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Exceptions;
using ManagementFinanceApp.Middleware;
using ManagementFinanceApp.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
      });

      services.AddDbContext<ManagementFinanceAppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
      );

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
          policy =>
          {
            policy
              .WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
      });
      //middelwery try cache, w middelrwareze mozna dodac logowanie i przekazywanie wiadomosci
      //w sensie middelrwerey dodać logike szczegolna, powinnien zmapowac wiadomosc do przegladarki
      //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.2
      services.AddControllers(options =>
        options.Filters.Add(new HttpResponseExceptionFilter()));

      services.AddControllers(options =>
      {
        options.Filters.Add(new HttpResponseExceptionFilter());
      });

      services.AddMemoryCache();
      services.AddResponseCaching();

      // Logger Microsoft.Extensions.Logging
      var serviceProvider = services.BuildServiceProvider();
      var logger = serviceProvider.GetService<ILogger<Startup>>();
      services.AddSingleton(typeof(ILogger), logger);

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
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

      app.UseStaticFiles();

      app.UseRouting();
      app.UseCors("CorsPolicy");

      app.UseMyMiddleware();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

    }
  }
}