### Миграции

 Прежде всего необходимо добавить в проект пакет Microsoft.EntityFrameworkCore.Tools. Кроме того, если мы работаем через .NET CLI, то также надо установить инструменты для EF Core с помощью команды:  
 ```dotnet tool install --global dotnet-ef```

 Устанавливаем пакет  
 ```dotnet add <имя проекта> package Microsoft.EntityFrameworkCore.Design```
 
 Для создания миграции в Visual Studio в окне Package Manager Console вводится следующая команда:  
 ```Add-Migration название_миграции```
 или
 ```dotnet ef migrations add название_миграции --project <имя проекта>```

### Дамп базы данных

 Для сохранения данных из контейнера используется команда 
 ```
 docker exec -i <имя_контейнера_с_СУБД> pg_dump postgres > ./dump.sql
 ```
 pg_dumb postgres - утилита создания дампов postgres. Аргумент postgres означает базу данных (по умолчанию postgres).

 ### Удаление контейнеров

 ```docker compose down``` - останавливает все контейнеры, включая те, для которых установлен ```restart: always```
 Для удаления старых контейнеров используйте ```--remove-orphans```

 ### Angular

 - Инициализируйте пустой проект `npm init -y`
 - Установить инструмент командной строки angular: `npm i --save-dev @angular/cli`
 - Создать конфигурацию рабочего пространства (workspace): `angular.json`
 - Устанавливаем первое приложение `npx ng g application <имя_приложения>`
 - Инициализируем tsconfig.json `npx tsc --init`
 - Меняем настройки typescript по [образцу](https://angular.dev/reference/configs/angular-compiler-options#example-1)
 - Загрузите минимально необходимы набор пакетов:
    - @angular/core
    - @angular/platform-browser
    - @angular/router

### Установка Nuget пакетов

```bash
dotnet add app/app.csproj package Swashbuckle.AspNetCore
```