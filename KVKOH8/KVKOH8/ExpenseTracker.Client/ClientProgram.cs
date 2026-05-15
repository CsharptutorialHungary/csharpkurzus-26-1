using ExpenseTracker.Core;

namespace ExpenseTracker.Client;

public class ClientProgram
{
    private static int Main(string[] args)
    {
        Console.WriteLine("Welcome to the Expense tracker!");
        Console.WriteLine("Please input your new record in the following format:");
        Console.WriteLine("\"[Income or Expense] / Amount\"");
        Console.Write(">");

        Boolean incorrectInput = true;

        while (incorrectInput)
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
                Console.Write(">");
            }
        }


        return 0;
    }
}
