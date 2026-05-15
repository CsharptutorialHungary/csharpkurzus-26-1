using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

using SzerepjatekCLI.Entities;
using SzerepjatekCLI.Items;
using SzerepjatekCLI.Services;
using SzerepjatekCLI.Services.JsonLoaders;
using SzerepjatekCLI.Story;
using SzerepjatekCLI.Utils;

namespace SzerepjatekCLI.Core
{
    public class Game
    {
        private GameState? _state;
        private readonly StoryManager _storyManager = new StoryManager();
        private OutputService _outputService;
        private WeaponLoadService _weaponService = new WeaponLoadService();

        private bool _isSaved = true;

        public void Run()
        {
            int choice = Menu.ShowMainMenu();

            switch (choice)
            {
                case 1:
                    _state = NewGame();
                    break;

                case 2:
                    _state = new LoadService().LoadGame();
                    _outputService = new OutputService(_state.Player.Name);
                    break;

                case 3:
                    InputResult.InputHelper();
                    break;

                case 4:
                    return;
            }
            GameLoop();
        }

        private GameState NewGame()
        {
            Console.Clear();
            Console.WriteLine("=== ÚJ JÁTÉK ===");

            //Játékos neve
            string name = InputResult.ReadName("Add meg a karaktered nevét:");
            _outputService = new OutputService(name);


            //Karakter választás
            Console.WriteLine("\nVálassz karaktert:");
            Console.WriteLine("1 - Harcos");
            Console.WriteLine("2 - Vándor");
            Console.WriteLine("3 - Mágus");

            int choice = InputResult.ReadPureIntInRange(":", 1, 3);
            Player player = choice switch
            {
                1 => new Warrior(),
                2 => new Rogue(),
                3 => new Mage(),
                _ => throw new Exception("Invalid choice")
            };
            player.Name = name;

            // Inventory
            player.Inventory = new Inventory();
            player.Inventory.Add(_weaponService.GetWeaponById(0)); // alap kard
            player.Inventory.Add(new MoneyItem(Money.Arany, 10));
            

            // GameState létrehozása
            var state = new GameState
            {
                Player = player,
                CurrentLocation = "Megbízólevél", // a story.json első node-ja
            };
            Console.Clear();
            return state;
        }

        private void GameLoop()
        {
            while (true)
            {
                StoryNode node = _storyManager.GetNode(_state.CurrentLocation);
                _outputService.Write(node);

                if (_storyManager.IsEndNode(_state.CurrentLocation))
                {
                    Console.WriteLine("A játék véget ért.");
                    break;
                }

                if (node.Action != null && node.Action.Contains("shop"))
                {
                    _state = _storyManager.HandleShopAction(node.Action, _state); //így hogy vásárlás nélkül is felül csapja az erdetit, így tudok majd belerakni később kiható elemeket (már beszélt ezzel, azzal, vett, jobb lesz a kapcsolata velük, kihathat későbbre)
                    _state = _state with
                    {
                        CurrentLocation = node.Choices[0].Next
                    };
                    _isSaved = false;
                    Console.Clear();
                    continue;
                }

                for (int i = 0; i < node.Choices.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {node.Choices[i].Text}");
                }


                //itt kell belépni az actionnek mert még a vásárlás döntését ki kell írni ha van
                


                // választás
                InputResult choice = InputResult.ReadIntInRange(":", 1, node.Choices.Count, _state);

                switch (choice.Action)
                {
                    case InputAction.Help:
                        InputResult.InputHelper();
                        continue;

                    case InputAction.ShowInventory:

                        Console.WriteLine("Hátizsák:");

                        Console.WriteLine(_state.Player.Inventory.ToString());

                        Console.ReadKey();
                        continue;

                    case InputAction.InGameMenu: //ingame menüre váltás

                        MenuResult menuResult =
                            Menu.ShowInGameMenu(_state, _isSaved);

                        switch (menuResult)
                        {
                            case MenuResult.Continue:
                                continue;

                            case MenuResult.BackToMainMenu:
                                Menu.ShowMainMenu();
                                return;

                            case MenuResult.Exit:
                                Environment.Exit(0);
                                break;
                        }

                        break;
                }




                // állapot frissítés
                _state = _state with
                {
                    CurrentLocation = node.Choices[choice.Value.Value - 1].Next
                };
                _isSaved = false;
                Console.Clear();
            }

        }
    }
}
