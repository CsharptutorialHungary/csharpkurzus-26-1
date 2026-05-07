using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Entities;

namespace SzerepjatekCLI.Services;

public class OutputService
{
    private readonly string _playerName;
    private readonly Character _character;

    public OutputService(string playerName)
    {
        this._playerName = playerName;
    }

    public void Write(string message)
    {
        Console.WriteLine(FormatMessage(message));
    }

    private string FormatMessage(string message)
    {
        return message.Replace("{PlayerName}", _playerName);
    }
}