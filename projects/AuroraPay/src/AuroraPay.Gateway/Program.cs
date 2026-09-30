using AuroraPay.Contracts;
using AuroraPay.Payments;
using MediatR;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePaymentHandler>());
builder.Services.AddSingleton<IPaymentStore, InMemoryPaymentStore>();
builder.Services.AddSingleton<IIdempotencyStore, InMemoryIdempotencyStore>();
builder.Services.AddSingleton<ILedgerClient, FakeLedgerClient>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapPost("/api/payments", async (CreatePaymentRequest request, IMediator mediator, CancellationToken ct) =>
{
    var payment = await mediator.Send(new CreatePaymentCommand(request), ct);
    return Results.Created($"/api/payments/{payment.Id}", payment);
})
.WithName("CreatePayment");

app.MapGet("/health", () => Results.Ok(new { status = "ok", service = "AuroraPay.Gateway" }));

app.Run();

sealed class InMemoryPaymentStore : IPaymentStore
{
    private readonly ConcurrentDictionary<Guid, PaymentDto> _items = new();
    public Task InsertAsync(PaymentDto payment, CancellationToken ct)
    {
        _items[payment.Id] = payment;
        return Task.CompletedTask;
    }
}

sealed class InMemoryIdempotencyStore : IIdempotencyStore
{
    private readonly ConcurrentDictionary<string, PaymentDto> _items = new();
    public Task<PaymentDto?> GetAsync(string key, CancellationToken ct)
        => Task.FromResult(_items.TryGetValue(key, out var p) ? p : null);
    public Task SetAsync(string key, PaymentDto payment, CancellationToken ct)
    {
        _items[key] = payment;
        return Task.CompletedTask;
    }
}

sealed class FakeLedgerClient : ILedgerClient
{
    public Task<Guid> PostDoubleEntryAsync(Guid paymentId, Guid merchantId, decimal amount, string currency, CancellationToken ct)
        => Task.FromResult(Guid.NewGuid());
}
