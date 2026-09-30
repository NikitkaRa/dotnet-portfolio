using SynapseDesk.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ILlmClient, EchoLlmClient>();
builder.Services.AddSingleton<TicketTool>();
builder.Services.AddSingleton<AgentOrchestrator>();

var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.MapPost("/api/agent/chat", async (ChatRequest req, AgentOrchestrator agent, CancellationToken ct) =>
{
    var reply = await agent.ChatAsync(req.Message, ct);
    return Results.Ok(new { reply });
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.Run();

record ChatRequest(string Message);
