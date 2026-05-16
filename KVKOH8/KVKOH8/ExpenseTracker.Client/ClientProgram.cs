using ExpenseTracker.Core;

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
            Console.WriteLine(AppContext.BaseDirectory);
            Console.WriteLine("=====================================================");
            Console.WriteLine("Please Select one of the following options:");
            Console.WriteLine("1. Input new record");
            Console.WriteLine("2. List all stored records");
            Console.WriteLine("3. List expense statistics");
            Console.Write("> ");
            option = Console.ReadLine() ?? string.Empty;
            if (option.Trim() == "1" || option.Trim() == "2")
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
        bool incorrectInput = true;
        while (incorrectInput && option.Trim() == "1")
        {
            string input = Console.ReadLine() ?? string.Empty;
            try
            {
                Transaction parsedTransaction = TransactionParser.Parse(input);

                Console.WriteLine("Transaction successfully parsed");
                Console.WriteLine(parsedTransaction);

                incorrectInput = false;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("> ");
            }
        }

        if(option.Trim() == "2")
        {
            //TODO: List all stored records
        }

        if (option.Trim() == "3")
        {
            //TODO: List statistics
        }

        return 0;
    }
}
