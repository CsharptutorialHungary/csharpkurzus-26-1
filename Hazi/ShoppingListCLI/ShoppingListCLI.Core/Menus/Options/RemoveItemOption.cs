using ShoppingListCLI.Core.Storage;
using ShoppingListCLI.ShoppingListCLI.Core.Models;
using ShoppingListCLI.ShoppingListCLI.Core.Services;

namespace ShoppingListCLI.ShoppingListCLI.Core.Menus.Options;

internal class RemoveItemOption(IStorage storage) : IOption
{

    private readonly IStorage _storage = storage;

    public async Task Open()
    {
        Console.Clear();

        Console.WriteLine("===========================\n" +
                  "Bevásárlólista törlése\n" +
                  "===========================");

        List<ShoppingList> shoppingLists = await _storage.LoadAsync();

        shoppingLists = RemoveItemOptionService.ReadUserRemoveShoppingListInput(shoppingLists);

        await _storage.SaveAsync(shoppingLists);
            
       
        Console.WriteLine("Nyomj meg egy gombot a visszalépéshez...");
        Console.ReadKey();
        Menu.ShowMenu();
    }
}
