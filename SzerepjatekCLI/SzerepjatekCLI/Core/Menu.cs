using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Services;
using SzerepjatekCLI.Story;
using SzerepjatekCLI.Utils;

namespace SzerepjatekCLI.Core

{
    public enum MenuResult
    {
        Continue,
        BackToMainMenu,
        Exit
    }

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
            return InputResult.ReadPureIntInRange("Választás:", 1, 4); //azért return és nem itt kezelem le, mert ettől függ a Game beállítása, ennek az osztálynak nincsenek hozzáférései hozzájuk, A Game osztálynak kell tudnia a választást
        }

        public static MenuResult ShowInGameMenu(GameState gameState, bool isSaved)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("=== MENÜ ===");
                Console.WriteLine("1. Folytatás");
                Console.WriteLine("2. Mentés");
                Console.WriteLine("3. Segítség");
                Console.WriteLine("4. Főmenü");
                Console.WriteLine("5. Kilépés");
                int choice = InputResult.ReadPureIntInRange("Választás:", 1, 5);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        return MenuResult.Continue;
                    case 2:
                        isSaved = SaveService.SaveGame(gameState, "Data/save.json");
                        continue;
                    case 4:
                        {
                            if (isSaved)
                            {
                                Console.Clear();
                                return MenuResult.BackToMainMenu;
                            }
                            else
                            {
                                Console.WriteLine("Nem mentetted a játékot! Biztosan vissza akarsz térni a főmenüre? (y/n)");
                                string input = Console.ReadLine();
                                if (input == "y" || input == "Y")
                                {
                                    Console.Clear();
                                    return MenuResult.BackToMainMenu;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                    case 5:
                        {
                            if (isSaved)
                            {
                                return MenuResult.Exit;
                            }

                            Console.WriteLine("Nem mentetted a játékot! Biztosan ki akarsz lépni? (y/n)");
                            string input = Console.ReadLine();
                            if (input == "y" || input == "Y")
                            {
                                return MenuResult.Exit;
                            }
                            else
                            {
                                //mentés meghívása, majd kilépés
                                SaveService.SaveGame(gameState, "Data/save.json");
                                return MenuResult.Exit;
                            }

                        }

                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("Segítség:");
                            Console.WriteLine("- Használd a számbillentyűket a menüpontok kiválasztásához.");
                            List<string> list = new List<string> { "h = Segítség", "m = Menu", "i = Hátizsák", "s = Karakter statisztikák" };
                            foreach (var helps in list)
                            {
                                Console.WriteLine(helps);
                            }
                            Console.WriteLine("- Mentsd gyakran a játékot, hogy ne veszítsd el a haladásodat!");
                            Console.WriteLine("Nyomj meg egy gombot a visszatéréshez...");
                            Console.ReadKey();
                            continue;
                        }
                }
            }
        }
    }
}