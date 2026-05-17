internal class ElementDB
{
    public static readonly ElementDB Instance = new ElementDB();

    private readonly Dictionary<string, Element> _elementsByName = new Dictionary<string, Element>();
    private readonly Dictionary<int, Element> _elementsById = new Dictionary<int, Element>();

    private ElementDB()
        {
        AddElement(new Element(0, "Air", new ElementColor(255, 255, 190), null, null, "You somehow created air... Something must've went wrong!"));
        AddElement(new Element(1, "Earth", new ElementColor(119, 50, 50), null, null, "You somehow created earth... Something must've went wrong!"));
        AddElement(new Element(2, "Fire", new ElementColor(255, 0, 0), null, null, "You somehow created fire... Something must've went wrong!"));
        AddElement(new Element(3, "Water", new ElementColor(0, 0, 190), null, null, "You somehow created water... Something must've went wrong!"));
        AddElement(new Element(4, "Aether", new ElementColor(255, 255, 255), null, null, "You somehow created aether... Something must've went wrong!"));
        AddElement(new Element(5, "Steam", new ElementColor(200, 200, 255), "Fire", "Water", "You've jjust created steam! A little more and you'll have steam machines too!"));
        AddElement(new Element(6, "Dust", new ElementColor(200, 119, 119), "Air", "Earth", "Achoo! Looks like you've made dust... Achoo!"));
        AddElement(new Element(7, "Lava", new ElementColor(230, 150, 0), "Earth", "Fire", "You have made lava, the most forbidden of beverages..."));
        AddElement(new Element(8, "Rain", new ElementColor(0, 119, 119), "Air", "Water", "You've made it rain! Now everything smells of petrichor..."));
        AddElement(new Element(9, "Mud", new ElementColor(190, 150, 119), "Earth", "Water", "It's a patch of mud! Let's get down and dirty!"));
        AddElement(new Element(10, "Smoke", new ElementColor(70, 50, 50), "Air", "Fire", "You have created Smoke! A lot of people pay good money to have this in their lungs..."));
        AddElement(new Element(11, "Energy", new ElementColor(200, 255, 0), "Air", "Aether", "You have created Energy... Which itself required energy... Huh..."));
        AddElement(new Element(12, "Stone", new ElementColor(119, 119, 119), "Earth", "Aether", "You have created Stone! Oongas and boongas and such..."));
        AddElement(new Element(13, "Life", new ElementColor(0, 255, 0), "Water", "Aether", "You've forsaken God and have created Life!"));
        AddElement(new Element(14, "Star", new ElementColor(255, 210, 190), "Fire", "Aether", "You have created a Star! Good luck fitting it in your pocket..."));
        AddElement(new Element(15, "Plant", new ElementColor(0, 119, 0), "Earth", "Life", "You have created a Plant! It will eventually serve as a great defense against zombies..."));
        AddElement(new Element(16, "Morning Dew", new ElementColor(190, 190, 200), "Steam", "Plant", "You have created Morning Dew! Not sure if it's safe to drink, but let's find out!"));
        AddElement(new Element(17, "Metal", new ElementColor(50, 50, 70), "Stone", "Aether", "You have created Metal! That includes gold, so you've kinda won?"));
        AddElement(new Element(18, "Sunflower", new ElementColor(255, 200, 0), "Plant", "Star", "You've grown a Sunflower, one of the coolest flowers... You can fight me on that!"));
        AddElement(new Element(19, "Steam Machine", new ElementColor(190, 170, 150), "Steam", "Metal", "You've built a Steam Machine! Ha, I told you!"));
        AddElement(new Element(20, "Philosopher's Stone", new ElementColor(255, 100, 100), "Life", "Metal", "Congratulations!You have created the legendary Philosopher's Stone! You've won!.. I guess?.. It's 3 AM... I'm tired..."));
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