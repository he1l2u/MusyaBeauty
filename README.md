# MusyaBeauty

MusyaBeauty — web-приложение для онлайн-записи в салон красоты, разработанное на ASP.NET Core и Blazor Server.

## Возможности

* просмотр услуг салона;
* просмотр мастеров;
* онлайн-запись на услуги;
* работа с базой данных PostgreSQL;
* контейнеризация через Docker;
* Entity Framework Core Code First;
* миграции базы данных.

## Технологии

* .NET 8
* ASP.NET Core
* Blazor Server
* Entity Framework Core
* PostgreSQL
* Docker
* Docker Compose

## Структура проекта

```text
Components/        - Razor-компоненты и страницы
Data/              - DbContext и инициализация БД
Models/            - модели сущностей
Services/          - бизнес-логика
Migrations/        - миграции EF Core
wwwroot/           - статические файлы
```

## Запуск проекта

### Клонирование репозитория

```bash
git clone https://github.com/he1l2u/Musyabeauty.git
cd MusyaBeauty
```

### Запуск через Docker

```bash
docker compose up -d --build
```

### Проверка контейнеров

```bash
docker compose ps
```

## Адрес приложения

```text
http://localhost:8001
```

## Работа с базой данных

### Просмотр миграций

```bash
dotnet ef migrations list
```

### Применение миграций

```bash
dotnet ef database update
```

## Docker Hub

```text
https://hub.docker.com/
```

## Автор

Акрамова Муслима Икрамовна
