// Debe coincidir con backend/Models/Tarea.cs (en camelCase).
export type Tarea = {
  id: number;
  texto: string;
  completada: boolean;
};

export type NuevaTarea = Omit<Tarea, "id">;

export type ChatMessage = { role: "user" | "assistant"; content: string };