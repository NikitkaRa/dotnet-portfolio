using DeskForge.Models;
using Microsoft.EntityFrameworkCore;

namespace DeskForge.Data;

public sealed class DeskForgeDbContext(DbContextOptions<DeskForgeDbContext> options) : DbContext(options)
{
    public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
}

public sealed class EfCatalogRepository(DeskForgeDbContext db) : ICatalogRepository
{
    public async Task<IReadOnlyList<CatalogItem>> GetAllAsync(CancellationToken ct)
        => await db.CatalogItems.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

    public async Task AddAsync(CatalogItem item, CancellationToken ct)
    {
        db.CatalogItems.Add(item);
        await db.SaveChangesAsync(ct);
    }
}
