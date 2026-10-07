import { useState, type FormEvent } from "react";
import { api } from "../api";

export function TareaForm({ onCreated }: { onCreated: () => void }) {
  const [texto, setTexto] = useState("");
  const [saving, setSaving] = useState(false);

  const submit = async (e: FormEvent) => {
    e.preventDefault();
    const value = texto.trim();
    if (!value) return;
    setSaving(true);
    try {
      await api.crearTarea({ texto: value, completada: false });
      setTexto("");
      onCreated();
    } catch (err) {
      alert(err instanceof Error ? err.message : "No se pudo guardar");
    } finally {
      setSaving(false);
    }
  };

  return (
    <form className="card form" onSubmit={submit}>
      <h3>Agregar tarea</h3>
      <input placeholder="¿Qué tienes pendiente?" value={texto} onChange={(e) => setTexto(e.target.value)} required />
      <button className="primary" disabled={saving || !texto.trim()}>
        {saving ? "Guardando…" : "Agregar"}
      </button>
    </form>
  );
}