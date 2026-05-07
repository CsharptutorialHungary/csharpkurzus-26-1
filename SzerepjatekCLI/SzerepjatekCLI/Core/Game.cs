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
        private GameState? _state;
        private readonly StoryManager _storyManager = new StoryManager();
        private OutputService _outputService;

        public void Run()
        {
            int choice = Menu.ShowMainMenu();

            if (choice == 1)
                _state = NewGame();
            if(choice == 2)
                _state = new LoadService().LoadGame();

            GameLoop();
        }

        private GameState NewGame()
        {
            Console.Clear();
            Console.WriteLine("=== ÚJ JÁTÉK ===");

            //Név
            string name = InputHandler.ReadName("Add meg a karaktered nevét:");
            _outputService = new OutputService(name);


            //Karakter választás
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
            player.Name = name;

            // Inventory
            List<Item> inventory = new List<Item>
            {
               new Weapon { Name = "Másfélkezes kard", Description = "Ez a saját kedvenc kardod", Damage = 10, Defense = 5 },
               new MoneyItem(Money.Arany, 10)

            };
            player.Inventory = inventory;

            // GameState létrehozása
            var state = new GameState
            {
                Player = player,
                CurrentLocation = "megbizolevel", // a story.json első node-ja
                //Inventory = player.Inventory
            };

            return state;
        }

        private void GameLoop()
        {
            while (true)
            {
                StoryNode node = _storyManager.GetNode(_state.CurrentLocation);
                // Console.WriteLine(node.Text);
                _outputService.Write(node.Text);

                if (node.Choices.Count == 0)
                {
                    Console.WriteLine("A játék véget ért.");
                    break;
                }


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

        public void ContinueGame(GameState gameState)
        {
            _state = gameState;
            _outputService.Write((_storyManager.GetNode(_state.CurrentLocation)).Text);
            GameLoop();
        }
    }
}
