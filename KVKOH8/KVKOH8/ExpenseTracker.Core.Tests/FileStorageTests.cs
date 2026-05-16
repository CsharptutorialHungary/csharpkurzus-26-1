using ExpenseTracker.Core;
using ExpenseTracker.Core.Data;

namespace ExpenseTracker.Core.Tests;

internal class FileStorageTests
{
    private string _testFilePath = string.Empty;

    [SetUp]
    public void SetUp()
    {
        _testFilePath = Path.Combine(
            Path.GetTempPath(),
            "expense-tracker-test.json");

        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }

    [Test]
    public void SaveRecord_CreatesFileAndSavesTransaction()
    {
        FileSaver fileSaver = new FileSaver(_testFilePath);
        FileLoader fileLoader = new FileLoader(_testFilePath);

        fileSaver.SaveRecord(
            new Transaction(
                new DateTime(2026, 5, 16),
                TransactionType.Expense,
                5000)
            );
        fileSaver.SaveRecord(
            new Transaction(
                new DateTime(2026, 5, 16),
                TransactionType.Income,
                150)
            );

        List<Transaction> transactions = fileLoader.LoadFile();

        Assert.That(transactions.Count, Is.EqualTo(2));

        Assert.That(transactions[0].type, Is.EqualTo(TransactionType.Expense));
        Assert.That(transactions[0].amount, Is.EqualTo(5000));

        Assert.That(transactions[1].type, Is.EqualTo(TransactionType.Income));
        Assert.That(transactions[1].amount, Is.EqualTo(150));
    }

    [Test]
    public void LoadFile_ReturnsEmptyList_WhenFileDoesNotExist()
    {
        FileLoader fileLoader = new FileLoader(_testFilePath);

        List<Transaction> transactions = fileLoader.LoadFile();

        Assert.That(transactions, Is.Empty);
    }
}
