using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Entities;
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
        private StoryManager _storyManager = new StoryManager();

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
            string name = InputHandler.ReadName("Add meg a neved:");

            // 2. Karakter választás
            Console.WriteLine("\nVálassz karaktert:");
            Console.WriteLine("1 - Harcos");
            Console.WriteLine("2 - Íjász");
            Console.WriteLine("3 - Mágus");

            int choice = InputHandler.ReadIntInRange(":", 1, 3);
           Character character = choice switch
            {
                1 => new Mage(),
                2 => new Warrior(),
                3 => new Rogue(),
                _ => throw new Exception("Invalid choice")
            };

            Player player = new Player(character);
            // 3. Inventory
            List<Item> inventory = new List<Item>
            {
                //new Weapon { Name = "Rozsdás kard", Damage = 5 }
            };

            // 4. GameState létrehozása
            var state = new GameState
            {
                Player = character,
                CurrentLocation = "megbizolevel", // ez legyen a story.json első node-ja
                Inventory = inventory
            };

            return state;
        }

        private void GameLoop()
        {
            while (true)
            {
                StoryNode node = _storyManager.GetNode(_state.CurrentLocation);
                Console.WriteLine(node.Text);
                

                for (int i = 0; i < node.Choices.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {node.Choices[i].Text}");
                }
                // választás
                int choice = InputHandler.ReadIntInRange(":", 1, node.Choices.Count, _state);

                // állapot frissítés
                _state = _state with
                {
                    CurrentLocation = node.Choices[choice - 1].Next
                };
            }
        }
    }
}
