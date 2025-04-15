using Kruger.Marketplace.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services
       .AddDatabase(configuration, environment)
       .AddAppSettingsConfiguration(configuration)
       .AddArquivoSettingsConfiguration(configuration)
       .AddJWTConfiguration(configuration)
       .AddAutoMapper(typeof(AutomapperConfig))
       .AddApiBehaviorConfig()
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