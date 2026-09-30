> Публичная выжимка стека из коммерческого проекта. Детали предметной области и полный код — под NDA.

# SynapseDesk

Fullstack-платформа с AI-агентами: чат-ассистент для внутренних процессов, инструменты (tools), расписание задач.

## Зачем проект
Под вакансии **Full-stack .NET + AI-агенты**: ASP.NET + React/TS + LLM.

## Стек
- C# / .NET 8 / ASP.NET Core
- React + TypeScript
- JWT / ASP.NET Identity
- EF Core + PostgreSQL
- Quartz.NET (фоновые агенты)
- OpenAI-compatible LLM API
- xUnit
- Docker

## Возможности
- Чат с агентом, tool-calling (поиск по базе, создание тикета)
- Планировщик: агент раз в N минут сам тянет задачи
- Роли и JWT
- React SPA на Vite

## Структура
```
backend/SynapseDesk.Api
frontend/   — React + TS
```

## Запуск
```bash
docker compose up -d postgres
dotnet run --project backend/SynapseDesk.Api
cd frontend && npm i && npm run dev
```
EOF

