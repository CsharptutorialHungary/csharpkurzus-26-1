using ShoppingListCLI.ShoppingListCLI.Core.Models;

namespace ShoppingListCLI.Core.Storage;

public interface IStorage
{
    public Task SaveAsync(List<ShoppingList> shoppingList);

    public Task<List<ShoppingList>> LoadAsync();
}
