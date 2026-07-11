# Garage Manager

> Учебный проект на C#/.NET 8: учёт обслуживания автомобиля и расходов - от консольного приложения до REST API.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-EF%20Core-336791)
![License](https://img.shields.io/badge/license-MIT-green)

**Garage Manager** - практичный учебный проект для отработки backend-навыков на живой задаче: вести журнал работ по машине и считать расходы.

Проект прошёл эволюцию:

- **v1.0** - консольное приложение с хранением данных в SQLite (ручные репозитории на `Microsoft.Data.Sqlite`) и тестами на xUnit.
- **v2.0 (текущий этап)** - REST API на ASP.NET Core Web API с EF Core и PostgreSQL, документированный через Swagger UI.

Оба слоя лежат в одном решении.

## Содержание

- [Возможности](#возможности)
- [Технологии](#технологии)
- [Структура решения](#структура-решения)
- [Требования](#требования)
- [Запуск REST API](#запуск-rest-api)
- [Эндпоинты API](#эндпоинты-api)
- [Запуск консольного приложения](#запуск-консольного-приложения)
- [Тесты](#тесты)
- [Развитие проекта](#развитие-проекта)
- [Содействие](#содействие)
- [Лицензия](#лицензия)

## Возможности

- Учёт машин: добавление, просмотр, изменение, удаление.
- Записи обслуживания по машине: добавление и просмотр (дата, описание, пробег, стоимость).
- Подсчёт суммарных расходов по машине.
- Health-check и версия API.
- Интерактивная документация и ручное тестирование эндпоинтов через Swagger UI.

## Технологии

- C# / .NET 8
- ASP.NET Core Web API (контроллеры)
- Entity Framework Core + Npgsql (PostgreSQL)
- Swagger / Swashbuckle
- SQLite (`Microsoft.Data.Sqlite`) - в консольной версии
- xUnit - тесты

## Структура решения

```
GarageManager.sln
├─ GarageManager.Api/          # REST API (v2.0)
│  ├─ Controllers/             #   Cars, ServiceRecords, Health
│  ├─ Data/                    #   GarageDbContext (EF Core)
│  ├─ Models/                  #   Car, ServiceRecord
│  ├─ DTOs/                    #   Create/Update DTO для валидируемого ввода
│  ├─ Migrations/              #   EF Core миграции (PostgreSQL)
│  └─ Program.cs               #   Конфигурация приложения и Swagger
├─ GarageManager/              # Консольное приложение (v1.0, SQLite)
│  ├─ Models/                  #   Car, ServiceRecord
│  ├─ Services/                #   Репозитории + инициализация SQLite
│  ├─ UI/                      #   ConsoleInput (валидация ввода)
│  └─ Program.cs               #   Текстовое меню
└─ GarageManager.Tests/        # xUnit-тесты на репозитории консольной версии
```

> Примечание: API и консольное приложение пока не разделяют общий слой данных - у каждого своя реализация моделей и доступа к БД. Вынос общего ядра в отдельную библиотеку - возможный следующий шаг (см. [Развитие проекта](#развитие-проекта)).

## Требования

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (для REST API)
- Инструмент EF Core CLI (для применения миграций):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

## Запуск REST API

1. Убедитесь, что PostgreSQL запущен, и создайте (или подготовьте) базу для проекта.

2. Задайте строку подключения `GarageDb`. **Не храните реальный пароль в `appsettings.json`** - используйте [user-secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) для локальной разработки:

   ```bash
   cd GarageManager.Api
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:GarageDb" "Host=localhost;Port=5432;Database=garagemanager;Username=postgres;Password=ВАШ_ПАРОЛЬ"
   ```

3. Примените миграции, чтобы создать таблицы:

   ```bash
   dotnet ef database update --project GarageManager.Api
   ```

4. Запустите API:

   ```bash
   dotnet run --project GarageManager.Api
   ```

5. Откройте Swagger UI в браузере: `http://localhost:5189/swagger` (или `https://localhost:7064/swagger`).

## Эндпоинты API

| Метод | Маршрут | Описание |
|---|---|---|
| `GET` | `/api/health` | Проверка живости сервиса |
| `GET` | `/api/health/version` | Версия API |
| `GET` | `/api/cars` | Список всех машин |
| `GET` | `/api/cars/{id}` | Одна машина по id |
| `POST` | `/api/cars` | Добавить машину |
| `PUT` | `/api/cars/{id}` | Изменить машину |
| `DELETE` | `/api/cars/{id}` | Удалить машину |
| `GET` | `/api/cars/{carId}/records` | Записи обслуживания по машине |
| `POST` | `/api/cars/{carId}/records` | Добавить запись обслуживания |
| `GET` | `/api/cars/{carId}/total-cost` | Сумма расходов по машине |

## Запуск консольного приложения

Консольная версия использует локальную базу SQLite (`garage.db`) и не требует PostgreSQL:

```bash
dotnet run --project GarageManager
```

При первом запуске файл `garage.db` создаётся автоматически. Приложение выводит текстовое меню для добавления/просмотра машин и записей обслуживания.

## Тесты

```bash
dotnet test
```

Тесты интеграционные: каждый создаёт временную SQLite-базу и проверяет репозитории консольной версии (добавление и чтение машин, удаление по id, проверка существования).

## Развитие проекта

**Сделано:**

1. Консольное приложение с хранением в SQLite (v1.0).
2. Интеграционные xUnit-тесты на репозитории.
3. REST API на ASP.NET Core: CRUD по машинам, записи обслуживания, сумма расходов.
4. EF Core + PostgreSQL, миграции.
5. Swagger UI.

**В планах:**

6. Валидация ввода и корректные коды ответов (400/404) на уровне API.
7. Интеграционные тесты API (`WebApplicationFactory`).
8. Скриншоты Swagger в README.
9. Релиз **v2.0.0**.
10. Возможный вынос общего слоя данных в отдельную библиотеку.

## Содействие

Идеи, предложения и улучшения приветствуются. Откройте issue или отправьте pull request.

## Лицензия

[MIT](LICENSE.txt)
