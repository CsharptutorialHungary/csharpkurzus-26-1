using ShoppingListCLI.Core.Storage;
using ShoppingListCLI.ShoppingListCLI.Core.Menus.Options;

namespace ShoppingListCLI.ShoppingListCLI.Core.Menus;

public class Menu
{
    internal static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=========================\n" +
                          "ShoppingListCLI\n" +
                          "=========================");

        Console.WriteLine("1 - Új bevásárlólista");
        Console.WriteLine("2 - Bevásárlólisták megnyitása");
        Console.WriteLine("3 - Bevásárlólista törlés");
        Console.WriteLine("4 - Kilépés");
        Console.Write("Válassz egy opciót(szám): ");
    }

    public static async Task Start()
    {
        IStorage storage = new JsonStorage();
        ShowMenu();

        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            bool isValidInput = Enum.TryParse<MenuOptions>(input, out MenuOptions option);
            if (!isValidInput)
            {
                Console.WriteLine("Hibás input, próbáld újra!");
            }

            if (option == MenuOptions.Exit)
            {
                return;
            }

            MenuOptionsExtensions.MenuOption(option, storage).Open().Wait();
        }
    }
}
