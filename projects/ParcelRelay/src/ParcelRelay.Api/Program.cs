using ParcelRelay.Adapters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ICarrierAdapter, DemoCarrierAdapter>();
builder.Services.AddSingleton<CarrierRouter>();

var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.MapPost("/api/shipments", async (CreateShipment req, CarrierRouter router, CancellationToken ct) =>
{
    var result = await router.CreateAsync(req.Provider, new ShipmentRequest(req.OrderId, req.CityTo, req.WeightKg), ct);
    return Results.Ok(result);
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.Run();

record CreateShipment(string Provider, string OrderId, string CityTo, decimal WeightKg);
