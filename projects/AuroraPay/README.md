> Публичная выжимка стека из коммерческого проекта. Детали предметной области и полный код — под NDA.

# AuroraPay

Платёжный highload-бэкенд для fintech: приём платежей, антифрод-пайплайн, баланс кошельков, webhooks мерчантам.

## Зачем проект
Закрывает стек вакансий уровня **.NET Developer (Highload, Fintech)** / Senior backend.

## Стек
- C# / .NET 8
- ASP.NET Core Web API
- gRPC (внутренние сервисы)
- Kafka (платёжные события)
- RabbitMQ (outbox / retries)
- PostgreSQL + Dapper (write path) / EF Core (admin)
- Redis (идемпотентность, rate limits, сессии)
- Docker + Kubernetes
- CI/CD (GitHub Actions)
- xUnit, DDD / CQRS / MediatR
- OpenTelemetry → Prometheus / Grafana

## Сервисы
| Сервис | Роль |
|--------|------|
| `Gateway` | REST API, JWT, rate limiting |
| `Payments` | оркестрация платежей, идемпотентность |
| `Ledger` | двойная запись по счетам (PostgreSQL) |
| `Risk` | скоринг транзакций (Kafka consumer) |
| `Notify` | webhooks мерчантам (RabbitMQ) |

## Архитектура
```
Client → Gateway → Payments ──gRPC──► Ledger
                      │
                   Kafka topic payments.events
                      ├─► Risk
                      └─► Notify (via RabbitMQ)
```

## Локальный запуск
```bash
docker compose up -d
dotnet run --project src/AuroraPay.Gateway
```

## Что показать на собесе
1. Идемпотентные платежи через Redis + unique keys в Postgres
2. Outbox pattern перед Kafka
3. gRPC контракты между Payments и Ledger
4. Метрики latency p99 в Grafana
EOF

