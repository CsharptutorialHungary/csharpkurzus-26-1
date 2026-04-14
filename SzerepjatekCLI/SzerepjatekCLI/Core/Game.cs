using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Characters;
using SzerepjatekCLI.Items;
using SzerepjatekCLI.Services;
using SzerepjatekCLI.Story;
using SzerepjatekCLI.Utils;

namespace SzerepjatekCLI.Core
{
    //
    public class Game
    {
        private GameState _state;
        private StoryManager _storyManager;

        public void Run()
        {
            int choice = Menu.ShowMainMenu();

            if (choice == 1)
                _state = NewGame();
            /*if(choice == 2)
                _state = SaveService.Load*/

            GameLoop();
        }

        private GameState NewGame()
        {
            Console.Clear();
            Console.WriteLine("=== ÚJ JÁTÉK ===");

            // 1. Név
            string name = InputHandler.ReadString("Add meg a neved:");

            // 2. Karakter választás
            Console.WriteLine("\nVálassz karaktert:");
            Console.WriteLine("1 - Harcos");
            Console.WriteLine("2 - Íjász");
            Console.WriteLine("3 - Mágus");

            int choice = InputHandler.ReadIntInRange(":", 1, 3);

            Character player = choice switch
            {
                1 => new Character { Name = name, Health = 150, Attack = 20 },
                2 => new Character { Name = name, Health = 120, Attack = 25 },
                3 => new Character { Name = name, Health = 100, Attack = 30 },
                _ => throw new Exception("Invalid choice")
            };

            // 3. Inventory
            List<Item> inventory = new List<Item>
    {
        new Weapon { Name = "Rozsdás kard", Damage = 5 }
    };

            // 4. GameState létrehozása
            var state = new GameState
            {
                Player = player,
                CurrentLocation = "start", // ez legyen a story.json első node-ja
                Inventory = inventory
            };

            return state;
        }

        }

        private void GameLoop()
        {
            while (true)
            {
                StoryNode node = _storyManager.GetNode(_state.CurrentLocation);

                Console.WriteLine(node.Text);

                // választás
                int choice = InputHandler.ReadIntInRange(":", 1, node.Choices.Count);

                // állapot frissítés
                _state = _state with
                {
                    CurrentLocation = node.Choices.ElementAt(choice - 1).Value
                };
            }
        }
    }
}
