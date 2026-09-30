namespace AuroraPay.Contracts;

public sealed record CreatePaymentRequest(
    string IdempotencyKey,
    Guid MerchantId,
    decimal Amount,
    string Currency,
    string? Description);

public sealed record PaymentDto(
    Guid Id,
    Guid MerchantId,
    decimal Amount,
    string Currency,
    string Status,
    DateTimeOffset CreatedAt);

public interface ILedgerClient
{
    Task<Guid> PostDoubleEntryAsync(
        Guid paymentId,
        Guid merchantId,
        decimal amount,
        string currency,
        CancellationToken ct);
}
