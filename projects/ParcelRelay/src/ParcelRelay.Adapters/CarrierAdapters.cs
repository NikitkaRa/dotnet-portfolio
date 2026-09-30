namespace ParcelRelay.Adapters;

public sealed record ShipmentRequest(string OrderId, string CityTo, decimal WeightKg);
public sealed record ShipmentResult(string Provider, string TrackingNumber, string RawPayload);

public interface ICarrierAdapter
{
    string Name { get; }
    Task<ShipmentResult> CreateAsync(ShipmentRequest request, CancellationToken ct);
}

public sealed class DemoCarrierAdapter : ICarrierAdapter
{
    public string Name => "demo-carrier";

    public Task<ShipmentResult> CreateAsync(ShipmentRequest request, CancellationToken ct)
    {
        var tracking = $"{Name.ToUpperInvariant()}-{request.OrderId}-{Random.Shared.Next(10000, 99999)}";
        var raw = $"{{\"orderId\":\"{request.OrderId}\",\"tracking\":\"{tracking}\"}}";
        return Task.FromResult(new ShipmentResult(Name, tracking, raw));
    }
}

public sealed class CarrierRouter(IEnumerable<ICarrierAdapter> adapters)
{
    public Task<ShipmentResult> CreateAsync(string provider, ShipmentRequest request, CancellationToken ct)
    {
        var adapter = adapters.FirstOrDefault(a => a.Name.Equals(provider, StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException($"Unknown provider: {provider}");
        return adapter.CreateAsync(request, ct);
    }
}
