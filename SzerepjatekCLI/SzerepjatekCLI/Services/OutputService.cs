using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Entities;
using SzerepjatekCLI.Story;

namespace SzerepjatekCLI.Services;

public class OutputService
{
    private readonly string _playerName;
    private readonly Character _character;

    public OutputService(string playerName)
    {
        this._playerName = playerName;
    }

    public void Write(StoryNode node)
    {
        if(!node.Id.Contains("vege"))
            Console.WriteLine(node.Id + "\n\n");
        Console.WriteLine(FormatMessage(node.Text));
    }

    private string FormatMessage(string message)
    {
        return message.Replace("{PlayerName}", _playerName);
    }
}