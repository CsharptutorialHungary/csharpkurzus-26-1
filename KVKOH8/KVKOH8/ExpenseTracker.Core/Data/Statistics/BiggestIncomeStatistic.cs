using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Core.Data.Statistics;

internal class BiggestIncomeStatistic : IStatistics
{
    public string Name => "Biggest income";

    public IEnumerable<string> Calculate(List<Transaction> transactions)
    {
        Transaction? biggestExpense = transactions
            .Where(transaction => transaction.type == TransactionType.Income)
            .OrderByDescending(transaction => transaction.amount)
            .FirstOrDefault();

        if (biggestExpense == null)
        {
            yield return "No income found.";
        }
        yield return biggestExpense.amount.ToString();
    }
}
