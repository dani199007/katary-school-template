# Instrucciones para el asistente de código

Este proyecto es parte de una clase de Katary School. Quien te escribe está aprendiendo: explica en español, en pocas palabras, qué vas a hacer y por qué.

## Antes de cualquier tarea
1. Lee **IDEA.md**: describe el proyecto del estudiante (qué es, sus elementos, el "dato extra", nombre, eslogan y color). Todo lo que construyas debe seguir esa idea.
2. Si IDEA.md está vacío o incompleto, pídele al estudiante que lo complete antes de cambiar código.
3. El código de ejemplo puede ser todavía la plantilla vieja ("Mi lista de tareas"): IDEA.md manda, adapta el código a ella.

## El proyecto
- `backend/`: API en .NET 9 (Minimal API). Puerto fijo **http://localhost:5080** (`Properties/launchSettings.json`).
  - Modelos en `Models/`, datos de ejemplo en memoria en `Data/` (se borran al reiniciar).
  - `Program.cs`: todos los endpoints (comprueba las rutas reales ahí, no las supongas) y `POST /api/agent` (chat del producto).
  - El agente se configura en `appsettings.json` → `Agent:*`; la API key va con `dotnet user-secrets` (nunca en el repo).
- `frontend/`: React + Vite + TypeScript. Puerto **5173** (`strictPort` en `vite.config.ts`).
  - CORS en la API solo permite `http://localhost:5173`.
  - `src/api.ts` usa `VITE_API_URL`; copia `frontend/.env.example` a `frontend/.env` si no existe.
  - `src/types.ts` debe coincidir con los modelos del backend en camelCase (`Stock` → `stock`).
  - `src/config.ts`: nombre, eslogan y color (deben coincidir con IDEA.md).

## Verificación (no hay lint, tests ni CI)
- Backend: `dotnet build` dentro de `backend/`.
- Frontend: `npm run build` dentro de `frontend/` (ejecuta `tsc -b`, así que también hace typecheck).
- Corrige los errores antes de terminar. No inventes comandos de lint/test que no existen.

## Reglas
- Haz **cambios pequeños** y solo en los archivos que pide la tarea.
- No inicies servidores de larga duración (`dotnet run`, `npm run dev`): el estudiante los corre en sus propias terminales.
- **Nunca** escribas API keys ni contraseñas en archivos. Los secretos van con `dotnet user-secrets`.
- No hagas `git push` ni cambies de rama salvo que te lo pidan. Antes de un commit, resume los cambios.
- Pide confirmación antes de instalar software o borrar archivos.
- Al terminar, di qué archivos cambiaste y cómo verificarlo.

CLAUDE.md solo importa este archivo (`@AGENTS.md`); mantenlo aquí.
