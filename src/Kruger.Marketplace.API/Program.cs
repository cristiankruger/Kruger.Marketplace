using Kruger.Marketplace.Core.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiBehaviorConfig()
       .AddDatabase()
       .AddAppSettingsConfiguration()
       .AddArquivoSettingsConfiguration()
       .AddJWTConfiguration()
       .AddAutoMapper()
       .AddCorsConfig()
       .AddEndPointsExplorer()
       .AddSwaggerConfig()
       .AddIdentityConfig()
       .ResolveDependecies();

var app = builder.Build();

app.UseApiConfiguration()
   .UseSwaggerConfig()
   .UseEndPointsConfiguration()
   .MigrateDatabase().Wait();

app.Run();