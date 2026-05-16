using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ExpenseTracker.Core.Data;

internal class FileLoader
{
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "transactions.json");

    internal List<Transaction> LoadFile()
    {

        if (!File.Exists(FilePath))
        {
            return new List<Transaction>();
        }

        string json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Transaction>();
        }

        List<Transaction>? result = JsonSerializer.Deserialize<List<Transaction>>(json);

        return result ?? new List<Transaction>();
    }
}
