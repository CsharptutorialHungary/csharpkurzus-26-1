using System.Text.Json;
using System.Text.Json.Serialization;

internal class FileHandler
{
    public static void SaveCollection() {
        Console.WriteLine("Please enter the name or path of the file to save the collection into:");
        string? fileName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(fileName)) {
            Console.WriteLine("File name cannot be empty. Please enter a valid file name:");
            fileName = Console.ReadLine();
        }
        try {
            using (StreamWriter writer = new StreamWriter(fileName))
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
        
    }

    public static void LoadCollection() {
        Console.WriteLine("Please enter the name or path of the file to load the collection from:");
        string? fileName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(fileName)) {
            Console.WriteLine("File name cannot be empty. Please enter a valid file name:");
            fileName = Console.ReadLine();
        }
        try {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the collection: {ex.Message}");
        }
    }
}