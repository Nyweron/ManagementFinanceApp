using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Exceptions;
using ManagementFinanceApp.Middleware;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Settings;
using ManagementFinanceApp.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
      var authenticationSettings = new AuthenticationSettings();

      Configuration.GetSection("Authentication").Bind(authenticationSettings);

      services.AddSingleton(authenticationSettings);

      services.AddAuthentication(option =>
      {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
      }).AddJwtBearer(cfg =>
      {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
          ValidIssuer = authenticationSettings.JwtIssuer,
          ValidAudience = authenticationSettings.JwtIssuer,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
        };
      });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("HasNick", builder =>
        {
          builder.RequireClaim("Nick");
        });
      });



      services.AddSwaggerGen(c =>
      {

        {
          c.SwaggerDoc("v1", new OpenApiInfo
          {
            Title = "My API",
            Version = "v1"
          });
          c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
          {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
          });
          c.AddSecurityRequirement(new OpenApiSecurityRequirement {
           {
             new OpenApiSecurityScheme
             {
               Reference = new OpenApiReference
               {
                 Type = ReferenceType.SecurityScheme,
                 Id = "Bearer"
               }
              },
              new string[] { }
            }
          });
        }

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

      services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

      services.AddScoped<IPasswordHasher<RegisterUserDto>, PasswordHasher<RegisterUserDto>>();
      services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

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

      app.UseMyMiddleware();

      app.UseAuthentication();
      app.UseHttpsRedirection();

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

      app.UseAuthorization();

      app.UseCors("CorsPolicy");

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

    }
  }
}