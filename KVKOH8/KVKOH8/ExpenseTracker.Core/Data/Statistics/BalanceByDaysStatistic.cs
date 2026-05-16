using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Core.Data.Statistics;

internal class BalanceByDaysStatistic : IStatistics
{
    public string Name => "Average spending by days";

    public IEnumerable<string> Calculate(List<Transaction> transactions)
    {
        IEnumerable<IGrouping<DateTime, Transaction>> groupedTransactions = 
            transactions
            .GroupBy(transaction => transaction.date.Date)
            .OrderBy(group => group.Key);

        foreach (IGrouping<DateTime, Transaction> group in groupedTransactions) {
            decimal dailyIncome = group
                .Where(transaction => transaction.type == TransactionType.Income)
                .Sum(transaction => transaction.amount);

            decimal dailyExpense = group
                .Where(transaction => transaction.type == TransactionType.Expense)
                .Sum(transaction => transaction.amount);

            decimal dailyBalance = dailyIncome - dailyExpense;

            yield return $"{group.Key:yyyy-MM-dd} | Income: {dailyIncome} HUF | Expense: {dailyExpense} HUF | Balance: {dailyBalance} HUF";
        }
    }
}
