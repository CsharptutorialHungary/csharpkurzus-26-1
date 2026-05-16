namespace ExpenseTracker.Core.Data;

public sealed class DataProvider
{
    private readonly FileLoader _fileLoader = new FileLoader();
    internal List<Transaction> GetAllRecords()
    {
        return _fileLoader.LoadFile();
    }

    public List<string> ListAllRecords()
    {
        List<string> result = [];
        foreach (Transaction transaction in GetAllRecords())
        {
            result.Add(transaction.date.ToShortDateString()
                + " | "
                + (transaction.type == TransactionType.Expense ? "Expense" : "Income ")
                + " | "
                + transaction.amount 
                + " HUF"
                );
        }

        return result;
    }
}
