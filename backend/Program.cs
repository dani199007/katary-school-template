using KataryApi.Agent;
using KataryApi.Data;
using KataryApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Permite que el frontend de Vite (puerto 5173) llame a esta API.
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddSingleton<TareaStore>();
builder.Services.Configure<AgentOptions>(builder.Configuration.GetSection("Agent"));
builder.Services.AddHttpClient<AgentService>();

var app = builder.Build();
app.UseCors();

app.MapGet("/", () => "Mi lista de tareas - API funcionando. Prueba /api/tareas");

// ---------- Tareas: los elementos de tu idea ----------
app.MapGet("/api/tareas", (TareaStore store) => store.GetAll());

app.MapPost("/api/tareas", (Tarea tarea, TareaStore store) =>
{
    if (string.IsNullOrWhiteSpace(tarea.Texto))
        return Results.BadRequest("El texto de la tarea es obligatorio.");
    var created = store.Add(tarea);
    return Results.Created($"/api/tareas/{created.Id}", created);
});

app.MapDelete("/api/tareas/{id:int}", (int id, TareaStore store) =>
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
