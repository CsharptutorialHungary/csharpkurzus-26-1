using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using SzerepjatekCLI.Core;

namespace SzerepjatekCLI.Services;

internal class LoadService
{
    private readonly string saveDirectory = "Data";
    private GameState _gameState;
    public GameState LoadGame()
    {
        return LoadFromFile($"{saveDirectory}/save.json");
    }
    private GameState LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Save file not found.", filePath);

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<GameState>(json);
    }
}