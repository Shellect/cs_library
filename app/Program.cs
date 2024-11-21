using app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using app.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем в приложение сервис подключения к базе данных
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(connection));

// Добавляем в приложение сервис подключения к базе данных
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(connection));
// Добавляем сервис обработки пользователей
builder.Services.AddUserService(); 
// Добавляем сервис работы с токенами
builder.Services.AddTokenService(builder.Configuration);

// добавляем сервисы MVC
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Подключаем развернутую страницу ошибки для разработчиков
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
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