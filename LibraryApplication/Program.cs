using LibraryApplication.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using LibraryApplication.BusinessLayer;
using LibraryApplication.DataAccessLayer;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbcontext"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddBusinessLayerServices();
builder.Services.AddDataAccessLayerServices();

string dateTime = DateTime.Now.ToString("dd-MM-yyyy", new System.Globalization.CultureInfo("tr-TR"));

Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .WriteTo.Debug()
                            .WriteTo.File($"logs/log_{dateTime}.txt", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Book/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
