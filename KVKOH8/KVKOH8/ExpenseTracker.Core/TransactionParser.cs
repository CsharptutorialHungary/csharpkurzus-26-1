using ExpenseTracker.Core.Data;
using static System.StringSplitOptions;

namespace ExpenseTracker.Core;

public sealed class TransactionParser
{
    private static readonly FileSaver _saver = new FileSaver();
    public static void Parse(string input)
    {
        string[] parts = input.Split('/', TrimEntries | RemoveEmptyEntries);
        
        if(parts.Length != 2)
        {
            throw new FormatException("Please follow the format: '[Income or Expense] / Amount'");
        }

        TransactionType transactionType;

        switch(parts[0].ToLower())
        {
            case "income":
                transactionType = TransactionType.Income;
                break;
            case "expense":
                transactionType = TransactionType.Expense;
                break;
            default:
                throw new FormatException("Transaction type must be Income or Expense");
        }

        decimal amount = 0;

        if (!decimal.TryParse(parts[1], out amount))
        {
            throw new FormatException("Amount must be a valid number");
        }

        _saver.SaveRecord(new Transaction(DateTime.UtcNow, transactionType, amount));
    }
}
