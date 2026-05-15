using ShoppingListCLI.Core.Storage;
using ShoppingListCLI.ShoppingListCLI.Core.Models;
using ShoppingListCLI.ShoppingListCLI.Core.Services;

namespace ShoppingListCLI.ShoppingListCLI.Core.Menus.Options;

internal class ViewItemsOption(IStorage storage) : IOption
{
    private readonly IStorage _storage = storage;

    public async Task Open()
    {
        Console.Clear();

        Console.WriteLine("================================\n" +
                  "Bevásárlólisták (ABC sorrendben)\n" +
                  "================================\n\n");

        List<ShoppingList> shoppingLists = await _storage.LoadAsync();

        ViewItemsOptionService.
                ShowUserShoppingLists(shoppingLists);

        Console.WriteLine("Nyomj meg egy gombot a visszalépéshez...");
        Console.ReadKey();
        Menu.ShowMenu();
    }
}
