using DeskForge.Models;

namespace DeskForge.Presenters;

public sealed class CatalogPresenter(ICatalogView view, ICatalogRepository repository)
{
    public async Task LoadAsync(CancellationToken ct = default)
    {
        try
        {
            var items = await repository.GetAllAsync(ct);
            view.ShowItems(items);
        }
        catch (Exception ex)
        {
            view.ShowError(ex.Message);
        }
    }

    public async Task AddAsync(CancellationToken ct = default)
    {
        var name = view.AskItemName();
        if (string.IsNullOrWhiteSpace(name))
            return;

        await repository.AddAsync(new CatalogItem
        {
            Code = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant(),
            Name = name.Trim(),
            IsActive = true
        }, ct);

        await LoadAsync(ct);
    }
}
