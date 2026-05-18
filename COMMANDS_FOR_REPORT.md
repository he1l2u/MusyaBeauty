# Команды для выполнения и скриншотов

## 1. Проверка структуры проекта

```powershell
cd "C:\Users\akram\OneDrive\Рабочий стол\ВУЗ\КСИПО Курсовая\BeautySalonCoursework"
dir
```

## 2. Восстановление зависимостей

```powershell
dotnet restore
```

## 3. Запуск PostgreSQL

```powershell
docker compose up -d db
docker compose ps
```

## 4. Применение миграций

```powershell
dotnet ef database update
```

Если dotnet ef не установлен:

```powershell
dotnet tool install --global dotnet-ef
```

## 5. Запуск приложения локально

```powershell
dotnet run
```

## 6. Сборка Docker-образа

```powershell
docker build -t he1l2u/beauty-salon-coursework:01 .
```

## 7. Запуск через Docker Compose

```powershell
docker compose up -d --build
docker compose ps
```

## 8. Проверка сайта

Открыть в браузере:

```text
http://localhost:8001
http://localhost:8001/services
http://localhost:8001/masters
http://localhost:8001/appointments
```

## 9. Проверка базы данных

```powershell
docker compose exec db psql -U beauty_user -d beautysalon_db -c "SELECT * FROM \"Services\";"
docker compose exec db psql -U beauty_user -d beautysalon_db -c "SELECT * FROM \"Appointments\";"
```

## 10. Публикация образа

```powershell
docker login
docker push he1l2u/beauty-salon-coursework:01
```
