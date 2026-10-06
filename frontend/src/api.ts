import type { ChatMessage, Item, NewItem } from "./types";

const API_URL = import.meta.env.VITE_API_URL ?? "http://localhost:5080";

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const res = await fetch(`${API_URL}${path}`, {
    headers: { "Content-Type": "application/json" },
    ...init,
  });
  if (!res.ok) {
    const text = await res.text();
    let message = text;
    try {
      message = JSON.parse(text).detail ?? text;
    } catch {
      /* respuesta no JSON */
    }
    throw new Error(message || `Error ${res.status}`);
  }
  return res.status === 204 ? (undefined as T) : res.json();
}

export const api = {
  getItems: () => request<Item[]>("/api/items"),
  createItem: (item: NewItem) => request<Item>("/api/items", { method: "POST", body: JSON.stringify(item) }),
  deleteItem: (id: number) => request<void>(`/api/items/${id}`, { method: "DELETE" }),
  askAgent: (messages: ChatMessage[]) =>
    request<{ reply: string }>("/api/agent", { method: "POST", body: JSON.stringify({ messages }) }),
};
