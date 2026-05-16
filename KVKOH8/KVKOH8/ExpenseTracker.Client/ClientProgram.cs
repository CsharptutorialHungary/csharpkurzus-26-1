using ExpenseTracker.Core;
using ExpenseTracker.Core.Data;
using ExpenseTracker.Core.Data.Statistics;

namespace ExpenseTracker.Client;

public class ClientProgram
{
    private static int Main(string[] args)
    {
        Console.WriteLine("Welcome to the Expense tracker!");

        bool incorrectOption = true;
        string option = "";

        while(incorrectOption)
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("Please Select one of the following options:");
            Console.WriteLine("1. Input new record");
            Console.WriteLine("2. List all stored records");
            Console.WriteLine("3. List expense statistics");
            Console.Write("> ");

            option = Console.ReadLine() ?? string.Empty;
            if (option.Trim() == "1" || option.Trim() == "2" || option.Trim() == "3")
            {
                incorrectOption = false;
            } 
            else
            {
                Console.WriteLine("Incorrect input!");
            }
        }

        if(option.Trim() == "1")
        {
            Console.WriteLine("Use the following format for entering records: \"[Income or Expense] / Amount\"");
            Console.Write("> ");
        }
        bool correctInput = false;
        while (!correctInput && option.Trim() == "1")
        {
            string input = Console.ReadLine() ?? string.Empty;
            try
            {
                Transaction transaction = TransactionParser.Parse(input);

                correctInput = DataProvider.SaveTransaction(transaction);
                if(correctInput)
                {
                    Console.WriteLine("Transaction successfully saved");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("> ");
            }
        }

        if (option.Trim() == "2")
        {
            foreach (string transaction in DataProvider.ListAllRecords())
            {
                Console.WriteLine($"{transaction}");
            }
        }

        if (option.Trim() == "3")
        {
            foreach (string statistic in StatisticsProvider.GetAllStatistics())
            {
                Console.WriteLine(statistic);
            }
        }

        return 0;
    }
}
