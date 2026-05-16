using System;
using FK1C6Z.Core;
using FK1C6Z.Infrastructure;

namespace ExpenseTracker.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Expense Tracker ===");
            var repository = new FileRepository("data.json");
            ExpenseManager manager;

            try
            {
                var loadedExpenses = repository.LoadFromFile();
                manager = new ExpenseManager(loadedExpenses);
                Console.WriteLine("Data succesfully loaded");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Couldn't load data from file. Reason: {ex.Message}");
                Console.ResetColor();

                manager = new ExpenseManager(new List<Expense>());
            }

            // Példa adatok hozzáadása, ha nincs semmi a fájlban
            if (manager.GetAllExpenses().Count <= 0) {
                manager.AddExpense(new Expense(Guid.NewGuid(), "Food", "Bread and milk", 1500m, DateTime.Now));
                manager.AddExpense(new Expense(Guid.NewGuid(), "Utilities", "Electric bill", 12000m, DateTime.Now.AddDays(-2)));
                manager.AddExpense(new Expense(Guid.NewGuid(), "Food", "Cheese", 2000m, DateTime.Now));
            }

            Console.WriteLine("\n--- Breakdown by category ---");
            foreach (var stat in manager.GetTotalExpensesByCategory())
            {
                Console.WriteLine($"{stat.Key}: {stat.Value} Ft");
            }

            try
            {
                repository.SaveToFile(manager.GetAllExpenses());
                Console.WriteLine("\nData saved succesfully. Exit...");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Couldn't save date to file. Reason: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}