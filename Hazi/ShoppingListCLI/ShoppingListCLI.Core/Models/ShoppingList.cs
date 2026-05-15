namespace ShoppingListCLI.ShoppingListCLI.Core.Models;

public record class ShoppingList
{
    public string? Name { get; set; }
    public List<Item>? Items { get; set; }
}
