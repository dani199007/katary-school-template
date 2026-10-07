using System.Collections.Concurrent;
using KataryApi.Models;

namespace KataryApi.Data;

// Almacén en memoria: se reinicia cada vez que corres la API.
public class TareaStore
{
    private readonly ConcurrentDictionary<int, Tarea> _tareas = new();
    private int _nextId;

    public TareaStore()
    {
        Add(new Tarea(0, "Comprar pan", false));
        Add(new Tarea(0, "Terminar la tarea de matemáticas", false));
    }

    public IEnumerable<Tarea> GetAll() => _tareas.Values.OrderBy(t => t.Id);

    public Tarea Add(Tarea tarea)
    {
        var created = tarea with { Id = Interlocked.Increment(ref _nextId) };
        _tareas[created.Id] = created;
        return created;
    }

    public bool Remove(int id) => _tareas.TryRemove(id, out _);
}
