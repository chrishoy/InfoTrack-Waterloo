using Application;
using Serilog;
using System.Reflection;
using Waterloo.Infrastructure;
using Waterloo.Web;
using Waterloo.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapEndpoints();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapFallbackToFile("/index.html");

app.UseRequestContextLogging();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseCors("AllowAll");

app.Run();
