using System.Text.Json;

namespace ExpenseTracker.Core.Data;

internal sealed class FileSaver
{
    private readonly string _filePath;
    private readonly FileLoader _fileLoader;
    internal FileSaver()
    {
        _filePath = Path.Combine(Environment.CurrentDirectory, "transactions.json");
        _fileLoader = new FileLoader(_filePath);
    }

    internal FileSaver(string filePath)
    {
        _filePath = filePath;
        _fileLoader = new FileLoader(filePath);
        
    }

    internal bool SaveRecord(Transaction newTransaction)
    {
        if (newTransaction == null)
        {
            return false;
        }

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }

        List<Transaction> transactions = _fileLoader.LoadFile();
        transactions.Add(newTransaction);
        
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        string updatedJson = JsonSerializer.Serialize(transactions, options);

        File.WriteAllText(_filePath, updatedJson);
        return true;
    }
}
