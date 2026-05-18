using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using SzerepjatekCLI.Items;

namespace SzerepjatekCLI.Services.JsonLoaders;

public class PotionLoadService
{
    private readonly string potionFile = "Data/potions.json";

    public List<Potion> Potions { get; private set; } = new();

    public PotionLoadService()
    {
        LoadPotions();
    }

    private void LoadPotions()
    {
        if (!File.Exists(potionFile))
            throw new FileNotFoundException(potionFile);

        string json = File.ReadAllText(potionFile, Encoding.UTF8);

        Potions =
            JsonSerializer.Deserialize<List<Potion>>(json)
            ?? new List<Potion>();
    }

    public Potion? GetPotionById(int id) => Potions.Find(p => p.Id == id);
}
