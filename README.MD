# Требования

- PostgreSQL 11
- .NET Core SDK > 5.0.0
- Node Package Manageer (NPM) > 7.6
- MiniO object storage (https://hub.docker.com/r/minio/minio/)

# Сборка 

В *C:\Users\Max\source\repos\BarsGroup.Hackathon\BarsGroup.Hackathon\appsettings.json* 

1. указать данные для подключения к PostgreSQL: User ID и Password (и порт, если стандартный изменен)

2. Запустить MiniO через докер-образ на порте 9000. Стандартные учетные данные уже установлены в appsettings.json

   2.1  Перейти на http://localhost:9000/ (панель управления Minio) логин/пароль - minioadmin/minioadmin. В личном кабинете создать корзину с именем *barsgroup-hackathon* (Справа снизу "+" -> Create Bucket)

3. В папке *\BarsGroup.Hackathon\barsgrouphackathon.fronend* установить React-библиотеки c помощью команды *npm install*

4. Установить утилиты Entity Framework Core: 
*dotnet tool install --global dotnet-ef*

5. Запустить миграции из папки *\BarsGroup.Hackathon\BarsGroup.Hackathon*  через командную строку: *dotnet ef database update* 

6. Запустить back-end с помощью команды *dotnet run* из той же папки *\BarsGroup.Hackathon\BarsGroup.Hackathon*

7. Запустить front-end через команду *npm start* в папке 

8. По адресу http://localhost:5000/swagger/index.html должен находиться API бек-энда.

9. Фронт-енд должен запуститься на *http://localhost:3000/*
