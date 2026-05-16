using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Core.Data.Statistics;

internal interface IStatistics
{
    string Name {  get; }
    IEnumerable<string> Calculate(List<Transaction> transactions);
}
