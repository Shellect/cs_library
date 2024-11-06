using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using app.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Добавляем в приложение сервис подключения к базе данных
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(connection));

// Добавляем в приложение сервис аутентификации пользователей
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>{
        options.LoginPath = new PathString("/Account/Login");
    });

// добавляем поддержку контроллеров
builder.Services.AddControllers();
var app = builder.Build();

// Подключаем развернутую страницу ошибки для разработчиков
if (app.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Подключаем аутентификацию и авторизацию
app.UseAuthentication();
app.UseAuthorization();

// устанавливаем сопоставление маршрутов с контроллерами
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
 

app.Run();