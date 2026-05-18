using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using SzerepjatekCLI.Core;

namespace SzerepjatekCLI.Services
{
    internal class SaveService
    {
        public static bool SaveGame(GameState gameState, string filePath)
        {
            try {
            Console.WriteLine("Mentés...");
            string json = JsonSerializer.Serialize(gameState, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json, Encoding.UTF8);
            Console.WriteLine("Játék sikeresen mentve: {0}. Nyomj meg egy gombot a folytatáshoz...", filePath);
            Console.ReadKey();
            return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a mentés során: {0}", ex.Message);
                return false;
            }
        }
    }
}
