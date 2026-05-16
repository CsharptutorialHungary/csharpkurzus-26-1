internal class FileHandler
{
    public static void SaveCollection() {
        Console.WriteLine("Please enter the name or path of the file to save the collection into:");
        string fileName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(fileName)) {
            Console.WriteLine("File name cannot be empty. Please enter a valid file name:");
            fileName = Console.ReadLine();
        }
        try {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var element in FoundElementDB.Instance.Elements)
                {
                    JSONElement jsonElement = new JSONElement
                    {
                        Id = element.id,
                        Name = element.name,
                        Color = element.color,
                        FirstIngredient = element.firstIngredient,
                        SecondIngredient = element.secondIngredient,
                        Message = element.message
                    };
                    writer.WriteLine(JsonConvert.SerializeObject(jsonElement));
                }
            }
            Console.WriteLine("Collection saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving the collection: {ex.Message}");
        }
        
    }

    public static void LoadCollection() {
        Console.WriteLine("Please enter the name or path of the file to load the collection from:");
        string fileName = Console.ReadLine();
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
                    JSONElement jsonElement = JsonConvert.DeserializeObject<JSONElement>(line);
                    if (jsonElement != null)
                    {
                        FoundElementDB.Instance.AddFoundElement(jsonElement.Id);
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