### Миграции

 Прежде всего необходимо добавить в проект пакет Microsoft.EntityFrameworkCore.Tools. Кроме того, если мы работаем через .NET CLI, то также надо установить инструменты для EF Core с помощью команды:  
 ```dotnet tool install --global dotnet-ef```

 Устанавливаем пакет  
 ```dotnet add package Microsoft.EntityFrameworkCore.Design```
 
 Для создания миграции в Visual Studio в окне Package Manager Console вводится следующая команда:  
 ```Add-Migration название_миграции```
 или
 ```dotnet ef migrations add название_миграции```

### Дамп базы данных

 Для сохранения данных из контейнера используется команда 
 ```
 docker exec -i <имя_контейнера_с_СУБД> pg_dump postgres > ./dump.sql
 ```
 pg_dumb postgres - утилита создания дампов postgres. Аргумент postgres означает базу данных (по умолчанию postgres).

 ### Удаление контейнеров

 ```docker compose down``` - останавливает все контейнеры, включая те, для которых установлен ```restart: always```
 Для удаления старых контейнеров используйте ```--remove-orphans```

 