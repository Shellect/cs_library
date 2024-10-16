### Миграции

 Прежде всего необходимо добавить в проект пакет Microsoft.EntityFrameworkCore.Tools. Кроме того, если мы работаем через .NET CLI, то также надо установить инструменты для EF Core с помощью команды:  
 ```dotnet tool install --global dotnet-ef```

 Устанавливаем пакет  
 ```dotnet add package Microsoft.EntityFrameworkCore.Design```
 
 Для создания миграции в Visual Studio в окне Package Manager Console вводится следующая команда:  
 ```Add-Migration название_миграции```
 или
 ```dotnet ef migrations add название_миграции```
 