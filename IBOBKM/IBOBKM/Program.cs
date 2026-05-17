using System.IO.Pipelines;
using System.Text.Json;

internal class Program
{
    private readonly string[] _ingredients = new string[2];
    private IEnumerable<Element> _foundElements = FoundElementDB.Instance.Elements.OrderBy(e => e.id);
    private static void Main(string[] args)
    {
        Program program = new Program();
        program.Run();
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the Alchemical Laboratory!");

        Beginning();
    }

    public void Beginning() {
        Console.WriteLine("Combine two elements, and you might just create something new!");

        Console.WriteLine("Type 'X' to view your element collection, 'Q' to quit the game and any other key to combine elements.");
        try
        {
            switch (Console.ReadLine()?.ToUpper())
            {
                case "X":
                    ViewCollection();
                    break;
                case "Q":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    GiveIngredientOne();
                    break;
            }
        } catch (FormatException ex)
        {
            Console.WriteLine($"Input format is invalid: {ex.Message}");
            Beginning();
            return;
        } catch (Exception ex)       {
            Console.WriteLine($"An error occurred while handling your choice: {ex.Message}");
            Beginning();
            return;
        }
    }

    private void ViewCollection() {
        Console.WriteLine("Your Element Collection:");
        try
        {
            _foundElements = FoundElementDB.Instance.Elements.OrderBy(e => e.id);
            foreach (var element in _foundElements)
            {
                ConsolePainter.SetConsoleColor(element.color);
                Console.WriteLine($"{element.id}: {element.name}");
            }
            ConsolePainter.ResetConsoleColor();
            Console.WriteLine("Elements found: " + FoundElementDB.Instance.Elements.Count() + "/" + ElementDB.Instance.Elements.Count());
            if (FoundElementDB.Instance.Elements.Count() == ElementDB.Instance.Elements.Count())
            {
                Console.WriteLine("Congratulations! You've found all the elements! You're a true alchemist!");
            }
            Console.WriteLine("Press S to save your collection, L to load a saved collection, or any other key to return to the lab.");
        } catch (ElementNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Beginning();
            return;
        } catch (FormatException ex)
        {
            Console.WriteLine($"Input format is invalid: {ex.Message}");
            Beginning();
            return;
        }
        catch (Exception ex) {
            Console.WriteLine($"An error occurred while displaying your collection: {ex.Message}");
            Beginning();
            return;
        }
        try
        {
            switch (Console.ReadLine()?.ToUpper())
            {
                case "S":
                    SaveCollection();
                    break;
                case "L":
                    LoadCollection();
                    break;
                default:
                    Beginning();
                    break;
            }
        } catch (FormatException ex)
        {
            Console.WriteLine($"Input format is invalid: {ex.Message}");
            Beginning();
            return;
        } catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while handling your choice: {ex.Message}");
            Beginning();
            return;
        }
        
    }

    private void GiveIngredientOne() {
        _foundElements = FoundElementDB.Instance.Elements.OrderBy(e => e.id);

        Console.WriteLine("Please choose the first ingredient by typing its ID:");

        try {
            foreach (var element in _foundElements)
            {
                ConsolePainter.SetConsoleColor(element.color);
                Console.WriteLine($"{element.id}: {element.name}");
            }
            ConsolePainter.ResetConsoleColor();
        } catch (ElementNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Beginning();
            return;
        } catch (FormatException ex)       {
            Console.WriteLine($"Input format is invalid: {ex.Message}");
            Beginning();
            return;
        }
        
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while displaying your collection: {ex.Message}");
            Beginning();
            return;
        }

        try {
            _ingredients[0] = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(_ingredients[0], out int elementId))
            {
                if (FoundElementDB.Instance.IsElementFound(elementId))
                {
                    _ingredients[0] = ElementDB.Instance.GetElementById(elementId)?.name ?? string.Empty;
                    GiveIngredientTwo();
                    return;
                }
                else
                {
                    Console.WriteLine("You haven't found that element yet!");
                    GiveIngredientOne();
                    return;
                }
            } else
            {
                Console.WriteLine("Please enter a valid element ID!");
                GiveIngredientOne();
                return;
            }
        }catch (FormatException ex)
        {
            Console.WriteLine($"Input format is invalid: {ex.Message}");
            Beginning();
            return;
        } catch (ElementNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Beginning();
            return;
        } catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading first ingredient: {ex.Message}");
            Beginning();
            return;
        }
    }

    private void GiveIngredientTwo() {
        try {
            Console.WriteLine("Please choose the second ingredient!");
            _ingredients[1] = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(_ingredients[1], out int elementId))
            {
                if (FoundElementDB.Instance.IsElementFound(elementId))
                {
                    _ingredients[1] = ElementDB.Instance.GetElementById(elementId)?.name ?? string.Empty;
                    CombineIngredients();
                    return;
                }
                else
                {
                    Console.WriteLine("You haven't found that element yet!");
                    GiveIngredientTwo();
                    return;
                }
            } else
            {
                Console.WriteLine("Please enter a valid element ID!");
                GiveIngredientTwo();
                return;
            }
        } catch (ElementNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Beginning();
            return;
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Input format is invalid: {ex.Message}");
            Beginning();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading second ingredient: {ex.Message}");
            Beginning();
            return;
        }
    }

    private void CombineIngredients() {
        IEnumerable<Element> result;
        try {
            result =
            from element in ElementDB.Instance.Elements
            where (element.firstIngredient == _ingredients[0] && element.secondIngredient == _ingredients[1]) ||
                (element.firstIngredient == _ingredients[1] && element.secondIngredient == _ingredients[0])
            select element;
        } catch (ElementNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Beginning();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching combined element: {ex.Message}");
            Beginning();
            return;
        }
        Element? combinedElement = result.FirstOrDefault();
        try {
            if (combinedElement != null) {
                if (!FoundElementDB.Instance.IsElementFound(combinedElement.id))
                {
                    ConsolePainter.SetConsoleColor(combinedElement.color);
                    Console.WriteLine(combinedElement.message);
                    FoundElementDB.Instance.AddFoundElement(combinedElement.id);
                    ConsolePainter.ResetConsoleColor();
                } else {
                    Console.WriteLine("You have already discovered that element!: ");
                    ConsolePainter.SetConsoleColor(combinedElement.color);
                    Console.WriteLine($"{combinedElement.id}: {combinedElement.name}");
                    ConsolePainter.ResetConsoleColor();
                }

            } else {
                Console.WriteLine("...Looks like that was a dud... Try again!");
            }
        } catch (ElementNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while handling fetched element: {ex.Message}");
        }
        Beginning();
    }

    public void SaveCollection() {
        Console.WriteLine("Please enter the name of the file to save the collection into:");
        string? fileName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(fileName)) {
            Console.WriteLine("File name cannot be empty. Please enter a valid file name:");
            fileName = Console.ReadLine();
        }
        try {
            using (StreamWriter writer = new StreamWriter("saves/" + fileName))
            {
                foreach (var element in FoundElementDB.Instance.Elements)
                {
                    String jsonElement = JsonSerializer.Serialize(element);
                    writer.WriteLine(jsonElement);
                }
            }
            Console.WriteLine("Collection saved successfully!");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"A JSON exception occurred while saving the collection: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred while saving the collection: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred while saving the collection: {ex.Message}");
        }
        
        Beginning();
    }

    public void LoadCollection() {
        Console.WriteLine("Please enter the nameof the file to load the collection from:");
        string? fileName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(fileName)) {
            Console.WriteLine("File name cannot be empty. Please enter a valid file name:");
            fileName = Console.ReadLine();
        }
        try {
            using (StreamReader reader = new StreamReader("saves/" + fileName))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Element? element = JsonSerializer.Deserialize<Element>(line);
                    if (element != null)
                    {
                        FoundElementDB.Instance.AddFoundElement(element.id);
                    } else
                    {
                        throw new JsonException("Deserialized element is null.");
                    }
                }
            }
            Console.WriteLine("Collection loaded successfully!");
        } catch (JsonException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (IOException ex)
        {
            Console.WriteLine($"There's no such file!: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the collection: {ex.Message}");
        }
        
        Beginning();
    }
}




