using ShoppingListCLI.ShoppingListCLI.Core.Models;

namespace ShoppingListCLI.ShoppingListCLI.Core.Services;

internal static class AddItemOptionService
{

    internal static ShoppingList ReadUserShoppingListInput()
    {
        Console.Write("Add meg a bevásárlólista nevét: ");

        string? shoppingListName = Console.ReadLine();
        if (shoppingListName == null || shoppingListName.Equals(""))
        {
            Console.WriteLine("A bevásárlólista neve nem lehet üres!\n" +
                "Próbáld újra!");
            return ReadUserShoppingListInput();
        }

        ShoppingList shoppingList = new ShoppingList();
        shoppingList.Items = new List<Item>();
        shoppingList.Name = shoppingListName;
        bool isAddingItems = true;
        while (isAddingItems)
        {
            Console.Write("Szeretnél elemet adni a bevásárlólistához(I/n): ");
            string? input = Console.ReadLine()?.ToLower();
            if (input == "i")
            {
                var item = ReadUserItemInput();
                shoppingList.Items.Add(item);
            }
            else if (input == "n")
            {
                isAddingItems = false;
            }
            else
            {
                Console.WriteLine("Érvénytelen input! Kérem válassz 'I' vagy 'N' opciót!");
            }
        }
        return shoppingList;
    }

    private static Item ReadUserItemInput()
    {
        Console.Write("Add meg az elem nevét: ");
        string? itemName = Console.ReadLine();
        if (itemName == null || itemName.Equals(""))
        {
            Console.WriteLine("Az elem neve nem lehet üres!\n" +
                "Próbáld újra!");
            return ReadUserItemInput();
        }

        Console.Write("Add meg az elem mennyiségét(db/g): ");
        string? itemQuantity = Console.ReadLine();
        if (itemQuantity == null || itemQuantity.Equals("") || !int.TryParse(itemQuantity, out _))
        {
            Console.WriteLine("Az elem mennyisége nem lehet üres és csak szám lehet!\n" +
                "Próbáld újra!");
            return ReadUserItemInput();
        }

        Item item = new Item();
        item.ItemName = itemName;
        item.Quantity = int.Parse(itemQuantity);
        return item;
    }
}