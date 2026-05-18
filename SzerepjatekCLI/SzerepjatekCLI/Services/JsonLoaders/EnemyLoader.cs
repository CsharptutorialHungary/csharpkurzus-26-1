using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using SzerepjatekCLI.Entities;
using SzerepjatekCLI.Items;

namespace SzerepjatekCLI.Services.JsonLoaders;

internal class EnemyLoader
{
    private readonly string enemyFile = "Data/characters.json";

    public List<Enemy> Enemies { get; private set; } = new();

    public EnemyLoader()
    {
        LoadEnemies();
    }

    private void LoadEnemies()
    {
        if (!File.Exists(enemyFile))
            throw new FileNotFoundException(enemyFile);

        string json = File.ReadAllText(enemyFile, Encoding.UTF8);

        Enemies =
            JsonSerializer.Deserialize<List<Enemy>>(json)
            ?? new List<Enemy>();
    }

    public Enemy? GetEnemyById(string id) => Enemies.Find(e => e.Id == id);

}
