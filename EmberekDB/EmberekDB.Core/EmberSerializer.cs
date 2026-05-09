using System.Text.Json;

namespace EmberekDB.Core;

internal class EmberSerializer
{
    public List<Ember> Load()
    {
        List<Ember> emberek = new List<Ember>();
        if (!File.Exists("emberek.json")) {
            try
            {
                File.WriteAllText("emberek.json", "[]");
            }
            catch (IOException ex) {
                Console.WriteLine("Hiba történt a file létrehozásakor!");
                //Console.WriteLine(ex.Message);
            }
        }
        try
        {
            string json = File.ReadAllText("emberek.json");
            emberek = JsonSerializer.Deserialize<List<Ember>>(json) ?? [];
        }
        catch (IOException ex) {
            Console.WriteLine("Hiba történt a file beolvasásakor!");
            //Console.WriteLine(ex.Message);
        }

        return emberek;
    }
    

    public void Save(List<Ember> emberek)
    {
        try
        {
            var json = JsonSerializer.Serialize(emberek);
            File.WriteAllText("emberek.json", json);
        }
        catch (IOException ex) {
            Console.WriteLine("A mentés sikertelen!");
            //Console.WriteLine(ex.Message);
        }
    }
}
