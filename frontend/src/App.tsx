import { useEffect, useState } from "react";
import { api } from "./api";
import { config } from "./config";
import type { Tarea } from "./types";
import { AgentChat } from "./components/AgentChat";
import { TareaForm } from "./components/TareaForm";
import { TareaList } from "./components/TareaList";

export default function App() {
  const [tareas, setTareas] = useState<Tarea[]>([]);
  const [error, setError] = useState<string | null>(null);

  const load = () =>
    api
      .getTareas()
      .then((data) => {
        setTareas(data);
        setError(null);
      })
      .catch(() => setError("No pudimos conectar con la API. ¿Está corriendo `dotnet run` en backend/?"));

  useEffect(() => {
    load();
  }, []);

  return (
    <div style={{ ["--accent" as string]: config.accentColor }}>
      <header className="hero">
        <div className="container">
          <p className="kicker">Katary Dev School</p>
          <h1>{config.brandName}</h1>
          <p className="tagline">{config.tagline}</p>
        </div>
      </header>

      <main className="container layout">
        <section>
          <h2>{config.itemsTitle}</h2>
          {error && <p className="error">{error}</p>}
          <TareaList tareas={tareas} onDelete={(id) => api.borrarTarea(id).then(load)} />
          <TareaForm onCreated={load} />
        </section>
        <AgentChat />
      </main>
    </div>
  );
}