using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавляем в приложение сервис подключения к базе данных
builder.Services.AddDbContextPool<app.Models.ApplicationContext>(opt => opt.UseNpgsql(connection));

// добавляем в приложение сервисы Razor Pages
builder.Services.AddRazorPages();
var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// добавляем поддержку маршрутизации для Razor Pages
app.MapRazorPages();

app.Run();