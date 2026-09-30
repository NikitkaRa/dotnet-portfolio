> Публичная выжимка стека из коммерческого проекта. Детали предметной области и полный код — под NDA.

# MercatoPulse

Аналитика и обработка заказов для ритейла: поток заказов → операционная БД → витрины в ClickHouse.

## Зачем проект
Под вакансии вроде **Программист .Net** в ритейле (К&Б и аналоги): отчёты, Redis-кэш, ClickHouse, Jenkins.

## Стек
- C# / .NET 8 / ASP.NET Core
- MS SQL Server (операционка) + Dapper
- EF Core (CRUD админки)
- ClickHouse (OLAP-витрины)
- Redis (кэш витрин и feature flags)
- Background workers
- Jenkins + Linux agents
- xUnit

## Модули
- `Orders.Api` — REST API заказов
- `Orders.Worker` — агрегация в ClickHouse батчами
- `Reporting.Api` — быстрые отчёты из ClickHouse / Redis

## Фичи
- Инкрементальная выгрузка заказов в ClickHouse
- Кэш топ-SKU в Redis с TTL и stampede protection
- SQL-heavy отчёты через Dapper
- Jenkins pipeline: build → test → publish docker → deploy

## Локальный запуск
```bash
docker compose up -d
dotnet run --project src/MercatoPulse.Orders.Api
```
EOF

