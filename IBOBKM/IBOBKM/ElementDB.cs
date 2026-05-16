internal class ElementDB
{
    public static readonly ElementDB Instance = new ElementDB();

    private readonly Dictionary<string, Element> _elementsByName = new Dictionary<string, Element>();
    private readonly Dictionary<int, Element> _elementsById = new Dictionary<int, Element>();

    private ElementDB()
        {
        AddElement(new Element(0, "Air", new ElementColor(255, 0, 255), null, null, "You somehow created air... Something must've went wrong!"));
        AddElement(new Element(1, "Earth", new ElementColor(255, 0, 0), null, null, "You somehow created earth... Something must've went wrong!"));
        AddElement(new Element(2, "Fire", new ElementColor(255, 0, 0), null, null, "You somehow created fire... Something must've went wrong!"));
        AddElement(new Element(3, "Water", new ElementColor(0, 0, 255), null, null, "You somehow created water... Something must've went wrong!"));
        AddElement(new Element(4, "Aether", new ElementColor(255, 255, 255), null, null, "You somehow created aether... Something must've went wrong!"));
        AddElement(new Element(5, "Lava", new ElementColor(119, 0, 0), "Earth", "Fire", "You have created Lava!"));
        AddElement(new Element(6, "Steam", new ElementColor(119, 0, 119), "Fire", "Water", "You have created Steam!"));
        AddElement(new Element(7, "Dust", new ElementColor(0, 119, 0), "Air", "Earth", "You have created Dust!"));
        AddElement(new Element(8, "Rain", new ElementColor(0, 119, 119), "Air", "Water", "You have created Rain!"));
        AddElement(new Element(9, "Mud", new ElementColor(0, 0, 119), "Earth", "Water", "You have created Mud!"));
    }

    private void AddElement(Element element)
    {
        _elementsById[element.id] = element;
        _elementsByName[element.name] = element;
    }

    
    public Element? GetElementByName(string name)
    {
        if (!_elementsByName.ContainsKey(name))
        {
            throw new ElementNotFoundException($"Element with name '{name}' not found.");
        } else if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Element name cannot be null or whitespace.", nameof(name));
        }
        return _elementsByName.TryGetValue(name, out var element) ? element : null;
    }

    public Element? GetElementById(int id)
    {
        if (!_elementsById.ContainsKey(id))
        {
            throw new ElementNotFoundException($"Element with ID '{id}' not found.");
        } else if (id < 0)
        {
            throw new ArgumentException("Element ID cannot be negative.", nameof(id));
        }
        return _elementsById.TryGetValue(id, out var element) ? element : null;
    }
    
    public IEnumerable<Element> Elements => _elementsById.Values;
}