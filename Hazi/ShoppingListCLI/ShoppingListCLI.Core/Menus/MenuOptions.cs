using ShoppingListCLI.Core.Storage;
using ShoppingListCLI.ShoppingListCLI.Core.Menus.Options;

namespace ShoppingListCLI.ShoppingListCLI.Core.Menus;

internal enum MenuOptions
{
    AddItem = 1,
    ViewList = 2,
    RemoveList = 3,
    Exit = 4
}

internal static class MenuOptionsExtensions
{
    public static IOption MenuOption(this MenuOptions option, IStorage storage)
    {
        return option switch
        {
            MenuOptions.AddItem => new AddItemOption(storage),
            MenuOptions.ViewList => new ViewItemsOption(storage),
            MenuOptions.RemoveList => new RemoveItemOption(storage),
            _ => throw new InvalidOperationException()
        };
    }
}
