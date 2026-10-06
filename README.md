# Katary Dev School — Template

Proyecto base para la sesión práctica: **API .NET 9 + React (Vite) + agente de IA**.
Adáptalo a tu idea: tienda, portafolio, recetario, lo que quieras.

```
backend/   Minimal API en C#  → http://localhost:5080
frontend/  React + TypeScript → http://localhost:5173
IDEA.md    Tu idea en 3 líneas
```

## Requisitos
- Git, Node.js 20+, .NET SDK 9
- Una cuenta de Google (API key gratis de Gemini)

## Correr el proyecto

```bash
# Terminal 1 — API
cd backend
dotnet run

# Terminal 2 — Frontend
cd frontend
npm install
cp .env.example .env
npm run dev
```

## Conectar el agente de IA (gratis)

### Opción A — Google Gemini (recomendada)
1. Crea tu key en https://aistudio.google.com/apikey
2. En `backend/`:
   ```bash
   dotnet user-secrets set "Agent:ApiKey" "TU_KEY"
   ```

### Opción B — Groq
1. Crea tu key en https://console.groq.com/keys
2. En `backend/`:
   ```bash
   dotnet user-secrets set "Agent:ApiKey" "TU_KEY_DE_GROQ"
   dotnet user-secrets set "Agent:BaseUrl" "https://api.groq.com/openai/v1/"
   dotnet user-secrets set "Agent:Model" "llama-3.3-70b-versatile"
   ```

Reinicia la API después de guardar la key. **Nunca** pongas la key en `appsettings.json` ni hagas commit de ella.

## Dónde personalizar

| Qué | Archivo |
|---|---|
| Modelo de datos | `backend/Models/Item.cs` |
| Datos de ejemplo | `backend/Data/ItemStore.cs` |
| Personalidad del agente | `backend/appsettings.json` → `Agent:SystemPrompt` |
| Lógica del agente | `backend/Agent/AgentService.cs` |
| Nombre, eslogan y color | `frontend/src/config.ts` |
| Tipo `Item` en TypeScript | `frontend/src/types.ts` |
| Tarjeta de cada elemento | `frontend/src/components/ItemList.tsx` |

Para probar la API sin frontend usa `backend/KataryApi.http` (VS Code + extensión REST Client, o Rider/Visual Studio).

## ¿Cómo funciona el agente?

`POST /api/agent` recibe la conversación, y `AgentService`:
1. Toma tu **system prompt** (la personalidad).
2. Agrega tu **catálogo actual** en JSON (grounding: el modelo responde con tus datos reales).
3. Envía todo a `{BaseUrl}/chat/completions` (formato compatible con OpenAI, así que sirve para Gemini, Groq y otros).

## ¿Dónde se guardan los datos?

En **memoria** (`backend/Data/ItemStore.cs`): los elementos viven mientras la API está corriendo y vuelven a los datos de ejemplo al reiniciarla. Nada sale de tu computador. Conectar una base de datos real es uno de los retos para casa.

## Retos para casa
1. Persistencia: guarda los items en una base de datos (SQLite o PostgreSQL) con Entity Framework Core.
2. Tools / function calling: que el agente pueda crear elementos.
3. Historial de chat por usuario.
4. Deploy del frontend y la API.
