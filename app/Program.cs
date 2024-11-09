using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавляем в приложение сервис подключения к базе данных
builder.Services.AddDbContext<app.Models.ApplicationContext>(opt => opt.UseNpgsql(connection));

// добавляем сервисы MVC
builder.Services.AddControllers(); 
var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// устанавливаем сопоставление маршрутов с контроллерами
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
 

app.Run();