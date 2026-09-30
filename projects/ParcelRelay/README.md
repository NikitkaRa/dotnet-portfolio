> Публичная выжимка стека из коммерческого проекта. Детали предметной области и полный код — под NDA.

# ParcelRelay

Интеграционный хаб логистических провайдеров: единый API → адаптеры СДЭК / Boxberry / внутренний carrier.

## Зачем проект
Под вакансии уровня **Senior C# логистические интеграции** (Ozon Domestic и аналоги).

## Стек
- C# / .NET 8 / ASP.NET Core Web API
- RabbitMQ (очереди доставки статусов)
- NATS (легкий pub/sub между адаптерами)
- MongoDB (сырые payload провайдеров)
- PostgreSQL (нормализованные отправления)
- Docker
- Polly (retry / circuit breaker)
- xUnit + Testcontainers

## Поток
```
CreateShipment API
   → Adapter (HTTP to provider)
   → Mongo raw + Postgres normalized
   → RabbitMQ status.events
   → Webhook to merchant
```

## Фичи
- Единый контракт отправлений
- Адаптеры провайдеров как плагины
- Идемпотентные webhooks
- DLQ для failed jobs
EOF

