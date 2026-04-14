using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Utils;

namespace SzerepjatekCLI.Core

{
    internal class Menu
    {

        public static int ShowMainMenu()
        {
            Console.WriteLine("Válassz egy lehetőséget:");
            Console.WriteLine("1. Új játék");
            Console.WriteLine("2. Játék betöltése");
            //Console.WriteLine("3. Menü");
            return InputHandler.ReadIntInRange("Választás:", 1, 2);
        }

        public static void ShowInGameMenu()
        {
            Console.Clear();
            Console.WriteLine("=== MENÜ ===");
            Console.WriteLine("1. Folytatás");
            Console.WriteLine("2. Mentés");
            Console.WriteLine("3. Főmenü");
            Console.WriteLine("4. Kilépés");
            int choice = InputHandler.ReadIntInRange("Választás:", 1, 2);
            if (choice == 1)
            {
                //folytatás, kiírja az utolós szöveget és az input opciókat
            }
            else if (choice == 2)
            {
                Console.WriteLine("Mentés... Még fejlesztés alatt");
            }
            else if (choice == 3)
            {
                // Főmenüre vissza
                Console.Clear();
                Program.Main(null);
            }
            else if (choice == 4)
            {
                // Kilépés
                Environment.Exit(0);
            }

        }
    }
}
