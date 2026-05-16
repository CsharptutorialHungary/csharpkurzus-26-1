using System.Drawing;

internal record Element
{
    public int id { get; }
    public string name { get; }
    public ElementColor color { get; }
    public string? firstIngredient { get; }
    public string? secondIngredient { get; }
    public string message { get; }

    public Element(int id, string name, ElementColor color, string? firstIngredient, string? secondIngredient, string message)
    {
        this.id = id;
        this.name = name;
        this.color = color;
        this.firstIngredient = firstIngredient;
        this.secondIngredient = secondIngredient;
        this.message = message;
    }
}