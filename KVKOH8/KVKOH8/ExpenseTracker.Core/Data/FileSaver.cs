using System.Text.Json;

namespace ExpenseTracker.Core.Data;

internal sealed class FileSaver
{
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "transactions.json");

    private readonly FileLoader _fileLoader = new FileLoader();
    internal void SaveRecord(Transaction newTransaction)
    {
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
        }

        List<Transaction> transactions = _fileLoader.LoadFile();
        transactions.Add(newTransaction);
        
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        string updatedJson = JsonSerializer.Serialize(transactions, options);

        File.WriteAllText(FilePath, updatedJson);
    }
}
