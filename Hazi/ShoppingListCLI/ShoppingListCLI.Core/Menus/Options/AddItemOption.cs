using ShoppingListCLI.Core.Storage;
using ShoppingListCLI.ShoppingListCLI.Core.Models;
using ShoppingListCLI.ShoppingListCLI.Core.Services;

namespace ShoppingListCLI.ShoppingListCLI.Core.Menus.Options;

public class AddItemOption(IStorage storage) : IOption
{
    private readonly IStorage _storage = storage;

    public async Task Open()
    {
        Console.Clear();

        Console.WriteLine("===========================\n" +
                  "Új bevásárlólista létrehozása\n" +
                  "===========================");

        List<ShoppingList> shoppingLists = await _storage.LoadAsync();

        ShoppingList shoppingList = AddItemOptionService.ReadUserShoppingListInput();

        shoppingLists.Add(shoppingList);
        await _storage.SaveAsync(shoppingLists);
        Console.WriteLine("Sikeres hozzáadás!");
        Console.WriteLine("Nyomj meg egy gombot a visszalépéshez...");
        Console.ReadKey();
        Menu.ShowMenu();
    }
}
