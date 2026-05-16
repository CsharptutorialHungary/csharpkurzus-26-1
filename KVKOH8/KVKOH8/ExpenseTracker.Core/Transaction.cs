namespace ExpenseTracker.Core;

internal record Transaction(
    DateTime date,
    TransactionType type,
    decimal amount
);
