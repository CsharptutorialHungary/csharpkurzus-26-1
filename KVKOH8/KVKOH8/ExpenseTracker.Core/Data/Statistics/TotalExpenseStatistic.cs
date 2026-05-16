using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Core.Data.Statistics;

internal class TotalExpenseStatistic : IStatistics
{
    public string Name => "Total Expense";

    public IEnumerable<string> Calculate(List<Transaction> transactions)
    {
        decimal total = transactions.
            Where(transaction => transaction.type == TransactionType.Expense).
            Sum(transaction => transaction.amount);

        yield return total.ToString();
    }
}
