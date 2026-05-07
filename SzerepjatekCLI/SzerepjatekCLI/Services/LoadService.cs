using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using SzerepjatekCLI.Core;

namespace SzerepjatekCLI.Services;

public class LoadService
{
    private readonly string saveDirectory = "Data";
    private GameState _gameState;
    public GameState LoadGame()
    {
        return LoadFromFile($"save.json");
    }
    private GameState LoadFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Save file not found.", filePath);

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<GameState>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading game: {ex.Message}");
            return null;
        }
    }
}