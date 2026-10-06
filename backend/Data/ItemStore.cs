using System.Collections.Concurrent;
using KataryApi.Models;

namespace KataryApi.Data;

// Almacén en memoria: se reinicia cada vez que corres la API.
// Reto para casa: reemplázalo por una base de datos con Entity Framework Core.
public class ItemStore
{
    private readonly ConcurrentDictionary<int, Item> _items = new();
    private int _nextId;

    public ItemStore()
    {
        // 👉 PASO .NET: reemplaza estos datos de ejemplo por los de TU idea.
        Seed(new Item(0, "Café Nariño 500 g", "Notas a panela y cítricos, tostión media.", "Café", 42000));
        Seed(new Item(0, "Café Huila 250 g", "Dulce, cuerpo medio, ideal para método V60.", "Café", 28000));
        Seed(new Item(0, "Prensa francesa", "Cafetera de 600 ml en vidrio y acero.", "Accesorios", 89000));
        Seed(new Item(0, "Molino manual", "Molienda ajustable en cerámica.", "Accesorios", 120000));
    }

    public IEnumerable<Item> GetAll() => _items.Values.OrderBy(i => i.Id);

    public Item? Get(int id) => _items.GetValueOrDefault(id);

    public Item Add(Item item)
    {
        var created = item with { Id = Interlocked.Increment(ref _nextId) };
        _items[created.Id] = created;
        return created;
    }

    public bool Remove(int id) => _items.TryRemove(id, out _);

    private void Seed(Item item) => Add(item);
}
