using Calculator.Core;

namespace Calculator.Tests;

internal class NumberStackTests
{
    [Test]
    public void Count_StackIsEmpty_ReturnsZero()
    {
        // Arrange
        NumberStack stack = new();

        // Act
        int count = stack.Count;

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void Push_NumberIsPushed_CountIncreases()
    {
        // Arrange
        NumberStack stack = new();

        // Act
        stack.Push(5);
        int count = stack.Count;

        // Assert
        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void Pop_NumberIsPopped_CountDecreases()
    {
        // Arrange
        NumberStack stack = new();
        stack.Push(5);

        // Act
        _ = stack.Pop();
        int count = stack.Count;

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
    {
        // Arrange
        NumberStack stack = new();

        // Act & Assert
        Assert.That(stack.Pop, Throws.InvalidOperationException);
    }
}
