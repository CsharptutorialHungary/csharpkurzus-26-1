internal class Program
{
    private static void Main(string[] args)
    {
        Program program = new Program();
        program.Run();
    }

    private void Run()
    {
        Array<string>ingredients = new Array<string>(2);
        ConsoleColor originalForeground = Console.ForegroundColor;

        Console.WriteLine("Welcome to the Alchemical Laboratory!");
    }

    private void Beginning() {
        Console.WriteLine("Combine two elements, and you might just create something new!");
        Console.WriteLine("Please choose the first ingredient by either typing its ID or its name:");

        foreach (var element in FoundElementDB.Instance.Elements)
        {
            Console.ForegroundColor = element.color;
            Console.WriteLine($"{element.id}: {element.name}");
        }
        Console.ForegroundColor = originalForeground;

        Console.WriteLine("Press 'X' to view your element collection.\n
        Press 'Q' to quit the game.");

        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.X:
                ViewCollection();
                break;
            case ConsoleKey.Q:
                Console.WriteLine("Goodbye!");
                return;
            default:
                CombineElements();
                break;
        }
    }

    private void ViewCollection() {
        Console.WriteLine("Your Element Collection:");
        foreach (var element in FoundElementDB.Instance.Elements)
        {
            Console.ForegroundColor = element.color;
            Console.WriteLine($"{element.id}: {element.name}");
        }
        Console.ForegroundColor = originalForeground;
        Console.WriteLine("Press S to save your collection, L to load a saved collection, or any other key to return to the lab.");
        if (Console.ReadKey(true).Key == ConsoleKey.S)
        {
            FileHandler.SaveCollection();
        } else if (Console.ReadKey(true).Key == ConsoleKey.L) {
            FileHandler.LoadCollection();
        } else {
            Beginning();
        }
    }

    private void GiveIngredientOne() {

    }
}







ingredients[0] = Console.ReadLine() ?? string.Empty;
if (int.TryParse(ingredients[0], out int elementId))
{
    if (FoundElementDB.Instance.IsElementFound(elementId))
    {
        ingredients[0] = ElementDB.Instance.GetElementById(elementId)?.name ?? string.Empty;
    }
    else
    {
        Console.WriteLine("You haven't found that element yet!");
        return;
    }
}

Console.WriteLine("Please choose the second ingredient!");
ingredients[1] = Console.ReadLine() ?? string.Empty;
if (int.TryParse(ingredients[1], out int elementId))
{
    if (FoundElementDB.Instance.IsElementFound(elementId))
    {
        ingredients[1] = ElementDB.Instance.GetElementById(elementId)?.name ?? string.Empty;
    }
    else
    {
        Console.WriteLine("You haven't found that element yet!");
        return;
    }
}

Element result = from element in ElementDB.Instance.Elements
                 where (element.firstIngredient == ingredients[0] && element.secondIngredient == ingredients[1]) ||
                       (element.firstIngredient == ingredients[1] && element.secondIngredient == ingredients[0])
                 select element;

if (result.FirstOrDefault() != null) {
    if (!FoundElementDB.Instance.IsElementFound(result.FirstOrDefault().id))
    {
        Console.WriteLine("Congratulations! You have discovered a new element:");
        Console.ForegroundColor = result.FirstOrDefault().color;
        Console.WriteLine(result.FirstOrDefault().name);
        FoundElementDB.Instance.AddFoundElement(result.FirstOrDefault().id);
    } else {
        Console.WriteLine("You have already discovered that element!");
    }

} else {
    Console.WriteLine("...Looks like that was a dud... Try again!");
}
