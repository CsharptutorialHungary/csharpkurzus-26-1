internal class ConsolePainter
{
    public static void SetConsoleColor(ElementColor color)
    {
        Console.Write($"\u001b[38;2;{color.red};{color.green};{color.blue}m");
    }

    public static void ResetConsoleColor()
    {
        Console.Write("\u001b[0m");
    }
}