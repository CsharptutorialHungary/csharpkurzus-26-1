internal record ElementColor
{
    public int red { get; }
    public int green { get; }
    public int blue { get; }
    public ElementColor(int red, int green, int blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }
}