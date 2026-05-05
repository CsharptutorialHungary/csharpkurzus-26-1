using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Core;

namespace SzerepjatekCLI.Utils
{
    internal static class InputHandler
    {
        public static int ReadIntInRange(string prompt, int min, int max, GameState gameState = null)
        {
            while (true)
            {
                Console.Write($"{prompt} ");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteError("Üres bemenet!");
                    continue;
                }

                if (ReadString(input, gameState))
                    continue;

                if (!int.TryParse(input, out int value))
                {
                    WriteError("Nem számot adott meg vagy nem a megadott elemeket (h = Segítség, m = Menu)!");
                    continue;
                }

                if (value < min || value > max)
                {
                    WriteError($"Csak {min} és {max} között lehet!");
                    continue;
                }

                return value;
            }
        }

        internal static string ReadName(string v)
        {

            while (true)
            {
                Console.Write(v + " ");
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                   
                Console.WriteLine("A név nem lehet üres!");
            }
        }

        private static bool ReadString(string input, GameState gameState)
        {
            if(gameState == null)
                return false;

            if (input == "h")
            {
                List<string> list = new List<string> { "h = Segítség", "m = Menu" };
                foreach (var helps in list)
                {
                    Console.WriteLine(helps);
                    return true;
                }
                
            }
            if (input == "m")
            {
                Menu.ShowInGameMenu(gameState);
                return true;
            }
            return false;
        }


        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
