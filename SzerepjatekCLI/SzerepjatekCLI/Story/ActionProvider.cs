using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SzerepjatekCLI.Story;

internal class ActionProvider
{
    private readonly Dictionary<string, Dictionary<string, List<string>>> _actions;

    public ActionProvider(string jsonPath)
    {
        var json = File.ReadAllText(jsonPath);
        _actions = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<string>>>>(json);
    }

    public List<string> GetActions(string characterType, string actionType)
    {
        if (_actions.TryGetValue(characterType, out var actionsByType) && actionsByType.TryGetValue(actionType, out var actions))
        {
            return actions;
        }
        return new List<string>();
    }       
}
