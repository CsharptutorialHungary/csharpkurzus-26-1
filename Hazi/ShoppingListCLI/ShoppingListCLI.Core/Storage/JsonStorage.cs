using System.Text.Json;
using ShoppingListCLI.ShoppingListCLI.Core.Models;

namespace ShoppingListCLI.Core.Storage;

public class JsonStorage : IStorage
{
    private readonly string _filePath = "shoppingList.json";

    public async Task<List<ShoppingList>> LoadAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
            { 
                return new List<ShoppingList>();
            }
            
            await using FileStream stream = File.OpenRead(_filePath);
            return await JsonSerializer.DeserializeAsync<List<ShoppingList>>(stream) ?? new List<ShoppingList>();
            
        }
        catch (IOException e)
        {
            Console.WriteLine("Hiba történt a fájl olvasása közben: " + e.Message);
            return new List<ShoppingList>();
        }
        catch (JsonException e)
        {
            Console.WriteLine("Hiba történt a JSON deszerializálása közben: " + e.Message);
            return new List<ShoppingList>();
        }
        catch (Exception e)
        {
            Console.WriteLine("Váratlan hiba történt: " + e.Message);
            return new List<ShoppingList>();
        }
    }

    public async Task SaveAsync(List<ShoppingList> shoppingList)
    {
        try
        { 
            if (!File.Exists(_filePath)) 
            {
                await using FileStream stream = File.Create(_filePath);
                await JsonSerializer.SerializeAsync(stream, shoppingList);
            }
            else
            {
                await using FileStream stream = File.Create(_filePath);
                await JsonSerializer.SerializeAsync(stream, shoppingList);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Hiba történt a fájl írása közben: " + e.Message);
        }
        catch (JsonException e)
        {
            Console.WriteLine("Hiba történt a JSON szerializálása közben: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Váratlan hiba történt: " + e.Message);
        }
    }
}