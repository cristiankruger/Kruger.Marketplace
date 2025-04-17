using Kruger.Marketplace.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

configuration.SetBasePath(environment.ContentRootPath)
             .AddJsonFile("appsettings.json", true, true)
             .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
             .AddEnvironmentVariables();

builder.Services
       .MvcBehaviorConfig()
       .AddDatabase(configuration, environment)
       .AddAppSettingsConfiguration(configuration)
       .AddArquivoSettingsConfiguration(configuration)
       .AddAutoMapper(typeof(AutomapperConfig))       
       .ResolveDependecies();

var app = builder.Build();

app.MigrateDatabase().Wait();
app.UseMvcConfiguration();

app.Run();