using Stark.WebApp.MVC.Configuration;
using Stark.WebApp.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.json.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    //builder.Services.AddUserSecrets<Startup>();
}

builder.Services.AddIdentityConfiguration();

builder.Services.AddMvcConfiguration(builder.Configuration);

builder.Services.RegisterServices(builder.Configuration);


var app = builder.Build();

app.UseMvcConfiguration(builder.Environment);

app.Run();
