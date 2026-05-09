using System.Diagnostics.Tracing;

using EmberekDB.Core;
internal class Program
{
    private static int Main(string[] args)
    {
        EmberManager manager = new EmberManager();
        EmberSerializer serializer = new EmberSerializer();
        List<Ember> emberek = serializer.Load();
        Console.WriteLine("Üdv az emberek adatbázisban!");
        Info();

        string input = Console.ReadLine() ?? String.Empty;
        while (input != "exit") {
            switch (input)
            {
                case "add":
                    Console.WriteLine("Adja meg az ember nevét!");
                    string name = String.Empty;
                    while (String.IsNullOrEmpty(name))
                    {
                        name = Console.ReadLine() ?? String.Empty;
                        if (String.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("A név megadása kötelező");
                            continue;
                        }
                    }
                    Console.WriteLine("Adja meg az ember életkorát!");
                    int age = -1;
                    while (age < 0 || age > 150) {
                        age = int.Parse(Console.ReadLine() ?? "-1");
                        if (age < 0 || age > 150)
                        {
                            Console.WriteLine("Adjon meg egy valós életkort! (0-150)");
                            continue;
                        }
                    }
                    Console.WriteLine("Adja meg az ember nemét!");
                    string gender = String.Empty;
                    while (String.IsNullOrEmpty(gender))
                    {
                        gender = Console.ReadLine() ?? String.Empty;
                        if (String.IsNullOrEmpty(gender))
                        {
                            Console.WriteLine("A nem megadása kötelező");
                            continue;
                        }
                    }
                    manager.AddEmber(new Ember(name, age, gender),emberek);
                    Console.WriteLine("Új ember sikeresen hozzáadva!\n");
                    Info();
                    input = Console.ReadLine() ?? String.Empty;
                    break;

                case "min":
                    try
                    {
                        Console.WriteLine(manager.Youngest(emberek));
                    }
                    catch (InvalidOperationException ex) {
                        Console.WriteLine("Az adatbázis jelenleg nem tartalmaz embereket!\n");
                    }
                    Info();
                    input = Console.ReadLine() ?? String.Empty;
                    break;
                case "max":
                    try
                    {
                        Console.WriteLine(manager.Oldest(emberek));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine("Az adatbázis jelenleg nem tartalmaz embereket!\n");
                    }
                    Info();
                    input = Console.ReadLine() ?? String.Empty;
                    break;
                case "avg":
                    try
                    {
                        Console.WriteLine(manager.AverageAge(emberek));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine("Az adatbázis jelenleg nem tartalmaz embereket!\n");
                    }
                    Info();
                    input = Console.ReadLine() ?? String.Empty;
                    break;
                case "abc":
                    IEnumerable<Ember> abcemberek = manager.Alphabetical(emberek);
                    foreach (Ember ember in abcemberek) {
                        Console.WriteLine(ember);
                    }
                    Info();
                    input = Console.ReadLine() ?? String.Empty;
                    break;
                case "search":
                    Console.WriteLine("Adja meg a keresni kivánt nevet!");
                    string kereses = Console.ReadLine() ?? String.Empty;
                    IEnumerable<Ember> keresettEmberek = manager.NameSearch(emberek, kereses);
                    foreach (Ember keresettEmber in keresettEmberek) {
                        Console.WriteLine(keresettEmber);
                    }
                    Info();
                    input = Console.ReadLine() ?? String.Empty;
                    break;
                case "gender":
                    IEnumerable<IGrouping<string, Ember>> genderEmberek = manager.ByGender(emberek);
                    Console.WriteLine("\n Tárolt emberek nem szerint rendszerezve:");
                    foreach (var emberGender in genderEmberek) {
                        Console.WriteLine(emberGender.Key);
                        foreach (Ember ember in emberGender) {
                            Console.WriteLine(ember.Name);
                        }

                    }
                    Info();
                    input = Console.ReadLine() ?? String.Empty;
                    break;

            }
        }

        serializer.Save(emberek);
        Console.WriteLine("Köszönjünk, hogy az ember adatbázist használta!");
        Console.ReadKey();


        return 0;
    }

    private static void Info() {
        Console.WriteLine("\"add\" új ember hozzáadásához");
        Console.WriteLine("\"min\" legfiatalabb tárolt ember lekéréséhez");
        Console.WriteLine("\"max\" legidősebb tárolt ember lekéréséhez");
        Console.WriteLine("\"avg\" az átlagéletkor lekéréséhez");
        Console.WriteLine("\"abc\" az összes tárolt ember lekérése névsorban");
        Console.WriteLine("\"search\" névszerinti kereséshez");
        Console.WriteLine("\"gender\" emberek nem szerinti rendezéséhez.");
        Console.WriteLine("\"exit\" kilépéshez");
    }
}