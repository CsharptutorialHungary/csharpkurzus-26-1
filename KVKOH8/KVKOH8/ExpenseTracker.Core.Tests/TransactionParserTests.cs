using ExpenseTracker.Core;

namespace ExpenseTracker.Core.Tests;

internal class TransactionParserTests
{

    [TestCase("expense/ 100", TransactionType.Expense, 100)]
    [TestCase("ExPeNSe / 625", TransactionType.Expense, 625)]
    [TestCase("Income /300", TransactionType.Income, 300)]
    [TestCase("inCoMe / 86", TransactionType.Income, 86)]
    public void Parser_CorrectlyParsesString_OnValidInput(
        string input,
        TransactionType expectedType,
        decimal expectedAmount)
    {
        Transaction transaction = TransactionParser.Parse(input);

        Assert.That(transaction.type, Is.EqualTo(expectedType));
        Assert.That(transaction.amount, Is.EqualTo(expectedAmount));
    }


    [TestCase("")]
    [TestCase("Income")]
    [TestCase("Income / asd")]
    [TestCase("asd / 1000")]
    [TestCase("Income / 1000 / extra")]
    public void Parse_ThrowsFormatException_OnInvalidInput(string input)
    {
        Assert.Throws<FormatException>(() => 
            TransactionParser.Parse(input));
    }
}