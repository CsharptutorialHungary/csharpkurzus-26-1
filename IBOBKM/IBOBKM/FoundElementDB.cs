internal class FoundElementDB
{
    public static readonly FoundElementDB Instance = new FoundElementDB();

    private readonly Dictionary<int, Element> _foundElements = new Dictionary<int, Element>();

    private FoundElementDB() {
        AddFoundElement(0);
        AddFoundElement(1);
        AddFoundElement(2);
        AddFoundElement(3);
    }

    public void AddFoundElement(int elementId)
    {
        var element = ElementDB.Instance.GetElementById(elementId);
        if (element != null)
        {
            _foundElements[element.id] = element;
        }
    }

    public bool IsElementFound(int elementId)
    {
        return _foundElements.ContainsKey(elementId);
    }

    public IEnumerable<Element> Elements => _foundElements.Values;
}