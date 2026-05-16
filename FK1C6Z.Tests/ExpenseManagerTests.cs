using System;
using System.Collections.Generic;
using FK1C6Z.Core;
using Xunit;

namespace ExpenseTracker.Tests
{
    public class ExpenseManagerTests
    {
        [Fact]
        public void GetMostExpensiveItem_WithValidData_ReturnsHighestAmount()
        {
            var expenses = new List<Expense>
            {
                new Expense(Guid.NewGuid(), "Food", "Bread", 500, DateTime.Now),
                new Expense(Guid.NewGuid(), "Utilities", "Electric bill", 15000, DateTime.Now)
            };
            var manager = new ExpenseManager(expenses);

            var result = manager.GetMostExpensiveItem();

            Assert.NotNull(result);
            Assert.Equal(15000, result.Amount);
        }

        [Fact]
        public void AddExpense_WithNegativeAmount()
        {
            var manager = new ExpenseManager(new List<Expense>());
            var invalidExpense = new Expense(Guid.NewGuid(), "Error", "Negativ cost", -5000, DateTime.Now);

            Assert.Throws<ArgumentException>(() => manager.AddExpense(invalidExpense));
        }

        [Fact]
        public void GetExpensesByCategory_ReturnsFilteredAndSortedExpenses()
        {
            var expenses = new List<Expense>
            {
                new Expense(Guid.NewGuid(), "Food", "Bread", 500, new DateTime(2026, 5, 10)),
                new Expense(Guid.NewGuid(), "Utilities", "Electric bill", 12000, new DateTime(2026, 5, 11)),
                new Expense(Guid.NewGuid(), "Food", "Milk", 400, new DateTime(2026, 5, 15)),
                new Expense(Guid.NewGuid(), "FOOD", "Cheese", 2000, new DateTime(2026, 5, 1))
            };

            var manager = new ExpenseManager(expenses);

            var result = manager.GetExpensesByCategory("food").ToList();

            Assert.Equal(3, result.Count);

            Assert.Equal("Milk", result[0].Description);
            Assert.Equal("Bread", result[1].Description);
            Assert.Equal("Cheese", result[2].Description);
        }
    }
}