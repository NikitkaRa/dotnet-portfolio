namespace DeskForge.Models;

public sealed class CatalogItem
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public interface ICatalogView
{
    void ShowItems(IReadOnlyList<CatalogItem> items);
    void ShowError(string message);
    string? AskItemName();
}

public interface ICatalogRepository
{
    Task<IReadOnlyList<CatalogItem>> GetAllAsync(CancellationToken ct);
    Task AddAsync(CatalogItem item, CancellationToken ct);
}
