using Kruger.Marketplace.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.MvcBehaviorConfig()
       .AddDatabase()
       .AddAppSettingsConfiguration()
       .AddArquivoSettingsConfiguration()
       .ResolveDependecies();

var app = builder.Build();

app.UseMvcConfiguration()
   .MigrateDatabase().Wait();

app.Run();