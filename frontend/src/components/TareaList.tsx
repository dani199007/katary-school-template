import type { Tarea } from "../types";

export function TareaList({ tareas, onDelete }: { tareas: Tarea[]; onDelete: (id: number) => void }) {
  if (!tareas.length) return <p className="muted">Aún no hay tareas. ¡Agrega la primera!</p>;

  return (
    <ul className="grid">
      {tareas.map((tarea) => (
        <li key={tarea.id} className="card">
          <span className="badge">{tarea.completada ? "✓ Hecha" : "Pendiente"}</span>
          <h3>{tarea.texto}</h3>
          <div className="card-footer">
            <button className="link" onClick={() => onDelete(tarea.id)}>
              Eliminar
            </button>
          </div>
        </li>
      ))}
    </ul>
  );
}