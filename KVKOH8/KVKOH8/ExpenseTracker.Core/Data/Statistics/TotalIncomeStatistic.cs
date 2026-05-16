using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Core.Data.Statistics;

internal class TotalIncomeStatistic : IStatistics
{
    public string Name => "Total Income";

    public IEnumerable<string> Calculate(List<Transaction> transactions)
    {
        decimal total = transactions.
            Where(transaction => transaction.type == TransactionType.Income).
            Sum(transaction => transaction.amount);

        yield return total.ToString();
    }
}
