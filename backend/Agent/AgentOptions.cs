namespace KataryApi.Agent;

// Se llena desde appsettings.json (sección "Agent") y user-secrets (ApiKey).
public class AgentOptions
{
    // Cualquier proveedor compatible con la API de OpenAI:
    //   Gemini: https://generativelanguage.googleapis.com/v1beta/openai/
    //   Groq:   https://api.groq.com/openai/v1/
    public string BaseUrl { get; set; } = "https://generativelanguage.googleapis.com/v1beta/openai/";
    public string Model { get; set; } = "gemini-2.5-flash";
    public string ApiKey { get; set; } = "";
    public string SystemPrompt { get; set; } = "Eres un asistente útil.";
}
