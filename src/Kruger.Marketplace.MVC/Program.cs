//using Kruger.Marketplace.Application.Extensions;
//using Kruger.Marketplace.MVC;

//WebApplication.CreateBuilder(args).UseStartup<Startup>();

using Kruger.Marketplace.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services
       .AddDatabase(configuration, environment)
       .AddAppSettingsConfiguration(configuration)
       .AddArquivoSettingsConfiguration(configuration)
       .AddAutoMapper(typeof(AutomapperConfig))
       .MvcBehaviorConfig()
       .ResolveDependecies(false);

var app = builder.Build();

app.MigrateDatabase().Wait();
app.UseMvcConfiguration();

app.Run();