using System.Text.Json;

namespace SzerepjatekCLI.Items;

internal static class WeaponLoaderHelpers
{
    public static List<Weapon> LoadWeapons(string path)
    {
        string json = File.ReadAllText(path);

        List<Weapon>? weapons =
            JsonSerializer.Deserialize<List<Weapon>>(json);

        return weapons ?? new List<Weapon>();
    }
}