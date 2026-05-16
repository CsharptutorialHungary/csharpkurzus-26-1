namespace ExpenseTracker.Core.Data;

public static class DataProvider
{
    internal static List<Transaction> GetAllRecords()
    {
        FileLoader _fileLoader = new FileLoader();
        return _fileLoader.LoadFile();
    }

    public static List<string> ListAllRecords()
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

    public static bool SaveTransaction(Transaction transaction)
    {
        FileSaver saver = new FileSaver();
        saver.SaveRecord(transaction);
        return true;
    }
}
