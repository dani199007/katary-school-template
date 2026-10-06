# Instrucciones para el asistente de código

Este proyecto es parte de una clase de Katary School. Quien te escribe está aprendiendo: explica en español, en pocas palabras, qué vas a hacer y por qué.

## Antes de cualquier tarea
1. Lee **IDEA.md**: describe el proyecto del estudiante (qué es, sus elementos, el "dato extra", nombre, eslogan y color). Todo lo que construyas debe seguir esa idea.
2. Si IDEA.md está vacío o incompleto, pídele al estudiante que lo complete antes de cambiar código.

## El proyecto
- `backend/`: API en .NET 9 (Minimal API). Corre con `dotnet run` en http://localhost:5080.
  - `Models/Item.cs`: el modelo (un `record`). `Data/ItemStore.cs`: datos de ejemplo en memoria.
  - `Program.cs`: endpoints `/api/items` y `/api/agent` (chat del producto).
- `frontend/`: React + Vite + TypeScript. Corre con `npm run dev` en http://localhost:5173.
  - `src/types.ts` debe coincidir con `Item.cs` en camelCase (`Stock` → `stock`).
  - `src/config.ts`: nombre, eslogan y color. `src/components/ItemList.tsx`: las tarjetas.

## Reglas
- Haz **cambios pequeños** y solo en los archivos que pide la tarea.
- Después de cambiar el backend ejecuta `dotnet build` en `backend/`; después de cambiar el frontend ejecuta `npm run build` en `frontend/`. Corrige los errores antes de terminar.
- No inicies servidores de larga duración (`dotnet run`, `npm run dev`): el estudiante los corre en sus propias terminales.
- **Nunca** escribas API keys ni contraseñas en archivos. Los secretos van con `dotnet user-secrets`.
- No hagas `git push` ni cambies de rama salvo que te lo pidan. Antes de un commit, resume los cambios.
- Pide confirmación antes de instalar software o borrar archivos.
- Al terminar, di qué archivos cambiaste y cómo verificarlo.
