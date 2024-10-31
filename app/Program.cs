using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
var builder = WebApplication.CreateBuilder(args);

// Добавляем в приложение сервис подключения к базе данных
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<app.Models.ApplicationContext>(opt => opt.UseNpgsql(connection));

// добавляем поддержку контроллеров
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