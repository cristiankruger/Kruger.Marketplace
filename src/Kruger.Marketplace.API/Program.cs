using Kruger.Marketplace.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

configuration.SetBasePath(environment.ContentRootPath)
             .AddJsonFile("appsettings.json", true, true)
             .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
             .AddEnvironmentVariables();

builder.Services
       .AddApiBehaviorConfig()
       .AddDatabase(configuration, environment)
       .AddAppSettingsConfiguration(configuration)
       .AddArquivoSettingsConfiguration(configuration)
       .AddJWTConfiguration(configuration)
       .AddAutoMapper(typeof(AutomapperConfig))
       .AddCorsConfig()
       .AddEndpointsApiExplorer()
       .AddSwaggerConfig()
       .AddIdentityConfig()
       .ResolveDependecies();

var app = builder.Build();

app.UseApiConfiguration(environment)
   .UseSwaggerConfig()
   .UseEndPointsConfiguration()
   .MigrateDatabase().Wait();

app.Run();