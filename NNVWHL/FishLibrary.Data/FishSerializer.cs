using System.Text.Json;
using FishLibrary.Core;

namespace FishLibrary.Data
{
    public class FishSerializer
    {
        private readonly string _filePath = "fishes.json";
        private readonly JsonSerializerOptions _options;

        public FishSerializer()
        {
            _options = new JsonSerializerOptions { WriteIndented = true };
        }

        public List<Fish> LoadFishes()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<Fish>();
                }
                string jsonString = File.ReadAllText(_filePath);
                List<Fish>? fishes = JsonSerializer.Deserialize<List<Fish>>(jsonString);

                return fishes ?? new List<Fish>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error while loading the JSON file: {ex.Message}");
                return new List<Fish>(); 
            }
        }

        public void SaveFishes(List<Fish> fishes)
        {
            try
            { 
                string jsonString = JsonSerializer.Serialize(fishes,_options);
                File.WriteAllText(_filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The fish data could not be saved: {ex.Message}");
            }
        }

    }
}
