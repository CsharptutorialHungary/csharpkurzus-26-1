using System.Globalization;

using static System.StringSplitOptions;

namespace ExpenseTracker.Core;

public class TransactionParser
{
    public static Transaction Parse(string input)
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


        return new Transaction(DateTime.UtcNow, transactionType, amount);
    }
}
