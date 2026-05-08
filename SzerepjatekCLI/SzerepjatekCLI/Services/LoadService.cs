using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

using SzerepjatekCLI.Core;
using SzerepjatekCLI.Entities;
using SzerepjatekCLI.Items;

namespace SzerepjatekCLI.Services;

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

        return type switch
        {
            "Weapon" => new Weapon
            {
                Id = json["Id"].GetValue<int>(),
                Name = json["Name"].GetValue<string>(),
                Description = json["Description"].GetValue<string>(),
                Weight = json["Weight"].GetValue<int>(),
                //Damage = json["Damage"].GetValue<int>(),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!TODO
                Type = "Weapon"
            },

            "Potion" => new Potion
            {
                Id = json["Id"].GetValue<int>(),
                Name = json["Name"].GetValue<string>(),
                Description = json["Description"].GetValue<string>(),
                Weight = json["Weight"].GetValue<int>(),
                HealthModifier = json["HealthModifier"].GetValue<int>(),
                Type = "Potion"
            },

            _ => throw new Exception($"Unknown item type: {type}")
        };
    }
}