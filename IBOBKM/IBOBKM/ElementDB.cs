internal class ElementDB
{
    public static readonly ElementDB Instance = new ElementDB();

    private readonly Dictionary<string, Element> _elementsByName = new Dictionary<string, Element>();
    private readonly Dictionary<int, Element> _elementsById = new Dictionary<int, Element>();

    private ElementDB()
    {
        AddElement(new Element(0, "Air", ConsoleColor.fromArgb(255, 0, 255), null, null, "You somehow created air... Something must've went wrong!"));
        AddElement(new Element(1, "Earth", ConsoleColor.fromArgb(255, 0, 0), null, null, "You somehow created earth... Something must've went wrong!"));
        AddElement(new Element(2, "Fire", ConsoleColor.fromArgb(255, 0, 0), null, null, "You somehow created fire... Something must've went wrong!"));
        AddElement(new Element(3, "Water", ConsoleColor.fromArgb(0, 0, 255), null, null, "You somehow created water... Something must've went wrong!"));
        AddElement(new Element(4, "Aether", ConsoleColor.fromArgb(255, 255, 255), null, null, "You somehow created aether... Something must've went wrong!"));
        AddElement(new Element(5, "Lava", ConsoleColor.fromArgb(119, 0, 0), "Earth", "Fire", "You have created Lava!"));
        AddElement(new Element(6, "Steam", ConsoleColor.fromArgb(119, 0, 119), "Fire", "Water", "You have created Steam!"));
        AddElement(new Element(7, "Dust", ConsoleColor.fromArgb(0, 119, 0), "Air", "Earth", "You have created Dust!"));
        AddElement(new Element(8, "Rain", ConsoleColor.fromArgb(0, 119, 119), "Air", "Water", "You have created Rain!"));
        AddElement(new Element(9, "Mud", ConsoleColor.fromArgb(0, 0, 119), "Earth", "Water", "You have created Mud!"));
    }

    private void AddElement(Element element)
    {
        _elementsById[element.Id] = element;
        _elementsByName[element.Name] = element;
    }

    /*
    public Element GetElementByName(string name)
    {
        return _elementsByName.TryGetValue(name, out var element) ? element : null;
    }

    public Element GetElementById(int id)
    {
        return _elementsById.TryGetValue(id, out var element) ? element : null;
    }
    */
}