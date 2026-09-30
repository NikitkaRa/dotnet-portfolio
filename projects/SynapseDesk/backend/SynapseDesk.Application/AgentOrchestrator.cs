namespace SynapseDesk.Application;

public sealed record AgentMessage(string Role, string Content);

public sealed record AgentToolCall(string Name, string ArgumentsJson);

public interface ILlmClient
{
    Task<AgentMessage> CompleteAsync(IReadOnlyList<AgentMessage> history, CancellationToken ct);
}

public sealed class TicketTool
{
    public Task<string> CreateAsync(string title, string body, CancellationToken ct)
        => Task.FromResult($"TICKET-{Random.Shared.Next(1000, 9999)}: {title}");
}

/// <summary>Минимальный оркестратор AI-агента с tool-calling.</summary>
public sealed class AgentOrchestrator(ILlmClient llm, TicketTool tickets)
{
    public async Task<string> ChatAsync(string userText, CancellationToken ct)
    {
        var history = new List<AgentMessage>
        {
            new("system", "You are SynapseDesk assistant. You may create support tickets."),
            new("user", userText)
        };

        if (userText.Contains("тикет", StringComparison.OrdinalIgnoreCase) ||
            userText.Contains("ticket", StringComparison.OrdinalIgnoreCase))
        {
            var id = await tickets.CreateAsync("User request", userText, ct);
            return $"Создал тикет {id}";
        }

        var reply = await llm.CompleteAsync(history, ct);
        return reply.Content;
    }
}

public sealed class EchoLlmClient : ILlmClient
{
    public Task<AgentMessage> CompleteAsync(IReadOnlyList<AgentMessage> history, CancellationToken ct)
    {
        var last = history.LastOrDefault(m => m.Role == "user")?.Content ?? "";
        return Task.FromResult(new AgentMessage("assistant", $"Echo: {last}"));
    }
}
