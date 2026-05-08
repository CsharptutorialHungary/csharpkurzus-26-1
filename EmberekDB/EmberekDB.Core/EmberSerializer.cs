using System.Text.Json;

namespace EmberekDB.Core;

internal class EmberSerializer
{
    public List<Ember> Load()
    {
        if (!File.Exists("emberek.json")) {
            File.WriteAllText("emberek.json","[]");
        }
        string json = File.ReadAllText("emberek.json");
        return JsonSerializer.Deserialize<List<Ember>>(json) ?? new List<Ember>();
    }
    

    public void Save(List<Ember> emberek)
    {
        var json = JsonSerializer.Serialize(emberek);
        File.WriteAllText("emberek.json", json);
    }
}
