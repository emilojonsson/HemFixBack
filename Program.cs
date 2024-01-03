using System.Text;
using HemFixBack.Config;
using HemFixBack.Controllers;
using HemFixBack.Models;
using HemFixBack.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace HemFixBack
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      ConfigureServices(builder);

      var app = builder.Build();
      Configure(builder, app);
      app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
      var jwtKey = Environment.GetEnvironmentVariable("JwtKey");

      builder.Services.AddSwaggerGen(SwaggerConfig.ConfigureSwaggerGen);

      builder
        .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
          };
        });
      builder.Services.AddAuthorization();

      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSingleton<IUserService, UserService>();
      builder.Services.AddSingleton<ISimpleTaskService, SimpleTaskService>();
      builder.Services.AddScoped<SimpleTaskController>();
      builder.Services.AddSingleton<IGardenTaskService, GardenTaskService>();
      builder.Services.AddScoped<GardenTaskController>();
      builder.Services.AddSingleton<IMaintenanceTaskService, MaintenanceTaskService>();
      builder.Services.AddScoped<MaintenanceTaskController>();
      builder.Services.AddSingleton<IPurchaseTaskService, PurchaseTaskService>();
      builder.Services.AddScoped<PurchaseTaskController>();
      builder.Services.AddSingleton<ITaskService, TaskService>();
      builder.Services.AddScoped<TaskController>();

      builder.Services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
      });
    }

    private static void Configure(WebApplicationBuilder builder, WebApplication app)
    {
      app.UseSwagger();
      app.UseAuthorization();
      app.UseAuthentication();
      app.UseCors();

      ConfigureEndpoints(
        app,
        builder.Services.BuildServiceProvider().GetService<IUserService>(),
        builder.Configuration
      );

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // <- Empty string means the root path
      });

      void ConfigureEndpoints(
        WebApplication app,
        IUserService userService,
        IConfiguration configuration
      )
      {
        app.MapPost(
          "/login",
          (UserLogin user) => AuthController.Login(user, userService, configuration)
        )
        .Accepts<UserLogin>("application/json")
        .Produces<string>();

        app.MapPost(
          "/simpletask/create",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (SimpleTask task, SimpleTaskController controller) => controller.Create(task)
        )
        .Accepts<SimpleTask>("application/json")
        .Produces<SimpleTask>(statusCode: 200, contentType: "application/json");

        app.MapGet(
          "/simpletask/get",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Standard, Administrator"
          )]
          (string id, SimpleTaskController controller) => controller.Get(id)
        )
        .Produces<SimpleTask>();

        app.MapGet("/simpletask/list", 
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Standard, Administrator"
          )] (SimpleTaskController controller) => controller.List()
        )
        .Produces<List<SimpleTask>>(statusCode: 200, contentType: "application/json");

        app.MapPut(
          "/simpletask/update",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (SimpleTask newTask, SimpleTaskController controller) => controller.Update(newTask)
        )
        .Accepts<SimpleTask>("application/json")
        .Produces<SimpleTask>(statusCode: 200, contentType: "application/json");

        app.MapDelete(
          "/simpletask/delete",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (string id, SimpleTaskController controller) => controller.Delete(id)
        );

        app.MapPost(
          "/gardentask/create",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (GardenTask task, GardenTaskController controller) => controller.Create(task)
        )
        .Accepts<GardenTask>("application/json")
        .Produces<GardenTask>(statusCode: 200, contentType: "application/json");

        app.MapGet(
          "/gardentask/get",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Standard, Administrator"
          )]
          (string id, GardenTaskController controller) => controller.Get(id)
        )
        .Produces<GardenTask>();

        app.MapGet("/gardentask/list",           
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Standard, Administrator"
          )] (GardenTaskController controller) => controller.List())
        .Produces<List<GardenTask>>(statusCode: 200, contentType: "application/json");

        app.MapPut(
          "/gardentask/update",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (GardenTask newTask, GardenTaskController controller) => controller.Update(newTask)
        )
        .Accepts<GardenTask>("application/json")
        .Produces<GardenTask>(statusCode: 200, contentType: "application/json");

        app.MapDelete(
          "/gardentask/delete",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (string id, GardenTaskController controller) => controller.Delete(id)
        );

        app.MapPost(
          "/maintenancetask/create",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (MaintenanceTask task, MaintenanceTaskController controller) => controller.Create(task)
        )
        .Accepts<MaintenanceTask>("application/json")
        .Produces<MaintenanceTask>(statusCode: 200, contentType: "application/json");

        app.MapGet(
          "/maintenancetask/get",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Standard, Administrator"
          )]
          (string id, MaintenanceTaskController controller) => controller.Get(id)
        )
        .Produces<MaintenanceTask>();

        app.MapGet(
          "/maintenancetask/list", 
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Standard, Administrator"
          )] 
          (MaintenanceTaskController controller) => controller.List()
        )
        .Produces<List<MaintenanceTask>>(statusCode: 200, contentType: "application/json");

        app.MapPut(
          "/maintenancetask/update",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (MaintenanceTask newTask, MaintenanceTaskController controller) =>
            controller.Update(newTask)
        )
        .Accepts<MaintenanceTask>("application/json")
        .Produces<MaintenanceTask>(statusCode: 200, contentType: "application/json");

        app.MapDelete(
          "/maintenancetask/delete",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (string id, MaintenanceTaskController controller) => controller.Delete(id)
        );

        app.MapPost(
          "/purchasetask/create",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (PurchaseTask task, PurchaseTaskController controller) => controller.Create(task)
        )
        .Accepts<PurchaseTask>("application/json")
        .Produces<PurchaseTask>(statusCode: 200, contentType: "application/json");

        app.MapGet(
          "/purchasetask/get",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Standard, Administrator"
          )]
          (string id, PurchaseTaskController controller) => controller.Get(id)
        )
        .Produces<PurchaseTask>();

        app.MapGet(
          "/purchasetask/list",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Standard, Administrator"
          )] 
          (PurchaseTaskController controller) => controller.List()
        )
        .Produces<List<PurchaseTask>>(statusCode: 200, contentType: "application/json");

        app.MapPut(
          "/purchasetask/update",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (PurchaseTask newTask, PurchaseTaskController controller) =>
            controller.Update(newTask)
        )
        .Accepts<PurchaseTask>("application/json")
        .Produces<PurchaseTask>(statusCode: 200, contentType: "application/json");

        app.MapDelete(
          "/purchasetask/delete",
          [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = "Administrator"
          )]
          (string id, PurchaseTaskController controller) => controller.Delete(id)
        );

        app.MapGet("/alltasks/list", (TaskController controller) => controller.List())
          .Produces<List<ITask>>(statusCode: 200, contentType: "application/json");

      }
    }
  }
}
