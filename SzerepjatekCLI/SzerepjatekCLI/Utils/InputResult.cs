using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Core;
using SzerepjatekCLI.Items;

namespace SzerepjatekCLI.Utils
{
    public enum InputAction
    {
        None,
        Valid,
        Invalid,
        Help,
        MainMenu,
        InGameMenu,
        ShowInventory
    }


    public class InputResult
    {

        public InputAction Action { get; set; }
        public int? Value { get; set; }



        public static InputResult ReadIntInRange(
            string prompt,
            int min,
            int max,
            GameState gameState = null)
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

                input = input.ToLower();

                switch (input)
                {
                    case "h":
                        return new InputResult
                        {
                            Action = InputAction.Help
                        };

                    case "m":
                        return new InputResult
                        {
                            Action = InputAction.InGameMenu
                        };

                    case "i":
                        return new InputResult
                        {
                            Action = InputAction.ShowInventory
                        };
                }

                if (!int.TryParse(input, out int value))
                {
                    WriteError("Hibás input!");
                    continue;
                }

                if (value < min || value > max)
                {
                    WriteError($"Csak {min} és {max} között lehet!");
                    continue;
                }

                return new InputResult
                {
                    Value = value,
                    Action = InputAction.None
                };
            }
        }


        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void InputHelper()
        {
            Console.Clear();
            Console.WriteLine("Segítség:");
            Console.WriteLine("- Használd a számbillentyűket a menüpontok kiválasztásához.");
            List<string> list = new List<string> { "h = Segítség", "m = Menu", "i = Hátizsák" };
            foreach (var helps in list)
            {
                Console.WriteLine(helps);
            }
            Console.WriteLine("- Mentsd gyakran a játékot, hogy ne veszítsd el a haladásodat!");
            Console.WriteLine("Nyomj meg egy gombot a visszatéréshez...");
            Console.ReadKey();

        }
        public static string ReadName(string v)
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
        public static int ReadPureIntInRange(string prompt, int min, int max)
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
                if (!int.TryParse(input, out int value))
                {
                    WriteError("Nem számot adott meg!");
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
    }
    /*internal static class InputHandler
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

        private static bool ReadString(string input, GameState gameState)//a GameState azért kell neki, hogy a mentést meg tudja csinálni, illetve az inventoryt ki tudja írni
        {
            if (gameState == null)
                return false;

            if (input == "h")
            {
                InputHelper();
                return true;

            }
            if (input == "m")
            {
                MenuResult result = Menu.ShowInGameMenu(gameState);
                if (result == MenuResult.Continue)
                    Console.WriteLine("Játék folytatása...");//!!!!!!!!
                if (result == MenuResult.BackToMainMenu)
                    Menu.ShowMainMenu();
                if (result == MenuResult.Exit)
                    Environment.Exit(0);
                return true;
            }
            if (input == "i")
            {
                Console.WriteLine("Hátizsák:");
                foreach (var item in gameState.Player.Inventory)
                {
                    Console.WriteLine(item.ToString());
                }
                return true;
            }

            return false;
        }*/
}
