import { useEffect, useState } from "react";
import { api } from "./api";
import { config } from "./config";
import type { Item } from "./types";
import { AgentChat } from "./components/AgentChat";
import { ItemForm } from "./components/ItemForm";
import { ItemList } from "./components/ItemList";

export default function App() {
  const [items, setItems] = useState<Item[]>([]);
  const [error, setError] = useState<string | null>(null);

  const load = () =>
    api
      .getItems()
      .then((data) => {
        setItems(data);
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
          <ItemList items={items} onDelete={(id) => api.deleteItem(id).then(load)} />
          <ItemForm onCreated={load} />
        </section>
        <AgentChat />
      </main>
    </div>
  );
}
