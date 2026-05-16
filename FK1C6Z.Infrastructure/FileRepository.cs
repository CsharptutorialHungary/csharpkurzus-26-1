using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using FK1C6Z.Core;

namespace FK1C6Z.Infrastructure;

public class FileRepository
{
    private readonly string _filePath;

    public FileRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveToFile(List<Expense> expenses)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(expenses, options);
            File.WriteAllText(_filePath, json);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new Exception($"You don't have permission to edit this file: {_filePath}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error has occured, while saving the file.", ex);
        }
    }

    public List<Expense> LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new List<Expense>();
            }

            string json = File.ReadAllText(_filePath);
            var result = JsonSerializer.Deserialize<List<Expense>>(json);
            return result ?? new List<Expense>();
        }
        catch (JsonException ex)
        {
            throw new Exception($"Corrupted JSON file: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Critical error has occured, while reading the file: {ex.Message}", ex);
        }
    }
}

