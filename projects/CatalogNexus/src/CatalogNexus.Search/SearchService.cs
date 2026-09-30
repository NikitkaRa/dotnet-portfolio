namespace CatalogNexus.Search;

public sealed record SearchHit(string Sku, string Name, double Score);

/// <summary>Контракт gRPC/search API без зависимости от protoc на macOS ARM.</summary>
public interface ICatalogSearchService
{
    Task<IReadOnlyList<SearchHit>> SearchAsync(string query, int take, CancellationToken ct);
}

public sealed class InMemoryCatalogSearchService : ICatalogSearchService
{
    private readonly List<(string Sku, string Name)> _index =
    [
        ("BRK-001", "Тормозные колодки"),
        ("FLT-220", "Масляный фильтр"),
        ("BLT-015", "Ремень ГРМ")
    ];

    public Task<IReadOnlyList<SearchHit>> SearchAsync(string query, int take, CancellationToken ct)
    {
        var q = query.Trim();
        IReadOnlyList<SearchHit> hits = _index
            .Where(x => x.Sku.Contains(q, StringComparison.OrdinalIgnoreCase)
                     || x.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
            .Take(take)
            .Select(x => new SearchHit(x.Sku, x.Name, 1.0))
            .ToList();
        return Task.FromResult(hits);
    }
}
