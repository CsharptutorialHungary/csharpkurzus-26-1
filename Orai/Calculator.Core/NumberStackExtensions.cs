namespace Calculator.Core;

internal static class NumberStackExtensions
{
    extension(INumberStack stack)
    {
        public IReadOnlyList<double> PopAll()
        {
            double[] numbers = new double[stack.Count];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = stack.Pop();
            }

            return numbers;
        }
    }
}
