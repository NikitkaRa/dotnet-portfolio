> Публичная выжимка стека из коммерческого проекта. Детали предметной области и полный код — под NDA.

# CatalogNexus

Master Data и поисковый движок номенклатуры: нормализация SKU, синонимы, быстрый поиск, выдача по gRPC.

## Зачем проект
Под вакансии **поисковый движок / Master Data** (ROSSKO NCI и аналоги).

## Стек
- C# / .NET 8
- ASP.NET Core + gRPC
- PostgreSQL (source of truth)
- Redis (горячий кэш выдачи)
- Inverted index / полнотекст (Postgres FTS + опционально Elasticsearch)
- Kubernetes manifests
- CI/CD
- xUnit

## Сервисы
- `Catalog.Api` — REST админка справочников
- `Catalog.Search` — gRPC поиск
- `Catalog.Indexer` — worker переиндексации

## Фичи
- Нормализация артикулов (бренды, кроссы)
- Поиск с опечатками / синонимами
- gRPC low-latency API для витрин
- Горизонтальное масштабирование indexer-подов в K8s
EOF
