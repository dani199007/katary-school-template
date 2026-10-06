using KataryApi.Agent;
using KataryApi.Data;
using KataryApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Permite que el frontend de Vite (puerto 5173) llame a esta API.
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddSingleton<ItemStore>();
builder.Services.Configure<AgentOptions>(builder.Configuration.GetSection("Agent"));
builder.Services.AddHttpClient<AgentService>();

var app = builder.Build();
app.UseCors();

app.MapGet("/", () => "Katary Dev School API funcionando 🚀  Prueba /api/items");

// ---------- Items: los elementos de tu proyecto ----------
app.MapGet("/api/items", (ItemStore store) => store.GetAll());

app.MapGet("/api/items/{id:int}", (int id, ItemStore store) =>
    store.Get(id) is { } item ? Results.Ok(item) : Results.NotFound());

app.MapPost("/api/items", (Item item, ItemStore store) =>
{
    if (string.IsNullOrWhiteSpace(item.Name))
        return Results.BadRequest("El nombre es obligatorio.");
    var created = store.Add(item);
    return Results.Created($"/api/items/{created.Id}", created);
});

app.MapDelete("/api/items/{id:int}", (int id, ItemStore store) =>
    store.Remove(id) ? Results.NoContent() : Results.NotFound());

// ---------- Agente de IA ----------
app.MapPost("/api/agent", async (ChatRequest request, AgentService agent, CancellationToken ct) =>
{
    try
    {
        var reply = await agent.AskAsync(request.Messages, ct);
        return Results.Ok(new { reply });
    }
    catch (AgentException ex)
    {
        return Results.Problem(ex.Message, statusCode: 502);
    }
});

app.Run();
