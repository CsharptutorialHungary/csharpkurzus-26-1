using ShoppingListCLI.ShoppingListCLI.Core.Models;

namespace ShoppingListCLI.ShoppingListCLI.Core.Services;

internal static class RemoveItemOptionService
{

    internal static List<ShoppingList> ReadUserRemoveShoppingListInput(List<ShoppingList> shoppingLists)
    {
        Console.Write("Add meg a törölni kívánt bevásárlólista nevét: ");

        string? shoppingListName = Console.ReadLine();
        if (shoppingListName == null || shoppingListName.Equals(""))
        {
            Console.WriteLine("A bevásárlólista neve nem lehet üres!\n" +
                "Próbáld újra!");
            return ReadUserRemoveShoppingListInput(shoppingLists);
        }

        List<ShoppingList> shoppingListToRemove = shoppingLists.Where(list => list.Name == shoppingListName).ToList();

        if (shoppingListToRemove.Count != 0)
        {

            foreach (var item in shoppingListToRemove)
            {
                shoppingLists.Remove(item);
                Console.WriteLine("A bevásárlólista sikeresen törölve.");
            }
        }
        else
        {
            Console.WriteLine("Nem található ilyen nevű bevásárlólista.");
            return ReadUserRemoveShoppingListInput(shoppingLists);
        }

        return shoppingLists;
    }
}