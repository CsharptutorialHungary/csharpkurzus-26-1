namespace ShoppingListCLI.ShoppingListCLI.Core.Models;

public record class Item
{
    public string? ItemName { get; set; }
    public int? Quantity { get; set; }
}
