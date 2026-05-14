using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

using SzerepjatekCLI.Core;
using SzerepjatekCLI.Entities;
using SzerepjatekCLI.Items;

namespace SzerepjatekCLI.Services.JsonLoaders;

public class LoadService
{
    private readonly string saveDirectory = "Data";

    public GameState LoadGame()
    {
        return LoadFromFile($"{saveDirectory}/save.json");
    }

    private GameState LoadFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Save file not found.", filePath);

            string json = File.ReadAllText(filePath);

            JsonNode root = JsonNode.Parse(json);

            GameState state = new GameState();


            JsonNode playerNode = root["Player"];
            Player player = new Player
            {
                Name = playerNode["Name"].GetValue<string>(),
                MaxHealth = playerNode["MaxHealth"].GetValue<int>(),
                CurrentHealth = playerNode["CurrentHealth"].GetValue<int>(),
                AttackPower = playerNode["AttackPower"].GetValue<int>(),
                Defense = playerNode["Defense"].GetValue<int>()
            };


            List<Item> inventory = new();

            foreach (var itemNode in playerNode["Inventory"].AsArray())
            {
                inventory.Add(CreateItem(itemNode));
            }

            player.Inventory = inventory;

            state.Player = player;


            state.CurrentLocation = root["CurrentLocation"].GetValue<string>();

            return state;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading game: {ex}");
            return null;
        }
    }

    public static Item CreateItem(JsonNode json)
    {
        string type = json["Type"].GetValue<string>();
        int id = json["Id"].GetValue<int>();

        switch (type)
        {
            case "Weapon":
                var weaponService = new WeaponLoadService();
                var weapon = weaponService.GetWeaponById(id);
                if (weapon == null)
                    throw new Exception($"Nem találhato ilyen fegyver: {id}");
                return weapon;
            case "Potion":
                var potionService = new PotionLoadService();
                var potion = potionService.GetPotionById(id);
                if (potion == null)
                    throw new Exception($"Nem találhato ilyen potion: {id}");
                return potion;
            case "Money":
                MoneyItem moneyItem = new MoneyItem(Money.Bronz, json["Id"].GetValue<int>());
                return moneyItem;
            default:
                throw new Exception($"Rossz típus: {type}");
        }
    }
}