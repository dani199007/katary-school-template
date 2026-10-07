using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using KataryApi.Data;
using Microsoft.Extensions.Options;

namespace KataryApi.Agent;

public record ChatMessage(string Role, string Content);
public record ChatRequest(List<ChatMessage> Messages);

public class AgentException(string message) : Exception(message);

// El "agente": arma el contexto (personalidad + catálogo + conversación)
// y se lo envía al LLM por HTTP en formato compatible con OpenAI.
public class AgentService(HttpClient http, IOptions<AgentOptions> options, TareaStore store)
{
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);
    private readonly AgentOptions _options = options.Value;

    public async Task<string> AskAsync(List<ChatMessage> conversation, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(_options.ApiKey))
            throw new AgentException(
                "Falta la API key. Ejecuta en backend/: dotnet user-secrets set \"Agent:ApiKey\" \"TU_KEY\" y reinicia la API.");

        // Grounding: le damos al modelo los datos reales de tu proyecto en cada llamada.
        var catalog = JsonSerializer.Serialize(store.GetAll(), Json);
        var system = $"""
            {_options.SystemPrompt}

            Estos son los elementos actuales del proyecto (JSON). Responde usando solo esta información
            cuando te pregunten por ellos; si algo no está aquí, dilo con honestidad.
            {catalog}
            """;

        var messages = new List<object> { new { role = "system", content = system } };
        // Solo los últimos 10 mensajes para no gastar tokens de más.
        messages.AddRange(conversation.TakeLast(10).Select(m => new { role = m.Role, content = m.Content }));

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.BaseUrl.TrimEnd('/')}/chat/completions")
        {
            Content = JsonContent.Create(new { model = _options.Model, messages, temperature = 0.4 }),
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);

        using var response = await http.SendAsync(request, ct);
        var body = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            if ((int)response.StatusCode is 401 or 403 || body.Contains("API key", StringComparison.OrdinalIgnoreCase))
                throw new AgentException("Tu API key no es válida. Revísala y vuelve a guardarla con dotnet user-secrets.");
            throw new AgentException($"El proveedor de IA respondió {(int)response.StatusCode}: {body}");
        }

        var completion = JsonSerializer.Deserialize<Completion>(body, Json);
        return completion?.Choices.FirstOrDefault()?.Message.Content?.Trim()
            ?? throw new AgentException("El modelo no devolvió respuesta.");
    }

    private record Completion([property: JsonPropertyName("choices")] List<Choice> Choices);
    private record Choice([property: JsonPropertyName("message")] CompletionMessage Message);
    private record CompletionMessage([property: JsonPropertyName("content")] string? Content);
}
