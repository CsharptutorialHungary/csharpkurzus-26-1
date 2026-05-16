namespace ExpenseTracker.Core.Data.Statistics;

public static class StatisticsProvider
{
    private static readonly List<Transaction> _transactions = DataProvider.GetAllRecords();

    private static readonly List<IStatistics> _statistics = new List<IStatistics>
    {
        new TotalExpenseStatistic(),
        new BiggestExpenseStatistic(),
        new TotalIncomeStatistic(),
        new BiggestIncomeStatistic(),
        new BalanceByDaysStatistic()
    };

    public static IEnumerable<string> GetAllStatistics()
    {
        foreach (IStatistics statistic in _statistics)
        {
            yield return statistic.Name;

            foreach (string result in statistic.Calculate(_transactions))
            {
                yield return result;
            }

            yield return string.Empty;
        }
    }
}
