using FishLibrary.Core;
using FishLibrary.Data;

using NNVWHL.FishLibrary.Client;

internal class Program
{
    private static int Main(string[] args)
    {
        FishSerializer serializer = new FishSerializer();
        List<Fish> fishCollection = serializer.LoadFishes();

        UserInterface ui = new UserInterface(serializer, fishCollection);

        ui.Start();

        return 0;
    }

}

