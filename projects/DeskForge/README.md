> Публичная выжимка стека из коммерческого проекта. Детали предметной области и полный код — под NDA.

# DeskForge

Десктоп-клиент для внутренних бизнес-приложений: справочники, документы, печать, offline-friendly UI.

## Зачем проект
Под вакансии **WinForms (MVP), C#, .NET** / корпоративный desktop.

## Стек
- C# / .NET 8 (WinForms) — современный аналог .NET Framework desktop
- Паттерн **MVP**
- MS SQL Server
- Entity Framework Core
- DevExpress-подобный слой UI (гриды, lookup, ribbon)
- Опционально: ASP.NET Core Web API backend

## Структура
```
DeskForge.UI          — Views (Forms)
DeskForge.Presenters  — Presenters (MVP)
DeskForge.Models      — сущности / DTO
DeskForge.Data        — EF Core + SQL
DeskForge.Services    — бизнес-логика
```

## Фичи
- CRUD справочников с валидацией
- Мастер-детейл документы
- Печать / экспорт
- Разделение UI и логики через MVP
- Миграции EF Core

## Запуск
```bash
dotnet run --project src/DeskForge.UI
```
EOF

