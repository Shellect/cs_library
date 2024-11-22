using app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using app.Services;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавляем в приложение сервис подключения к базе данных
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(connection));
// Добавляем сервис обработки пользователей
builder.Services.AddUserService();
// Добавляем сервис работы с токенами
builder.Services.AddTokenService(builder.Configuration);

// добавляем сервисы MVC
builder.Services.AddControllers();
var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
     // Подключаем развернутую страницу ошибки для разработчиков
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

// устанавливаем сопоставление маршрутов с контроллерами
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();