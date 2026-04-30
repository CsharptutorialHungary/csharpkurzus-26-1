using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using SzerepjatekCLI.Core;

namespace SzerepjatekCLI.Services
{
    internal class SaveService
    {
        public static void SaveGame(GameState gameState, string filePath)
        {
            Console.WriteLine("Mentés...");
            string json = JsonSerializer.Serialize(gameState, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
