using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Services;
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
            Console.WriteLine("3. Segítség");
            //Console.WriteLine("3. Beállítások");
            Console.WriteLine("4. Kilépés");
            return InputHandler.ReadIntInRange("Választás:", 1, 4);
        }

        public static void ShowInGameMenu(GameState gameState)
        {
            Console.Clear();
            Console.WriteLine("=== MENÜ ===");
            Console.WriteLine("1. Folytatás");
            Console.WriteLine("2. Mentés");
            Console.WriteLine("3. Segítség");
            Console.WriteLine("4. Főmenü");
            Console.WriteLine("5. Kilépés");
            int choice = InputHandler.ReadIntInRange("Választás:", 1, 5);
            bool isSaved = false;
            if (choice == 1)
            {
                //folytatás, kiírja az utolsó szöveget és az input opciókat
            }
            else if (choice == 2)
            {
                
                SaveService.SaveGame(gameState, "Data/save.json");
                isSaved = true;
                Console.WriteLine("Sikeres mentés!");

            }
            else if (choice == 3)//vissza a főmenüre
            {
                if (isSaved)
                {
                    Console.Clear();
                    Program.Main(null);
                }
                else
                {
                    Console.WriteLine("Nem mentetted a játékot! Biztosan vissza akarsz térni a főmenüre? (y/n)");
                    string input = Console.ReadLine();
                    if (input == "y" || input == "Y")
                    {
                        Console.Clear();
                        Program.Main(null);
                    }
                    else
                    {
                        ShowInGameMenu(gameState);
                    }
                }
            }
            else if (choice == 4)//kilépés
            {
                if (isSaved)
                {
                    Environment.Exit(0);

                }

                Console.WriteLine("Nem mentetted a játékot! Biztosan ki akarsz lépni? (y/n)");
                string input = Console.ReadLine();
                if (input == "y" || input == "Y")
                {
                    Environment.Exit(0);
                }
                else
                {
                    //mentés meghívása, majd kilépés
                    SaveService.SaveGame(gameState, "Data/save.json");
                    Environment.Exit(0);
                }

            }
        }
    }
}