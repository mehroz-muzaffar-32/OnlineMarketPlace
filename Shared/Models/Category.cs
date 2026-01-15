using Torico.Shared.Utils;

namespace Torico.Shared.Models;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    // Relationship : One to Many
    public List<Product>? Products { get; set; }
}
