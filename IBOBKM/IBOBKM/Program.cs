Array<string>ingredients = new Array<string>(2);
ConsoleColor originalForeground = Console.ForegroundColor;

Console.WriteLine("Welcome to the Alchemical Laboratory!\n
Combine two elements, and you might just create something new!\n
Please choose the first ingredient by either typing its ID or its name:");
foreach (var element in FoundElementDB.Instance.Elements)
{
    Console.ForegroundColor = element.color;
    Console.WriteLine($"{element.id}: {element.name}");
}
Console.ForegroundColor = originalForeground;

Console.WriteLine("Press 'X' to view your element collection.\n
Press 'Q' to quit the game.");

ingredients[0] = Console.ReadLine() ?? string.Empty;
if (isdigit(ingredients[0]))
{
    int elementId = int.Parse(ingredients[0]);
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
if (isdigit(ingredients[1]))
{
    int elementId = int.Parse(ingredients[1]);
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