namespace ExpenseTracker.Core;

public record Transaction(
    DateTime date,
    TransactionType type,
    decimal amount
);
