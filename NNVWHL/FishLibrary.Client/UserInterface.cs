using FishLibrary.Core;
using FishLibrary.Data;

namespace NNVWHL.FishLibrary.Client;

internal class UserInterface
{
    private readonly FishSerializer _serializer;
    private readonly List<Fish> _fishes;

    public UserInterface(FishSerializer serializer, List<Fish> fishes)
    {
        _serializer = serializer;
        _fishes = fishes;
    }

    public void Start()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("=== Welcome to the FishLibrary ===");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1) Look up fish");
            Console.WriteLine("2) Add fish");
            Console.WriteLine("3) Remove fish");
            Console.WriteLine("4) List all fish");
            Console.WriteLine("X) Exit");
            Console.Write("> ");

            string option = Console.ReadLine() ?? string.Empty;

            switch (option)
            {
                case "1":
                    {
                        LookupFish();
                        break;
                    }
                case "2":
                    {
                        AddFish();
                        break;
                    }
                case "3":
                    {
                        RemoveFish();
                        break;
                    }
                case "4":
                    {
                        ListAllFishes();
                        break;
                    }
                case "X":
                    { 
                    isRunning = false;
                    Console.WriteLine("Goodbye!");
                    return;
                    }
                default:
                    { 
                    Console.WriteLine("Invalid option. Please select 1, 2, 3, 4, or X.");
                    break;
                    }
            }
        }
    }

    private void LookupFish()
    {
        if (_fishes.Count == 0)
        {
            Console.WriteLine("No fishes to look up!");
            return;
        }

        bool isRunning = true;
        while (isRunning) { 
        Console.WriteLine("--- Lookup Menu ---");
        Console.WriteLine("1) Search fish by name");
        Console.WriteLine("2) Show statistics");
        Console.WriteLine("X) Return to menu");
        Console.Write("> ");

        string subOption = Console.ReadLine() ?? string.Empty;
        
        switch (subOption)
        {
            case "1":
                {
                    Console.WriteLine("Enter the name to search for: ");
                    Console.WriteLine("> ");
                    string searchName = Console.ReadLine() ?? string.Empty;

                    List<Fish> foundFishes = _fishes
                        .Where(f => f.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (foundFishes.Count > 0)
                    {
                        Console.WriteLine($"--- Search Results ({foundFishes.Count} found) ---");
                        foreach (var fish in foundFishes)
                        {
                            Console.WriteLine($"- {fish.Name} (Age: {fish.Age}, Color: {fish.Color})");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No fish found matching that name.");
                    }
                    break;
                }
            case "2":
                {
                    Console.WriteLine("--- Fish Statistics ---");

                    double avgAge = _fishes.Average(f => f.Age);
                    Console.WriteLine($"Average age of all fishes: {avgAge:F1} days");

                    IEnumerable<IGrouping<string, Fish>> fishesByColor = _fishes.GroupBy(f => f.Color);

                    Console.WriteLine("Fishes grouped by color:");
                    foreach (var group in fishesByColor)
                    {
                        Console.WriteLine($"Color: {group.Key} - Count: {group.Count()}");
                    }
                    break;
                }
            case "X":
                {
                  isRunning = false;
                  return;
                }
            default:
                { 
                Console.WriteLine("Invalid option. Please select 1, 2, or X.");
                break;
                }
            }
        }
    }

    private void AddFish()
    {
        string name = string.Empty;
        string color = string.Empty;
        DateTime dateOfBuy = DateTime.MinValue;

        while (true)
        {
            Console.Write("Enter fish name (min 3 characters): ");
            name = Console.ReadLine()?.Trim() ?? string.Empty;

            if (name.Length >= 3)
            {
                break;
            }

            Console.WriteLine("The name must be at least 3 characters long and cannot be just empty spaces.");
        }

        if (_fishes.Any(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"A fish with the name '{name}' already exists!");
            return;
        }


        while (true)
        {
            Console.Write("Enter fish date of buy (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out dateOfBuy))
            {
                Console.WriteLine("Invalid date format!");
            }
            else
            {
                break;
            }
        }
 

        while (true)
        {
            Console.Write("Enter fish color: ");
            color = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!color.Equals(string.Empty))
            {
                break;
            }

            Console.WriteLine("You must give a color");
        }

        Fish newFish = new Fish(name, dateOfBuy, color);
        _fishes.Add(newFish);

        _serializer.SaveFishes(_fishes);
        Console.WriteLine($"Successfully added {name} to the library!");
    }

    private void RemoveFish()
    {
        Console.Write("Enter the exact name of the fish to remove: ");
        string name = Console.ReadLine() ?? string.Empty;

        Fish? fishToRemove = _fishes.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (fishToRemove != null)
        {
            _fishes.Remove(fishToRemove);
            _serializer.SaveFishes(_fishes);
            Console.WriteLine($"Fish '{name}' removed successfully.");
        }
        else
        {
            Console.WriteLine($"Fish '{name}' was not found.");
        }
    }

    private void ListAllFishes()
    {
        Console.WriteLine("--- All Fishes in Library ---");
        if (_fishes.Count == 0)
        {
            Console.WriteLine("No fish found.");
            return;
        }

        foreach (var fish in _fishes)
        {
            Console.WriteLine($"Name: {fish.Name} | Days owned: {fish.Age} | Bought: {fish.DateOfBuy.ToShortDateString()} | Color: {fish.Color}");
        }
    }
}
