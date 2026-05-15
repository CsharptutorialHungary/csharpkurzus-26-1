using ShoppingListCLI.ShoppingListCLI.Core.Models;

namespace ShoppingListCLI.ShoppingListCLI.Core.Services;

internal static class ViewItemsOptionService
{

    internal static void ShowUserShoppingLists(List<ShoppingList> shoppingLists)
    {
        shoppingLists = shoppingLists.OrderBy(list => list.Name).ToList();

        foreach (ShoppingList list in shoppingLists)
        {
            Console.WriteLine("Bevásárlólista neve: " + list.Name);
            Console.WriteLine("Elemek:");
            if (list.Items != null) 
            {
                foreach (Item item in list.Items)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("     -Elem neve: " + item.ItemName);
                    Console.WriteLine("     -Elem mennyiség(db/g): " + item.Quantity.ToString());
                }
                Console.WriteLine("==============================");
            }
        }
    }
}