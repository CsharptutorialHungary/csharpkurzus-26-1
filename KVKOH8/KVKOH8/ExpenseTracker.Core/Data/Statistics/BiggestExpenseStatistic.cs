using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Core.Data.Statistics;

internal class BiggestExpenseStatistic : IStatistics
{
    public string Name => "Biggest expense";

    public IEnumerable<string> Calculate(List<Transaction> transactions)
    {
        Transaction? biggestExpense = transactions
            .Where(transaction => transaction.type == TransactionType.Expense)
            .OrderByDescending(transaction => transaction.amount)
            .FirstOrDefault();

        if(biggestExpense == null)
        {
            yield return "No expenses found.";
        }
        yield return biggestExpense.amount.ToString();
    }
}
