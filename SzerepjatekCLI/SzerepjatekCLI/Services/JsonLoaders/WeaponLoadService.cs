using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using SzerepjatekCLI.Items;

namespace SzerepjatekCLI.Services.JsonLoaders;
public class WeaponLoadService
{
    private readonly string weaponFile = "Data/weapons.json";

    public List<Weapon> Weapons { get; private set; } = new();

    public WeaponLoadService()
    {
        LoadWeapons();
    }

    private void LoadWeapons()
    {
        if (!File.Exists(weaponFile))
            throw new FileNotFoundException(weaponFile);
        string json = File.ReadAllText(weaponFile, Encoding.UTF8);

        Weapons =
            JsonSerializer.Deserialize<List<Weapon>>(json)
            ?? new List<Weapon>();
    }

    public Weapon GetWeaponById(int id)
    {
        return Weapons.FirstOrDefault(w => w.Id == id);
    }
}
