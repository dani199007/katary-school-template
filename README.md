# Katary School — Plantilla

Proyecto base para la sesión práctica: **API .NET 9 + React (Vite)**, pensado para que lo adaptes a tu idea **dirigiendo a tu asistente de código** (OpenCode, Claude Code o Codex).

## Trabaja con tu asistente de código

1. Abre tu asistente **dentro de esta carpeta** (`opencode`, `claude` o `codex`).
2. Completa **IDEA.md** con su ayuda: es el contexto de tu proyecto.
3. **AGENTS.md** le dice a tu asistente que lea IDEA.md antes de cada tarea y qué reglas seguir (Claude Code lo lee a través de **CLAUDE.md**).
4. Pide cambios pequeños, pide que compile para verificar y **haz commit** después de cada paso.

¿No tienes las herramientas? Sigue la guía de preparación de la clase: https://school.katary.co/preparacion

```
AGENTS.md  Reglas para tu asistente de código (CLAUDE.md lo importa)
IDEA.md    Tu idea: el contexto de tu asistente
backend/   Minimal API en C#  → http://localhost:5080
frontend/  React + TypeScript → http://localhost:5173
```

## Requisitos
- Git, Node.js 20+, .NET SDK 9
- Un asistente de código: OpenCode (gratis), Claude Code o Codex
- (Bonus, opcional) Una cuenta de Google para la API key gratis de Gemini

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

## Bonus: el asistente de tu producto (chat con IA gratis)

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

## ¿Cómo funciona el chat del producto?

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
