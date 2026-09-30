using AuroraPay.Contracts;
using MediatR;

namespace AuroraPay.Payments;

public sealed record CreatePaymentCommand(CreatePaymentRequest Request) : IRequest<PaymentDto>;

public sealed class CreatePaymentHandler(
    IPaymentStore store,
    IIdempotencyStore idempotency,
    ILedgerClient ledger) : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    public async Task<PaymentDto> Handle(CreatePaymentCommand command, CancellationToken ct)
    {
        var req = command.Request;

        var existing = await idempotency.GetAsync(req.IdempotencyKey, ct);
        if (existing is not null)
            return existing;

        var payment = new PaymentDto(
            Guid.NewGuid(),
            req.MerchantId,
            req.Amount,
            req.Currency,
            "pending",
            DateTimeOffset.UtcNow);

        await store.InsertAsync(payment, ct);
        await ledger.PostDoubleEntryAsync(payment.Id, payment.MerchantId, payment.Amount, payment.Currency, ct);
        await idempotency.SetAsync(req.IdempotencyKey, payment, ct);

        return payment with { Status = "posted" };
    }
}

public interface IPaymentStore
{
    Task InsertAsync(PaymentDto payment, CancellationToken ct);
}

public interface IIdempotencyStore
{
    Task<PaymentDto?> GetAsync(string key, CancellationToken ct);
    Task SetAsync(string key, PaymentDto payment, CancellationToken ct);
}
