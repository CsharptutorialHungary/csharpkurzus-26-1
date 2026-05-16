using System;
using System.Collections.Generic;
using System.Text;

namespace FK1C6Z.Core;

public class ExpenseManager
{
    private readonly List<Expense> _expenses;

    public ExpenseManager(List<Expense> initialExpenses)
    {
        _expenses = initialExpenses ?? new List<Expense>();
    }

    public void AddExpense(Expense expense)
    {
        if (expense.Amount < 0)
        {
            throw new ArgumentException("Expense must be more than zero.");
        }


        _expenses.Add(expense);
    }

    public IEnumerable<Expense> GetExpensesByCategory(string category)
    {
        return _expenses
            .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(e => e.Date);
    }

    public Dictionary<string, decimal> GetTotalExpensesByCategory()
    {
        return _expenses
            .GroupBy(e => e.Category)
            .ToDictionary(
                group => group.Key,
                group => group.Sum(e => e.Amount)
            );
    }

    public Expense? GetMostExpensiveItem()
    {
        if (!_expenses.Any()) return null;
        return _expenses.OrderByDescending(e => e.Amount).FirstOrDefault();
    }

    public List<Expense> GetAllExpenses() => _expenses;
}

